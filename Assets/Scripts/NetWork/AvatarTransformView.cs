using System.Xml;
using UnityEngine;
using Photon.Pun;

///<summary>
/// 他のプレイヤーの座標と回転を補間するクラス
///</summary>
public class AvatarTransformView : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField, Tooltip("補間にかける時間")]
    float INTERPOLATION_PERIOD = 0.1f;

    [SerializeField, Tooltip("他のプレイヤーが移動しているかの判定値")]
    float MIN_MOVEMENT_THRESHOLD = 0.01f;

    // 経過時間
    float elapsedTime;

    // 他のプレイヤーが停止しているか
    bool isOtherPlayerMoving = true;

    // 補間の座標
    Vector3 startPosition;
    Vector3 endPosition;

    // 補間の移動速度
    Vector3 startSpeed;
    Vector3 endSpeed;

    // 補間の回転情報
    Quaternion startRotation;
    Quaternion endRotation;

    ///<summary>
    /// 初期化処理
    ///</summary>
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition;
        startSpeed = Vector3.zero;
        endSpeed = startSpeed;
        startRotation = transform.rotation;
        endRotation = startRotation;
        elapsedTime = Time.deltaTime;
    }

    ///<summary>
    /// フレーム毎の更新処理
    ///</summary>
    void Update()
    {
        if (photonView.IsMine)
        {
            // 自身は、毎フレームの移動量と経過時間を記録
            startPosition = endPosition;
            endPosition = transform.position;
            startRotation = endRotation;
            endRotation = transform.rotation;
            elapsedTime = Time.deltaTime;
        }
        else
        {
            // 他プレイヤーは、補間処理
            elapsedTime += Time.deltaTime;

            // 座標移動の補間
            if (isOtherPlayerMoving)
            {
                if (elapsedTime < INTERPOLATION_PERIOD)
                {
                    transform.position = HermiteSpline.Interpolate(startPosition, endPosition, startSpeed, endSpeed, elapsedTime / INTERPOLATION_PERIOD);
                }
                else
                {
                    transform.position = Vector3.LerpUnclamped(startPosition, endPosition, elapsedTime / INTERPOLATION_PERIOD);
                }
            }

            // 回転の補間
            transform.rotation = Quaternion.SlerpUnclamped(startRotation, endRotation, elapsedTime / INTERPOLATION_PERIOD);
        }
    }

    ///<summary>
    /// ネットワーク同期
    ///</summary>
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 自身のデータ送信
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            // 毎フレームの移動量と経過時間から、秒速を求めて送信
            stream.SendNext((endPosition - startPosition) / elapsedTime);
        }
        else
        {
            // 他プレイヤーのデータ受信
            var networkPosition = (Vector3)stream.ReceiveNext();
            var networkRotation = (Quaternion)stream.ReceiveNext();
            var networkVelocity = (Vector3)stream.ReceiveNext();
            var lag = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - info.SentServerTimestamp) / 1000f);

            // 座標
            startPosition = transform.position;                     // 受信時の座標を、補間の開始座標にする
            endPosition = networkPosition + networkVelocity * lag;  // 現在時刻における予測座標を、補間の終了座標にする

            // 速度
            startSpeed = endSpeed;                              // 前回の補間の終了速度を、補間の開始速度にする
            endSpeed = networkVelocity * INTERPOLATION_PERIOD;  // 受信した秒速を、補間にかける時間あたりの速度に変換して、補間の終了速度にする

            // 回転
            startRotation = transform.rotation; // 受信時の回転情報を、補間の開始回転にする
            endRotation = networkRotation;      // 現在時刻における予測回転情報を、補間の終了回転にする

            // 経過時間リセット
            elapsedTime = 0f;

            // 他のプレイヤーが停止しているかどうか判定
            isOtherPlayerMoving = networkVelocity.magnitude > MIN_MOVEMENT_THRESHOLD;
        }
    }
}
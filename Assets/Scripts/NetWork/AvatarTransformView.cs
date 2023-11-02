using Photon.Pun;
using UnityEngine;

public class AvatarTransformView : MonoBehaviourPunCallbacks, IPunObservable
{
    // 補間にかける時間
    const float InterpolationPeriod = 0.1f;

    Vector3 p1;
    Vector3 p2;
    Vector3 v1;
    Vector3 v2;
    Quaternion r1;
    Quaternion r2;
    float elapsedTime;

    // 他のプレイヤーが停止しているかどうかを示すフラグ
    bool isOtherPlayerMoving = true;

    void Start()
    {
        p1 = transform.position;
        p2 = p1;
        v1 = Vector3.zero;
        v2 = v1;
        r1 = transform.rotation;
        r2 = r1;
        elapsedTime = Time.deltaTime;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // 自身のネットワークオブジェクトは、毎フレームの移動量と経過時間を記録する
            p1 = p2;
            p2 = transform.position;
            r1 = r2;
            r2 = transform.rotation;
            elapsedTime = Time.deltaTime;
        }
        else
        {
            // 他プレイヤーのネットワークオブジェクトは、補間処理を行う
            elapsedTime += Time.deltaTime;

            // 座標移動
            if (isOtherPlayerMoving)
            {
                if (elapsedTime < InterpolationPeriod)
                {
                    transform.position = HermiteSpline.Interpolate(p1, p2, v1, v2, elapsedTime / InterpolationPeriod);
                }
                else
                {
                    transform.position = Vector3.LerpUnclamped(p1, p2, elapsedTime / InterpolationPeriod);
                }
            }

            // 回転処理
            transform.rotation = Quaternion.SlerpUnclamped(r1, r2, elapsedTime / InterpolationPeriod);
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            // 毎フレームの移動量と経過時間から、秒速を求めて送信する
            stream.SendNext((p2 - p1) / elapsedTime);
        }
        else
        {
            var networkPosition = (Vector3)stream.ReceiveNext();
            var networkRotation = (Quaternion)stream.ReceiveNext();
            var networkVelocity = (Vector3)stream.ReceiveNext();
            var lag = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - info.SentServerTimestamp) / 1000f);

            // 受信時の座標を、補間の開始座標にする
            p1 = transform.position;
            // 現在時刻における予測座標を、補間の終了座標にする
            p2 = networkPosition + networkVelocity * lag;
            // 前回の補間の終了速度を、補間の開始速度にする
            v1 = v2;
            // 受信した秒速を、補間にかける時間あたりの速度に変換して、補間の終了速度にする
            v2 = networkVelocity * InterpolationPeriod;
            // 受信時の回転情報を、補間の開始回転にする
            r1 = transform.rotation;
            // 現在時刻における予測回転情報を、補間の終了回転にする
            r2 = networkRotation;
            // 経過時間をリセットする
            elapsedTime = 0f;

            // 他のプレイヤーが停止しているかどうかを判定
            isOtherPlayerMoving = networkVelocity.magnitude > 0.01f;
        }
    }
}
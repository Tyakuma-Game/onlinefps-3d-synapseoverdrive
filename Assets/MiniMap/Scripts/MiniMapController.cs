using UnityEngine;

namespace MiniMap
{
    /// <summary>
    /// ミニマップの管理を行うクラス
    /// </summary>
    public class MiniMapController : MonoBehaviour
    {
        public static MiniMapController instance { get; private set; }

        [Header(" Settings ")]
        [SerializeField] float yPositionConstant = 0.0f;
        [SerializeField] float cameraIconDistance = 0.0f;
        [SerializeField] float iconRotation = 90f;

        [Header(" Elements ")]
        [SerializeField] Transform cameraTransform;
        [SerializeField] Transform iconTransform;

        Transform targetTransform;
        Vector3 miniMapPos;
        Vector3 targetRotation;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        // TODO:
        // OnMoveなどの移動イベントが発生した際にこの関数を登録する感じで設定するようにする
        // 現在はリファクタリング中の為Updateで更新しているが将来的には更新イベントで呼び出すようにし、効率化する！
        void Update()
        {
            if (targetTransform == null)
                Debug.LogWarning("ミニマップのTargetがNULL！");
            else
                UpdateMiniMapPosition();
        }

        /// <summary>
        /// ミニマップ上の位置とカメラ位置を更新
        /// </summary>
        void UpdateMiniMapPosition()
        {
            // ミニマップ上での位置を計算
            miniMapPos = new Vector3(targetTransform.position.x, yPositionConstant, targetTransform.position.z);

            // カメラ位置更新
            cameraTransform.position = miniMapPos;

            // カメラとアイコンの距離を考慮してアイコンの座標更新
            miniMapPos.y -= cameraIconDistance;
            iconTransform.position = miniMapPos;

            // アイコンの回転を更新
            targetRotation = targetTransform.eulerAngles;
            iconTransform.eulerAngles = new Vector3(iconRotation, targetRotation.y, targetRotation.z);
        }

        // TODO:
        // プレイヤーのスポーンイベント等から呼び出せるよう将来的に変更する

        /// <summary>
        /// 追従させるオブジェクトのトランスフォームをセット
        /// </summary>
        /// <param name="target">追従させるオブジェクトのトランスフォーム</param>
        public void SetMiniMapTarget(Transform target)
        {
            targetTransform = target;
        }
    }
}
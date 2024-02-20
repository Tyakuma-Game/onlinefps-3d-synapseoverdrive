using UnityEngine;

namespace MiniMap
{
    /// <summary>
    /// ミニマップ上での敵アイコンを管理するクラス
    /// </summary>
    public class EnemyIconController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] float iconElevation = 0.0f;
        [SerializeField] GameObject icon;
        [SerializeField] Transform enemyTransform;
        bool isIconVisible = true;

        // TODO: 処理効率化
        // Updateで無駄に関数を毎回呼び出しているから
        // 後にMoveアクションの関数に登録する形で座標更新処理に合わせて呼び出すように作り直す。
        void Update()
        {
            if (isIconVisible)
                UpdateIconPosition();
        }

        /// <summary>
        /// ミニマップ上でのアイコンの位置更新
        /// </summary>
        void UpdateIconPosition()
        {
            Vector3 iconPosition = new Vector3(enemyTransform.position.x, iconElevation, enemyTransform.position.z);
            icon.transform.position = iconPosition;
        }

        /// <summary>
        /// アイコンの表示切り替え
        /// </summary>
        /// <param name="isVisible">表示 = true / 非表示 = false</param>
        public void SetIconVisibility(bool isVisible)
        {
            if (isIconVisible != isVisible)
            {
                isIconVisible = isVisible;
                icon.SetActive(isVisible);

                // アイコンを表示する際に位置を更新
                if (isVisible)
                    UpdateIconPosition();
            }
        }
    }
}
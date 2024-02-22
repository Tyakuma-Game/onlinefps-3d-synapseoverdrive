using UnityEngine;

/// <summary>
/// Playerの着地判定を行うクラス
/// </summary>
public class PlayerLandDetector : MonoBehaviour
{
    [Tooltip("地面だと認識するレイヤー")]
    [SerializeField] LayerMask groundLayers;

    [Tooltip("着地しているかのフラグ")]
    bool isGrounded = true;

    /// <summary>
    /// 地面に着地しているか
    /// </summary>
    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 地面に接触していない & 衝突したオブジェクトが指定された地面のレイヤーに含まれているかチェック
        if (isGrounded == false && ((1 << collision.gameObject.layer) & groundLayers) != 0)
            isGrounded = true;
    }

    /// <summary>
    /// ジャンプ中にフラグを変更
    /// </summary>
    public void OnJunpingChangeFlag()
    {
        isGrounded = false;
    }
}
/// <summary>
/// Playerのアニメーションインターフェース
/// </summary>
public interface IPlayerAnimator
{
    /// <summary>
    /// アニメーションの更新処理
    /// </summary>
    /// <param name="playerAnimationState">現在選択中のアニメーション</param>
    void AnimationUpdate(PlayerAnimationState playerAnimationState);
}
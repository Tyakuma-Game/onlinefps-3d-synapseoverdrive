using UnityEngine;

/// <summary>
/// ゲーム中の入力処理管理クラス
/// ゲームの入力システムを初期化し、有効化・無効化する責務を持つ
/// </summary>
public class InputManager : MonoBehaviour
{
    /// <summary>
    /// ゲームの入力設定を格納する静的変数
    /// この変数を通じて、ゲーム全体で一貫した入力処理を提供する
    /// </summary>
    public static GameInputs Controls;

    void Awake()
    {
        Controls = new GameInputs();    // GameInputsインスタンス生成
        Controls.Enable();              // 入力システムを有効化
    }

    void OnDestroy()
    {
        Controls?.Disable();// 入力システムが存在する場合に無効化
    }
}
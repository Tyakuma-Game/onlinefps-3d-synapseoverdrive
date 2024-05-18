using UnityEngine;

/// <summary>
/// ゲーム中の入力処理管理クラス
/// 入力システムを初期化し有効化・無効化する責務を持つ
/// </summary>
public class InputManager : MonoBehaviour
{
    /// <summary>
    /// ゲームの入力設定を格納する静的変数
    /// この変数を通じて、ゲーム全体で一貫した入力処理を提供する
    /// </summary>
    public static GameInputs Controls { get; private set; }

    void Awake()
    {
        // 生成
        Controls = new GameInputs();

        if (Controls != null)
        {
            Controls.Enable(); // 入力システム有効化
            Debug.Log("Input system enabled.");
        }
        else
        {
            Debug.LogError("Failed to create input system.");
        }
    }

    void OnDestroy()
    {
        if (Controls != null)
        {
            Controls.Disable(); // 入力システム無効化
            Debug.Log("Input system disabled.");
        }
    }
}
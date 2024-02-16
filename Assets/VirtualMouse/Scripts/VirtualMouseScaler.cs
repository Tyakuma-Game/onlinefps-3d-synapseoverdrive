using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
#endif

/// <summary>
/// 仮想マウスの座標修正を行うクラス
/// </summary>
public class VirtualMouseScaler : InputProcessor<Vector2>
{
    public float scale = 1;

    const string ProcessorName = nameof(VirtualMouseScaler);
    const string VirtualMouseDeviceName = "VirtualMouse";

#if UNITY_EDITOR
    static VirtualMouseScaler() => Initialize();
#endif

    /// <summary>
    /// Processorの登録
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        // 重複登録の場合、Input ActionのProcessor一覧に正しく表示されない事があるため、
        // 重複チェックを行っている
        if (InputSystem.TryGetProcessor(ProcessorName) == null)
            InputSystem.RegisterProcessor<VirtualMouseScaler>(ProcessorName);
    }

    /// <summary>
    /// 独自のProcessorの処理
    /// </summary>
    /// <param name="value">入力値</param>
    /// <param name="control">入力コントロール</param>
    /// <returns>処理された入力値</returns>
    public override Vector2 Process(Vector2 value, InputControl control)
    {
        // VirtualMouseの場合、座標系問題が発生するためProcessor適用
        if (control.device.name == VirtualMouseDeviceName)
            value *= scale;

        return value;
    }
}
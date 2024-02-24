using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

internal class DPadMagnitudeComposite : InputBindingComposite<float>
{
    // 4方向ボタン入力
    [InputControl(layout = "Button")] public int up = 0;
    [InputControl(layout = "Button")] public int down = 0;
    [InputControl(layout = "Button")] public int left = 0;
    [InputControl(layout = "Button")] public int right = 0;

    /// <summary>
    /// 初期化
    /// </summary>
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod]
#else
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
    private static void Initialize()
    {
        // 初回にCompositeBindingを登録する必要がある
        InputSystem.RegisterBindingComposite(typeof(DPadMagnitudeComposite), "2DVectorMagnitude");
    }

    /// <summary>
    /// 4方向入力からベクトルの大きさに変換して返す
    /// </summary>
    public override float ReadValue(ref InputBindingCompositeContext context)
    {
        var upValue = context.ReadValue<float>(up);
        var downValue = context.ReadValue<float>(down);
        var leftValue = context.ReadValue<float>(left);
        var rightValue = context.ReadValue<float>(right);

        return DpadControl.MakeDpadVector(upValue, downValue, leftValue, rightValue).magnitude;
    }

    /// <summary>
    /// 値の大きさを返す
    /// </summary>
    public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
    {
        return ReadValue(ref context);
    }
}
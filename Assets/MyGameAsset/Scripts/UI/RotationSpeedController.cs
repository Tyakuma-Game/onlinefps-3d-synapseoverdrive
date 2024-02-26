using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class RotationSpeedController : MonoBehaviour
{
    [SerializeField] RotationSettings rotationSettings; // スクリプタブルオブジェクトの参照
    [SerializeField] Slider rotationSpeedSlider; // UIスライダーの参照
    [SerializeField] Text Text;
    void Start()
    {
        rotationSpeedSlider.minValue = 0.1f; // スライダーの最小値を設定
        rotationSpeedSlider.maxValue = 5f; // スライダーの最大値を設定
        rotationSpeedSlider.value = Mathf.Clamp(rotationSettings.rotationSpeed, 0.1f, 5f); // 初期値を設定し、範囲内に収める
        rotationSpeedSlider.onValueChanged.AddListener(UpdateRotationSpeed); // スライダーの値が変更された時のリスナーを追加
        Text.text = rotationSpeedSlider.value.ToString() + 'f';
    }

    void UpdateRotationSpeed(float value)
    {
        rotationSettings.rotationSpeed = Mathf.Clamp(value, 0.1f, 5f); // スライダーの値をrotationSpeedに反映し、範囲内に収める
        Text.text = value.ToString() + 'f';
    }
}
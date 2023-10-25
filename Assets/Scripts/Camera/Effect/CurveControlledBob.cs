using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// シリアライズ可能なクラスとして定義
[Serializable]
public class CurveControlledBob : MonoBehaviour
{
    [Header("ヘッドボブの範囲")]
    [Tooltip("ヘッドボブの水平範囲")]
    [SerializeField] float HorizontalBobRange = 0.33f;
    [Tooltip("ヘッドボブの垂直範囲")]
    [SerializeField] float VerticalBobRange = 0.33f;

    [Header("ヘッドボブのアニメーションカーブ")]
    [SerializeField]
    AnimationCurve Bobcurve = new AnimationCurve(
        new Keyframe(0f, 0f),
        new Keyframe(0.5f, 1f),
        new Keyframe(1f, 0f),
        new Keyframe(1.5f, -1f),
        new Keyframe(2f, 0f)); // サインカーブによるヘッドボブ

    [Header("垂直方向と水平方向の比率")]
    [Tooltip("垂直方向と水平方向のボブの比率")]
    [SerializeField] float VerticaltoHorizontalRatio = 1f;

    [Tooltip("ボブサイクルのX軸位置")]
    float m_CyclePositionX;
    [Tooltip("ボブサイクルのY軸位置")]
    float m_CyclePositionY;

    [Tooltip("ボブの基本間隔")]
    float m_BobBaseInterval;

    [Tooltip("オリジナルのカメラ位置を保持")]
    Vector3 m_OriginalCameraPosition;

    [Tooltip("タイムを計測")]
    float m_Time;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="camera">カメラの初期位置</param>
    /// <param name="bobBaseInterval">ボブの基本間隔</param>
    public void Setup(Camera camera, float bobBaseInterval)
    {
        m_BobBaseInterval = bobBaseInterval;
        m_OriginalCameraPosition = camera.transform.localPosition;

        // アニメーションカーブの最後のキーフレームの時間を取得
        m_Time = Bobcurve[Bobcurve.length - 1].time;
    }

    /// <summary>
    /// ヘッドボブを実行
    /// </summary>
    /// <param name="speed">動かす速度</param>
    /// <returns>新しいカメラ位置</returns>
    public Vector3 DoHeadBob(float speed)
    {
        // X軸方向のボブ位置とY軸方向のボブ位置を計算
        float xPos = m_OriginalCameraPosition.x + (Bobcurve.Evaluate(m_CyclePositionX) * HorizontalBobRange);
        float yPos = m_OriginalCameraPosition.y + (Bobcurve.Evaluate(m_CyclePositionY) * VerticalBobRange);

        // ボブサイクル位置を更新
        m_CyclePositionX += (speed * Time.deltaTime) / m_BobBaseInterval;
        m_CyclePositionY += ((speed * Time.deltaTime) / m_BobBaseInterval) * VerticaltoHorizontalRatio;

        // ボブサイクル位置がタイムを超えた場合、差分を取る
        if (m_CyclePositionX > m_Time)
        {
            m_CyclePositionX = m_CyclePositionX - m_Time;
        }
        if (m_CyclePositionY > m_Time)
        {
            m_CyclePositionY = m_CyclePositionY - m_Time;
        }

        // 新しいカメラ位置を返す
        return new Vector3(xPos, yPos, 0f);
    }
}

using UnityEngine;

/// <summary>
/// スプライン補間を行う静的クラス
/// </summary>
public static class HermiteSpline
{
    /// <summary>
    /// 2つの点と速度情報から補間値を計算
    /// </summary>
    /// <param name="p1">開始点の値</param>
    /// <param name="p2">終了点の値</param>
    /// <param name="v1">開始点の速度情報</param>
    /// <param name="v2">終了点の速度情報</param>
    /// <param name="t">補間の進行度合い（0から1の範囲）</param>
    /// <returns>補間された値</returns>
    public static float Interpolate(float p1, float p2, float v1, float v2, float t)
    {
        float a = 2f * p1 - 2f * p2 + v1 + v2;
        float b = -3f * p1 + 3f * p2 - 2f * v1 - v2;
        return t * (t * (t * a + b) + v1) + p1;
    }

    /// <summary>
    /// 2つのベクトルと速度情報から補間ベクトルを計算
    /// </summary>
    /// <param name="p1">開始点のベクトル</param>
    /// <param name="p2">終了点のベクトル</param>
    /// <param name="v1">開始点の速度情報ベクトル</param>
    /// <param name="v2">終了点の速度情報ベクトル</param>
    /// <param name="t">補間の進行度合い（0から1の範囲）</param>
    public static Vector2 Interpolate(Vector2 p1, Vector2 p2, Vector2 v1, Vector2 v2, float t)
    {
        return new Vector2(
            Interpolate(p1.x, p2.x, v1.x, v2.x, t),
            Interpolate(p1.y, p2.y, v1.y, v2.y, t)
        );
    }

    /// <summary>
    /// 2つのベクトルと速度情報から補間ベクトルを計算
    /// </summary>
    /// <param name="p1">開始点のベクトル</param>
    /// <param name="p2">終了点のベクトル</param>
    /// <param name="v1">開始点の速度情報ベクトル</param>
    /// <param name="v2">終了点の速度情報ベクトル</param>
    /// <param name="t">補間の進行度合い（0から1の範囲）</param>
    /// <returns>補間されたベクトル</returns>
    public static Vector3 Interpolate(Vector3 p1, Vector3 p2, Vector3 v1, Vector3 v2, float t)
    {
        return new Vector3(
            Interpolate(p1.x, p2.x, v1.x, v2.x, t),
            Interpolate(p1.y, p2.y, v1.y, v2.y, t),
            Interpolate(p1.z, p2.z, v1.z, v2.z, t)
        );
    }
}
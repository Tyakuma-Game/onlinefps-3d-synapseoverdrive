using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 現在の色状態
/// </summary>
public enum ColorState
{
    Red,    // 赤色
    Blue    // 青色
}

/// <summary>
/// playerの色管理クラス
/// </summary>
public class PlayerCollar : MonoBehaviour
{
    [Tooltip("頭のリング")]
    [SerializeField] GameObject magicRing;

    SpriteRenderer magicRingRenderer;
    ColorState currentColorState;


    void Start()
    {
        magicRingRenderer = magicRing.GetComponent<SpriteRenderer>();

        // 初期の赤色を設定する
        currentColorState = ColorState.Red;     // 色のステータスを設置する
        magicRingRenderer.color = Color.red;    // 色情報を設定
    }


    void Update()
    {
        // Qキーが押されたら色を切り替える
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 色状態を切り替え
            SwitchColor();
        }
    }


    /// <summary>
    /// 色を切り替える
    /// </summary>
    void SwitchColor()
    {
        // 現在の色が赤なら青に、青なら赤に切り替える
        if (currentColorState == ColorState.Red)
        {
            
            magicRingRenderer.color = Color.blue;
            currentColorState = ColorState.Blue;
        }
        else
        {
            magicRingRenderer.color = Color.red;

            currentColorState = ColorState.Red;
        }
    }


    /// <summary>
    /// 現在の色情報を取得する
    /// </summary>
    /// <returns>現在の色情報</returns>
    public ColorState GetColorState()
    {
        return this.currentColorState;
    }
}

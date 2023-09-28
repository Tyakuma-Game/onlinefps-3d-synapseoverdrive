using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// プレイヤーのマウスカーソルロック管理クラス
/// </summary>
public class PlayerMouseCursorLock : MonoBehaviourPunCallbacks
{
    [Tooltip("現在カーソルをロックしているか")]
    bool isCursorLock = true;

    void Update()
    {
        //自分以外の場合は
        if (!photonView.IsMine)
        {
            //処理終了
            return;
        }

        //マウスカーソルのロック状態を更新
        UpdateCursorLock();
    }

    /// <summary>
    /// マウスカーソルのロック状態を更新
    /// </summary>
    public void UpdateCursorLock()
    {
        //Controlキーが押されているかチェック
        bool controlKeyPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        //ロック状態更新
        if (controlKeyPressed)
        {
            isCursorLock = false;
        }
        else
        {
            isCursorLock = true;
        }

        //表示切替
        if (isCursorLock)
        {
            //カーソルを中央に固定し非表示に
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            //カーソルを表示
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OffLineRoomManager : MonoBehaviour
{

    [SerializeField] string OnLineConnectTitlescene;
    [SerializeField] string ToPracticeGroundScene;


    /// <summary>
    /// 練習場を読み込む
    /// </summary>
    public void TransitionToPracticeGround()
    {
        // シーンを非同期で読み込む
        SceneManager.LoadSceneAsync(ToPracticeGroundScene);
    }


    /// <summary>
    /// Internet接続のタイトルを読み込む関数
    /// </summary>
    public void TransitionToOnlineMode()
    {
        // シーンを非同期で読み込む
        SceneManager.LoadSceneAsync(OnLineConnectTitlescene);
    }


    /// <summary>
    /// ゲームを終了する関数
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        // エディターモードで実行している場合、エディターを停止
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // エディターモード以外ではアプリケーションを終了
        Application.Quit();
#endif
    }
}
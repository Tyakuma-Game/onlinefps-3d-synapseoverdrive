using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;

#if UNITY_EDITOR
using UnityEditor;

namespace Tabsil.Mineral
{
    /// <summary>
    /// フォルダの色付けとアイコン設定を行うエディタの拡張クラス
    /// </summary>
    [InitializeOnLoad]
    static class ColoredFoldersEditor
    {
        static string iconName;

        static ColoredFoldersEditor()
        {
            // エディタのプロジェクトウィンドウアイテムの描画時にメソッドを呼び出し
            EditorApplication.projectWindowItemOnGUI -= OnGUI;
            EditorApplication.projectWindowItemOnGUI += OnGUI;
        }

        /// <summary>
        /// プロジェクトウィンドウでフォルダの色付けとアイコンの描画
        /// </summary>
        /// <param name="guid">アセットのGUID</param>
        /// <param name="selectionRect">選択領域のRect</param>
        static void OnGUI(string guid, Rect selectionRect)
        {
            Color backgroundColor;
            Rect folderRect = GetFolderRect(selectionRect, out backgroundColor);

            string iconGuid = MineralPrefs.GetString(guid, "");

            // 何も設定されていない　OR　Noneが設定されている
            if (iconGuid == "" || iconGuid == "00000000000000000000000000000000")
                return;

            // フォルダの背景色を描画
            EditorGUI.DrawRect(folderRect, backgroundColor);

            // フォルダに設定されたアイコンを描画
            string folderTexturePath = AssetDatabase.GUIDToAssetPath(iconGuid);
            Texture2D folderTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(folderTexturePath);
            GUI.DrawTexture(folderRect, folderTexture);
        }

        /// <summary>
        /// フォルダのRectを取得
        /// </summary>
        /// <param name="selectionRect">選択領域のRect</param>
        /// <param name="backgroundColor">背景色</param>
        /// <returns>フォルダのRect</returns>
        static Rect GetFolderRect(Rect selectionRect, out Color backgroundColor)
        {
            Rect folderRect;
            backgroundColor = new Color(.2f, .2f, .2f);

            if (selectionRect.x < 15)
            {
                // 第二列、小さいスケール
                folderRect = new Rect(selectionRect.x + 3, selectionRect.y, selectionRect.height, selectionRect.height);
            }
            else if (selectionRect.x >= 15 && selectionRect.height < 30)
            {
                // 第一列
                folderRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.height, selectionRect.height);
                backgroundColor = new Color(0.22f, 0.22f, 0.22f);
            }
            else
            {
                // 第二列、大きいスケール
                folderRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width, selectionRect.width);
            }

            return folderRect;
        }

        /// <summary>
        /// フォルダのアイコン設定をリセット
        /// </summary>
        public static void ResetFolderTexture()
        {
            // アクティブなオブジェクトのフォルダパスとGUIDを取得
            string folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            string folderGuid = AssetDatabase.GUIDFromAssetPath(folderPath).ToString();

            // フォルダのアイコン設定を削除
            MineralPrefs.DeleteKey(folderGuid);
        }
    }
}

#endif
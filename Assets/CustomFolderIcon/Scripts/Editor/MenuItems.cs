using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace Tabsil.Mineral
{
    /// <summary>
    /// フォルダのアイコンと関連メニューを管理するクラス
    /// </summary>
    static class MenuItems
    {
        const int priority = 10000;

        /// <summary>
        /// カスタムアイコンを選択するメニュー項目
        /// </summary>
        [MenuItem("Assets/Custom Folder Icon/Custom Icon...", false, priority + 11)]
        static void Custom()
        {
            IconFoldersEditor.ChooseCustomIcon();
        }

        /// <summary>
        /// フォルダアイコンをリセットするメニュー項目
        /// </summary>
        [MenuItem("Assets/Custom Folder Icon/Reset Icon", false, priority + 23)]
        static void ResetIcon()
        {
            ColoredFoldersEditor.ResetFolderTexture();
        }

        /// <summary>
        /// フォルダメニュー項目の有効性を検証するメソッド
        /// </summary>
        [MenuItem("Assets/Custom Folder Icon/Custom Icon...", true)]
        [MenuItem("Assets/Custom Folder Icon/Reset Icon", true)]
        static bool ValidateFolder()
        {
            // 選択されたオブジェクトが存在しない場合、メニュー項目を無効にする
            if (Selection.activeObject == null)
            {
                return false;
            }

            // 選択されたオブジェクトのパス取得
            Object selectedObject = Selection.activeObject;
            string objectPath = AssetDatabase.GetAssetPath(selectedObject);

            // 選択されたオブジェクトがフォルダであるかどうかを検証し、結果を返す
            return AssetDatabase.IsValidFolder(objectPath);
        }
    }
}

#endif
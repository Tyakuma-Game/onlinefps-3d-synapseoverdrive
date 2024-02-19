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
        const int MENU_PRIORITY = 10000;
        const string CUSTOM_ICON_MENU_PATH = "Assets/Custom Folder Icon/Custom Icon...";
        const string RESET_ICON_MENU_PATH = "Assets/Custom Folder Icon/Reset Icon";

        /// <summary>
        /// カスタムアイコンを選択するメニュー項目
        /// </summary>
        [MenuItem(CUSTOM_ICON_MENU_PATH, false, MENU_PRIORITY)]
        static void SelectCustomIcon()
        {
            IconFoldersEditor.ChooseCustomIcon();
        }

        /// <summary>
        /// フォルダアイコンをリセットするメニュー項目
        /// </summary>
        [MenuItem(RESET_ICON_MENU_PATH, false, MENU_PRIORITY + 1)]
        static void ResetFolderIcon()
        {
            ColoredFoldersEditor.ResetFolderTexture();
        }

        /// <summary>
        /// フォルダメニュー項目の有効性を検証するメソッド
        /// </summary>
        [MenuItem(CUSTOM_ICON_MENU_PATH, true)]
        [MenuItem(RESET_ICON_MENU_PATH, true)]
        static bool IsFolderSelected()
        {
            // 選択されたオブジェクトが存在しない場合、メニュー項目を無効
            if (Selection.activeObject == null)
                return false;

            // 選択されたオブジェクトのパス取得
            Object selectedObject = Selection.activeObject;
            string objectPath = AssetDatabase.GetAssetPath(selectedObject);

            // 選択されたオブジェクトがフォルダであるかどうかを検証し、結果を返す
            return AssetDatabase.IsValidFolder(objectPath);
        }
    }
}

#endif
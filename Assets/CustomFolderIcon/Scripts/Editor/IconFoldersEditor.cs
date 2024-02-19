using UnityEngine;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;

namespace Tabsil.Mineral
{
    /// <summary>
    /// アイコンフォルダーエディタークラス
    /// </summary>
    [InitializeOnLoad]
    static class IconFoldersEditor
    {
        static string selectedFolderGuid;
        static int controlID;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static IconFoldersEditor()
        {
            // プロジェクトウィンドウのアイテム描画時にコールバックを登録
            EditorApplication.projectWindowItemOnGUI -= OnGUI;
            EditorApplication.projectWindowItemOnGUI += OnGUI;
        }

        // <summary>
        /// プロジェクトウィンドウのアイテムGUIを描画
        /// </summary>
        /// <param name="guid">アイテムのGUID</param>
        /// <param name="selectionRect">選択範囲の矩形</param>
        static void OnGUI(string guid, Rect selectionRect)
        {
            // 選択されたフォルダのGUIDと一致しない場合は何もしない
            if (guid != selectedFolderGuid)
                return;

            // オブジェクト選択が更新された場合、選択されたオブジェクトのGUIDを取得して保存
            if (Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == controlID)
            {
                Object selectedObject = EditorGUIUtility.GetObjectPickerObject();

                string folderTextureGuid = AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(selectedObject)).ToSafeString();
                MineralPrefs.SetString(selectedFolderGuid, folderTextureGuid);
            }
        }

        /// <summary>
        /// カスタムアイコンを選択
        /// </summary>
        public static void ChooseCustomIcon()
        {
            // 選択されたフォルダのGUIDを取得
            selectedFolderGuid = Selection.assetGUIDs[0];

            // オブジェクトピッカーを表示するためのコントロールIDを取得し、オブジェクトピッカーを表示
            controlID = EditorGUIUtility.GetControlID(FocusType.Passive);
            EditorGUIUtility.ShowObjectPicker<Sprite>(null, false, "", controlID);
        }
    }
}

#endif
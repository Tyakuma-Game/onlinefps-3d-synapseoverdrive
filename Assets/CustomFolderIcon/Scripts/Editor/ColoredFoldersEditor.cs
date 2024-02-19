using UnityEngine;

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
        const string NONE_ICON_GUID = "00000000000000000000000000000000";
        const float SMALL_SCALE_OFFSET            = 3f;
        const float SMALL_SCALE_THRESHOLD         = 15f;
        const float SMALL_SCALE_HEIGHT            = 30f;
        const float DEFAULT_BACKGROUND_COLOR      = 0.2f;
        const float FIRST_COLUMN_BACKGROUND_COLOR = 0.22f;

        /// <summary>
        /// コンストラクタ
        /// </summary>
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
            if (iconGuid == "" || iconGuid == NONE_ICON_GUID)
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
            backgroundColor = new Color(DEFAULT_BACKGROUND_COLOR, DEFAULT_BACKGROUND_COLOR, DEFAULT_BACKGROUND_COLOR);

            if (selectionRect.x < SMALL_SCALE_THRESHOLD)
            {
                // 第二列、小さいスケール
                folderRect = new Rect(selectionRect.x + SMALL_SCALE_OFFSET, selectionRect.y, selectionRect.height, selectionRect.height);
            }
            else if (selectionRect.x >= SMALL_SCALE_THRESHOLD && selectionRect.height < SMALL_SCALE_HEIGHT)
            {
                // 第一列
                folderRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.height, selectionRect.height);
                backgroundColor = new Color(FIRST_COLUMN_BACKGROUND_COLOR, FIRST_COLUMN_BACKGROUND_COLOR, FIRST_COLUMN_BACKGROUND_COLOR);
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
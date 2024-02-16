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
        const string NoneIconGUID = "00000000000000000000000000000000";
        const float SmallScaleOffset = 3f;
        const float SmallScaleThreshold = 15f;
        const float SmallScaleHeight = 30f;
        const float DefaultBackgroundColor = .2f;
        const float FirstColumnBackgroundColor = 0.22f;

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
            if (iconGuid == "" || iconGuid == NoneIconGUID)
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
            backgroundColor = new Color(DefaultBackgroundColor, DefaultBackgroundColor, DefaultBackgroundColor);

            if (selectionRect.x < SmallScaleThreshold)
            {
                // 第二列、小さいスケール
                folderRect = new Rect(selectionRect.x + SmallScaleOffset, selectionRect.y, selectionRect.height, selectionRect.height);
            }
            else if (selectionRect.x >= SmallScaleThreshold && selectionRect.height < SmallScaleHeight)
            {
                // 第一列
                folderRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.height, selectionRect.height);
                backgroundColor = new Color(FirstColumnBackgroundColor, FirstColumnBackgroundColor, FirstColumnBackgroundColor);
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
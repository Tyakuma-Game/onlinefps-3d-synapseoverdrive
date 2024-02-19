using UnityEngine;
using System.IO;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;

namespace Tabsil.Mineral
{
    /// <summary>
    /// エディタ内で使用する設定データ管理クラス
    /// </summary>
    [InitializeOnLoad]
    public static class MineralPrefs
    {
        const string DATA_ASSET_NAME = "CustomFolderData";
        static string dataPath;
        static Data dataObject;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static MineralPrefs()
        {
            dataPath = GetAssetPath(DATA_ASSET_NAME);
            LoadData();
        }

        /// <summary>
        /// 指定されたアセット名に基づいてパス取得
        /// </summary>
        /// <param name="assetName">アセット名</param>
        /// <returns>アセットのパス</returns>
        static string GetAssetPath(string assetName)
        {
            string[] assetPaths = AssetDatabase.FindAssets(assetName);
            if (assetPaths.Length > 0)
            {
                string assetGUID = assetPaths[0];
                return AssetDatabase.GUIDToAssetPath(assetGUID);
            }
            else
            {
                Debug.LogError("アセットが見つかりません: " + assetName);
                return null;
            }
        }

        /// <summary>
        /// データを読み込み
        /// ・ファイルが存在しない場合は新規作成
        /// </summary>
        static void LoadData()
        {
            try
            {
                if (!File.Exists(dataPath))
                {
                    dataObject = new Data();
                    SaveData();
                }
                else
                {
                    string data = File.ReadAllText(dataPath);
                    dataObject = JsonUtility.FromJson<Data>(data);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("データの読み込みに失敗しました: " + e.Message);
            }
        }

        /// <summary>
        /// データ保存
        /// </summary>
        static void SaveData()
        {
            try
            {
                string data = JsonUtility.ToJson(dataObject, true);
                File.WriteAllText(dataPath, data);
            }
            catch (System.Exception e)
            {
                Debug.LogError("データの保存に失敗しました: " + e.Message);
            }
        }

        /// <summary>
        /// 指定したキーに対応する文字列取得
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="defaultValue">キーが存在しない場合のデフォルト値</param>
        /// <returns>キーに対応する文字列、またはデフォルト値</returns>
        public static string GetString(string key, string defaultValue)
        {
            return dataObject.GetString(key, defaultValue);
        }

        /// <summary>
        /// 指定したキーと文字列設定
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="value">設定する文字列</param>
        public static void SetString(string key, string value)
        {
            dataObject.SetString(key, value);
            SaveData();
        }

        /// <summary>
        /// 指定したキーに対応する文字列を削除
        /// </summary>
        /// <param name="key">キー</param>
        public static void DeleteKey(string key)
        {
            dataObject.DeleteKey(key);
            SaveData();
        }
    }
}

#endif
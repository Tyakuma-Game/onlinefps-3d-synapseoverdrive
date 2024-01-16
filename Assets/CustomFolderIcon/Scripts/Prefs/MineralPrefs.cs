using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;

namespace Tabsil.Mineral
{
    // folderIconData
    /// <summary>
    /// フォルダーの設定を管理するクラス
    /// </summary>
    [InitializeOnLoad]
    public static class MineralPrefs
    {
        static string dataPath;
        static Data dataObject;

        static MineralPrefs()
        {
            dataPath = GetAssetPath("CustomFolderData");
            LoadData();
        }

        /// <summary>
        /// アセットのパスを取得するメソッド
        /// </summary>
        /// <param name="assetName">アセットの名前</param>
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
        /// データの読み込み
        /// </summary>
        static void LoadData()
        {
            if(!File.Exists(dataPath))
            {
                // データファイルを作成
                FileStream fs = new FileStream(dataPath, FileMode.Create);

                Data dataObject = new Data();

                string data = JsonUtility.ToJson(dataObject);
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);

                fs.Write(dataBytes);
                fs.Close();
            }
            else
            {
                string data = File.ReadAllText(dataPath);

                if(data.Length <= 0 )
                {
                    return;
                }

                dataObject = JsonUtility.FromJson<Data>(data);
            }
        }

        /// <summary>
        /// データ保存
        /// </summary
        static void SaveData()
        {
            string data = JsonUtility.ToJson(dataObject, true);
            File.WriteAllText(dataPath, data);
        }

        /// <summary>
        /// 指定したキーに対応する文字列を取得
        /// </summary>
        /// <param name="key">取得する値のキー</param>
        /// <param name="defultValue">存在しなかった際に返す値</param>
        /// <returns>キーに対応する値</returns>
        public static string GetString(string key, string defultValue)
        {
            return dataObject.GetString(key, defultValue);
        }

        /// <summary>
        /// 指定したキーに対応する文字列を設定
        /// </summary>
        /// <param name="key">保存するキー</param>
        /// <param name="value">保存する値</param>
        public static void SetString(string key,string value)
        {
            dataObject.SetString(key, value);
            SaveData();
        }

        /// <summary>
        /// 指定したキーに対応するデータの削除
        /// </summary>
        /// <param name="key">削除するキー</param>
        public static void DeleteKey(string key)
        {
            dataObject.DeleteKey(key);
            SaveData();
        }
    }
}

#endif
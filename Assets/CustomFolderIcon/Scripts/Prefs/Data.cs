using UnityEngine;
using System.Collections.Generic;

namespace Tabsil.Mineral
{
    /// <summary>
    /// 設定データクラス
    /// </summary>
    [System.Serializable]
    public class Data
    {
        /// <summary>
        /// キーと値のペアを保持するクラス
        /// </summary>
        [System.Serializable]
        public class KeyValuePair
        {
            public string key;
            public string value;
        }

        [SerializeField] List<KeyValuePair> keyValuePairs = new List<KeyValuePair>();   // シリアライズ用のキーと値のペアリスト
        Dictionary<string, string> dictionary = new Dictionary<string, string>();       // 実際のデータデータ操作辞書

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Data()
        {
            // キーと値のペアから辞書構築
            foreach (var pair in keyValuePairs)
                dictionary[pair.key] = pair.value;
        }

        /// <summary>
        /// 指定したキーに対応する文字列取得
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="defaultValue">キーが存在しない場合のデフォルト値</param>
        /// <returns>対応する文字列、またはデフォルト値</returns>
        public string GetString(string key, string defaultValue)
        {
            if (dictionary.TryGetValue(key, out string value))
                return value;

            return defaultValue;
        }

        /// <summary>
        /// 指定したキーと文字列を設定
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="value">設定する文字列</param>
        public void SetString(string key, string value)
        {
            dictionary[key] = value;

            // シリアライズ用のリスト更新
            var existingPair = keyValuePairs.Find(pair => pair.key == key);
            if (existingPair != null)
                existingPair.value = value;
            else
                keyValuePairs.Add(new KeyValuePair { key = key, value = value });
        }

        /// <summary>
        /// 指定したキーに対応するデータ削除
        /// </summary>
        /// <param name="key">キー</param>
        public void DeleteKey(string key)
        {
            dictionary.Remove(key);
            keyValuePairs.RemoveAll(pair => pair.key == key);
        }
    }
}
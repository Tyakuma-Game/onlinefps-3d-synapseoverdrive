using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tabsil.Mineral
{
    /// <summary>
    /// 設定データ管理クラス
    /// </summary>
    [System.Serializable]
    public class Data
    {
        [SerializeField]
        List<string> keys;

        [SerializeField]
        List<string> values;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Data()
        {
            keys = new List<string>();
            values = new List<string>();
        }

        /// <summary>
        /// 指定したキーに対応する文字列取得
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="defultValue">存在しなかった際に返されるデフォルトの値</param>
        /// <returns></returns>
        public string GetString(string key, string defultValue)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] == key)
                {
                    return values[i];
                }
            }

            return defultValue;
        }

        /// <summary>
        /// 指定したキーに対応する文字列を設定
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="value">存在しなかった際に返されるデフォルトの値</param>
        public void SetString(string key, string value)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] == key)
                {
                    values[i] = value;
                    return;
                }
            }

            // キーが存在しない場合は、新しく追加
            keys.Add(key);
            values.Add(value);
        }

        /// <summary>
        /// 指定したキーに対応するデータを削除
        /// </summary>
        public void DeleteKey(string key)
        {
            int indexToRemove = -1;

            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] == key)
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove < 0)
            {
                return;
            }

            keys.RemoveAt(indexToRemove);
            values.RemoveAt(indexToRemove);
        }
    }
}
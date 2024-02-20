
namespace OnDamageEvent
{
    /// <summary>
    /// ダメージイベントで使用するデータクラス
    /// </summary>
    public class DamageEventData
    {
        public int DamageAmount { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="damageAmount">ダメージ量</param>
        public DamageEventData(int damageAmount)
        {
            DamageAmount = damageAmount;
        }
    }
}
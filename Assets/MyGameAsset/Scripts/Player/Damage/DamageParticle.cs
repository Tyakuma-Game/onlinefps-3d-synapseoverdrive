using UnityEngine;

namespace OnDamageEvent
{

    public class DamageParticle : MonoBehaviour
    {
        [SerializeField] GameObject damageEffect;

        void Start()
        {
            //Å@FindObjectOfType<Player>().OnDamage += CreateDamageParticle;
        }

        void CreateDamageParticle(DamageEventData eventData)
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);
        }
    }
}
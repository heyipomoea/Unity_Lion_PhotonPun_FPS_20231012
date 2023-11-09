using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Heyipomoea
{
    /// <summary>
    /// 受傷系統
    /// </summary>
    public class DamageSystem : MonoBehaviour
    {
        private float hp = 100;
        private float maxHp = 100;

        [SerializeField, Header("血條")]
        private Image imgHp;
        [SerializeField, Header("血量文字")]
        private TextMeshProUGUI textHp;

        private string bulletName = "子彈";

        /// <summary>
        /// 受傷
        /// </summary>
        /// <param name="damage">獲得的傷害</param>
        private void Damage(float damage)
        {
            hp -= damage;
            imgHp.fillAmount = hp / maxHp;
            textHp.text = $"{hp} / {maxHp}";
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.name.Contains(bulletName))
            {
                float bulletDamage = collision.gameObject.GetComponent<Bullet>().bulletDamage;
                Damage(bulletDamage);
            }
        }
    }
}


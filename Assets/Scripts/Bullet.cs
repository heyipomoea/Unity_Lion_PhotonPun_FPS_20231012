using UnityEngine;

namespace Heyipomoea
{
    /// <summary>
    /// 子彈
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        /// <summary>
        /// 子彈的目標
        /// </summary>
        [HideInInspector]
        public Vector3 targetPoint;

        [SerializeField, Header("子彈移動速度"), Range(0, 1000)]
        private float speed = 60;

        [Header("子彈傷害"), Range(0, 50)]
        public float bulletDamage = 10;

        private void Update()
        {
            Move();
        }

        /// <summary>
        /// 移動
        /// </summary>
        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}


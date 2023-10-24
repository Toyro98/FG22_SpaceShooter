using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        /*
        [SerializeField] private float _speed = 100.0f;
        [SerializeField] private int _damage = 10;

        private void Update()
        {
            transform.position += _speed * Time.deltaTime * transform.up;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
        */
    }
}

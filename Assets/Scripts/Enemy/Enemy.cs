using UnityEngine;

namespace SpaceShooter
{
    public sealed class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _health = 1;
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _speed = 1.0f;

        private Transform _player;

        void Start()
        {
            _player = GameManager.Instance.Player.transform;
        }

        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, Time.deltaTime * _speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                player.TakeDamage(_damage);
                Destroy();
            }
        }

        public void TakeDamage(int amount)
        {
            _health -= amount;

            if (_health <= 0) 
            { 
                Destroy();
            }
        }

        public void Destroy()
        {
            GameManager.Instance.WaveManager.EnemyDied();
            Destroy(gameObject);
        }
    }
}

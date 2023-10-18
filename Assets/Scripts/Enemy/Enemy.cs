using UnityEngine;

namespace SpaceShooter
{
    public sealed class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _health = 1;
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _speed = 1.0f;

        private Player _player;

        void Start()
        {
            _player = GameManager.Instance.Player;
        }

        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, Time.deltaTime * _speed);

            if (Vector2.Distance(transform.position, _player.transform.position) < 1.0f)
            {
                _player.TakeDamage(_damage);
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

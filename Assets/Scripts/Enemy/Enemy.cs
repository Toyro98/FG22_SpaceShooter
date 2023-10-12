using UnityEngine;

namespace SpaceShooter
{
    public sealed class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] int _health = 1;
        [SerializeField] float _speed = 1.0f;

        Transform _player;

        void Start()
        {
            _player = GameManager.Instance.Player.transform;
        }

        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, Time.deltaTime * _speed);
        }

        public void TakeDamage(int amount)
        {
            _health -= amount;

            if (_health <= 0) 
            { 
                Destroy(gameObject);
            }
        }
    }
}
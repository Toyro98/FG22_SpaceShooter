using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public sealed class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _currentHealth = 100;
        [SerializeField] private int _maxHealth;

        private void Start()
        {
            _maxHealth = _currentHealth;
            GameManager.Instance.Player = this;
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;

            if (_currentHealth <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }

        public void RestoreHealth()
        {
            _currentHealth = _maxHealth;
        }
    }
}

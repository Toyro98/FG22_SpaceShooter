using UnityEngine;

namespace SpaceShooter
{
    public sealed class Player : MonoBehaviour, IDamageable
    {
        // [SerializeField] int _health = 10;

        void Start()
        {
            GameManager.Instance.Player = this;
        }

        public void TakeDamage(int amount)
        {
            throw new System.NotImplementedException();
        }
    }
}

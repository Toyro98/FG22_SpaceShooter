using UnityEngine;

namespace SpaceShooter
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public int Id;

        public void TakeDamage(int amount)
        {
            GameManager.Instance.WaveManager.RemoveEnemy(this);
        }
    }
}

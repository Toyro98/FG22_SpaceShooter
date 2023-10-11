using UnityEngine;

namespace SpaceShooter
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public Player Player;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }

            Instance = this;
        }
    }
}

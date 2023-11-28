using UnityEngine;

namespace SpaceShooter
{
    public class Player : MonoBehaviour
    {
        public float MoveSpeed;
        public float RotationSpeed;

        public float FireRate;
        public float LastTimeFired;
        public GameObject ProjectilePrefab;
    }
}

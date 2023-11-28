using Unity.Entities;

namespace SpaceShooter
{
    public struct PlayerProperties : IComponentData
    {
        public float MoveSpeed;
        public float RotationSpeed;

        public float FireRate;
        public float LastTimeFired;
        public Entity ProjectilePrefab;
    }
}

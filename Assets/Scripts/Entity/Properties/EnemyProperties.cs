using Unity.Entities;

namespace SpaceShooter
{
    public struct EnemyProperties : IComponentData
    {
        public int Health;
        public float MoveSpeed;
    }
}

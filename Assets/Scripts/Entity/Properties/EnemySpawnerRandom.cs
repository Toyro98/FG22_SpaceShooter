using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShooter
{
    public struct EnemySpawnerRandom : IComponentData
    {
        public Random Value;
    }
}

using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShooter
{
    public struct EnemySpawnerProperties : IComponentData
    {
        public float2 SpawnRange;
        public float TimeToSpawnEnemies;
        public int NumberOfEnemiesToSpawn;
        public Entity EnemyPrefab;
    }
}

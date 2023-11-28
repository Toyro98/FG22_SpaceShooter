using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShooter
{
    public class EnemySpawnerBaker : Baker<EnemySpawner>
    {
        public override void Bake(EnemySpawner authoring)
        {
            var spawnerEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(spawnerEntity, new EnemySpawnerProperties
            {
                SpawnRange = authoring.SpawnRange,
                CurrentWaveNumber = authoring.CurrentWaveNumber,
                TimeToSpawnEnemies = authoring.TimeToSpawnEnemies,
                NumberOfEnemiesToSpawn = authoring.NumberOfEnemiesToSpawn,
                EnemyPrefab = GetEntity(authoring.EnemyPrefab, TransformUsageFlags.Dynamic),
            });

            AddComponent(spawnerEntity, new EnemySpawnerRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
        }
    }
}

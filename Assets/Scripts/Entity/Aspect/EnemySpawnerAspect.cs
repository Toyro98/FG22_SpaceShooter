using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShooter
{
    public readonly partial struct EnemySpawnerAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<EnemySpawnerProperties> _enemySpawnerProperties;
        private readonly RefRW<EnemySpawnerRandom> _enemySpawnerRandom;

        public float TimeToSpawnEnemies => _enemySpawnerProperties.ValueRW.TimeToSpawnEnemies;
        public int NumberOfEnemiesToSpawn => _enemySpawnerProperties.ValueRO.NumberOfEnemiesToSpawn;
        public Entity EnemyPrefab => _enemySpawnerProperties.ValueRO.EnemyPrefab;

        public LocalTransform SetEnemyTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = quaternion.identity,
                Scale = 1.0f
            };
        }

        private float3 GetRandomPosition()
        {
            return _enemySpawnerRandom.ValueRW.Value.NextFloat3(Min, Max);
        }

        private float3 Min => _transform.ValueRO.Position - SpawnRange;
        private float3 Max => _transform.ValueRO.Position + SpawnRange;
        private float3 SpawnRange => new()
        {
            x = _enemySpawnerProperties.ValueRO.SpawnRange.x,
            y = _enemySpawnerProperties.ValueRO.SpawnRange.y,
            z = 0.0f
        };

        public void UpdateSpawnTime(float newTime)
        {
            _enemySpawnerProperties.ValueRW.TimeToSpawnEnemies = newTime;
        }

        public void IncreaseWaveDifficulty()
        {
            int wave = _enemySpawnerProperties.ValueRW.CurrentWaveNumber++;
            _enemySpawnerProperties.ValueRW.NumberOfEnemiesToSpawn = _enemySpawnerProperties.ValueRW.NumberOfEnemiesToSpawn * wave * wave;
        }
    }
}

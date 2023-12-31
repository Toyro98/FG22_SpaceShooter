using Unity.Collections;
using Unity.Entities;

namespace SpaceShooter
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct EnemySpawnerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemySpawnerProperties>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var enemySpawnerEntity = SystemAPI.GetSingletonEntity<EnemySpawnerProperties>();
            var enemySpawner = SystemAPI.GetAspect<EnemySpawnerAspect>(enemySpawnerEntity);

            if (enemySpawner.TimeToSpawnEnemies > SystemAPI.Time.ElapsedTime)
            {
                return;
            }

            enemySpawner.UpdateSpawnTime(float.MaxValue);
            enemySpawner.IncreaseWaveDifficulty();

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            for (int i = 0; i < enemySpawner.NumberOfEnemiesToSpawn; i++)
            {
                var newEnemy = ecb.Instantiate(enemySpawner.EnemyPrefab);
                var newEnemyTransform = enemySpawner.SetEnemyTransform();

                ecb.SetComponent(newEnemy, newEnemyTransform);
            }

            ecb.Playback(state.EntityManager);
        }

        public void OnDestory(ref SystemState state) {}
    }
}

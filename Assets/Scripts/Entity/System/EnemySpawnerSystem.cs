using Unity.Collections;
using Unity.Burst;
using Unity.Entities;

namespace SpaceShooter
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct EnemySpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemySpawnerProperties>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var enemySpawnerEntity = SystemAPI.GetSingletonEntity<EnemySpawnerProperties>();
            var enemySpawner = SystemAPI.GetAspect<EnemySpawnerAspect>(enemySpawnerEntity);

            if (enemySpawner.TimeToSpawnEnemies > SystemAPI.Time.ElapsedTime)
            {
                return;
            }

            enemySpawner.UpdateSpawnTime(float.MaxValue);
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            for (int i = 0; i < enemySpawner.NumberOfEnemiesToSpawn; i++)
            {
                var enemy = ecb.Instantiate(enemySpawner.EnemyPrefab);
                var enemyTransform = enemySpawner.GetRandomEnemyTransform();

                ecb.SetComponent(enemy, enemyTransform);
            }

            ecb.Playback(state.EntityManager);
        }

        [BurstCompile]
        public void OnDestory(ref SystemState state)
        {

        }
    }
}

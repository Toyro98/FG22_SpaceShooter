using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace SpaceShooter
{
    [BurstCompile]
    public partial struct EnemySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;

            new EnemyJob
            {
                DeltaTime = deltaTime,
                PlayerPosition = playerPosition,
            }.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestory(ref SystemState state)
        {

        }
    }
}

using Unity.Entities;
using Unity.Transforms;

namespace SpaceShooter
{
    public partial struct EnemySystem : ISystem
    {
        public void OnCreate(ref SystemState state) {}

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

        public void OnDestory(ref SystemState state) {}
    }
}

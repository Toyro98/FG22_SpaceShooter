using Unity.Burst;
using Unity.Entities;

namespace SpaceShooter
{
    [BurstCompile]
    public partial struct ProjectileSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) 
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            new ProjectileJob
            {
                DeltaTime = deltaTime,
            }.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestory(ref SystemState state) { }
    }
}

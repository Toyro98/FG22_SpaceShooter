using Unity.Entities;

namespace SpaceShooter
{
    public partial struct ProjectileSystem : ISystem
    {
        public void OnCreate(ref SystemState state) { }

        public void OnUpdate(ref SystemState state) 
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            new ProjectileJob
            {
                DeltaTime = deltaTime,
            }.ScheduleParallel();
        }

        public void OnDestory(ref SystemState state) { }
    }
}

using Unity.Burst;
using Unity.Entities;

namespace SpaceShooter
{
    [BurstCompile]
    public partial struct ProjectileJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(ProjectileAspect projectile)
        {
            projectile.Move(DeltaTime);
        }
    }
}

using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShooter
{
    [BurstCompile]
    public partial struct EnemyJob : IJobEntity
    {
        public float DeltaTime;
        public float3 PlayerPosition;

        [BurstCompile]
        private void Execute(EnemyAspect enemy)
        {
            enemy.Move(DeltaTime, PlayerPosition);
        }
    }
}

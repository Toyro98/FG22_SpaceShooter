using Unity.Burst;
using Unity.Entities;

namespace SpaceShooter
{
    [BurstCompile]
    public partial struct PlayerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state) 
        {
            state.RequireForUpdate<PlayerProperties>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerProperties>();
            var player = SystemAPI.GetAspect<PlayerAspect>(playerEntity);
            float deltaTime = SystemAPI.Time.DeltaTime;

            player.Move(deltaTime);
        }

        [BurstCompile]
        public void OnDestory(ref SystemState state) {}
    }
}

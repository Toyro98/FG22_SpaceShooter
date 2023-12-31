using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace SpaceShooter
{
    public partial struct PlayerSystem : ISystem
    {
        public void OnCreate(ref SystemState state) 
        {
            state.RequireForUpdate<PlayerProperties>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerProperties>();
            var player = SystemAPI.GetAspect<PlayerAspect>(playerEntity);

            float deltaTime = SystemAPI.Time.DeltaTime;
            float elapsedTime = (float)SystemAPI.Time.ElapsedTime;

            player.Movement(deltaTime);

            if (!Input.GetKey(KeyCode.Space))
            {
                return;
            }

            if (!player.CanShootProjectile(elapsedTime))
            {
                return;
            }

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            var newProjectile = ecb.Instantiate(player.ProjectilePrefab);
            var newProjectileTransform = player.SetProjectileTransform();

            ecb.SetComponent(newProjectile, newProjectileTransform);
            ecb.Playback(state.EntityManager);
        }

        public void OnDestory(ref SystemState state) {}
    }
}

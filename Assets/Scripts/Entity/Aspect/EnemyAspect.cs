using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShooter
{
    public readonly partial struct EnemyAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRO<EnemyProperties> _properties;

        public void Move(float deltaTime, float3 playerPosition)
        {
            _transform.ValueRW.Position = Extensions.MoveTowards(_transform.ValueRW.Position, playerPosition, _properties.ValueRO.MoveSpeed * deltaTime);
        }
    }
}

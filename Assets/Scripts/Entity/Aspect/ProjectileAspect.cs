using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShooter
{
    public readonly partial struct ProjectileAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRO<ProjectileProperties> _properties;

        public float3 Position => _transform.ValueRO.Position;

        public void Move(float deltaTime)
        {
            _transform.ValueRW.Position += _transform.ValueRW.Up() * _properties.ValueRO.Speed * deltaTime;
        }
    }
}

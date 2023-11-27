using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SpaceShooter
{
    public readonly partial struct PlayerAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRO<PlayerProperties> _properties;

        public void Move(float deltaTime)
        {
            var forward = Input.GetAxis("Vertical") * deltaTime;
            var rotate = Input.GetAxis("Horizontal") * deltaTime;

            _transform.ValueRW.Position += _transform.ValueRW.Up() * forward * _properties.ValueRO.MoveSpeed;
            _transform.ValueRW.Rotation = math.mul(quaternion.RotateZ(-rotate * _properties.ValueRO.RotationSpeed), _transform.ValueRO.Rotation);
        }
    }
}

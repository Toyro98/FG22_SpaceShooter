using System;
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
        private readonly RefRW<PlayerProperties> _properties;

        public Entity ProjectilePrefab => _properties.ValueRO.ProjectilePrefab;

        public void Movement(float deltaTime)
        {
            var forward = Input.GetAxis("Vertical") * deltaTime;
            var rotate = Input.GetAxis("Horizontal") * deltaTime;

            _transform.ValueRW.Position += _transform.ValueRW.Up() * forward * _properties.ValueRO.MoveSpeed;
            _transform.ValueRW.Rotation = math.mul(quaternion.RotateZ(-rotate * _properties.ValueRO.RotationSpeed), _transform.ValueRO.Rotation);
        }

        public bool CanShootProjectile(float elapsedTime)
        {
            if (elapsedTime - _properties.ValueRO.LastTimeFired > _properties.ValueRO.FireRate)
            {
                _properties.ValueRW.LastTimeFired = elapsedTime;
                return true;
            }

            return false;
        }

        public LocalTransform SetProjectileTransform()
        {
            return new LocalTransform()
            {
                Position = _transform.ValueRW.Position,
                Rotation = _transform.ValueRW.Rotation,
                Scale = 1.0f
            };
        }
    }
}

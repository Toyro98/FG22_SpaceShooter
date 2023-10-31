using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;

namespace SpaceShooter
{
    [BurstCompile]
    public struct EnemyJob : IJob
    {
        private Vector2 _playerPosition;
        private int _health;
        private int _damage;
        private float _speed;
        private float _deltaTime;
        private Vector2 _currentPosition;

        private NativeArray<Vector2> _positionResult;

        public EnemyJob(Vector2 playerPosition, int health, int damage, float speed, float deltaTime, Vector2 currentPosition, NativeArray<Vector2> positionResult)
        {
            _playerPosition = playerPosition;
            _health = health;
            _damage = damage;
            _speed = speed;
            _deltaTime = deltaTime;
            _currentPosition = currentPosition;
            _positionResult = positionResult;
        }

        public void Execute()
        {
            _currentPosition = Vector2.MoveTowards(_currentPosition, _playerPosition, _deltaTime * _speed);
            _positionResult[0] = _currentPosition;
        }
    }
}

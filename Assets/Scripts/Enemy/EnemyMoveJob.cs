using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace SpaceShooter
{
    public class EnemyMoveJob : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private int _health = 1;
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _speed = 1.0f;

        JobHandle _jobHandle;
        NativeArray<Vector2> _positionResult;

        private void Awake()
        {
            _player = GameManager.Instance.Player;
            _positionResult = new NativeArray<Vector2>(1, Allocator.Persistent);
        }

        public void Update()
        {
            EnemyJob enemyJob = new EnemyJob()
            {
                CurrentPosition = transform.position,
                PlayerPosition = Vector2.zero,
                Speed = _speed,
                Health = _health,
                Damage = _damage,
                DeltaTime = Time.deltaTime,
                PositionResult = _positionResult,
            };

            _jobHandle = enemyJob.Schedule();
            _jobHandle.Complete();

            transform.position = _positionResult[0];
        }

        public void TakeDamage(int amount)
        {
            _health -= amount;

            if (_health <= 0)
            {
                Destroy();
            }
        }

        public void Destroy()
        {
            GameManager.Instance.WaveManager.EnemyDied();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _positionResult.Dispose();
        }
    }
}

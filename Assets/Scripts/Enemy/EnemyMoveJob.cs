using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace SpaceShooter
{
    public class EnemyMoveJob : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private int _health = 1;
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _speed = 1.0f;

        private NativeArray<Vector2> _positionResult;

        private JobHandle _jobHandle;

        private void Awake()
        {
            _positionResult = new NativeArray<Vector2>(1, Allocator.Persistent);
        }

        private void Start()
        {
            _player = GameManager.Instance.Player;
        }

        public void Update()
        {
            EnemyJob enemyJob = new EnemyJob(
                _player.transform.position,
                _health,
                _damage,
                _speed,
                Time.deltaTime,
                transform.position,
                _positionResult
            );

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

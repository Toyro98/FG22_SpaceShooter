using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace SpaceShooter
{
    public sealed class WaveManager : MonoBehaviour
    {
        [Header("Wave")]
        [SerializeField] private int _threshold = 50;
        [SerializeField] private int _currentWave = 1;
        [SerializeField] private float _spawnRadius = 100.0f;

        [Header("Time")]
        [SerializeField] private float _timeWhenToSpawnEnemies = 2.5f;
        [SerializeField] private float _timeDelayToSpawnNextWave = 10.0f;

        [Header("Enemy")]
        [SerializeField] private int _enemiesAlive = 0;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private List<Enemy> _enemies;
        [SerializeField] private List<Enemy> _enemiesToDelete;

        JobHandle _jobHandle;
        NativeArray<Vector3> _nativeEnemyPosition;
        NativeArray<float> _nativeDistanceToPlayer;

        private void Start()
        {
            _timeWhenToSpawnEnemies = Time.timeSinceLevelLoad + _timeWhenToSpawnEnemies;
        }

        private void Update()
        {
            if (Time.timeSinceLevelLoad > _timeWhenToSpawnEnemies)
            {
                SpawnNewWave();
                _timeWhenToSpawnEnemies = float.MaxValue;
            }

            if (_enemies.Count == 0)
            {
                return;
            }

            UpdateEnemyPosition();
        }

        private void UpdateEnemyPosition()
        {
            int enemiesCount = 0;

            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] != null)
                {
                    enemiesCount++;
                }
            }

            _nativeEnemyPosition = new NativeArray<Vector3>(enemiesCount, Allocator.TempJob);
            _nativeDistanceToPlayer = new NativeArray<float>(enemiesCount, Allocator.TempJob);

            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] == null)
                {
                    continue;
                }

                _nativeEnemyPosition[i] = _enemies[i].transform.position;
            }

            EnemyJob job = new EnemyJob()
            {
                CurrentPosition = _nativeEnemyPosition,
                PlayerPosition = GameManager.Instance.Player.transform.position,
                Speed = 1, // Constant speed
                DeltaTime = Time.deltaTime,
                DistanceToPlayer = _nativeDistanceToPlayer
            };

            _jobHandle = job.Schedule(_enemies.Count, 64);
        }

        public void LateUpdate()
        {
            _jobHandle.Complete();

            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] == null)
                {
                    continue;
                }

                _enemies[i].transform.position = _nativeEnemyPosition[i];
            }

            _nativeEnemyPosition.Dispose();
            _nativeDistanceToPlayer.Dispose();

            if (_enemiesToDelete.Count == 0)
            {
                return;
            }

            for (int i = 0; i < _enemiesToDelete.Count; i++)
            {
                for (int j = 0; j < _enemies.Count; j++)
                {
                    if (_enemies[i] == null)
                    {
                        continue;
                    }

                    if (_enemiesToDelete[i].Id == _enemies[j].Id)
                    {
                        Destroy(_enemies[j]);
                        break;
                    }
                }
            }

            _enemiesToDelete = null;
        }

        public void RemoveEnemy(Enemy enemy)
        {
            _enemiesToDelete.Add(enemy);
        }

        public void SpawnNewWave()
        {
            int enemiesToSpawn = _enemiesAlive = _threshold * _currentWave * _currentWave * _currentWave;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Vector2 spawnLocation = new Vector2(Random.Range(-_spawnRadius, _spawnRadius), Random.Range(-_spawnRadius, _spawnRadius));
                var spawnedEnemy = Instantiate(_enemyPrefab, spawnLocation, Quaternion.identity);

                spawnedEnemy.name = "Enemy " + (i + 1);
                spawnedEnemy.Id = i;

                _enemies.Add(spawnedEnemy);
            }
        }

        public void EnemyDied()
        {
            _enemiesAlive--;

            if (_enemiesAlive == 0)
            {
                _currentWave++;
                _timeWhenToSpawnEnemies = Time.timeSinceLevelLoad + _timeDelayToSpawnNextWave;

                GameManager.Instance.Player.RestoreHealth();
            }
        }
    }
}

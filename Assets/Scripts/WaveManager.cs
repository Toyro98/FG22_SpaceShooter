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

            float startTime = Time.realtimeSinceStartup;

            var enemyPosition = new NativeArray<Vector3>(_enemies.Count, Allocator.Persistent);

            for (int i = 0; i < _enemies.Count; i++)
            {
                enemyPosition[i] = _enemies[i].transform.position;
            }

            EnemyJob job = new EnemyJob()
            {
                CurrentPosition = enemyPosition,
                PlayerPosition = GameManager.Instance.Player.transform.position,
                Speed = 1, // Constant speed
                DeltaTime = Time.deltaTime,
            };

            job.Schedule(_enemies.Count, 64).Complete();

            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].transform.position = enemyPosition[i];
            }

            enemyPosition.Dispose();

            Debug.Log($"Time: {(Time.realtimeSinceStartup - startTime) * 1000.0f}ms");
        }

        public void SpawnNewWave()
        {
            int enemiesToSpawn = _enemiesAlive = _threshold * _currentWave * _currentWave * _currentWave;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Vector2 spawnLocation = new Vector2(Random.Range(-_spawnRadius, _spawnRadius), Random.Range(-_spawnRadius, _spawnRadius));
                var spawnedEnemy = Instantiate(_enemyPrefab, spawnLocation, Quaternion.identity);

                spawnedEnemy.name = "Enemy " + (i + 1);

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

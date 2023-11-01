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
        [SerializeField] private GameObject _enemyPrefab;

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
        }

        public void SpawnNewWave()
        {
            int enemiesToSpawn = _enemiesAlive = _threshold * _currentWave * _currentWave * _currentWave;
            Debug.Log($"WaveManager: Spawning {enemiesToSpawn} enemies", this);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Vector2 spawnLocation = new Vector2(Random.Range(-_spawnRadius, _spawnRadius), Random.Range(-_spawnRadius, _spawnRadius));
                GameObject spawnedEnemy = Instantiate(_enemyPrefab, spawnLocation, Quaternion.identity);

                spawnedEnemy.name = "Enemy " + (i + 1);
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

using Unity.Mathematics;
using UnityEngine;

namespace SpaceShooter
{
    public class EnemySpawner : MonoBehaviour
    {
        public float2 SpawnRange;
        public float TimeToSpawnEnemies;
        public int CurrentWaveNumber;
        public int NumberOfEnemiesToSpawn;
        public GameObject EnemyPrefab;
        public uint RandomSeed;
    }
}

using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnerEnemies
{
    public class RandomSpawner : MonoBehaviour
    {
        [Header("SpawnSettings")]
        [SerializeField, Range(0f, 60f)] private float _spawnDelay; 
        [SerializeField, Range(0f, 360f)] private float _minRotationY = 0f;
        [SerializeField, Range(0f, 360f)] private float _maxRotationY = 360f;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _prefabSpawnBody;
        private float _amountTime;
        
        private void SpawnInRandomPoint()
        {
            var point = GetRandomSpawnPoint();
            var randomRotation = SetRandomRotationY();
            point.rotation = Quaternion.Euler(point.rotation.x, randomRotation, point.rotation.z);
            Instantiate(_prefabSpawnBody, point.position, point.rotation);
        }

        private void Update()
        {
            _amountTime += Time.deltaTime;
            if (_amountTime >= _spawnDelay)
            {
                _amountTime = 0f;
                SpawnInRandomPoint();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.TryGetComponent(out Enemy.Enemy enemy))
                Destroy(enemy.gameObject);
        }

        private Transform GetRandomSpawnPoint() => _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        private float SetRandomRotationY() => Random.Range(_minRotationY, _maxRotationY);
    }
}
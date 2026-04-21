using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn Point")]
    [SerializeField] private Transform spawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("Falta asignar enemyPrefab o spawnPoint");
            return;
        }

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
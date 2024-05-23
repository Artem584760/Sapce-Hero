using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawn : MonoBehaviour
{
   
    
    [System.Serializable]
    public struct EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public float spawnChance; // От 0 до 100
    }
    
    public List<EnemySpawnInfo> enemiesToSpawn;
    public float spawnRadius = 10f;
    public float minSpawnDistance = 5f;
    public float spawnIntervalMin = 2f;
    public float spawnIntervalMax = 5f;

    [SerializeField] private GameObject crossPrefab;
    [SerializeField] private float crossSpawnBeforeEnemySpawnPause;
 
    private GameObject enemyCanvasObject;

    private void Start()
    {
        enemyCanvasObject = GameObject.FindWithTag("EnemyCanvas");
        float f = 0;

        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            f += enemiesToSpawn[i].spawnChance;
        }

        if (f <= 100)
        {
            StartCoroutine(SpawnEnemiesRoutine());
        }
        else
        {
            Debug.Log("Сумма шансов спавна врагов должна быть меньше или равна 100");
        }
        
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax+1));
            
            int enemiesCount = Random.Range(10, 12); // Спавним от 1 до 4 врагов за раз
            for (int i = 0; i < enemiesCount; i++)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        
        var selectedEnemyOption = ChooseEnemyToSpawn();

        
        if (selectedEnemyOption.HasValue && selectedEnemyOption.Value.enemyPrefab != null)
        {
            Vector3 spawnPosition = RandomPositionWithinRadius();
            StartCoroutine(SpawnEnemyWithCrossCoroutine(spawnPosition, selectedEnemyOption.Value));
        }
    }


    IEnumerator SpawnEnemyWithCrossCoroutine(Vector3 spawnPosition, EnemySpawnInfo enemyInfo)
    {
        GameObject crossInstance = Instantiate(crossPrefab, spawnPosition, Quaternion.identity, enemyCanvasObject.transform);
        yield return new WaitForSeconds(crossSpawnBeforeEnemySpawnPause);
        
        Destroy(crossInstance);
        Instantiate(enemyInfo.enemyPrefab, spawnPosition, Quaternion.identity, enemyCanvasObject.transform);
    }

    private EnemySpawnInfo? ChooseEnemyToSpawn()
    {
        List<EnemySpawnInfo> possibleSpawns = new List<EnemySpawnInfo>();
        
        foreach (var enemy in enemiesToSpawn)
        {
            if (Random.Range(0, 100) < enemy.spawnChance)
            {
                possibleSpawns.Add(enemy);
            }
        }

      
        if (possibleSpawns.Count > 0)
        {
            int randomIndex = Random.Range(0, possibleSpawns.Count);
            return possibleSpawns[randomIndex];
        }
        
        return null;
    }

    private Vector3 RandomPositionWithinRadius()
    {
        Vector3 randomDirection = transform.position + Random.insideUnitSphere * spawnRadius;    
        
        while (Vector3.Distance(transform.position, randomDirection) < minSpawnDistance)
        {
            randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection += transform.position;
            randomDirection.y = 0;
        }

        return randomDirection;
    }
}
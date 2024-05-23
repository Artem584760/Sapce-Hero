using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WizardSummoner : Enemy
{
    [Header("SummonerSettings")]
    [SerializeField] private GameObject miniPetPrefab;
    [SerializeField] private int petsCount;
    [SerializeField] private float petsSpawnRadius, summonPause;
    [SerializeField] private float comfortDistanceToPlayer;

    private GameObject enemyCanvas;
    private float startSpeed;
    private bool isSummoning = true;

    void Start()
    {
        base.Start();
        enemyCanvas = GameObject.FindWithTag("EnemyCanvas");
        startSpeed = enemySpeed;
        StartCoroutine(SummonPets());
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);


        float comfortZoneThreshold = 0.5f;

        if (distanceToPlayer < comfortDistanceToPlayer - comfortZoneThreshold)
        {
            enemySpeed = -startSpeed;
           
        }
        else if (distanceToPlayer > comfortDistanceToPlayer + comfortZoneThreshold)
        {
            enemySpeed = startSpeed;
           
        }
        else
        {
            enemySpeed = 0;
           

        }
    }


    IEnumerator SummonPets()
    {
        while (isSummoning)
        {
            // Проверяем, стоит ли враг на месте или подходит к игроку (скорость больше или равна 0)
            if (enemySpeed >= 0)
            {
                for (int i = 0; i < petsCount; i++)
                {
                    Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle.normalized * petsSpawnRadius;
                    Instantiate(miniPetPrefab, spawnPosition, Quaternion.identity, enemyCanvas.transform);
                }

                // Добавляем задержку перед началом следующего цикла спавна
                yield return new WaitForSeconds(summonPause);
            }
            else
            {
                // Если враг отходит от игрока, ждем немного перед следующей проверкой
                yield return new WaitForSeconds(1f);
            }
        }
    }

}
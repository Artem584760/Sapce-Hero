using System.Collections;
using UnityEngine;

public class Gun : DirectionBase
{
    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("GunSettings")]
    [SerializeField] private float attackDelay;
    [SerializeField] private int burstCount;
    [SerializeField] private float burstSpeed;
    [SerializeField] private float maxEnemyDistance; 

    [Header("BulletSettings")]
    public float speed;
    public float damage;
    public float lifeTime;

    private bool isAttack;

    protected void Update()
    {
        base.Update();
        if (!isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    Vector3 FindClosestEnemyDirection(out float distance)
    {
       
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float currentDistance = Vector3.Distance(enemy.transform.position, position);
          
            if (currentDistance < minDistance)
            {
                closest = enemy;
                minDistance = currentDistance;
            }
        }

        distance = minDistance;
        if (closest != null)
        {
            return closest.transform.position - position;
        }
        else
        {
            return Vector3.zero;
        }
    }

    IEnumerator Attack()
    {
        isAttack = true;
        int i = 0;

        while (i < burstCount)
        {
            float enemyDistance;
            Vector3 direction = FindClosestEnemyDirection(out enemyDistance);
            
            if (enemyDistance <= maxEnemyDistance)
            {
               
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                GameObject projectile = Instantiate(bulletPrefab, transform.position + direction.normalized * OffsetDistance,
                    Quaternion.Euler(new Vector3(0f, 0f, angle)));
                projectile.transform.right = direction.normalized;

             
                yield return new WaitForSeconds(burstSpeed);
            }
            i++;
        }

        yield return new WaitForSeconds(attackDelay);
        isAttack = false;
    }
}

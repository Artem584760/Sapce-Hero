using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElectroZone : MonoBehaviour
{
    public static List<GameObject> damagedEnemy = new List<GameObject>();
    
    private List<GameObject> instanceDamagedEnemies = new List<GameObject>();
    [HideInInspector]
    public float maxscale;

    private ElectroGun _electroGun;
    public static bool isSpawn = true;

    void Start()
    {
        
        _electroGun = FindObjectOfType<ElectroGun>();
        _electroGun.CurrentZonedamage = _electroGun.StartZoneDamage;
    }

    void FixedUpdate()
    {
        if (transform.localScale.x < maxscale )
        {
            transform.localScale += Vector3.one / 15;
            Debug.Log("up");
        }
        else if (transform.localScale.x != 0)
        {
            transform.localScale = new Vector3(maxscale, maxscale, maxscale);
            StartCoroutine(Delete());
        }

        Debug.Log(maxscale);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !damagedEnemy.Contains(other.gameObject) && isSpawn && other.GetComponentInChildren<ElectroZone>() == null)
        {
            other.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            GameObject newZone = Instantiate(_electroGun.DamageZonePrefab, other.transform.position, Quaternion.identity, other.transform);
            newZone.GetComponent<ElectroZone>().maxscale = other.GetComponent<CircleCollider2D>().radius + 0.25f;
            Enemy _enemy = other.GetComponent<Enemy>();
        
            
            _enemy.EnemyGiveDamage(ElectroGun.s_CurrentZonedamage,"Electric");

            
            ElectroGun.s_CurrentZonedamage = Mathf.Max(1, ElectroGun.s_CurrentZonedamage - _electroGun.MinusZoneDamagevalue);
            
        
            StartCoroutine(damaged(other.gameObject));

           
            if (ElectroGun.s_CurrentZonedamage <= _electroGun.MinusZoneDamagevalue)
            {
                isSpawn = false;
            }
          
        }
    }

    
    IEnumerator Delete()
    {

        foreach (var obj in instanceDamagedEnemies)
        {
            damagedEnemy.Remove(obj);
        }

        if (transform.parent != null)
        {
             if(!damagedEnemy.Contains(transform.parent.gameObject) && transform.parent.gameObject != null)
             {
                 GetComponent<CircleCollider2D>().enabled = false;
                 damagedEnemy.Add(transform.parent.gameObject);
             }
        }

        yield return new  WaitForSeconds(2f);


        if (transform.parent != null)
        {
            damagedEnemy.Remove(transform.parent.gameObject);
            transform.parent.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }

       
        Destroy(gameObject);
    }
    
    IEnumerator damaged(GameObject obj)
    {
        damagedEnemy.Add(obj);
        instanceDamagedEnemies.Add(obj);
        yield return new WaitForSeconds(0.3f);
    }
}
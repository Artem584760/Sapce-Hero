using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirectionRegulatorZone : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float enemySpeed;

    private void Start()
    {
        _rb = transform.parent.GetComponent<Rigidbody2D>();
        enemySpeed = transform.parent.GetComponent<Enemy>().enemySpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
           Force(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
           //Force(other.gameObject);
        }
    }


    void Force(GameObject other)
    {
        Vector2 directionAwayFromEnemy = (transform.parent.position - other.transform.position).normalized;
        
        Vector2 force = directionAwayFromEnemy * (enemySpeed / 1.5f); 

        _rb.AddForce(force, ForceMode2D.Impulse); 
    }
}
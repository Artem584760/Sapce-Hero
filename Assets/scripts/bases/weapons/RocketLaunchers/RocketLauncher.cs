using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : DirectionBase
{
    [SerializeField] private float attackDelay;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject RocketPrefab;

    [Header("RocketSettings")] 
    public float speed;
    public float damage, lifeTime,explosionRadius,explosionForce,explosionDamage;
    
    
    private bool isAttack;

    protected override void Update()
    {
        base.Update();     
        if (direction.magnitude > 0.1f)
        {
            SetAttackDirection(direction.normalized);
            if (!isAttack)
            {
                StartCoroutine(Attack(direction.normalized, attackDelay));
            }
        }
    }


   
    IEnumerator Attack(Vector3 direction, float wait)
    {
        isAttack = true;
        
        
        GameObject projectile = Instantiate(RocketPrefab, transform.position + direction * OffsetDistance,
            Quaternion.Euler(new Vector3(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)));
        projectile.transform.right = direction;
        Debug.Log(projectile);
        
        yield return new WaitForSeconds(wait); 
        isAttack = false;
    }

}




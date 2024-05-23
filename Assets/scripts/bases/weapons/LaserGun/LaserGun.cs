using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : DirectionBase
{
    [Header("GunSettings")]
    [SerializeField] private float attackDelay;
    [SerializeField] private int burstCount;
    [SerializeField] private float burstSpeed;
    [SerializeField] private GameObject laserRayPrefab;
    
    [Header("LaserRaySettings")]
    public float speed;
    public float damage;
    public float lifeTime;
    public float growSpeed;

    [Header("FireEffect")] 
    public float fireDamageInterval;
    public float fireDamage, burnTime;
    public short fireEffectLvl;
   

    private bool isAttack;
    protected void Update()
    {
        base.Update();
        SetAttackDirection(direction.normalized);
        if (!isAttack)
        {
            StartCoroutine(Attack(burstSpeed));
        }
    }
    
    
    IEnumerator Attack(float wait)
    {
        isAttack = true;
        int i = 0;

        while (i < burstCount)
        {
            GameObject projectile = Instantiate(laserRayPrefab, transform.position + direction * OffsetDistance,
                        Quaternion.Euler(new Vector3(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)));
            projectile.transform.right = direction;
            i++;
            yield return new WaitForSeconds(wait); 
        }
        
        yield return new WaitForSeconds(attackDelay);
        isAttack = false;
    }
}

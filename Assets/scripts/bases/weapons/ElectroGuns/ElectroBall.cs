using System;
using UnityEngine;

public class ElectroBall : ProjectileBase
{
    public GameObject DamageZonePrefab;
  
    private float startZoneDamage, minusZoneDamagevalue,currentZonedamage;
    protected void Start()
    {
        ElectroGun _gun = FindObjectOfType<ElectroGun>();
       
        ElectroZone.isSpawn = true;
        
        speed = _gun.Speed;
        damage = _gun.Damage;
        lifeTime = _gun.LifeTime;

        DamageZonePrefab = _gun.DamageZonePrefab;
        
        _gun.CurrentZonedamage = startZoneDamage = _gun.StartZoneDamage;
        minusZoneDamagevalue = _gun.MinusZoneDamagevalue;
        base.Start();
    }


    protected void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other,"Electric");
        if (other.CompareTag("Enemy"))
        {
            GameObject zone = Instantiate(DamageZonePrefab, other.transform.position, Quaternion.identity,
                other.transform);
            
            other.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            zone.GetComponent<ElectroZone>().maxscale = other.GetComponent<CircleCollider2D>().radius+0.25f ;
            if (other.GetComponent<Enemy>().currentHealthPoints <= 0)
            {
                zone.transform.SetParent(null);
            }

            
            Destroy(gameObject);
            //other.GetComponent<Enemy>().currentHealthPoints += FindObjectOfType<ElectroGun>().GetComponent<ElectroGun>().CurrentZonedamage;
        }
    }
}

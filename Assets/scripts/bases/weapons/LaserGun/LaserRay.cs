
using Unity.VisualScripting;
using UnityEngine;

public class LaserRay : ProjectileBase
{
    private float growSpeed,fireDamage,fireDamageInterval,burnTime;
    private short fireEffectLvl;    
    void Start()
    {
        LaserGun _laserGun = FindObjectOfType<LaserGun>();
        
        speed = _laserGun.speed;
        damage = _laserGun.damage;
        lifeTime = _laserGun.lifeTime;
        growSpeed = _laserGun.growSpeed;

        fireDamage = _laserGun.fireDamage;
        fireDamageInterval = _laserGun.fireDamageInterval;
        burnTime = _laserGun.burnTime;
        fireEffectLvl = _laserGun.fireEffectLvl;


        base.Start();
    }
    void Update()
    {
        base.Update();
        transform.localScale = new Vector3(transform.localScale.x + growSpeed/100,transform.localScale.y,0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other,"Laser");
        if (other.CompareTag("Enemy") && other.GetComponent<Fire>() == null)
        {
            Fire _fire = other.AddComponent<Fire>(); ;
            _fire.StartBurning(fireDamageInterval, fireDamage, burnTime,fireEffectLvl);
        }
    }
}

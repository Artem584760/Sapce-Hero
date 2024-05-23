using UnityEngine;

public class Bullet : ProjectileBase
{
    private void Start()
    {
        Gun _gun = FindObjectOfType<Gun>();
        
        speed = _gun.speed;
        damage = _gun.damage;
        lifeTime = _gun.lifeTime;

        base.Start();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other,"Physic");
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
          
        }
    }
}
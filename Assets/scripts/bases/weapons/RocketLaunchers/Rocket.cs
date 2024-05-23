using UnityEngine;

public class Rocket : ProjectileBase
{
    private float explosionForce, blastRadius, explosionDamage;

    void Start()
    {
        RocketLauncher _rpg = FindObjectOfType<RocketLauncher>();

        speed = _rpg.speed;
        damage = _rpg.damage;
        lifeTime = _rpg.lifeTime;

        blastRadius = _rpg.explosionRadius;
        explosionForce = _rpg.explosionForce;
        explosionDamage = _rpg.explosionDamage;
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other,"Explosion");
        if (other.CompareTag("Enemy"))
        {
            Explode();
            Destroy(gameObject);
        }
    }
    
    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.gameObject.tag == "Enemy")
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                
                Vector2 explosionDirection = (rb.transform.position - transform.position).normalized;
                
                rb.AddForce(explosionDirection * explosionForce * Time.fixedDeltaTime, ForceMode2D.Impulse);

                hit.GetComponent<Enemy>().EnemyGiveDamage(explosionDamage,"Explosion");
            }
        }
    }
}
using UnityEngine;
public class ProjectileBase : MonoBehaviour
{
    [HideInInspector]
    public float speed,damage,lifeTime;

    protected virtual void Start()
    {
       Destroy(gameObject,lifeTime);
    }

    protected virtual void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
    
    protected void OnTriggerEnter2D(Collider2D other, string damageType)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.EnemyGiveDamage(damage,damageType);
        }
    }

}
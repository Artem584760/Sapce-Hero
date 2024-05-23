using System.Collections;
using UnityEngine;

public class WeaponInHand : DirectionBase
{
    [Header("Prefabs")]
    [SerializeField] private GameObject projectilePrefab;
    
    [Header("WeaponSettings")]
    [SerializeField] private float attackDelay;
    
    [Header("SlashSettings")]
    public float speed;
    public float damage;
    public float lifeTime;
    public float increaseSpeed;
    
    private bool isAttack;

    protected void Start()
    {
        base.Start();
    }
    
    protected void Update()
    {
        base.Update();
        
        SetAttackDirection(direction.normalized);
        if (!isAttack)
        {
            StartCoroutine(Attack(direction.normalized, attackDelay));
        }
    }

    protected IEnumerator Attack(Vector3 direction, float wait)
    {
        isAttack = true;
        
        GameObject projectile = Instantiate(projectilePrefab, transform.position + direction * OffsetDistance,
            Quaternion.Euler(new Vector3(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)), transform);
        projectile.transform.right = direction; // Направление движения снаряда
        
        yield return new WaitForSeconds(wait); 
        isAttack = false;
}

}


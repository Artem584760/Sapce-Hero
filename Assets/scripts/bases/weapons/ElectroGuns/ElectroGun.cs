using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ElectroGun : DirectionBase
{
    [SerializeField] private float attackDelay;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject ballPrefab;

    [Header("BallSettings")] 
    public float Speed;

    public static float s_CurrentZonedamage;
    
    public GameObject DamageZonePrefab;
    
    private bool isAttack;
    
    public float Damage, LifeTime,StartZoneDamage, MinusZoneDamagevalue,CurrentZonedamage;


    void Start()
    {
        base.Start();
        s_CurrentZonedamage = CurrentZonedamage;
    }
    
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

        GameObject projectile = Instantiate(ballPrefab, transform.position + direction * OffsetDistance,
            Quaternion.Euler(new Vector3(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)));
        projectile.transform.right = direction;

        yield return new WaitForSeconds(wait); 
        isAttack = false;
    }
}

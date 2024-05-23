using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedWolf : Enemy
{
    [SerializeField] private float angryRadius, dashSpeed, dashDamage;
    [SerializeField] private float pauseBeforeDash, pauseAfterDash,dashesPause;
    
    private CircleCollider2D angryZoneCollider;
    private bool isAttack, isCanAttack = true;
    
    private static WaitForSeconds s_pauseBeforeDashWait;
    private static  WaitForSeconds s_pauseAfterDashWait;
    private static  WaitForSeconds s_dashesPauseWait;
    

    void Awake()
    {
        s_pauseBeforeDashWait = new WaitForSeconds(pauseBeforeDash);
        s_pauseAfterDashWait = new WaitForSeconds(pauseAfterDash);
        s_dashesPauseWait = new WaitForSeconds(dashesPause);
    }

    void Start()
    {
        base.Start();
        
       
        angryZoneCollider = GetComponentInChildren<CircleCollider2D>();
        angryZoneCollider.radius = angryRadius;
        
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isCanAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && isAttack)
        {
            other.gameObject.GetComponent<Player>().PlayerGiveDamage(dashDamage, "Physic");
            Poison _poison = other.gameObject.AddComponent<Poison>();
            _poison.StartPoisoning(1, 3,6,1.5f,1);
        }
    }
    
    IEnumerator Attack()
    {
        
        
        
        gameObject.layer = LayerMask.NameToLayer("Space");;
        isAttack = true;
        float originalSpeed = enemySpeed;
        enemySpeed = 0;

        Vector2 direction = (playerTransform.position - transform.position).normalized;

        yield return s_pauseBeforeDashWait;
        isCanAttack = false;

        _rb.velocity = direction * dashSpeed;

        yield return s_pauseAfterDashWait;

        _rb.velocity = Vector2.zero;
        enemySpeed = 0;
        
        yield return s_dashesPauseWait;
        isAttack = false;
        isCanAttack = true;
        enemySpeed = originalSpeed;



    }
}



using System.Collections;
using UnityEngine;

public class Moskito : Enemy
{
    [Header("MoskitoSettings")]
    [SerializeField] private GameObject stripPrefab;
    
    [SerializeField] private float angryRadius, dashSpeed, dashDamage;
    [SerializeField] private float pauseBeforeDash, pauseAfterDash, dashesPause, retreatSpeed;
    
    [SerializeField] private Vector3 stripScale;

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
        angryZoneCollider = GetComponentsInChildren<CircleCollider2D>()[1];
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
        }
    }
    
    IEnumerator Attack()
    {
        angryZoneCollider.enabled = false;

        LayerMask startLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Space");;
        isAttack = true;
        float originalSpeed = enemySpeed;
        enemySpeed = 0;

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        GameObject strip = Instantiate(stripPrefab, transform.position, Quaternion.identity,transform);
        strip.transform.localScale = stripScale;
        strip.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        yield return s_pauseBeforeDashWait;
        isCanAttack = false;

        _rb.velocity = direction * dashSpeed;
        Destroy(strip);

        yield return s_pauseAfterDashWait;

        _rb.velocity = Vector2.zero;
        isAttack = false;
        enemySpeed = -retreatSpeed;

        yield return s_dashesPauseWait;

        isCanAttack = true;
        angryZoneCollider.enabled = true;
        gameObject.layer = startLayer;
        enemySpeed = originalSpeed;
    }
}

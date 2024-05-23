using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Healths")]
    private Slider healthProgressBar;
    public float currentHealthPoints, startHealthPoints;

    [Header("BaseValues")]
    public float enemySpeed;
    public float damageInSecond;
    [SerializeField] private float expReward;

    [HideInInspector] public Transform playerTransform;
    private KillsCount killsCount;
    [HideInInspector] public Rigidbody2D _rb;

    private SpriteRenderer _spriteRenderer;
    
    [Serializable] public struct Resists
    {   
        public string name;
        public float resist;
    }
    
    public Resists[] resists;
    private string[] damageTypes;

    protected void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        
        killsCount = FindObjectOfType<KillsCount>();

        currentHealthPoints = startHealthPoints;
        
        healthProgressBar = GetComponentInChildren<Slider>();
   
        UpdateHealthBar();

        damageTypes = new string[resists.Length];
        for (int i = 0; i < resists.Length; i++)
        {
            damageTypes[i] = resists[i].name;
        }
    }

    protected void FixedUpdate()
    {
        MoveTowardsPlayer();
    }
    
    void MoveTowardsPlayer()
    {
        Vector2 newPosition = Vector2.zero;
        if (Vector2.Distance(_rb.position, playerTransform.position) > 0.1f && _rb.velocity.magnitude < 0.9f)
        {
            newPosition = Vector2.MoveTowards(_rb.position, playerTransform.position, enemySpeed * Time.fixedDeltaTime);

            
            if (newPosition.x < transform.position.x && !_spriteRenderer.flipX)
            {
                _spriteRenderer.flipX = true;
            }
            else if(newPosition.x > transform.position.x)
            {
                _spriteRenderer.flipX = false;
            }
            
            _rb.MovePosition(newPosition);
        }
    }


    public void EnemyGiveDamage(float amount,string damageType)
    {
       

        if (!damageTypes.Contains(damageType))
        {
            Debug.Log($"Тип урона '{damageType}' не был найден");
        }

        float resist = resists[Array.IndexOf(damageTypes, damageType)].resist;

        
        if (amount >= 0)
        {
            amount = amount * (10 / (10 + resist));
        }
        else
        {
            amount = amount * (2 - 10 / (10 + resist));
        }

        
        
        
        currentHealthPoints -= amount;
        UpdateHealthBar();
        if (currentHealthPoints <= 0)
        {
            EnemyDeath();
        }
        
    }
    public void UpdateHealthBar()
    {
        healthProgressBar.value = Mathf.Clamp01(currentHealthPoints / startHealthPoints);
    }
    
    public void EnemyDeath()
    {
        killsCount.killsCount++;
        killsCount.UpdateKillsCountText();

        Expreince _exp = playerTransform.gameObject.GetComponent<Expreince>();
        GameObject expCrystal = Instantiate(_exp.ExpCrystalPrefab, transform.position, Quaternion.identity);
        expCrystal.GetComponent<ExpCrystal>().ExpPlusValue = expReward;

        Destroy(gameObject);
    }
}

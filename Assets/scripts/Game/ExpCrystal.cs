using UnityEngine;
public class ExpCrystal : MonoBehaviour
{
    private Expreince _expreince;

    public float ExpPlusValue;

    private float magnetToPlayerSpeed;
    
    void Start()
    {
        _expreince = FindObjectOfType<Expreince>();
        magnetToPlayerSpeed = FindObjectOfType<Player>().expMagnetSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _expreince.UpgradeExpValue(ExpPlusValue);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("ExpMagnetZone"))
        {
            transform.position =
               Vector2.MoveTowards(transform.position, other.transform.position, magnetToPlayerSpeed * Time.fixedDeltaTime);
        }
    }
}

  

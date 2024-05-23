using UnityEngine;

public class DirectionBase : MonoBehaviour
{
    protected static Vector3 direction;
    protected static FloatingJoystick _joystick;
    protected static GameObject attackDirectionPrefab;
    protected static float OffsetDistance;
    
   
    
    [Header("Values")]
    [SerializeField] private float offsetDistance; 

    protected virtual void Start()
    {
        OffsetDistance = offsetDistance;
        _joystick = GameObject.FindWithTag("JoyStick").GetComponent<FloatingJoystick>();
        attackDirectionPrefab = GameObject.FindWithTag("AttackDirection");
    }

    protected virtual void Update()
    {
        if (_joystick.Horizontal != 0 && _joystick.Vertical != 0)
        {
            direction = new Vector3(_joystick.Horizontal, _joystick.Vertical);
        }
        
        
    }
    
    public virtual void SetAttackDirection(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        attackDirectionPrefab.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        attackDirectionPrefab.transform.position = transform.position + direction.normalized * OffsetDistance;
    }
}
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D _rb; 
    
    [Header("PlayerSpeed")]
    [SerializeField] private float walkSpeed;
    private Vector2 direction;

    [Header("Joystick")]
    public  FloatingJoystick joystick;
    [SerializeField] private GameObject fingerPosObject;
    
    [Header("CameraControl")]
    [SerializeField] private float smoothSpeed = 5f; 
    

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction.x = joystick.Horizontal;
        direction.y = joystick.Vertical;
        
        if (Input.touchCount > 0 && (direction.x != 0 || direction.y != 0) )
        {
            fingerPosObject.SetActive(true);
            fingerPosObject.transform.position = Input.GetTouch(0).position;

        } 
        else
        {
            fingerPosObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + direction * walkSpeed * Time.fixedDeltaTime);
        
        if (Camera.main != null)
        {
            Vector3 desiredPosition = transform.position;
            Vector3 smoothedPosition = Vector3.Lerp(Camera.main.transform.position, desiredPosition, smoothSpeed);    
            Camera.main.transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, Camera.main.transform.position.z);
        }
    }
}
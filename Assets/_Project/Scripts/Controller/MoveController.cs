using UnityEngine;

public class MoveController : MonoBehaviour
{
    public FixedJoystick fixedJoystick;
    private Vector2 moveInput = Vector2.zero;
    private Rigidbody2D rb;
    private PlayerMove controls;
    private Vector3 currentLocalScale;
    public float speed = 5f;
    private float posX = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += _ => moveInput = Vector2.zero;
    }

    void Start()
    {
        currentLocalScale = transform.localScale;
    }

    void Update()
    {
        Vector2 joyStick = new(fixedJoystick.Horizontal, fixedJoystick.Vertical);

        moveInput = joyStick.magnitude > 0.2f ? joyStick : controls.Player.Move.ReadValue<Vector2>();

        posX = moveInput.x > 0 ? 1f : -1f;

        if (moveInput.x == 0) return;

        transform.localScale = new Vector3(currentLocalScale.x * posX, currentLocalScale.y, currentLocalScale.z);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput.normalized * speed;
    }
    
    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}
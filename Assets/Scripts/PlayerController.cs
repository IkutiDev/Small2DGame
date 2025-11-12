using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody2D _rigidbody2D;

    private Vector2 _moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _input.actions["Move"].performed += Onperformed;
        _input.actions["Move"].canceled += Onperformed;
        _input.actions["Move"].started += Onperformed;
    }

    private void Onperformed(InputAction.CallbackContext obj)
    {
        _moveInput = obj.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.linearVelocity = _moveInput * _moveSpeed;
        ClampPositionWithinScreenBounds();
    }
    
    private void ClampPositionWithinScreenBounds()
    {
        Vector2 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}

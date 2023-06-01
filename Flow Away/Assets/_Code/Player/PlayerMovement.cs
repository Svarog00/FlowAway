using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string DashAbilityName = "Dash";

    public Rigidbody2D rb2;
    public Animator animator;

    public LayerMask ObstacleLayer;

    public Vector2 Direction => _direction;

    [SerializeField] private int _maxDashCount;

    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashTimer;
    [SerializeField] private float _dashDistance;

    [SerializeField] private float _movementSpeed = 5f;

    private GadgetManager _gadgetManager;

    private Vector2 _direction;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponentInParent<Rigidbody2D>();
        _gadgetManager = GetComponentInParent<GadgetManager>();
        _gadgetManager.ActivateGadget(DashAbilityName);
    }

    private void FixedUpdate()
    {
        rb2.MovePosition(rb2.position + _movementSpeed * Time.deltaTime * _direction);
    }

    public void StopMove()
    {
        _direction = Vector2.zero;
    }

    public void HandleMove(Vector2 vector)
    {
        HandleMove(vector.x, vector.y);
    }

    public void HandleMove(float x, float y)
    {
        _direction.x = x;
        _direction.y = y;

        AnimateMove();

        if (_direction != new Vector2(0, 0))
        {
            ChangeAnimationDir();
        }
    }

    private void AnimateMove()
    {
        animator.SetFloat("Horizontal", _direction.x);
        animator.SetFloat("Vertical", _direction.y);
        animator.SetFloat("Speed", _direction.sqrMagnitude);
    }

    private void ChangeAnimationDir()
    {
        animator.SetFloat("Dir_Horizontal", _direction.x);
        animator.SetFloat("Dir_Vertical", _direction.y);
    }
}

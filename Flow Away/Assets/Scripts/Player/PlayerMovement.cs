using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string ObstaclesLayerName = "Obstacles";
    private const string DashAbilityName = "Dash";
    private const string DashSoundEffect = "PlayerDashSound";

    public Rigidbody2D rb2;
    public Animator animator;

    public LayerMask ObstacleLayer;

    [SerializeField] private int _maxDashCount;

    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashTimer;
    [SerializeField] private float _dashDistance;

    [SerializeField] private float _movementSpeed = 5f;

    private GadgetManager _gadgetManager;

    private int _curDashCounter;
    private float _curDashTimer = 0f;

    //private Vector2 _movement;
    private Vector2 _direction;
    private Vector3 _dashTarget;

    private bool _isDashing;
    private bool _isPressedDash;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponentInParent<Rigidbody2D>();
        _gadgetManager = FindObjectOfType<GadgetManager>();
        _gadgetManager.ActivateGadget(DashAbilityName);
        ObstacleLayer = LayerMask.GetMask(ObstaclesLayerName);
        _curDashCounter = _maxDashCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (_curDashTimer > 0f)
        {
            CooldownDash();
        }
    }

    private void FixedUpdate()
    {
        rb2.MovePosition(rb2.position + _direction * _movementSpeed * Time.deltaTime);
        Dash();
    }

    public void StopMove()
    {
        _direction = Vector2.zero;
    }

    public void HandleMove(Vector2 vector, bool dash)
    {
        HandleMove(vector.x, vector.y, dash);
    }

    public void HandleMove(float x, float y, bool dash)
    {
        _direction.x = x;
        _direction.y = y;

        AnimateMove();

        if (_direction != new Vector2(0, 0))
        {
            ChangeAnimationDir();
        }

        if (dash && _curDashCounter > 0)
        {
            _curDashTimer = _dashTimer;
            _curDashCounter--;
            _isPressedDash = dash;
            AudioManager.Instance.Play(DashSoundEffect);
        }
    }

    #region dash
    private void Dash()
    {
        if (_isPressedDash)
        {
            CheckDash(_dashDistance, _direction);

            if (_isDashing)
            {
                rb2.velocity = Vector2.zero;
                float distSqr = (_dashTarget - transform.position).sqrMagnitude; //Дистанция до точки перемещения

                if (distSqr < 0.1) //если дистанция слишком мала, то не перемещать
                {
                    _isDashing = false;
                    _dashTarget = Vector3.zero;
                }
                else
                {
                    rb2.MovePosition(Vector3.Lerp(transform.position, _dashTarget, _dashForce * Time.deltaTime)); //дэш к точке
                }
            }
            _isPressedDash = false;
        }
    }

    private void CheckDash(float dashDistance, Vector2 direction)
    {
        _isDashing = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, dashDistance, ObstacleLayer); //Выстрел лучом на дистанцию дэша по слою препятствий
        if(hit)
        {
            _dashTarget = transform.position + (Vector3)(dashDistance * hit.fraction * direction); //Если попал луч, то точка назначения - место попадания
        }
        else
        {
            _dashTarget = transform.position + (Vector3)(direction * dashDistance); //если луч ничего не достал, то перемещение на полную дистанцию
        }

        rb2.velocity = Vector2.zero;
    }
    #endregion

    private void CooldownDash()
    {
        _curDashTimer -= Time.deltaTime;
        _gadgetManager.Timer(_curDashTimer, _dashTimer, DashAbilityName);

        if (_curDashTimer <= 0f)
        {
            _curDashCounter = _maxDashCount;
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
        //_direction = _movement;
        animator.SetFloat("Dir_Horizontal", _direction.x);
        animator.SetFloat("Dir_Vertical", _direction.y);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
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

    private Vector2 _movement;
    private Vector2 _direction;
    private Vector3 _dashTarget;

    private bool _isDashing;
    private bool _isPressedDash;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponentInParent<Rigidbody2D>();
        _gadgetManager = FindObjectOfType<GadgetManager>();
        _gadgetManager.ActivateGadget("Dash");
        ObstacleLayer = LayerMask.GetMask("Obstacles");
        _curDashCounter = _maxDashCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (_curDashTimer > 0f)
        {
            _curDashTimer -= Time.deltaTime;
            _gadgetManager.Timer(_curDashTimer, _dashTimer, "Dash");
            if (_curDashTimer <= 0f)
                _curDashCounter = _maxDashCount;
        }
        transform.position += transform.right * _movement.x * Time.deltaTime * _movementSpeed;
        transform.position += transform.up * _movement.y * Time.deltaTime * _movementSpeed;
    }

    private void FixedUpdate()
    {
        //rb2.MovePosition(rb2.position + _movement * _movementSpeed * Time.deltaTime);
        Dash();
    }

    public void HandleMove(float x, float y, bool dash)
    {
        _movement.x = x;
        _movement.y = y;

        AnimateMove();

        if (_movement != new Vector2(0, 0))
        {
            ChangeDir();
        }

        if (dash && _curDashCounter > 0) //if timer is null
        {
            //Update timer
            //minus one dash
            _curDashTimer = _dashTimer;
            _curDashCounter--;
            _isPressedDash = true;
            FindObjectOfType<AudioManager>().Play("PlayerDashSound");
        }
    }

    private void Dash()
    {
        if (_isPressedDash)
        {
            Dash(_dashDistance, _direction);

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
                    transform.position = Vector3.Lerp(transform.position, _dashTarget, _dashForce * Time.deltaTime);
                    //rb2.MovePosition(Vector3.Lerp(transform.position, _dashTarget, _dashForce * Time.deltaTime)); //дэш к точке
                }
            }
            //CameraShake.Instance.ShakeCamera(1f, .1f);
            _isPressedDash = false;
        }
    }

    private void Dash(float dashDistance, Vector2 direction)
    {
        _isDashing = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, dashDistance, ObstacleLayer); //Выстрел лучом на дистанцию дэша по слою препятствий
        if(hit)
        {
            _dashTarget = transform.position + (Vector3)(direction * dashDistance * hit.fraction); //Если попал луч, то точка назначения - место попадания
        }
        else
        {
            _dashTarget = transform.position + (Vector3)(direction * dashDistance); //если луч ничего не достал, то перемещение на полную дистанцию
        }

        rb2.velocity = Vector2.zero;
    }

    private void AnimateMove()
    {
        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Vertical", _movement.y);
        animator.SetFloat("Speed", _movement.sqrMagnitude);
    }

    private void ChangeDir()
    {
        _direction = _movement;
        animator.SetFloat("Dir_Horizontal", _direction.x);
        animator.SetFloat("Dir_Vertical", _direction.y);
    }


}

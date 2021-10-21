using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _maxSpeed;

    private Rigidbody2D _rb2;
    private Vector2 _direction;
    private bool _canMove;
    private bool _faceRight;

    public bool CanMove
    {
        get => _canMove;
        set 
        { 
            _canMove = value;
            if (_canMove)
            {
                _currentSpeed = _maxSpeed;
            }
            else
            {
                _currentSpeed = 0;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _currentSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_canMove)
        {
            _rb2.MovePosition(_rb2.position - _direction * _maxSpeed * Time.deltaTime); //movement
        }

    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
        if (_direction.x > 0 && _faceRight == true)
        {
            Flip();
        }
        if (_direction.x < 0 && _faceRight == false)
        {
            Flip();
        }
    }

    public void SetPoint(Vector3 point)
    {
        _direction = (transform.position - point) / Vector2.Distance(transform.position, point);
        SetDirection(_direction);
    }

    private void Flip() //turn left or right depends on player position
    {
        _faceRight = !_faceRight;
        Vector3 Scaler = gameObject.transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}

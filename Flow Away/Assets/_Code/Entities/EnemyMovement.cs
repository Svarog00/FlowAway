using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private const string SpeedAnimatorTag = "Speed";
    private const float Accuracy = 0.6f;

    [SerializeField] private Animator _animator;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _maxSpeed;

    private Rigidbody2D _rb2;

    private Vector3 _targetPosition;
    private Vector2 _direction;
    private bool _canMove;
    private bool _faceRight;

    private Pathfinding _pathfinding;
    private int _currentPathIndex = 0;

    [SerializeField] private List<Vector3> _pathVectorList = new List<Vector3>();

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
        _pathfinding = Pathfinding.Instance;
    }

    private void Update()
    {
        _animator.SetFloat(SpeedAnimatorTag, _currentSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!_canMove)
        {
            return;
        }

        if (_pathVectorList == null)
        {
            return;
        }

        _rb2.MovePosition(_rb2.position - _direction * _maxSpeed * Time.deltaTime); //movement

        if (Vector3.Distance(transform.position, _targetPosition) <= Accuracy)
        {
            _currentPathIndex++;
            if (_currentPathIndex >= _pathVectorList.Count)
            {
                _canMove = false;
                _currentSpeed = 0;
                return;
            }

            _targetPosition = _pathVectorList[_currentPathIndex];
            _direction = (_targetPosition - transform.position).normalized;
            SetSpriteDirection(-_direction);
        }
    }

    public void SetTargetPosition(Vector3 targetPostion)
    {
        _currentPathIndex = 0;
        _pathVectorList = _pathfinding.FindPath(transform.position, targetPostion);
        _targetPosition = _pathVectorList[_currentPathIndex];
    }

    public void SetSpriteDirection(Vector2 direction)
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

    private void Flip() //turn left or right depends on player position
    {
        _faceRight = !_faceRight;
        Vector3 Scaler = gameObject.transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}

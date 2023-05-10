using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.Gadgets.Hook
{
    public class HookInstance : MonoBehaviour
    {
        [Header("Hook Specifics")]
        public LayerMask ObstacleLayer;

        private const string HookableObjTag = "Enemy";
        private const string ObstacleTag = "Obstacle";

        [SerializeField] private float _speed;
        [SerializeField] private float _returnSpeed;
        [SerializeField] private float _range;
        [SerializeField] private float _stopRange;

        [SerializeField] private LineRenderer _line;

        [SerializeField] private Transform _caster;

        private bool _isActive;

        private Action _onCollideAction;

        private Transform _collidedWith;
        private bool _hasCollided;

        private Vector3 _collisionPoint;
        private Vector3 _direction;
        private float _distanceToCaster;

        private PlayerControl _playerControl;

        private void Start()
        {
            _playerControl = _caster.parent.gameObject.GetComponent<PlayerControl>();
            _onCollideAction = Pull;
        }

        private void OnEnable()
        {
            _direction = GetDirectionToMouse();
            _isActive = true;
        }

        private void Update()
        {
            if (_isActive)
            {
                Cast();
            }
        }

        private void OnDisable()
        {
            _hasCollided = false;
            _isActive = false;
        }

        private void Cast()
        {
            _line.SetPosition(0, _caster.position);
            _line.SetPosition(1, transform.position);

            _distanceToCaster = Vector2.Distance(transform.position, _caster.position);

            if (_hasCollided)
            {
                _onCollideAction();
            }
            else
            {
                if (_distanceToCaster > _range)
                {
                    Collision(null);
                }
            }

            transform.Translate(_speed * Time.deltaTime * _direction);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_hasCollided &&
                (collision.CompareTag(HookableObjTag) || collision.CompareTag(ObstacleTag)))
            {
                HandleHookHit(collision);
            }
        }

        private void HandleHookHit(Collider2D collision)
        {
            if (collision.CompareTag(HookableObjTag))
            {
                _onCollideAction = Pull;
            }
            else if (collision.CompareTag(ObstacleTag))
            {
                //Check hook hit
                RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction, _range, ObstacleLayer); //Выстрел лучом на дистанцию дэша по слою препятствий
                if (hit) //Get hit point to pull player to
                {
                    _collisionPoint = transform.position + _range * hit.fraction * _direction; //Если попал луч, то точка назначения - место попадания
                }

                _playerControl.CanMove = false;
                _onCollideAction = Reach;

            }

            Collision(collision.transform);
        }

        private void Collision(Transform collision)
        {
            _speed = _returnSpeed;

            _hasCollided = true;
            if (collision)
            {
                transform.position = collision.position;
                _collidedWith = collision;
            }
            else
            {
                _onCollideAction = Pull;
                _collidedWith = null;
            }
        }

        private void Reach()
        {
            _direction = GetDirectionToHitPoint(_collisionPoint);
            _caster.parent.Translate(_speed * Time.deltaTime * _direction);
            transform.position = _collisionPoint;
            StopCast();
        }


        private void Pull()
        {
            _direction = -GetDirectionToHitPoint(transform.position);

            if (_collidedWith)
            {
                _collidedWith.position = transform.position;
            }

            StopCast();
        }

        private void StopCast()
        {
            if (_distanceToCaster <= _stopRange)
            {
                _playerControl.CanMove = true;
                gameObject.SetActive(false);
                _collisionPoint = _caster.position;
            }
        }


        private Vector2 GetDirectionToMouse()
        {
            Vector3 mousePosition = UtilitiesClass.GetWorldMousePosition();
            Vector3 aimDir = (mousePosition - transform.position).normalized;
            return aimDir;
        }

        private Vector3 GetDirectionToHitPoint(Vector3 point)
        {
            Vector3 aimDir = (point - _caster.position).normalized;
            return aimDir;
        }
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.Gadgets
{
    public class HookInstance : MonoBehaviour
    {
        private const string HookableObjTag = "Enemy";
        private const string ObstacleTag = "Obstacle";
        
        private float _speed;
        private float _returnSpeed;
        private float _range;
        private float _stopRange;

        private LineRenderer _line;

        private Transform _collidedWith;
        private Transform _caster;
        private bool _hasCollided;

        private bool _isActive;

        public void Initialize(Transform caster)
        {
            _caster = caster;
        }

        void Start()
        {
            _line.GetComponentInChildren<LineRenderer>();
        }

        private void OnEnable()
        {
            _isActive = true;
        }

        private void Update()
        {
            if(_isActive)
            {
                Cast();
            }
        }

        private void Cast()
        {
            _line.SetPosition(0, _caster.position);
            _line.SetPosition(1, transform.position);

            if(true)
            {

            }

            transform.Translate(GetDirection() * _speed * Time.deltaTime);

        }

        private Vector2 GetDirection()
        {
            Vector3 mousePosition = UtilitiesClass.GetWorldMousePosition();
            Vector2 aimDir = (mousePosition - transform.position).normalized;
            return aimDir;
        }

        private void OnDisable()
        {
            _isActive = false;
        }
    }
}
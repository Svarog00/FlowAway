using System.Collections;
using UnityEngine;

public class DashAbility : Gadget
{
    private const string ObstaclesLayerName = "Obstacles";
    private const string DashSoundEffect = "PlayerDashSound";
    private const string DashAbilityName = "Dash";

    public LayerMask ObstacleLayer;

    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private int _maxDashCount;

    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashTimer;
    [SerializeField] private float _dashDistance;

    private Rigidbody2D _rb2;

    private int _curDashCounter;
    private float _curDashTimer = 0f;

    private Vector3 _dashTarget;

    private bool _isDashing;
    private bool _isPressedDash;

    private void Start()
    {
        _rb2 = GetComponentInParent<Rigidbody2D>();
        ObstacleLayer = LayerMask.GetMask(ObstaclesLayerName);
        _curDashCounter = _maxDashCount;
    }

    private void FixedUpdate()
    {
        Dash();
    }

    public override void HandleActivate()
    {
        if (_curDashCounter <= 0)
        {
            return;
        }

        StopCoroutine(CooldownDash());
        StartCoroutine(CooldownDash());

        _curDashCounter--;
        _isPressedDash = true;
        AudioManager.Instance.Play(DashSoundEffect);
    }

    private void Dash()
    {
        if (!_isPressedDash)
        {
            return;
        }

        CheckDash(_dashDistance, _playerMovement.Direction);

        if (_isDashing)
        {
            _rb2.velocity = Vector2.zero;
            float distSqr = (_dashTarget - transform.position).sqrMagnitude; //Дистанция до точки перемещения

            if (distSqr < 0.1) //если дистанция слишком мала, то не перемещать
            {
                _isDashing = false;
                _dashTarget = Vector3.zero;
            }
            else
            {
                _rb2.MovePosition(Vector3.Lerp(transform.position, _dashTarget, _dashForce)); //дэш к точке
            }
        }
        _isPressedDash = false;
    }

    private void CheckDash(float dashDistance, Vector2 direction)
    {
        _isDashing = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, dashDistance, ObstacleLayer); //Выстрел лучом на дистанцию дэша по слою препятствий
        if (hit)
        {
            _dashTarget = transform.position + (Vector3)(dashDistance * hit.fraction * direction); //Если попал луч, то точка назначения - место попадания
        }
        else
        {
            _dashTarget = transform.position + (Vector3)(direction * dashDistance); //если луч ничего не достал, то перемещение на полную дистанцию
        }

        _rb2.velocity = Vector2.zero;
    }

    private IEnumerator CooldownDash()
    {
        _curDashTimer = _dashTimer;
        while (true)
        {
            _curDashTimer -= Time.deltaTime;
            GadgetManager.CooldownTimer(_curDashTimer, _dashTimer, DashAbilityName);

            if (_curDashTimer <= 0f)
            {
                _curDashCounter = _maxDashCount;
                yield break;
            }

            yield return null;
        }
    }
}

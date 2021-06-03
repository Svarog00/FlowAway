using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : SurveillanceScript, IDamagable
{
    [Header("Children script")]
    public GameObject shotPrefab;
    public Transform firePoint;

    [SerializeField] private float _shotCooldown = 0;
    [SerializeField] private float _curCooldown;
    [SerializeField] private float _hp;
    private float _distanceToPlayer;
    private Vector2 _headingToPlayer;

    private void OnEnable()
    {
        _curCooldown = 0f;
    }

    protected override void Reaction()
    {
        if (_curCooldown <= 0f)
        {
            GameObject shot = Instantiate(shotPrefab, firePoint.position, firePoint.rotation.normalized);
            shot.GetComponent<ShotScript>().speed = new Vector2(5, 5) * -GetDirection();
            _curCooldown = _shotCooldown;
        }
        StartCoroutine(Cooldown());
    }

    Vector2 GetDirection()
    {
        _headingToPlayer = transform.position - playerPosition; //направленный вектор к игроку / a vector to the player
        _distanceToPlayer = _headingToPlayer.magnitude; //длина вектора / lenght of the vector
        Vector2 direction = _headingToPlayer / _distanceToPlayer; //direction to player
        return direction;
    }

    protected IEnumerator Cooldown()
    {
        while (true)
        {
            _curCooldown -= Time.deltaTime;
            if (_curCooldown <= 0)
            {
                yield break;
            }
            yield return null;
        }
    }
    public void Hurt(int damage)
    {
        _hp -= damage;
        if(_hp <= 500*0.75)
        {
            //play sound
            QuestValues.Instance.SetStage("Gargoyle", 1);
            //gameObject.SetActive(false);
        }
    }
}

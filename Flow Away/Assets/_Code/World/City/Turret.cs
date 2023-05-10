using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : SurveillanceScript, IDamagable
{
    public event EventHandler OnTurretDestroyedEventHandler;

    private const string GargoyleQuestName = "GargoyleFirstMeet";

    [Header("Children script")]
    public GameObject shotPrefab;
    public Transform firePoint;

    [SerializeField] private float _shotCooldown = 0;
    [SerializeField] private float _curCooldown;
    [SerializeField] private float _hp;

    private void Awake()
    {
        if (QuestValues.Instance.GetStage(GargoyleQuestName) == -1)
        {
            QuestValues.Instance.Add(GargoyleQuestName);
        }
    }

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
        Vector2 direction = (transform.position - _playerPosition).normalized; //direction to player
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
        if(_hp <= 0)
        {
            //play sound
            QuestValues.Instance.SetStage("GargoyleFirstMeet", 1);
            gameObject.SetActive(false);
            OnTurretDestroyedEventHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}

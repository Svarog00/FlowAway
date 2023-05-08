using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleAnimaiton : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _initialPoint;

    private PlayerControl _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        QuestValues.Instance.Add("SpawnedPlayer");
        if (QuestValues.Instance.GetStage("SpawnedPlayer") == 0)
        {
            _player.gameObject.transform.position = _initialPoint.position;
            _player.CanMove = false;
            _player.CanAttack = false;
            _player.gameObject.SetActive(false);
        }
        else _animator.Play("Opened");
    }
    
    public void SpawnPlayer()
    {
        _player.gameObject.SetActive(true);
        QuestValues.Instance.SetStage("SpawnedPlayer", 1);
        _player.CanMove = true;
        _player.CanAttack = true;
        _animator.SetTrigger("Open");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleAnimaiton : MonoBehaviour
{
    public Animator animator;
    public float spawnTime = 1f;
    public PlayerControl player;


    // Start is called before the first frame update
    void Start()
    {
        QuestValues.Instance.Add("SpawnedPlayer");
        if (QuestValues.Instance.GetStage("SpawnedPlayer") == 0)
        {
            player.CanMove = false;
            player.CanAttack = false;
            player.gameObject.SetActive(false);
            StartCoroutine(SpawnTimer());
        }
        else animator.Play("Opened");
    }

    IEnumerator SpawnTimer()
    {
        animator.SetTrigger("Open");
        while (true)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                player.gameObject.SetActive(true);
                QuestValues.Instance.SetStage("SpawnedPlayer", 1);
                player.CanMove = true;
                player.CanAttack = true;
                animator.SetTrigger("Open");
                yield break;
            }
            yield return null;
        }
    }
}

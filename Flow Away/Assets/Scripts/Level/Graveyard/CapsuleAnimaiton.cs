using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleAnimaiton : MonoBehaviour
{
    public Animator animator;
    public float spawnTime = 1f;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    IEnumerator SpawnTimer()
    {
        while (true)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                player.SetActive(true);
                PlayerPrefs.SetInt("Spawned", 1);
                animator.SetTrigger("Open");
                yield break;
            }
            yield return null;
        }
    }
}

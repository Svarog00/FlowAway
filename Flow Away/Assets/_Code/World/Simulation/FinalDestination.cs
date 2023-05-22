using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDestination : MonoBehaviour
{
    [SerializeField] private EncounterManager _eventManager;
    [SerializeField] private Text _text;
    [SerializeField] private SurveillanceScript _surveillanceScript;

    private int _faliuresCount = 0;

    private void Start()
    {
        _surveillanceScript = FindObjectOfType<SurveillanceScript>();
        _surveillanceScript.OnPlayerDetected += SurveillanceScript_OnPlayerDetected;
        _eventManager.OnEventFinished += EventManager_OnEventFinished;

        _text = FindObjectOfType<Text>();

        var player = FindObjectOfType<PlayerHealthController>();
        player.gameObject.transform.position = Vector3.zero;
    }

    private void EventManager_OnEventFinished(object sender, System.EventArgs e)
    {
        _faliuresCount++;
    }

    private void SurveillanceScript_OnPlayerDetected(object sender, System.EventArgs e)
    {
        _faliuresCount++;
        _surveillanceScript.OnPlayerDetected -= SurveillanceScript_OnPlayerDetected;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _text.text = $"You failed {_faliuresCount} times. Press R to try again";
            if(Input.GetKeyDown(KeyCode.R))
            {
                collision.transform.position = Vector2.zero;

                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            }
            collision.GetComponent<Invisibility>().MaxTime -= _faliuresCount;
        }
    }
}

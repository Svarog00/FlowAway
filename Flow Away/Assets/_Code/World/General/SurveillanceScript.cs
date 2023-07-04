using System;
using UnityEngine;

public class SurveillanceScript : MonoBehaviour
{
    public event EventHandler OnPlayerDetected;

    [Header("Base script")]
    [SerializeField] private float _range = 0f;
    protected Vector3 _playerPosition;
    protected bool _playerDetected = false;

    // Update is called once per frame
    protected void Update()
    {
        ScanArea();
    }

    void ScanArea()
    {
        //Get every object in area of sight and find a player among them
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, _range);
        foreach (Collider2D detectedObject in detectedObjects)
        {
            if (detectedObject.GetComponent<PlayerHealthController>())
            {
                _playerPosition = detectedObject.transform.position;
                _playerDetected = true;
            }
        }

        if (!_playerDetected)
        {
            return;
        }

        //Check if player in line of sight
        RaycastHit2D[] objects = Physics2D.LinecastAll(transform.position, _playerPosition);
        foreach (RaycastHit2D hit in objects)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Reaction();
                _playerDetected = false;
                break;
            }
            else if (hit.collider.tag == "Border" || hit.collider.tag == tag)
            {
                continue;
            }
            else
            {
                break;
            }
        }
    }

    //Start spawn of enemies
    protected virtual void Reaction()
    {
        OnPlayerDetected?.Invoke(this, EventArgs.Empty);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}

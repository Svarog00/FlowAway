using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private SurveillanceScript _spawnTrigger;

	[SerializeField] private int _entitiesToSpawnCount;
	[SerializeField] private float _delay = 0.5f;

	private ObjectPool _objectPool;
	private float _curDelay;

	private void Start()
	{
		_objectPool = GetComponent<ObjectPool>();
		_spawnTrigger.OnPlayerDetected += Surveillance_OnPlayerDetected;
	}

	//If player detected - call spawn of enemies
    private void Surveillance_OnPlayerDetected(object sender, System.EventArgs e)
	{
		if(_entitiesToSpawnCount <= 0)
        {
			_spawnTrigger.OnPlayerDetected -= Surveillance_OnPlayerDetected;
			return;
        }

        if (_curDelay <= 0f)
        {
            _entitiesToSpawnCount--;
            GameObject gameObject = _objectPool.GetFromPool();
            gameObject.transform.position = transform.position;
            _curDelay = _delay;
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        while (true)
        {
            _curDelay -= Time.deltaTime;
            if(_curDelay <= 0f)
            {
                yield break;
            }

            yield return null;
        }
    }
}

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

    private void Update()
    {
		if(_curDelay > 0f)
        {
			_curDelay -= Time.deltaTime;
        }
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
            GameObject gameObject;
            gameObject = _objectPool.GetFromPool();
            gameObject.transform.position = transform.position;
            _curDelay = _delay;
        }
    }
}

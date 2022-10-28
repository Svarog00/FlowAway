using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class SpawnerScript : MonoBehaviour
{
	public SurveillanceScript Surveillance;

	[SerializeField] private int _count;
	[SerializeField] private float _delay = 0.5f;

	private ObjectPool _objectPool;
	private float _curDelay;

	private void Start()
	{
		_objectPool = GetComponent<ObjectPool>();
		Surveillance.OnPlayerDetected += Surveillance_OnPlayerDetected;
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
		if(_count > 0)
        {
			if(_curDelay <= 0f)
            {
				_count--;
				GameObject gameObject;
				gameObject = _objectPool.GetFromPool();
				gameObject.transform.position = transform.position;
            }
        }
		else 
			Surveillance.OnPlayerDetected -= Surveillance_OnPlayerDetected;
	}
}

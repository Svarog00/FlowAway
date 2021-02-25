using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
	public SurveillanceScript surveillance;
	public int count;
	public float delay = 0.5f;
	private ObjectPool objectPool;
	private float curDelay;

	private void Start()
	{
		objectPool = GetComponent<ObjectPool>();
		surveillance.OnPlayerDetected += Surveillance_OnPlayerDetected;
	}

    private void Update()
    {
		if(curDelay > 0f)
        {
			curDelay -= Time.deltaTime;
        }
    }

    private void Surveillance_OnPlayerDetected(object sender, System.EventArgs e)
	{
		if(count > 0)
        {
			if(curDelay <= 0f)
            {
				GameObject gameObject;
				gameObject = objectPool.GetFromPool();
				gameObject.transform.position = transform.position;
				count--;
            }
        }
		else 
			surveillance.OnPlayerDetected -= Surveillance_OnPlayerDetected;
	}
}

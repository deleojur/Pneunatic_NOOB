using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	public float shakeIntensity = 0.05f;
	public bool ShakeOnStart = false;

	void Start()
	{
		if(ShakeOnStart)
		{
			Shake();
		}
	}

	public void Shake ()
	{
		StartCoroutine( ShakeIt( ) );
	}

	private IEnumerator ShakeIt ()
	{
		for (int i = 0; i < 10; i++) 
		{
			shakeIntensity = (i % 2 == 0) ? shakeIntensity * 1f : shakeIntensity * -1f;
			Vector3 scaleVector = new Vector3 (shakeIntensity, shakeIntensity, 0);
			transform.position = transform.position + scaleVector;
			yield return new WaitForSeconds (0.04f);
		}
	}
}

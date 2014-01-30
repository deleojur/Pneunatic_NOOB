using UnityEngine;
using System.Collections;

public class EnemieMove : MonoBehaviour
{

	public 		string 		nameTargetObject;
	public 		float 		speed = 0.2f;
	public 		float 		checkTargetDelay = 3f;
	public 		float 		extraGravity = 0.5f;
	
	private CharacterController	controller;
	private 	bool 		facingLeft = false;
	private 	float 		newSpeedX = 0f;
	private 	float 		newSpeedY = 0f;
	private 	bool 		startingUp = false;
	private 	bool 		slowingDown = false;
	private 	bool 		objectNotMoving = true;

	void Start ()
	{
		
	}

	public void SwitchDirection ()
	{
			facingLeft = ! facingLeft;
			StartCoroutine("TurnEnemyAround");
	}

	IEnumerator TurnEnemyAround ()
	{
		int t = 0;
		while ( true ) 
		{
			t += 5;
			transform.Rotate ( new Vector3 ( 0, 5, 0 ) );
			if ( t > 175 ) 
			{
				int v = facingLeft ? 0 : 180;
				transform.rotation.eulerAngles.Set (0, v, 0);
				StopCoroutine ("TurnEnemyAround");
			}
			yield return new WaitForSeconds (0.0f);
		}
	}

	IEnumerator SlowDown ()
	{
			slowingDown = true;
			newSpeedY = extraGravity;

			while (slowingDown) {

					newSpeedX *= 0.9f;
					if (newSpeedX < 0.001 && newSpeedX > -0.001) {
							newSpeedX = 0f;
							objectNotMoving = true;
							slowingDown = false;
							StopCoroutine ("SlowDown");
					}

					yield return null;
			}
	}

	IEnumerator StartUp (float currentSpeedX)
	{
			objectNotMoving = false;
			startingUp = true;
			newSpeedX = currentSpeedX;

			if (newSpeedX == 0)
					newSpeedX = facingLeft ? -0.01f : 0.01f;
				
			while (startingUp) {

					newSpeedX *= 1.3f;

					if (speed % newSpeedX == speed) {
							newSpeedX = Mathf.Round (newSpeedX * 10) / 10f;
							startingUp = false;
							StopCoroutine ("StartUp");
					}

					yield return null;
			}
	}

	private bool ObjectOnFloor ()
	{	
			if (transform == null)
					return false;
			var blaat = transform.FindChild ("CheckOnFloor");
			if (blaat == null) {
					Destroy (gameObject);
					return false;
			}
	
//			CheckOnFloor c = blaat.GetComponent<CheckOnFloor> () as CheckOnFloor;// transform.FindChild( "CheckOnFloor" ).GetComponent<CheckOnFloor>( ) as CheckOnFloor;
			/*if (c == null)
					return false;
			bool b = c.GetIsOnFloor ();//transform.FindChild ("CheckOnFloor").GetComponent<CheckOnFloor> ()
			//.GetIsOnFloor();
			return b;*/
		return false;
	}

	public bool getObjectFacingLeft ()
	{
			return facingLeft;
	}

	void Update ()
	{
			
			if (ObjectOnFloor ()) {
					// When on floor speed Y is 0
					newSpeedY = 0f;

					// If speed X is 0 then start up again
					if (objectNotMoving || slowingDown) {
							slowingDown = false;
							StopCoroutine ("SlowDown");
							StartCoroutine (StartUp (newSpeedX));
					}
					
					newSpeedX = facingLeft ? speed * -1 : speed;
					

			} else {
					
			if ( !slowingDown && objectNotMoving == false ) 
			{
						StartCoroutine (SlowDown ());
					}
			}

			// Add new values to X and Y velocity
			transform.position = new Vector3 ((transform.localPosition.x) + newSpeedX, transform.localPosition.y - newSpeedY, transform.localPosition.z);
	}
}







using UnityEngine;
using System.Collections;

public enum Direction 
{
	Left, 
	Right
};

public class TurnScript : MonoBehaviour 
{	
	public Direction			direction	{ get; set; }
	
	public void TurnAround( bool willSwitchDirection )
	{
		if ( willSwitchDirection )
		{
			if ( direction == Direction.Left )
				direction = Direction.Right;
			else direction = Direction.Left;
		}
		StartCoroutine( TurnAround( ) );		
	}
	
	IEnumerator TurnAround( float t = 36 )
	{
		float d	= 0;
		Quaternion endRotation	= transform.rotation * Quaternion.Euler( new Vector3( 0, 180, 0 ) );
		while ( true ) 
		{
			++d;			
			transform.Rotate( 0, 180 / t, 0 );
			//after 15 frames, the gameobject should have turned completely
			if ( d > t ) 
			{
				transform.rotation	= endRotation;
				break;
			}
			yield return null;
		}
	}
	
	public void TurnLeft( float t )
	{
		if ( direction == Direction.Left )
			return;
		
		direction	= Direction.Left;
		transform.rotation = Quaternion.Euler( new Vector3( 0, 0, 0 ) );
		StartCoroutine( TurnAround( t ) );
	}
	
	public void TurnRight( float t )
	{
		if ( direction == Direction.Right )
			return;
		
		direction	= Direction.Right;
		transform.rotation = Quaternion.Euler( new Vector3( 0, 180, 0 ) );
		StartCoroutine( TurnAround( t ) );
	}
}

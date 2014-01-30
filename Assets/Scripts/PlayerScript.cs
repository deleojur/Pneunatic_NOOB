using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
	private TurnScript 	_turnScript;
	
	private Vector3		_previousPosition;
	
	// Use this for initialization
	void Start( )
	{
		_previousPosition	= transform.position;
		_turnScript			= gameObject.GetComponent<TurnScript>( );
	}
	
	void Update( )
	{
		if ( transform.position.x > _previousPosition.x )
		{
			_turnScript.TurnRight( 10 );
		}
		else if ( transform.position.x < _previousPosition.x )
		{
			_turnScript.TurnLeft( 10 );
		}
		_previousPosition	= transform.position;
	}
}

using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
	public ParticleSystem[] moveParticleSystems;
	public ParticleSystem[] stationarySystems;
	public ParticleSystem[] jumpSystems;
	
	private TurnScript 		_turnScript;	
	private Vector3			_previousPosition;
	private bool 			_isMoving = false;
	
	// Use this for initialization
	void Start( )
	{
		_previousPosition	= transform.position;
		_turnScript			= gameObject.GetComponent<TurnScript>( );
	}
	
	void Update( )
	{
		_isMoving			= false;
		if ( transform.position.x > _previousPosition.x )
		{
			_isMoving		= true;
			_turnScript.TurnRight( 10 );
		}
		else if ( transform.position.x < _previousPosition.x )
		{
			_isMoving		= true;
			_turnScript.TurnLeft( 10 );
		}
		_previousPosition	= transform.position;
	}
	
	void LateUpdate( )
	{
		if ( _isMoving )
		{
			DisableParticleSystems( ref stationarySystems );
			EnableParticleSystem( ref moveParticleSystems );
		} else 
		{
			DisableParticleSystems( ref moveParticleSystems );
			EnableParticleSystem( ref stationarySystems );
		}
	}
	
	private void EnableParticleSystem( ref ParticleSystem[] particleSystems )
	{
		foreach ( ParticleSystem ps in particleSystems )
		{
			ps.enableEmission = true;
		}
	}
	
	private void DisableParticleSystems( ref ParticleSystem[] particleSystems )
	{
		foreach ( ParticleSystem ps in particleSystems )
		{
			ps.enableEmission = false;
		}
	}
}

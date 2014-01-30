using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	
	public float 				maxSpeed;	
	
	private CharacterController	_controller;
	private float 				_speed;
	private float 				_extraGravity;
	private TurnScript			_turnScript;
	
	void Start( ) 
	{
		_controller				= gameObject.GetComponent<CharacterController>( );
		_turnScript				= gameObject.GetComponent<TurnScript>( );
		_turnScript.direction	= SpawnObject.direction;
		_speed					= 0;
		
		if ( _turnScript.direction == Direction.Left )
			_turnScript.TurnAround( false );
	}
	
	//a boolean value specifying if this enemy has been grounded for the first time already.
	private bool _hasGrounded	= false;
	void Update( ) 
	{		
		if ( _controller.isGrounded )
		{
			_speed			= maxSpeed;
			_hasGrounded	= true;
			_extraGravity	= 0;
		} else if ( _hasGrounded )
		{
			_speed			= maxSpeed / 2;
			_extraGravity	= -0.1f;
			
		}
		Vector3 velocity	= _turnScript.direction == Direction.Left ? Vector3.left : Vector3.right;
		velocity			*= _speed;
		velocity.y 			= _extraGravity;
		_controller.Move( velocity );
	}
	
	void OnTriggerEnter( Collider other )
	{
		if ( other.tag == "Player" )
		{
			GameStats.setScore( 0 );
			Application.LoadLevel( "Level1" );
		}
	}
	
	public void OnHitWall( )
	{
		_turnScript.TurnAround( true );
	}
}

using UnityEngine;
using System.Collections;

public class LavaScript : MonoBehaviour 
{
    public GameObject go_platforms;
	private static float _increaseLavaAmount	= 0;
	
	public static Transform[]	lastPlatforms	{ get; private set; }
	public static float			lastplatHeight	{ get; private set; }
	public float height	{ get { return transform.position.y; } }
	

	void Start( )
	{
		soundManager.playLavaLoop( );
	}
	
	void OnTriggerEnter( Collider other )
	{
		if ( other.tag	== "Player" )
		{	
			StartCoroutine( ( other.gameObject.GetComponent<PlayerScript>( ) as PlayerScript ).PlayerDies( "Retry" ) );
		} if ( other.tag == "Enemy" )
		{
			Destroy( other.GetComponent<Rigidbody>( ) );
			Destroy( other.GetComponent<EnemyController>( ) );
			Destroy( other.GetComponent<CharacterMotor>( ) );
			Destroy( other.GetComponent<CharacterController>( ) );
			IncreaseLava( 0.5f );
		}
	}
	
	void Update( )
	{
		if ( _increaseLavaAmount != 0 )
		{
			transform.position		+= Vector3.up * _increaseLavaAmount;
			_increaseLavaAmount		= 0;
		}
		if ( transform.position.y <- 10 )
		{
			transform.position = new Vector3( transform.position.x, -10, transform.position.z );
		}
	}
	
	public static void IncreaseLava( float amount )
	{
		_increaseLavaAmount	= amount;
	}
	
	public static void DecreaseLava( float amount )
	{
		_increaseLavaAmount	= -amount;
	}
}

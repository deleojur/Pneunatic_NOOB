using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
	public ParticleSystem[] moveParticleSystems;
	public ParticleSystem[] stationarySystems;
	public ParticleSystem 	jumpSystem;
	public Light[]			lights;
	
	private TurnScript 			_turnScript;
	private CharacterMotor   	_motor;
	private Vector3				_previousPosition;
	private bool 				_isMoving 	= false;
	private bool 				_isJumping 	= false;
	
	// Use this for initialization
	void Start( )
	{
		soundManager.playHover( );
		_previousPosition	= transform.position;
		_turnScript			= gameObject.GetComponent<TurnScript>( );
		_motor				= gameObject.GetComponent<CharacterMotor>( );
		//jumpSystem.enableEmission	= false;
		foreach( Light light in lights )
		{
			light.intensity	= 0;
		}
	}	
		
	void Update( )
	{
		_isMoving	=  Mathf.Abs( _motor.movement.velocity.x ) > 0.1f;
		
		if ( _isMoving && transform.position.x > _previousPosition.x )
		{
			_turnScript.TurnRight( 10 );		
		}
		else if ( _isMoving && transform.position.x < _previousPosition.x )
		{
			_turnScript.TurnLeft( 10 );			
		}
		_previousPosition	= transform.position;
		foreach( Light light in lights )
		{
			if ( _isMoving || _motor.movement.velocity.y > 0 )
			{
				light.intensity += 0.067f;
				light.intensity = Mathf.Min( light.intensity, 3 );
				//StartCoroutine( AlterIntensityLight( light, 3, 300 ) );
			}
			else if ( light.intensity > 0 )
			{
				light.intensity -= 0.07f;
				light.intensity = Mathf.Max( light.intensity, 0 );
			}
		}
	}
	
	/// <summary>
	/// Alters the intensity of the light over a period of time.
	/// </summary>
	/// The light that should have its intensity altered.
	/// <param name='light'>
	/// Light.
	/// </param>
	/// <param name='intensity'>
	/// the new intensity.
	/// </param>
	/// <param name='t'>
	/// the time in which the alteration should take place in seconds.
	/// </param>
	private IEnumerator AlterIntensityLight( Light light, float newIntensity, float t )
	{
		float alterationPerFrame	= ( newIntensity - light.intensity ) / t;
		while( true )
		{
			if ( Mathf.Abs( newIntensity - light.intensity ) < alterationPerFrame )
			{
				light.intensity = newIntensity;
				break;				
			} else light.intensity += alterationPerFrame;
			yield return null;
		}
	}
	
	/// <summary>
	/// Alters the particle intensity.
	/// </summary>
	/// <returns>
	/// The particle intensity.
	/// </returns>
	/// <param name='ps'>
	/// Ps.
	/// </param>
	/// <param name='newIntensity'>
	/// New intensity.
	/// </param>
	/// <param name='t'>
	/// the time in which the alteration should take place should always be lower than the newIntensity integer.
	/// </param>
	private IEnumerator AlterParticleIntensity( ParticleSystem ps, int newIntensity, float t )
	{
		int particlesPerFrame	= ( int )( ( newIntensity - ps.particleCount ) / t );
		particlesPerFrame		= ( int )Mathf.Max( 1, particlesPerFrame );
		while( true )
		{
			if ( Mathf.Abs( newIntensity - ps.maxParticles ) < particlesPerFrame )
			{
				ps.maxParticles = ( int )newIntensity;
				break;				
			} else ps.maxParticles += ( int )particlesPerFrame;
			yield return null;
		}
	}
	
	private IEnumerator PlayJumpParticles( )
	{
		//yield return new WaitForSeconds( 0.1f );
		jumpSystem.enableEmission	= true;
		yield return new WaitForSeconds( jumpSystem.startLifetime );
		jumpSystem.enableEmission	= false;
	}
	
	void LateUpdate( )
	{		
		if ( _isMoving || _motor.movement.velocity.y > 0 )
		{
			DisableParticleSystems( ref stationarySystems );
			EnableParticleSystem( ref moveParticleSystems );
		} else 
		{
			DisableParticleSystems( ref moveParticleSystems );
			EnableParticleSystem( ref stationarySystems );			
		}
		if ( _motor.IsJumping( ) && !_isJumping )
		{
			_isJumping		= true;
			soundManager.PlayJump( );
			//StartCoroutine(PlayJumpParticles( )  );
		} else if ( _motor.IsGrounded( ) )
		{
			_isJumping		= false;
		}
	}	
	
	private void EnableParticleSystem( ref ParticleSystem[] particleSystems )
	{
		foreach ( ParticleSystem ps in particleSystems )
		{
			ps.enableEmission 	= true;
			//StopCoroutine( "AlterParticleIntensity" );
			//StartCoroutine( AlterParticleIntensity( ps, ( int )Mathf.Abs( _motor.movement.velocity.x * 25 ), 200 ) );
		}
	}
	
	private void DisableParticleSystems( ref ParticleSystem[] particleSystems )
	{
		foreach ( ParticleSystem ps in particleSystems )
		{
			ps.enableEmission = false;
			//StopCoroutine( "AlterParticleIntensity" );
			//StartCoroutine( AlterParticleIntensity( ps, 0, 75 ) );
		}
	}
	
	public IEnumerator PlayerDies( string levelToLoad )
	{
		//make sure the player will have collision with platforms when the game resets.
		Physics.IgnoreLayerCollision( 9, 10, false );
		GameStats.setScore( 0 );
		_motor.canControl	= false;
		soundManager.playplayerDies( );
		
		Application.LoadLevel( levelToLoad );
		yield return new WaitForSeconds( 0 );
	}
}

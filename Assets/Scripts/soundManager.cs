using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class soundManager : MonoBehaviour 
{
	public AudioSource countDown;
	public AudioSource ennemyDies;
	public AudioSource hover;
	public AudioSource jump;
	public AudioSource jump2;
	public AudioSource lavaLoop;
	public AudioSource platformSwith;
	public AudioSource playerDies;

	private static AudioSource _countDown;
	private static AudioSource _enemyDies;
	private static AudioSource _hover;
	private static AudioSource _jump;
	private static AudioSource _jump2;
	private static AudioSource _lavaLoop;
	private static AudioSource _platformSwith;
	private static AudioSource _playerDies;

	public void Awake( )
	{
		_countDown 		= countDown;
		_enemyDies 	= ennemyDies;
		_hover 			= hover;
		_jump 			= jump;
		_jump2			= jump2;
		_lavaLoop 		= lavaLoop;
		_platformSwith 	= platformSwith;
		_playerDies 	= playerDies;

	}
	
	public static void playCountDown()
	{
		//_countDown.Play();
	}
	
	public static void playEnnemyDies()
	{
		//_enemyDies.Play ();

	}
	
	public static void PlayJump( )
	{
		if ( Random.value > 0.5f )
			_jump.Play( );
		else _jump2.Play( );
	}
	
	public static void playHover()
	{
		_hover.Play ();
	}
	
	public static void playLavaLoop()
	{
		_lavaLoop.Play ();
	}
	
	public static void playPlatformSwith()
	{
		_platformSwith.Play ();
	}
	
	public static void playplayerDies()
	{
		_playerDies.Play ();
	}
}

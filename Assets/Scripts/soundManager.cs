using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class soundManager : MonoBehaviour 
{
	public AudioSource countDown;
	public AudioSource ennemyDies;
	public AudioSource hover;
	public AudioSource jump;
	public AudioSource lavaLoop;
	public AudioSource platformSwith;
	public AudioSource playerDies;

	private static AudioSource _countDown;
	private static AudioSource _ennemyDies;
	private static AudioSource _hover;
	private static AudioSource _jump;
	private static AudioSource _lavaLoop;
	private static AudioSource _platformSwith;
	private static AudioSource _playerDies;

	public void Start()
	{
		_countDown = countDown;
		_ennemyDies = ennemyDies;
		_hover = hover;
		_jump = jump;
		_lavaLoop = lavaLoop;
		_platformSwith = platformSwith;
		_playerDies = playerDies;

	}
	
	public static void playCountDown()
	{
		_countDown.Play();
	}
	
	public static void playEnnemyDies()
	{
		_ennemyDies.Play ();

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

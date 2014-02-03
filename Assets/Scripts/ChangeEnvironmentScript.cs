using UnityEngine;
using System.Collections;
using System;

internal enum CurrentEnvironment
{ 
    Blue,
    Red,
    Green,
    Yellow
};

public class ChangeEnvironmentScript : MonoBehaviour
{
    private CurrentEnvironment _currentEnvironment;
    private CurrentEnvironment _previousEnviroment;
    public GameObject go_platformList;
	// Use this for initialization
    IEnumerator Start( )
    {
		_previousEnviroment = CurrentEnvironment.Yellow;
		MoveDown();
        _currentEnvironment = CurrentEnvironment.Blue;        
        ChangeEnvironment( );
        
		yield return new WaitForSeconds( 0.1f );
       	StartCoroutine( InputHandling( ) );
    }

    private IEnumerator InputHandling()
    {
		while ( true )
		{
	        if ( Input.GetKeyDown( "joystick button 0" ) || Input.GetKeyDown( KeyCode.DownArrow ) )
	        {
	            _currentEnvironment = CurrentEnvironment.Green;
	        } else if ( Input.GetKeyDown( "joystick button 1" ) || Input.GetKeyDown( KeyCode.RightArrow ) )
	        {
	            _currentEnvironment = CurrentEnvironment.Red;
	        } else if ( Input.GetKeyDown( "joystick button 2" ) || Input.GetKeyDown( KeyCode.LeftArrow ) )
	        {
	            _currentEnvironment = CurrentEnvironment.Blue;
	        } else if ( Input.GetKeyDown( "joystick button 3" ) || Input.GetKeyDown( KeyCode.UpArrow ) )
	        {
	            _currentEnvironment = CurrentEnvironment.Yellow;
	        }
	        if (_currentEnvironment != _previousEnviroment)
	        {
	            ChangeEnvironment( );
				yield return new WaitForSeconds( 0.05f );
	        }
			yield return null;
		}
    }  
    private void ChangeEnvironment()
    {
		foreach(Transform t in go_platformList.transform)
		{
            //checks the current enviroment. If the colour matches the current colour it updates every platform with the same color
            if (_currentEnvironment == CurrentEnvironment.Blue && t.name.StartsWith("Blue"))
            {
                t.GetChild(0).GetComponent<PlatformHandler>().FoldOut();
            } else if (_currentEnvironment == CurrentEnvironment.Green && t.name.StartsWith("Green"))
            {
                t.GetChild(0).GetComponent<PlatformHandler>().FoldOut();
            } else if (_currentEnvironment == CurrentEnvironment.Red && t.name.StartsWith("Red"))
            {
                t.GetChild(0).GetComponent<PlatformHandler>().FoldOut();
            } else if (_currentEnvironment == CurrentEnvironment.Yellow && t.name.StartsWith("Orange"))
            {
                t.GetChild(0).GetComponent<PlatformHandler>().FoldOut();
            }

            //checks the previous enviroment. If the colour matches the current colour it updates every platform with the same color
            if (_previousEnviroment == CurrentEnvironment.Blue && t.name.StartsWith("Blue"))
            {
                t.GetChild(0).GetComponent<PlatformHandler>().FoldIn();
            }
            else if (_previousEnviroment == CurrentEnvironment.Green && t.name.StartsWith("Green"))
            {
                t.GetChild(0).GetComponent<PlatformHandler>().FoldIn();
            }
            else if (_previousEnviroment == CurrentEnvironment.Red && t.name.StartsWith("Red"))
            {
                t.GetChild(0).GetComponent<PlatformHandler>().FoldIn();
            }
            else if (_previousEnviroment == CurrentEnvironment.Yellow && t.name.StartsWith("Orange"))
            {
                t.GetChild(0).GetComponent<PlatformHandler>().FoldIn();
            }
        }
		GameObject go			= GameObject.FindGameObjectWithTag( "Player" );
		CharacterController cc	= go.GetComponent<CharacterController>( ) as CharacterController;
		
		//soundManager.playPlatformSwith( );
		if ( cc.isGrounded )
		{
			StartCoroutine( HopOnPlatformChange( go.transform ) );
		}		
        _previousEnviroment = _currentEnvironment;
    }
	
	private IEnumerator HopOnPlatformChange( Transform playerTransform )
	{
		for ( int i = 0; i < 25; ++i )
		{
			playerTransform.position += Vector3.up * 0.15f;
			yield return null;
		}		
	}
	
    private void MoveDown()
    {
        foreach (Transform t in go_platformList.transform)
        {
             t.GetChild(0).GetComponent<PlatformHandler>().FoldIn();
        }
    }

}
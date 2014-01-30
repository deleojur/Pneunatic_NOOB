using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public string sceneName;
	
	IEnumerator Start( )
	{
		while ( true )
		{
			if ( Input.GetKeyDown( "joystick button 0" ) )
			{
				OnMouseDown( );
			}
			yield return null;
		}
	}
	
	void OnMouseDown( )
	{
		//Assign scenes in build settings
		Application.LoadLevel( sceneName );
	}

}

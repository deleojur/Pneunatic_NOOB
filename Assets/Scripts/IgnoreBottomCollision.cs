using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]

public class IgnoreBottomCollision : MonoBehaviour 
{
	public GameObject[]	gameObjectsToIgnore;
	public Collider		collider;
			
	void Start ( ) 
	{
		
	}
	
	void OnTriggerEnter( Collider other )
	{
		foreach ( GameObject go in gameObjectsToIgnore )
		{
			if ( go == other.gameObject )
			{
				Collider c	= go.GetComponent<Collider>( );
				if ( collider.enabled && c.enabled )
					Physics.IgnoreCollision( collider, c, true );
			}
		}		
	}
	
	void OnTriggerExit( Collider other )
	{
		foreach ( GameObject go in gameObjectsToIgnore )
		{
			if ( go == other.gameObject )
			{
				Collider c	= go.GetComponent<Collider>( );
				if ( collider.enabled && c.enabled )
					Physics.IgnoreCollision( collider, c, false );
			}
		}		
	}
}

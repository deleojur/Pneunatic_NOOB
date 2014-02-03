using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal struct BottomCollisionInfo
{
	public GameObject gameObject{ get; set; }
	public Collider collider 	{ get; set; }
	public bool isColliding		{ get; set; }
};

[RequireComponent( typeof( Collider ) )]
public class IgnoreBottomCollision : MonoBehaviour 
{
	public GameObject[]				gameObjectsToIgnore;
	public Collider					collider;
	
	private BottomCollisionInfo[] 	_colliderInfo;
	
	void Start ( )
	{
		_colliderInfo	= new BottomCollisionInfo[gameObjectsToIgnore.Length];
		for ( int i = 0; i < gameObjectsToIgnore.Length; ++i )
		{
			_colliderInfo[i]	= new BottomCollisionInfo{ collider = gameObjectsToIgnore[i].collider, gameObject = gameObjectsToIgnore[i] };
		}
	}
	
	void LateUpdate( )
	{
		if ( collider.enabled )
		{
			foreach ( BottomCollisionInfo c in _colliderInfo )
			{
				if ( c.collider.enabled )
				{
					Physics.IgnoreCollision( collider, c.collider, c.isColliding );
				}
			}			
		}		
	}
	
	void OnTriggerEnter( Collider other )
	{
		for ( int i = 0; i < _colliderInfo.Length; ++i )
		{
			GameObject go = _colliderInfo[i].gameObject;
			if ( go == other.gameObject )
			{				
				Collider c		= go.GetComponent<Collider>( );
				if ( c )
				{
					_colliderInfo[i].isColliding	= true;
				}
			}
		}		
	}
	
	void OnTriggerExit( Collider other )
	{
		for ( int i = 0; i < _colliderInfo.Length; ++i )
		{
			GameObject go = _colliderInfo[i].gameObject;
			if ( go == other.gameObject )
			{				
				Collider c		= go.GetComponent<Collider>( );
				if ( c )
				{
					_colliderInfo[i].isColliding	= false;
				}
			}
		}		
	}
}

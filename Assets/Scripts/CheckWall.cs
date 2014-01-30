using UnityEngine;
using System.Collections;

public class CheckWall : MonoBehaviour 
{
	private EnemyController _controller;
	
	void Start( )
	{
		_controller	= transform.parent.GetComponent<EnemyController>( );
	}
	
	void OnTriggerEnter( Collider other )
	{	
		if ( other.tag != "Enemy" && other.tag != "Pickup" )
		{
			_controller.OnHitWall( );
		}
	}
}

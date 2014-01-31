using UnityEngine;
using System.Collections;

public class SpawnObject : MonoBehaviour 
{
	public GameObject   	spawnObject;
	public GameObject		spawnIdentifier;
	public float    		spawnDelay 			= 1f;
	public bool    			spawnAtRandomSpeed 	= false;
	public float    		randomRangeMin 		= 1f;
	public float    		randomRangeMax 		= 10f;
	
	public float 			minPosX				= -12.5f;
	public float			maxPosX				= 12.5f;
	public float 			platformWidth		= 5;
		
	 // Use this for initialization
	void Start( ) 
	{
		Physics.IgnoreLayerCollision ( 8, 8 );
		Physics.GetIgnoreLayerCollision( 9, 10 );
		StartCoroutine( StartSpawning( ) );
	}
	
	public static Direction direction { get; private set; }
	
	IEnumerator StartSpawning( )
	{
		yield return new WaitForSeconds( 2 );
		while ( true )
		{
			float dif	= Mathf.Abs( maxPosX - minPosX );
			float value	= ( int )dif / platformWidth;
			float posX	= ( int )( Random.value * value ) * platformWidth;
			float minX	= platformWidth / 2;
			
			Vector3 position	= new Vector3( minX + posX - 12.5f, transform.position.y, transform.position.z );
			GameObject go 		= Instantiate( spawnObject, position, spawnObject.transform.rotation ) as GameObject;
			direction			= ( Direction )( int )Random.Range( 0, 2 );
			StartCoroutine( DisplayEnemyPos( position.x, 2 ) );
			
	   		if( spawnAtRandomSpeed ) 
				spawnDelay = Random.Range( randomRangeMin, randomRangeMax );
	   		yield return new WaitForSeconds( spawnDelay );
		}
	}
	
	IEnumerator DisplayEnemyPos( float posX, float t )
	{
		GameObject go	= GameObject.Instantiate( spawnIdentifier, new Vector3( posX, 12, 0 ), spawnIdentifier.transform.rotation ) as GameObject;
		yield return new WaitForSeconds( t );
		
		GameObject.Destroy( go );
	}
}
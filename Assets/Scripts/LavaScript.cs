using UnityEngine;
using System.Collections;

public class LavaScript : MonoBehaviour 
{
    public GameObject go_platforms;
	
	private PriorityQueue<PlatformComparer> _pq_platforms;
	private static float _increaseLavaAmount	= 0;
	
	public static Transform[]	lastPlatforms	{ get; private set; }
	public static float			lastplatHeight	{ get; private set; }
	public float height	{ get { return transform.position.y; } }
	

	/*void Awake( ){
        go_platforms = (GameObject)GameObject.Find("PlatformList");
        _pq_platforms = new PriorityQueue<PlatformComparer>();
        print(go_platforms);
        foreach (Transform t in go_platforms.transform)
		{
			PlatformComparer pl	= t.gameObject.GetComponent<PlatformComparer>( ) as PlatformComparer;
            _pq_platforms.AddElement(pl);
		}
		GetLastPlatforms( );
	}*/
	
	void OnTriggerEnter( Collider other )
	{
		if ( other.tag	== "Player" )
		{	
			GameStats.setScore( 0 );
			Application.LoadLevel( "Retry" );
		} if ( other.tag == "Enemy" )
		{
			Destroy( other.GetComponent<Rigidbody>( ) );
			Destroy( other.GetComponent<EnemyController>( ) );
			Destroy( other.GetComponent<CharacterMotor>( ) );
			Destroy( other.GetComponent<CharacterController>( ) );
			IncreaseLava( 0.5f );
		}
	}
	
	public void GetLastPlatforms( )
	{
		PlatformComparer[] plat;
        if (_pq_platforms.GetHighestPriorities(out plat))
		{
			lastPlatforms	= new Transform[plat.Length];
			for ( int i = 0; i < plat.Length; ++i )
			{
				lastPlatforms[i] = plat[i].transform;				
			}
			lastplatHeight	= lastPlatforms[0].transform.position.y;
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

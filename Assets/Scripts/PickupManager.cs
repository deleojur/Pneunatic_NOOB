using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupManager : MonoBehaviour 
{
	public GameObject 		pointPickup;
	public PlatformManager	platformManager;
	public LavaScript		lavaScript;
	public Light			timeLeftIdentIfier;
	
	public float xSize		= 0.0f;
	public float ySize		= 0.0f;
	public float nodeSize	= 0.0f;
	
	public int minVerticalDistLava;
	public int maxVerticalDistFromPlatform;
	public int platformWidth;
	
	private Node[,] _grid;
	private List<Node> _usableGrid;
	
	private int _numberXNodes;
	private int _numberYNodes;
	
	private int _posXMin;
	private int _posYMin;
	
	private bool _isPickupActive = false;
	private float _pickupTime, _originalPickupTime;
	
	void Awake( )
	{
		timeLeftIdentIfier.enabled	= false;
		_numberXNodes				= ( int )( xSize / nodeSize );
		_numberYNodes				= ( int )( ySize / nodeSize );
		
		_posXMin					= ( int )( -_numberXNodes / 2 );
		_posYMin					= ( int )( -_numberYNodes / 2 );
		
		_grid						= new Node[_numberXNodes, _numberYNodes];
		_usableGrid					= new List<Node>( );
				
		GenerateGrid( );
	}
	
	IEnumerator Start( )
	{
		yield return new WaitForSeconds( 1 );
		
		StartCoroutine( FlashPickups( ) );
		SpawnPickup( pointPickup, 12 );
		_isPickupActive	= true;
	}
	
	private void GenerateGrid( )
	{
		for ( int x = 0; x < _numberXNodes; ++x )
		{
			for ( int y = 0; y < _numberYNodes; ++y )
			{
				Node n 		= new Node( new Vector3( _posXMin + x * nodeSize, -7 + y * nodeSize ) );
				_grid[x, y]	= n;
			}
		}
	}
	
	private void GetIndex( Vector3 position, out int x, out int y )
	{
		x	= ( int )Mathf.Clamp( ( int )( position.x - _posXMin + ( nodeSize / 2 ) ) / nodeSize, 0, _numberXNodes );
		y	= ( int )Mathf.Clamp( ( int )( position.y + 7 + ( nodeSize / 2 ) ) / nodeSize, 0, _numberYNodes );
	}
	
	private void SetAllUsableNodes( Vector3[] positions )
	{
		_usableGrid.Clear( );
		for ( int i = 0; i < positions.Length; ++i )
		{
			int xIndex, yIndex;
			GetIndex( positions[i] + ( Vector3.left * platformWidth * 0.4f ), out xIndex, out yIndex );
			for ( int x = 0; x < platformWidth; ++x )
			{
				if ( lavaScript.height < positions[i].y )
				{
					for ( int y = 0; y < maxVerticalDistFromPlatform; ++y )
					{
						_usableGrid.Add( _grid[xIndex + x, yIndex + y + 2] );
					}
				}
			}
		}
	}
	
	private GameObject lastSpawnedObject = default( GameObject );
	private void SpawnPickup( GameObject go, float pickupTime )
	{
		SetAllUsableNodes( platformManager.positionList );
		int index				= ( int )( Random.value * _usableGrid.Count );
		Node n					= _usableGrid[index];
		lastSpawnedObject 		= GameObject.Instantiate( go, n.position, go.transform.rotation ) as GameObject;
		PickupScript pickup		= lastSpawnedObject.GetComponent<PickupScript>( );
		timeLeftIdentIfier.transform.position	= n.position;
		_pickupTime				= pickupTime;
		pickup.pickupManager	= this;
	}
	
	public IEnumerator FlashPickups( )
	{
		float time = 0;
				
		while ( true )
		{
			if ( _isPickupActive )
			{
				timeLeftIdentIfier.enabled	= !timeLeftIdentIfier.enabled;
				_pickupTime	-= ( _pickupTime / 12 );
				if ( _pickupTime <= 1 )
				{
					OnPlayerFails( );
				}
				yield return new WaitForSeconds( _pickupTime / 12 );
			} else yield return null;
		}
	}
	
	public void OnPlayerSucceeds( )
	{
		GameStats.IncreaseScore( 1 );
		LavaScript.DecreaseLava( 0.33f );
		SpawnPickup( pointPickup, 12 );
	}
	
	public void OnPlayerFails(  )
	{
		GameStats.IncreaseScore( -1 );
		GameObject.DestroyImmediate( lastSpawnedObject );
		SpawnPickup( pointPickup, 12 );
		LavaScript.IncreaseLava( 0.33f );
	}
}
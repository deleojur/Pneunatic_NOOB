using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class Sequence : MonoBehaviour 
{
	/*public GameObject 		sequenceComponent;
	public PlatformManager	platformManager;
	public LavaScript		lavaScript;
	public Light			sequenceIdentifier;
	
	public float xSize		= 0.0f;
	public float ySize		= 0.0f;
	public float nodeSize	= 0.0f;
	
	public int minDistYFromLava;
	public int maxDistYFromPlatform;
	public int platformWidth;
	
	private Node[,] _grid;
	private List<Node> _usableGrid;
	private List<PickupScript> _sequence;
	private int _sequenceID;
	
	private int _numberXNodes;
	private int _numberYNodes;
	
	private int _posXMin;
	private int _posYMin;
	
	private bool _isSequenceActive = false;
	private float _sequenceTime, _originalSequenceTime;
	
	void Awake( )
	{
		sequenceIdentifier.enabled	= false;
		_numberXNodes				= ( int )( xSize / nodeSize );
		_numberYNodes				= ( int )( ySize / nodeSize );
		
		_posXMin					= ( int )( -_numberXNodes / 2 );
		_posYMin					= ( int )( -_numberYNodes / 2 );
		
		_grid						= new Node[_numberXNodes, _numberYNodes];
		_usableGrid					= new List<Node>( );
		_sequence					= new List<SequenceComponentScript>( );
		
		GenerateGrid( );
	}
	
	IEnumerator Start( )
	{
		yield return new WaitForSeconds( 8 );
		
		SetAllUsableNodes( platformManager.positionList );
		
				
		StartCoroutine( FlashCoin( ) );
		StartCoroutine( CountDownSequenceTime( ) );
		
		UpdateSequence( 3, 20 );
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
	
	void Update( )
	{
		
	}
	
	public void UpdateLava( float lavaHeight )
	{
		
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
				for ( int y = 0; y < maxDistYFromPlatform; ++y )
				{
					if ( lavaScript.height < _grid[xIndex, yIndex + y + 2].position.y )
					//_grid[xIndex + x, yIndex + y + 2].state = NodeState.Free;
						_usableGrid.Add( _grid[xIndex + x, yIndex + y + 2] );
				}
			}
		}
	}
	
	public void UpdateSequence( int sequenceAmount, float time )
	{
		_isSequenceActive	= true;
		_sequence.Clear( );
		for ( int i = 0; i < sequenceAmount; ++i )
		{
			int index					= ( int )( Random.value * _usableGrid.Count );
			Node n						= _usableGrid[index];
			GameObject sc				= GameObject.Instantiate( sequenceComponent, n.position, sequenceComponent.transform.rotation ) as GameObject;
			SequenceComponentScript scs = sc.GetComponent<SequenceComponentScript>( );
			scs.sequenceManager			= this;
			scs.sequenceID				= i;
			_sequence.Add( scs );
		}
		_originalSequenceTime	= _sequenceTime	= time;
		IdentifyNextSequenceComponent( );
	}
	
	public void OnPlayerPicksUpSequenceComponent( int sequenceID )
	{
		if ( _sequenceID == sequenceID )
		{
			Destroy( _sequence[sequenceID].gameObject );
			_sequenceTime	= _originalSequenceTime;
			++_sequenceID;
			if ( _sequenceID == _sequence.Count )
			{				
				sequenceIdentifier.enabled	= false;
				StartCoroutine( OnPlayerCompletesSequence( ) );
			} else IdentifyNextSequenceComponent( );
		} else 
		{
			StartCoroutine( OnPlayerFailsSequence( ) );
		}
	}
	
	private void IdentifyNextSequenceComponent( )
	{
		sequenceIdentifier.enabled				= true;
		sequenceIdentifier.transform.position	= _sequence[_sequenceID].transform.position;
	}
	
	public IEnumerator FlashCoin( )
	{
		float time = 0;
				
		while ( true )
		{
			if ( _isSequenceActive )
			{
				sequenceIdentifier.enabled	= !sequenceIdentifier.enabled;
				
				if ( _sequenceTime <= 0 )
				{
					StartCoroutine( OnPlayerFailsSequence( ) );
				}
				yield return new WaitForSeconds( _sequenceTime / 12 );
			} else yield return null;
		}
	}
	
	public IEnumerator CountDownSequenceTime( )
	{
		while ( true )
		{
			if ( _isSequenceActive )
			{
				--_sequenceTime;
				yield return new WaitForSeconds( 1 );
			}
			yield return null;
		}
	}
	
	private IEnumerator OnPlayerCompletesSequence( )
	{
		int amount = _sequence.Count + 1;
		_sequence.Clear( );
		_sequenceID	= 0;
		GameStats.IncreaseScore( amount - 1 );
		LavaScript.DecreaseLava( 5 );
		//TODO
		
		// the sequence should get harder
		// every n number of completed sequences, the level should change, and one bad thing and one good thing should happen
		// score should increase.	
		
		LavaScript.DecreaseLava( );
		
		yield return new WaitForSeconds( 5 );
		UpdateSequence( amount, _originalSequenceTime );
	}
	
	private IEnumerator OnPlayerFailsSequence( )
	{
		_isSequenceActive				= false;
		sequenceIdentifier.enabled		= false;
		foreach ( SequenceComponentScript obj in _sequence )
		{
			if ( obj != null )
			Destroy( obj.gameObject );
		}
		int amount = _sequence.Count;
		_sequence.Clear( );	
		_sequenceID	= 0;
		LavaScript.IncreaseLava( 2 );		
		//TODO
		// temp changes:
		// either the enemy gets a burst spawns or the lava increases
		// every n number of failed sequences, the sequence gets easier
		// the score should decrease (muhaha).
		
		yield return new WaitForSeconds( 5 );
		UpdateSequence( amount, _originalSequenceTime );
	}*/
}

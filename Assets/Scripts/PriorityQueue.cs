using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PriorityQueue<T> : IEnumerable, IEnumerator where T : IComparable<T>
{
	private T[] _elements;
	public int length	{ get; private set; }
	private int _real_size;
	
	private int position	= -1;
	
	public PriorityQueue( )
	{
		Flush( );
	}	
	
	public void AddElement( T element )
	{
		if ( ++length == _real_size - 1 )
		{
			DoubleRealSize( );
		}
		_elements[length - 1]	= element;
		
		for ( int i = length - 1; i > 0; --i )
		{
			if ( _elements[i].CompareTo( _elements[i - 1] ) > 0 )
			{
				T temp 				= _elements[i];
				_elements[i] 		= _elements[i - 1];
				_elements[i - 1]	= temp;
			} else break;
		}
	}
	
	public IEnumerator GetEnumerator( )
	{
		return this;
	}

	#region IEnumerator implementation
	public bool MoveNext( )
	{
		++position;
		return position < length;
	}

	public void Reset( )
	{
		position = -1;
	}

	public object Current 
	{
		get 
		{
			try
			{
				return _elements[position];
			} catch( IndexOutOfRangeException )
			{
				throw new InvalidOperationException( );
			}
		}
	}
	#endregion	
	
	
	private void DoubleRealSize(  )
	{
		T[] elements_copy 	= _elements;
		_elements			= new T[_real_size *= 2];
		
		for ( int i = 0; i < elements_copy.Length; ++i )
		{
			_elements[i] 	= elements_copy[i];
		}
	}
	
	public void Flush( )
	{
		for ( int i = 0; i < length; ++i )
		{
			_elements[i]	= default( T );
		}
		
		_real_size	= 4;
		length		= 0;				
		_elements	= new T[_real_size];
	}
	
	/*public bool TryPopHighestPriority( out T element )
	{	
		element	= default( T );
		if ( length == 0 )
			return false;
		
		element	= _elements[0];
		for ( int i = 0; i < length; i++ )
		{
			T temp 				= _elements[i];
			_elements[i]		= _elements[i + 1];
			_elements[i + 1]	= temp;
		}
		_elements[--length] 	= default( T );
		return true;
	}	*/
	
	public bool TryPopHighestPriorities( out T[] elements )
	{
		elements		= new T[0];
		List<T>	elem	= new List<T>( );
		if ( length == 0 )
			return false;
		
		int lastComparison	= 0;
		while( true )
		{	
			if ( lastComparison == 1 )
				break;
			
			lastComparison	= _elements[0].CompareTo( _elements[1] );
			elem.Add( _elements[0] );
			for ( int i = 0; i < length; ++i )
			{
				T temp 				= _elements[i];
				_elements[i]		= _elements[i + 1];
				_elements[i + 1]	= temp;
			}
			_elements[--length] 	= default( T );
			
		}
		
		elements	= elem.ToArray( );
		return true;
	}
	
	public bool GetHighestPriorities( out T[] elements )
	{
		elements		= new T[0];
		List<T>	elem	= new List<T>( );
		if ( length == 0 )
			return false;
		
		int lastComparison	= 0;
		for ( int i = 0; i < _elements.Length; ++i )
		{	
			if ( lastComparison == 1 )
				break;
			
			//lastComparison	= _elements[i].CompareTo( _elements[i + 1] );
			//elem.Add( _elements[i] );			
		}
		
		elements	= elem.ToArray( );
		return true;
	}
	
	public bool TryPopLowestPriority( out T element )
	{	
		element	= default( T );
		
		if ( length == 0 )
			return false;
		
		element = _elements[length - 1];
		_elements[--length]	= default( T );
		return true;
	}
	
	public void RemoveElement( T element )
	{
		RemoveElement( GetIndex( element ) );
	}
	
	public void RemoveElement( int index )
	{
		_elements[index] 	= default( T );
				
		for ( int i = index; i > length - 2; ++i )
		{
			_elements[i]	= _elements[i + 1];
		}		
		length--;
	}
	
	public int GetIndex( T element )
	{
		//TODO maybe do this in a binary search later.
		for ( int i = length - 1; i > 0; --i )
		{
			if ( element.Equals( _elements[i] ) )
				return i;
		}
		//if it is not included in the queue, return -1.
		return -1;
	}
	
	public T this[int i]
	{
		get 	{ return _elements[i]; }
		set		{ _elements[i] = value; }
	}
	
	public override string ToString( )
	{
		string element_string	= string.Empty;
		
		for ( int i = 0; i < length; ++i )
		{
			element_string += _elements[i] + " ";
		}
		
		return string.Format( "[PriorityQueue: length={0}, elements={1}]", length, element_string );
	}
}
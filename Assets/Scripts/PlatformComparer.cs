using UnityEngine;
using System.Collections.Generic;
using System;

public class PlatformComparer : MonoBehaviour, IComparable<PlatformComparer>
{
	#region IComparable[PlatformComparer] implementation
	public int CompareTo( PlatformComparer other )
	{
		if ( transform.position.y == other.transform.position.y )
		{
			return 0;
		}
		
		else return transform.position.y < other.transform.position.y ? 1 : -1;
	}
	#endregion	
}

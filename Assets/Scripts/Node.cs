using UnityEngine;
using System.Collections;

public enum NodeState
{
	Free,
	Occupied,
	UnUsable
};

public class Node
{
	public Vector3	position 	{ get; private set; }
	public NodeState state 		{ get; set; }
		
	public Node( Vector3 position )
	{
		this.position	= position;		
	}
}

using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour 
{
	public PickupManager pickupManager	{ get; set; }
	
	void OnTriggerEnter( Collider other )
	{
		if ( other.tag == "Player" )
		{
			GameObject.Destroy( gameObject );
			pickupManager.OnPlayerSucceeds( );
		}
	}
}

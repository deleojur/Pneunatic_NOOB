using UnityEngine;
using System.Collections;

public class PlatformHandler : MonoBehaviour {

	// Use this for initialization
	public void FoldIn () 
	{
		transform.GetComponent<Animator>().Play("FOLDIN");
        transform.parent.GetComponent<BoxCollider>().enabled = false;
	}

    public void FoldOut()
    {
        transform.GetComponent<Animator>().Play("FOLDOUT");
		transform.parent.GetComponent<BoxCollider>().enabled = true;
       // transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}

using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {

	public Color color;
	public Color pressed;
	public Color hover;
	
	
	
	void Start()
	{
		transform.GetComponent<MeshRenderer> ().material.color = color;
	}

	void OnMouseEnter()
	{
		transform.GetComponent<MeshRenderer> ().material.color = hover;
	}
	
	void OnMouseExit()
	{
		transform.GetComponent<MeshRenderer> ().material.color = color;
	}
	
	void OnMouseDown()
	{
		//Assign scenes in build settings
		transform.GetComponent<MeshRenderer> ().material.color = pressed;
	}
	
	void OnMouseUp()
	{
		transform.GetComponent<MeshRenderer> ().material.color = color;
	}
}

using UnityEngine;

public class Invisible : MonoBehaviour {


	void Start ()
	{
		GetComponent<Renderer>().enabled = false;
	}
}

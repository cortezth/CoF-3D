using UnityEngine;

public class Cam : MonoBehaviour {

	public Vector3 offset;
	public GameObject player;
	public float smooth;
	
	void Update ()
	{
		transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + 
		offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z), smooth);
	}
}

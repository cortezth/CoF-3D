using UnityEngine;

public class Shadow : MonoBehaviour {

	public Transform player;

	public float distance;
	
	void Update ()
	{
		transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);

		distance = player.position.y - transform.position.y;
	}
}

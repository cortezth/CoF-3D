using UnityEngine;

public class Box : MonoBehaviour {

	public Player Scriptplayer;
	public Transform player;
	public bool box;

	void Start ()
	{
		//Scriptplayer = GetComponent<Player>();
	}
	
	void Update ()
	{
		if (Scriptplayer.boxingR)
		{
			transform.position = new Vector3(player.position.x + 1.05f, transform.position.y, transform.position.z);
		}

		if (Scriptplayer.boxingL)
		{
			transform.position = new Vector3(player.position.x - 1.05f, transform.position.y, transform.position.z);
		}
	}
}

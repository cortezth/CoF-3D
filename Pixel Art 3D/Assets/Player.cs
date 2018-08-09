using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed;
	public float gravity;
	public float maxFallSpeed;
	public float jumpSpeed;

	bool isGrounded;

	float xAxis, zAxis;

	bool right, left, forward, back;

	Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (right) xAxis = 1; else if (left) xAxis = -1; else xAxis = 0;
		if (forward) zAxis = 1; else if (back) zAxis = -1; else zAxis = 0;
	}

	private void FixedUpdate()
	{
		right = 
		(  !Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Vector3.right, .5f)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z), Vector3.right, .5f) 
		&& !Physics.Raycast(transform.position, Vector3.right, .5f) && Input.GetButton("Right") && !left);

		left = 
		(  !Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Vector3.left, .5f)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z), Vector3.left, .5f)
		&& !Physics.Raycast(transform.position, Vector3.left, .5f) && Input.GetButton("Left") && !right);

		forward = 
		(  !Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Vector3.forward, .5f)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z), Vector3.forward, .5f)
		&& !Physics.Raycast(transform.position, Vector3.forward, .5f) && Input.GetButton("Forward") && !back);

		back = 
		(  !Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Vector3.back, .5f)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z), Vector3.back, .5f)
		&& !Physics.Raycast(transform.position, Vector3.back, .5f) && Input.GetButton("Back") && !forward);

		rb.MovePosition(transform.position + new Vector3(xAxis, 0, zAxis) * speed * Time.deltaTime);

		rb.velocity += Vector3.down * gravity * Time.deltaTime;

		if (rb.velocity.y < -maxFallSpeed)
			rb.velocity = Vector3.down * maxFallSpeed;

		if (Input.GetButtonDown("Jump"))
			rb.velocity = Vector3.up * jumpSpeed;
	}
}

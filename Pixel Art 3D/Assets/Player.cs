using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed;
	public float gravity;
	public float maxFallSpeed;
	public float jumpSpeed;

	float rayD = .49f;

	bool isGrounded;

	float xAxis, zAxis;

	bool right, left, forward, back;

	public bool oR, oL, oF, oB;

	Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();

		oR = true; oL = true; oF = false; oB = false;
	}

	void Update()
	{
		if (right) xAxis = 1; else if (left) xAxis = -1; else xAxis = 0;
		if (forward) zAxis = 1; else if (back) zAxis = -1; else zAxis = 0;
	}

	private void FixedUpdate()
	{
		right = 
		(  !Physics.Raycast(new Vector3(transform.position.x, transform.position.y + rayD, transform.position.z), Vector3.right, .5f)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - rayD, transform.position.z), Vector3.right, .5f) 
		&& !Physics.Raycast(transform.position, Vector3.right, .5f) && Input.GetButton("Right") && !left && oR);

		left = 
		(  !Physics.Raycast(new Vector3(transform.position.x, transform.position.y + rayD, transform.position.z), Vector3.left, .5f)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - rayD, transform.position.z), Vector3.left, .5f)
		&& !Physics.Raycast(transform.position, Vector3.left, .5f) && Input.GetButton("Left") && !right && oL);

		forward = 
		(  !Physics.Raycast(new Vector3(transform.position.x, transform.position.y + rayD, transform.position.z), Vector3.forward, .5f)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - rayD, transform.position.z), Vector3.forward, .5f)
		&& !Physics.Raycast(transform.position, Vector3.forward, .5f) && Input.GetButton("Forward") && !back && oF);

		back = 
		(  !Physics.Raycast(new Vector3(transform.position.x, transform.position.y + rayD, transform.position.z), Vector3.back, .5f)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - rayD, transform.position.z), Vector3.back, .5f)
		&& !Physics.Raycast(transform.position, Vector3.back, .5f) && Input.GetButton("Back") && !forward && oB);

		rb.MovePosition(transform.position + new Vector3(xAxis, 0, zAxis) * speed * Time.deltaTime);

		isGrounded =
		(  Physics.Raycast(new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z), Vector3.down, .5f)
		|| Physics.Raycast(new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z), Vector3.down, .5f)
		|| Physics.Raycast(transform.position, Vector3.down, .5f));

		if (!isGrounded)
			rb.velocity += Vector3.down * gravity * Time.deltaTime;

		if (rb.velocity.y < -maxFallSpeed)
			rb.velocity = Vector3.down * maxFallSpeed;

		if (Input.GetButtonDown("Jump") && isGrounded)
			rb.velocity = Vector3.up * jumpSpeed;

		RaycastHit Hit;
		if (Physics.Raycast(transform.position, Vector3.right, out Hit, .49f) || Physics.Raycast(transform.position, Vector3.back, out Hit, .49f))
			if (Hit.transform.gameObject.name == "CollRB")
			{
				oR = false;
				oL = true;
				oF = true;
				oB = false;
			}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "TriggerH")
		{
			oR = true;
			oL = true;
			oF = false;
			oB = false;
		}

		if (other.name == "TriggerV")
		{
			oR = false;
			oL = false;
			oF = true;
			oB = true;
		}
	}
}

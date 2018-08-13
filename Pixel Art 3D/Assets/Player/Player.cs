using UnityEngine;

public class Player : MonoBehaviour {

	public float speed;
	public float gravity;
	public float maxFallSpeed;
	public float jumpSpeed;

	float rayD = .49f;
	float xAxis, zAxis;

	public bool boxingR, boxingL, boxR, boxL = false;
	bool isGrounded;
	bool right, left, forward, back;
	bool canMovV, canMovH;

	Rigidbody rb;
	Animator anim;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();

		canMovH = true; canMovV = false;
	}

	void Update()
	{
		if (right) xAxis = 1; else if (left) xAxis = -1; else xAxis = 0;
		if (forward) zAxis = 1; else if (back) zAxis = -1; else zAxis = 0;

		anim.SetBool("MovR", right); anim.SetBool("MovL", left); anim.SetBool("MovF", forward); anim.SetBool("MovB", back);
	}

	private void FixedUpdate()
	{
		right = 
		(  !Physics.Raycast(new Vector3(transform.position.x, transform.position.y + rayD, transform.position.z), Vector3.right, .5f, 1 << 0)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - rayD, transform.position.z), Vector3.right, .5f, 1 << 0)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - rayD), Vector3.right, .5f, 1 << 0)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + rayD), Vector3.right, .5f, 1 << 0)
		&& !Physics.Raycast(transform.position, Vector3.right, .5f, 1 << 0) && Input.GetButton("Right") && !left && canMovH && !boxR);

		left = 
		(  !Physics.Raycast(new Vector3(transform.position.x, transform.position.y + rayD, transform.position.z), Vector3.left, .5f, 1 << 0)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - rayD, transform.position.z), Vector3.left, .5f, 1 << 0)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - rayD), Vector3.left, .5f, 1 << 0)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + rayD), Vector3.left, .5f, 1 << 0)
		&& !Physics.Raycast(transform.position, Vector3.left, .5f, 1 << 0) && Input.GetButton("Left") && !right && canMovH && !boxL);

		forward = 
		(  !Physics.Raycast(new Vector3(transform.position.x, transform.position.y + rayD, transform.position.z), Vector3.forward, .5f, 1 << 0)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - rayD, transform.position.z), Vector3.forward, .5f, 1 << 0)
		&& !Physics.Raycast(new Vector3(transform.position.x + rayD, transform.position.y, transform.position.z), Vector3.forward, .5f, 1 << 0)
		&& !Physics.Raycast(new Vector3(transform.position.x - rayD, transform.position.y, transform.position.z), Vector3.forward, .5f, 1 << 0)
		&& !Physics.Raycast(transform.position, Vector3.forward, .5f, 1 << 0) && Input.GetButton("Forward") && !back && canMovV);

		back = 
		(  !Physics.Raycast(new Vector3(transform.position.x, transform.position.y + rayD, transform.position.z), Vector3.back, .5f, 1 << 0)
		&& !Physics.Raycast(new Vector3(transform.position.x, transform.position.y - rayD, transform.position.z), Vector3.back, .5f, 1 << 0)
		&& !Physics.Raycast(new Vector3(transform.position.x + rayD, transform.position.y, transform.position.z), Vector3.back, .5f, 1 << 0)
		&& !Physics.Raycast(new Vector3(transform.position.x - rayD, transform.position.y, transform.position.z), Vector3.back, .5f, 1 << 0)
		&& !Physics.Raycast(transform.position, Vector3.back, .5f, 1 << 0) && Input.GetButton("Back") && !forward && canMovV);

		rb.MovePosition(transform.position + new Vector3(xAxis, 0, zAxis) * speed * Time.deltaTime);

		isGrounded =
		(( Physics.Raycast(new Vector3(transform.position.x + rayD, transform.position.y, transform.position.z), Vector3.down, .8f)
		|| Physics.Raycast(new Vector3(transform.position.x - rayD, transform.position.y, transform.position.z), Vector3.down, .8f)
		|| Physics.Raycast(transform.position, Vector3.down, .8f)) && rb.velocity.y <= 0);

		if (!isGrounded)
			rb.velocity += Vector3.down * gravity * Time.deltaTime;

		if (rb.velocity.y < -maxFallSpeed)
			rb.velocity = Vector3.down * maxFallSpeed;

		if (Input.GetButton("Jump") && isGrounded && !boxingL && !boxingR)
			rb.velocity = Vector3.up * jumpSpeed;

		if (Input.GetButtonUp("Jump") && rb.velocity.y > 0) 
			rb.velocity = Vector3.up * rb.velocity.y * .5f;

		boxR =
		(  Physics.Raycast(new Vector3(transform.position.x, transform.position.y + rayD, transform.position.z), Vector3.right, .5f, 1 << 10)
		|| Physics.Raycast(new Vector3(transform.position.x, transform.position.y - rayD, transform.position.z), Vector3.right, .5f, 1 << 10)
		|| Physics.Raycast(transform.position, Vector3.right, .5f, 1 << 10));

		boxL =
		(  Physics.Raycast(new Vector3(transform.position.x, transform.position.y + rayD, transform.position.z), Vector3.left, .5f, 1 << 10)
		|| Physics.Raycast(new Vector3(transform.position.x, transform.position.y - rayD, transform.position.z), Vector3.left, .5f, 1 << 10)
		|| Physics.Raycast(transform.position, Vector3.left, .5f, 1 << 10));

		if (Input.GetButton("Box"))
		{
			if (boxR)
				boxingR = true;

			if (boxL)
				boxingL = true;
		}

		if (Input.GetButtonUp("Box") && (boxingL || boxingR))
		{
			boxingR = false; boxingL = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "TriggerH")
		{
			canMovH = true;
			canMovV = false;
		}

		if (other.name == "TriggerV")
		{
			canMovH = false;
			canMovV = true;
		}

		if (other.name == "TriggerHV")
		{
			canMovH = true;
			canMovV = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.name == "TriggerHV")
		{
			transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
		}
	}
}

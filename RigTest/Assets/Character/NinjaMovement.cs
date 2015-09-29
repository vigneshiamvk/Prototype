using UnityEngine;
using System.Collections;

public class NinjaMovement : MonoBehaviour 
{
	public float walkSpeed = 5f;
	public float downAcc= 0.75f;
	public float jumpSpeed = 5f;
	public float groundDistance = 0.1f;
	public bool Climb = false;
	public LayerMask ground;

	public GameObject LadderBottom;
	public GameObject LadderTop;
	
	Vector3 movement;
	
	Animator anim;
	
	Rigidbody ninjaRigidbody;
	
	// Update is called once per frame
	void Awake () 
	{
		anim = GetComponent <Animator> ();
		ninjaRigidbody = GetComponent <Rigidbody> ();

		LadderBottom = GameObject.FindWithTag ("LadderBottom");
		LadderTop = GameObject.FindWithTag ("LadderTop");
		
	}
	
	void FixedUpdate ()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		float j = Input.GetAxisRaw ("Jump");

		if (Climb == false) 
		{
		
			// Move the player around the scene.
			Move (h, j, v);
		
			//Jump
			Jump (h, j, v);
		
			// Animate the player.
			Animating (h, v);

			Grounded();
		
		} 

		else 
		
		{
			Climbing();
		}

	}



	void OnTriggerEnter(Collider coll1)
	{
		if ((coll1.gameObject == LadderBottom) && Climb==false)
		{
			Climb=true;
			ninjaRigidbody.useGravity = false;
		}

		else if ((coll1.gameObject == LadderBottom) && Climb==true)
		{
			Climb=false;
			ninjaRigidbody.useGravity = true;
		}
	}
	
	void OnTriggerExit(Collider coll2)
	{
		if ((coll2.gameObject == LadderTop) && Climb==true)
		{
			Climb=false;
			ninjaRigidbody.useGravity = true;
		}

	}
	
	void Move (float h,float j, float v)
	{
		// Set the movement vector based on the axis input.
		movement.Set (h,j,0f);
		movement = movement.normalized * walkSpeed * Time.deltaTime;
		
		// Move the player to it's current position plus the movement.
		ninjaRigidbody.MovePosition (transform.position + movement);
	}
	
	//Checks Grounded or not
	bool Grounded ()
	{
		return Physics.Raycast(transform.position, Vector3.down, groundDistance, ground);
	}
	
	void Jump(float h, float j, float v)
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			if (j > 0 && Grounded ()) 
			{
				//jump
				movement.y = jumpSpeed;
			} 
			
			else if (j == 0 && Grounded ()) 
			{
				//zero out our velocity
				movement.y = 0f;
			} 
			
			else
			{
				movement.y -= downAcc;
			}
			
		}
	}


	void Climbing()
	{


		if (Input.GetKey (KeyCode.I)) 
		{
			ninjaRigidbody.transform.Translate (Vector3.up * Time.deltaTime * walkSpeed);

		}

		else

		if (Input.GetKey (KeyCode.K)) 
		{
			ninjaRigidbody.transform.Translate (Vector3.down * Time.deltaTime * walkSpeed);

		}

		ninjaRigidbody.MovePosition (transform.position + movement);
	}




	
	void Animating (float h, float v)
	{
		// Create a boolean that is true if either of the input axes is non-zero.
		bool running = h != 0f;
		
		// Tell the animator whether or not the player is walking.
		anim.SetBool ("IsRunning", running);
		
	}
	
}

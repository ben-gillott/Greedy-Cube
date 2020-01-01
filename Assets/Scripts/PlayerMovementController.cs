using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementController : MonoBehaviour
{
    public float m_JumpForce = 400f;	
    private bool grounded = true;
    private Rigidbody2D m_Rigidbody2D;
    private float walkSpeed = 5;


    private void Awake()
	{
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
        int horizontalChange = 0;
		if (Input.GetKey("left"))
        {
            // print("left key was pressed");
            horizontalChange --;
        }
        if (Input.GetKey("right"))
        {
            // print("right key was pressed");
            horizontalChange ++;
        }
        // If the player should jump
		if (Input.GetKey("space") && grounded)
		{
            // print("space bar was pressed");
			// Add a vertical force to the player.
		    grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}

        Vector3 destination = this.transform.position + new Vector3(horizontalChange, 0, 0);
        this.transform.position = Vector3.Lerp(this.transform.position, destination, (float).1);
	}

    void OnCollisionExit2D(Collision2D other) {
        grounded = false;
    }
    void OnCollisionEnter2D(Collision2D col){
        grounded = true;
    }


}

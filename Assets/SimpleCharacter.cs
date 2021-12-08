using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacter : MonoBehaviour
{
	private Animator animator = null;
	private Rigidbody rigidbody = null;
	private UnityEngine.AI.NavMeshAgent agent;
	private float turnValue = 0;
	private float forwardValue = 0;

	// Start is called before the first frame update
	void Start()
    {
		this.rigidbody = GetComponentInChildren<Rigidbody>();
		this.animator = GetComponentInChildren<Animator>();
		this.agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
		this.agent.updatePosition = false;
		//this.agent.Stop();
		//this.agent.updateRotation = false;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		this.agent.nextPosition = transform.position;

		if( Input.GetKey( KeyCode.W ) )
		{
			this.forwardValue = 1;
		}
		else
		{
			this.forwardValue = 0;
		}
		if( Input.GetKey( KeyCode.A ) )
		{
			this.turnValue = -1;
		}
		else if( Input.GetKey( KeyCode.D ) )
		{
			this.turnValue = 1;
		}
		else
		{
			this.turnValue = 0;
		}
		Vector3 velocity = new Vector3( 0, 0, 1 );
		velocity = transform.TransformDirection( velocity );
		velocity.y = 0;
		//this.rigidbody.AddForce( velocity * this.animatorForward * 0.5f, ForceMode.VelocityChange );
		this.rigidbody.velocity = velocity * this.forwardValue * 5;


		//this.animator.SetFloat( "Turn", this.animatorTurn * this.animatorSide );
		Vector3 angularVelocity = this.rigidbody.angularVelocity;
		angularVelocity.y = this.turnValue * 10;
		this.rigidbody.angularVelocity = angularVelocity;
	}
}

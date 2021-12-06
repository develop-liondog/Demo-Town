using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGimmick : MonoBehaviour
{
	private Collider collider = null;
	private Rigidbody rigidbody = null;

    // Start is called before the first frame update
    void Start()
    {
		this.collider = GetComponent<Collider>();
		this.collider.isTrigger = true;
		this.rigidbody = GetComponent<Rigidbody>();
		this.rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnCollisionEnter( Collision other )
	{
	}

	void OnTriggerEnter( Collider other )
	{
		CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
		if( characterController != null )
		{
			this.collider.isTrigger = false;
			this.rigidbody.isKinematic = false;
		}
	}
}

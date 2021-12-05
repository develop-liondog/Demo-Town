using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownController : MonoBehaviour
{
	public float time = 8;
	public GameObject cloudParent = null;
	public Light directionalLight = null;

	void Start()
	{
        
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		this.time += Time.fixedDeltaTime;
		if( this.time >= 24 )
		{
			this.time = 0;
		}
		
		// 雲の更新
		UpdateCloud( Time.fixedDeltaTime );

		// ライトの更新
		UpdateLight( Time.fixedDeltaTime );
	}

	void UpdateCloud( float deltaTime )
	{
		this.cloudParent.transform.Rotate( 0, deltaTime, 0 );
	}

	void UpdateLight( float deltaTime )
	{
		Animator animator = this.directionalLight.GetComponent<Animator>();
		if( animator )
		{
			animator.SetFloat( "Motion Time", this.time / 24 );
		}
	}
}

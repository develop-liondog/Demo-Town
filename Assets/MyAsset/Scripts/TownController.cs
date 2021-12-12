using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownController : MonoBehaviour
{
	public float time = 8;
	public float timescale = 0.1f;
	public GameObject cloudParent = null;
	public Light directionalLight = null;
	public float deactiveStreetlampMinTime = 6;
	public float deactiveStreetlampMaxTime = 18;
	private List<StreetlampController> streetlampControllerList = new List<StreetlampController>();

	void Start()
	{
		foreach( var ctrl in GetComponentsInChildren<StreetlampController>() )
		{
			this.streetlampControllerList.Add( ctrl );
		}
	}

	/// <summary>
	/// 
	/// </summary>
	void FixedUpdate()
	{
		this.time += Time.fixedDeltaTime * this.timescale;
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
		// 太陽光
		Animator animator = this.directionalLight.GetComponent<Animator>();
		if( animator )
		{
			animator.SetFloat( "Motion Time", this.time / 24 );
		}

		// 街灯(夜間のみ点灯する)
		bool isActiveStreetlamp = false;
		if(
			( this.time < this.deactiveStreetlampMinTime )
			|| ( this.time > this.deactiveStreetlampMaxTime )
		)
		{
			isActiveStreetlamp = true;
		}
		foreach( var ctrl in this.streetlampControllerList )
		{
			ctrl.SetActiveLight( isActiveStreetlamp );
		}
	}
}

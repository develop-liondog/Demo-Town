using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PlayerCamera : MonoBehaviour
{
    private GameObject camera = null;
    public CharacterController owner = null;
    public Vector3 offset = new Vector3( 0, 1, 10 );
	public Vector3 lookAtOffset = new Vector3( 0, 3, 0 );
	public float angleX = 0;
	public float angleY = 0;
    public float speed = 15;

    // Start is called before the first frame update
    void Start()
    {
        this.camera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if( this.owner == null )
        {
            return;
        }

		// 右クリックを押している時だけカメラを回転させる
		if( Input.GetMouseButton( 1 ) )
		{
			this.angleX = Utility.Round< float >( this.angleX + Input.GetAxis( "Mouse Y" ) * this.speed, -2, -30 );
			this.angleY += Input.GetAxis( "Mouse X" ) * this.speed;
		}

		// ホイールによるカメラの前後移動
		this.offset.z -= Input.mouseScrollDelta.y;
		if( this.offset.z < 0.1f )
		{
			this.offset.z = 0.1f;
		}

		// プレイヤーを視点にカメラの位置を決定
		Vector3 rotOffset = Quaternion.Euler( this.angleX, this.angleY, 0 ) * this.offset;
		Vector3 cameraPos = this.owner.transform.position + rotOffset;

		// プレイヤーとカメラの間に障害物が挟まらないように位置補正
		Vector3 direction = cameraPos - this.owner.LookAtTransform.position;
		Ray ray = new Ray( this.owner.LookAtTransform.position, direction.normalized );
		RaycastHit hit = new RaycastHit();
		if( Physics.Raycast( ray, out hit, direction.magnitude ) )
		{
			cameraPos = hit.point;
		}
		this.camera.transform.position = cameraPos;
		this.camera.transform.LookAt( this.owner.LookAtTransform );
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.AI;
using Assets.Scripts.AI.Player;
using Assets.Scripts.AI.NonePlayer;

/// <summary>
/// キャラクター制御(プレイヤー、NPC)
/// </summary>
public class CharacterController : MonoBehaviour
{
	public enum State
	{
		Idle,
		PathWait,
		Running,
		Stopping,
	}
	private Animator animator = null;
	private UnityEngine.AI.NavMeshAgent agent;
	private State state = State.Idle;
	private float endDistance = 0;
	private Vector3 targetPos = Vector3.zero;
	private float animatorForward = 0;
	private float animatorSide = 0;
	private float animatorTurn = 0;
	private float animatorTurnMax = 1;
	private TownController townController = null;
	private CharacterStateMachine stateMachine = null;
	public bool isPlayer = false;
	public float passDistance = 0.5f;
	public Transform lookAtTransform = null;
	public Transform targetTransform = null;

	/// <summary>
	/// 現在の状態
	/// </summary>
	public State CurrentState
	{
		get
		{
			return this.state;
		}
	}

	/// <summary>
	/// カメラの注視点
	/// </summary>
	public Transform LookAtTransform
	{
		get
		{
			return this.lookAtTransform;
		}
	}

	/// <summary>
	/// ターゲット
	/// </summary>
	public Transform TargetTransform
	{
		get
		{
			return this.targetTransform;
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		this.townController = FindObjectOfType<TownController>();
		this.animator = GetComponentInChildren<Animator>();
		this.agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
		this.agent.updatePosition = false;
		this.animator.SetFloat( "Forward", 0);

		Rigidbody rigidbody = GetComponent<Rigidbody>();
//		rigidbody.detectCollisions = false;

		// AI
		this.stateMachine = new CharacterStateMachine( this );

		// プレイヤーなら
		if( this.isPlayer )
		{
			this.stateMachine.ChangeState( new PlayerState() );
		}
		else
		{
			this.stateMachine.ChangeState( new FollowState( 3, 6 ) );
		}
	}

	/// <summary>
	/// 更新
	/// </summary>
	void FixedUpdate()
	{
		// AI更新
		UpdateAI();

		// アニメーションパラメータの更新
		UpdateAnimatorParameteies();

		// 歩き更新
		UpdateWalk( Time.fixedDeltaTime );
	}

	/// <summary>
	/// ナビゲーション付きの移動
	/// </summary>
	/// <param name="pos"></param>
	public void StartWalk( Vector3 pos, float endDistance )
	{
		if( this.agent.isOnNavMesh )
		{
			this.agent.SetDestination( pos );
			this.state = State.PathWait;
			this.targetPos = pos;
			this.passDistance = endDistance;
		}
	}

	/// <summary>
	/// 行動制御
	/// </summary>
	private void UpdateAI()
	{
		this.stateMachine.Update();

	/*
		if( Input.GetMouseButton( 0 ) )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			if( Physics.Raycast( ray, out hit, 1000 ) )
			{
				StartWalk( hit.point );
			}
		}
	*/
	}

	/// <summary>
	/// 移動更新
	/// </summary>
	private void UpdateWalk( float deltaTime )
	{
		switch( this.state )
		{
		case State.Idle:
			{
			}
			break;

		case State.PathWait:
			{
				if( !this.agent.pathPending )
				{
					if(
						( this.agent.path.corners == null )
						|| ( this.agent.path.corners.Length == 0 )
						)
					{
						this.state = State.Idle;
					}
					else
					{
						this.state = State.Running;
					}
				}
			}
			break;

		case State.Running:
			{
				// 目的地についたら終了
				Vector3 nextPos = this.agent.steeringTarget;
				this.endDistance = GetDistanceXZ( this.agent.path.corners[ this.agent.path.corners.Length - 1 ], this.transform.position );
				if( this.passDistance > this.endDistance )
				{
					this.state = State.Stopping;
					break;
				}

				// 次に目指すべき位置を取得(Y軸はカットする、Y軸の角度がつくと坂で引っかかるので)

				Vector3 targetDir = nextPos - this.transform.position;
				//if( targetDir.y > 0 )
				{
					targetDir.y = 0;
				}

				// その方向に向けて旋回する(120度/秒)
				Quaternion targetRotation = Quaternion.LookRotation( targetDir );

				// 自分の向きと次の位置の角度差が30度以上の場合、その場で旋回
				float angle = Vector3.SignedAngle( targetDir, transform.forward, Vector3.up );
				if( Mathf.Abs( angle ) < 20f )
				{
					if( this.animator.GetCurrentAnimatorStateInfo( 0 ).nameHash == Animator.StringToHash( "Base Layer.Locomotion" ) )
					{
						// 以下、キャラクターの移動処理
						Vector3 velocity = new Vector3( 0, 0, 1 );
						velocity = transform.TransformDirection( velocity );
						//transform.localPosition += velocity * 4.0f * Time.deltaTime;
					}

					// もしもの場合の補正
					//if (Vector3.Distance(nextPoint, transform.position) < 0.5f)　transform.position = nextPoint;
					if( Mathf.Abs( angle ) > 5.0f )
					{
						AccelAnimatorTurn( angle < 0 ? 1 : -1, 0.2f, 0.4f );
					}
					else
					{
						transform.rotation = Quaternion.RotateTowards( transform.rotation, targetRotation, 120f * Time.deltaTime );
					}
					AccelAnimatorForward( 0.3f );
				}
				else
				{
					AccelAnimatorTurn( angle < 0 ? 1 : -1, 0.2f, 0.8f );
				}

				// 地面の高さに補正する
				Vector3 originPos = this.transform.position;
				originPos.y += 0.1f;
				Ray ray = new Ray( originPos, Vector3.down );
				RaycastHit hit = new RaycastHit();
				if( Physics.Raycast( ray, out hit, 100 ) )
				{
					Vector3 offset = new Vector3( 0, 0, 0 );
					this.transform.position = hit.point + offset;
				}

				// targetに向かって移動します。
				this.agent.SetDestination( this.targetPos );
				this.agent.nextPosition = transform.position;
			}
			break;

		case State.Stopping:
			{
				if( this.animator.GetCurrentAnimatorStateInfo( 0 ).nameHash == Animator.StringToHash( "Base Layer.Grounded" ) )
				{
					this.state = State.Idle;
				}
				else
				{
					// 以下、キャラクターの移動処理
					Vector3 velocity = new Vector3( 0, 0, 1 );
					velocity = transform.TransformDirection( velocity );
					transform.localPosition += velocity * 4.0f * this.animatorForward * deltaTime;
				}
			}
			break;
		}
	}

	/// <summary>
	/// アニメーションパラメータの更新
	/// </summary>
	private void UpdateAnimatorParameteies()
	{
		// 前進
		this.animatorForward *= 0.9f;
		if( Mathf.Abs( this.animatorForward ) < 0.01f )
		{
			this.animatorForward = 0;
		}
		this.animator.SetFloat( "Forward", this.animatorForward );

		// 左右回転
		this.animatorTurn *= 0.9f;
		if( this.animatorTurn < 0.01f )
		{
			this.animatorTurn = 0;
		}
		else
		{
			this.animatorTurn = Utility.Min<float>( this.animatorTurn, this.animatorTurnMax );
		}
		this.animator.SetFloat( "Turn", this.animatorTurn * this.animatorSide );
	}

	/// <summary>
	/// 前進
	/// </summary>
	/// <param name="value"></param>
	public void AccelAnimatorForward( float value = 0.1f )
	{
		if( ( this.animatorForward == 0 )
			|| ( ( this.animatorForward * value ) < 0 )
			)
		{
			this.animatorForward = value;
		}
		else
		{
			this.animatorForward *= 1.5f;
		}
		if( this.animatorForward > 1 )
		{
			this.animatorForward = 1;
		}
		if( this.animatorForward < -1 )
		{
			this.animatorForward = -1;
		}
		this.animator.SetFloat( "Forward", this.animatorForward );
	}

	/// <summary>
	/// 回転
	/// </summary>
	/// <param name="side"></param>
	/// <param name="value"></param>
	/// <param name="max"></param>
	public void AccelAnimatorTurn( int side, float value = 0.1f, float max = 0.25f )
	{
		this.animatorSide = side;
		this.animatorTurnMax = max;
		if( ( this.animatorTurn == 0 )
			|| ( ( this.animatorTurn * value ) < 0 )
			)
		{
			this.animatorTurn = value;
		}
		else
		{
			this.animatorTurn *= 1.5f;
		}
		this.animatorTurn = Utility.Min<float>( this.animatorTurn, this.animatorTurnMax );
		this.animator.SetFloat( "Turn", this.animatorTurn * side );
	}

	private void ClearAnimatorTurn()
	{
		this.animatorTurn = 0;
		this.animator.SetFloat( "Turn", this.animatorTurn );
	}


	/// <summary>
	/// XZでの距離を得る
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	private float GetDistanceXYZ( Vector3 a, Vector3 b )
	{
		Vector3 vec = a - b;
		return vec.magnitude;
	}


	/// <summary>
	/// XZでの距離を得る
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	private float GetDistanceXZ( Vector3 a, Vector3 b )
	{
		Vector3 vec = a - b;
		vec.y = 0;
		return vec.magnitude;
	}

	/// <summary>
	/// 次の目的値を得る
	/// </summary>
	/// <returns></returns>
	private Vector3 NextCorner( out bool isEnd )
	{
		Vector3 point = this.agent.path.corners[ this.agent.path.corners.Length - 1 ];
		if( this.passDistance >= GetDistanceXYZ( point, this.transform.position ) )
		{
			isEnd = true;
			return point;
		}

		Vector3 result = point;
		for( int i = 0 ; i < this.agent.path.corners.Length ; i++ )
		{
			if( this.passDistance < GetDistanceXYZ( this.agent.path.corners[ i ], this.transform.position ) )
			{
				result = this.agent.path.corners[ i ];
				break;
			}
		}
		isEnd = false;
		return result;
	}
	void OnCollisionEnter( Collision other )
	{
		Debug.LogFormat( "Collision {0}", other.gameObject.name );
	}
	void OnGUI()
	{
		if( this.townController == null )
		{
			return;
		}

		int y = 30;
		GUI.Label( new Rect( Screen.width - 245, y, 250, 30 ), string.Format( "Time:{0}", this.townController.time ) );
		y += 20;
		GUI.Label( new Rect( Screen.width - 245, y, 250, 30 ), string.Format( "State:{0}", this.state.ToString() ) );
		y += 20;
		GUI.Label( new Rect( Screen.width - 245, y, 250, 30 ), string.Format( "Forward:{0}", this.animatorForward ) );
		y += 20;
		GUI.Label( new Rect( Screen.width - 245, y, 250, 30 ), string.Format( "Turn:{0}", this.animatorTurn * this.animatorSide ) );
		y += 20;
		GUI.Label( new Rect( Screen.width - 245, y, 250, 30 ), string.Format( "End Distance:{0}", this.endDistance ) );
		y += 20;
		GUI.Label( new Rect( Screen.width - 245, y, 250, 30 ), string.Format( "Anim:{0}", this.animator.GetCurrentAnimatorStateInfo( 0 ).ToString() ) );
	}

	private void OnAnimatorIK( int layerIndex )
	{
		this.animator.SetLookAtWeight( 1.0f, 0.2f, 0.4f, 1.0f, 0.3f );

		if( this.TargetTransform != null )
		{
			this.animator.SetLookAtPosition( this.TargetTransform.position );
		}
		else
		{
			this.animator.SetLookAtPosition( ( this.state == State.Running ) ? this.targetPos : Camera.main.transform.position );
		}
	}

	private void OnDrawGizmos()
	{
		if( this.agent == null )
		{
			return;
		}

		// デバッグ描画
		if( !this.agent.pathPending )
		{
			if( this.agent.path != null )
			{
				for( int i = 0 ; i < this.agent.path.corners.Length ; i++ )
				{
					Vector3 point = this.agent.path.corners[ i ];
					Gizmos.DrawWireSphere( point, 0.1f );
					if( i > 0 )
					{
						Gizmos.DrawLine( this.agent.path.corners[ i - 1 ], point );
					}
				}
				Gizmos.DrawSphere( this.agent.steeringTarget, 0.1f );
			}
		}
	}
}

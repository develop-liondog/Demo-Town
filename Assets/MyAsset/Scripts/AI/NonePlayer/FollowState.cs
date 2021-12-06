using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.NonePlayer
{
	/// <summary>
	/// ターゲットについて歩く
	/// </summary>
	public class FollowState : CharacterState
	{
		/// <summary>
		/// ついて歩く最小距離(この距離を下回ると追いかけるのを辞める) 
		/// </summary>
		public float FollowMinDistance
		{
			get;
			set;
		}

		/// <summary>
		/// ついて歩く最大距離(この距離を超えると追いかける)
		/// </summary>
		public float FollowMaxDistance
		{
			get;
			set;
		}

		public FollowState( float minDistance, float maxDistance )
		{
			this.FollowMinDistance = minDistance;
			this.FollowMaxDistance = maxDistance;
		}

		public override void Update( CharacterStateContext context )
		{
			if( context.Owner.CurrentState == CharacterController.State.Idle )
			{
				if( context.Owner.TargetTransform != null )
				{
					float dist = Utility.GetDistanceXZ( context.Owner.TargetTransform.position, context.Owner.transform.position );
					if( dist > this.FollowMaxDistance )
					{
						context.Owner.StartWalk( context.Owner.TargetTransform.position, this.FollowMinDistance );
					}
				}
			}
		}
	}
}

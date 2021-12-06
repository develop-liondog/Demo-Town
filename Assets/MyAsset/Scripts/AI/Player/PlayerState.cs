using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.AI.Player
{
	/// <summary>
	/// プレイヤーステート
	/// </summary>
	public class PlayerState : CharacterState
	{
		public override void Update( CharacterStateContext context )
		{
			if( Input.GetMouseButton( 0 ) )
			{
				Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hit = new RaycastHit();
				if( Physics.Raycast( ray, out hit, 1000 ) )
				{
					context.Owner.StartWalk( hit.point, 1 );
				}
			}

			if( Input.GetKey( KeyCode.W ) )
			{
				context.Owner.AccelAnimatorForward( 0.1f );
			}
			if( Input.GetKey( KeyCode.A ) )
			{
				context.Owner.AccelAnimatorTurn( -1, 0.3f, 1.0f );
			}
			else if( Input.GetKey( KeyCode.D ) )
			{
				context.Owner.AccelAnimatorTurn( 1, 0.3f, 1.0f );
			}
		}

	}
}

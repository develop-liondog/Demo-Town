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
		private bool isKeyDownSpace = false;

		public override void FixedUpdate( CharacterStateContext context )
		{
			if( Input.GetMouseButton( 0 ) )
			{
				Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hit = new RaycastHit();
				if( Physics.Raycast( ray, out hit, 1000 ) )
				{
					context.Owner.StartNavigationWalk( hit.point, 1 );
				}
			}

			// ジャンプ
			if( this.isKeyDownSpace )
			{
				context.Owner.Jump();
			}

			// 十字キー入力による移動
			context.Owner.UpdateCrossKeyWalk(
				Input.GetKey( KeyCode.W ),
				Input.GetKey( KeyCode.D ),
				Input.GetKey( KeyCode.S ),
				Input.GetKey( KeyCode.A )
			);

			this.isKeyDownSpace = false;
		}

		public override void Update( CharacterStateContext context )
		{
			if( Input.GetKeyDown( KeyCode.Space ) )
			{
				this.isKeyDownSpace = true;
			}
		}
	}
}

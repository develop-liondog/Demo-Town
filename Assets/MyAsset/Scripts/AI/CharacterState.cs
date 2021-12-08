using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
	/// <summary>
	/// キャラクター用のステート
	/// </summary>
	public class CharacterState : State<CharacterStateContext>
	{
		public virtual void Enter( CharacterStateContext context )
		{
		}

		public virtual void Update( CharacterStateContext context )
		{
		}

		public virtual void FixedUpdate( CharacterStateContext context )
		{
		}

		public virtual void Leave( CharacterStateContext context )
		{
		}
	}
}

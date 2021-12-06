using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
	public class CharacterStateMachine : StateMachine<CharacterStateContext>
	{
		public CharacterStateMachine( CharacterController characterController )
			: base( new CharacterStateContext( characterController ) )
		{
		}
	}
}

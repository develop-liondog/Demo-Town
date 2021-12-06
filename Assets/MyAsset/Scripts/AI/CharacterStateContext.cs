using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
	/// <summary>
	/// キャラクター用のステートマシン
	/// </summary>
	public class CharacterStateContext
	{
		/// <summary>
		/// ステートマシンのオーナー
		/// </summary>
		public CharacterController Owner
		{
			get;
			set;
		}

		public CharacterStateContext( CharacterController characterController )
		{
			this.Owner = characterController;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
	/// <summary>
	/// 簡易ステートマシン
	/// </summary>
	public class StateMachine<T> where T : class
	{
		/// <summary>
		/// コンテキスト
		/// </summary>
		public T Context
		{
			get;
		}

		/// <summary>
		/// 現在のステート
		/// </summary>
		public State<T> CurrentState
		{
			get;
			private set;
		}

		/// <summary>
		/// 現在のステート
		/// </summary>
		public State<T> PreviousState
		{
			get;
			private set;
		}


		/// <summary>
		/// コンストラクタ
		/// </summary>
		public StateMachine( T context )
		{
			this.Context = context;
			this.CurrentState = new NullState<T>();
			this.PreviousState = new NullState<T>();
		}

		/// <summary>
		/// ステート切り替え
		/// </summary>
		/// <param name="state"></param>
		public void ChangeState( State<T> state )
		{
			this.PreviousState = this.CurrentState;
			this.CurrentState.Leave( this.Context );
			this.CurrentState = state;
			this.CurrentState.Enter( this.Context );
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void Update()
		{
			this.CurrentState.Update( this.Context );
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void FixedUpdate()
		{
			this.CurrentState.FixedUpdate( this.Context );
		}
	}
}

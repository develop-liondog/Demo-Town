using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
	/// <summary>
	/// 状態
	/// </summary>
	public interface State<T> where T : class
	{
		/// <summary>
		/// 遷移時
		/// </summary>
		/// <param name="obj"></param>
		public void Enter( T obj );

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="obj"></param>
		public void Update( T obj );

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="obj"></param>
		public void FixedUpdate( T obj );

		/// <summary>
		/// 退出時
		/// </summary>
		/// <param name="obj"></param>
		public void Leave( T obj );
	}
}

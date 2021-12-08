using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{

	/// <summary>
	/// 何もしない
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class NullState<T> : State<T> where T : class
	{
		public void Enter( T obj )
		{
		}

		public void Update( T obj )
		{
		}

		public void FixedUpdate( T obj )
		{
		}

		public void Leave( T obj )
		{
		}
	}
}

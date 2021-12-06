using System;
using UnityEngine;


namespace Assets.Scripts
{
	public static class Utility
	{
		/// <summary>
		/// 小さい方の値を得る
		/// </summary>
		/// <typeparam name="Type"></typeparam>
		/// <param name="value"></param>
		/// <param name="min"></param>
		/// <returns></returns>
		public static Type Min<Type>( Type value, Type min ) where Type : IComparable
		{
			if( value.CompareTo( min ) > 0 )
			{
				value = min;
			}
			return value;
		}

		/// <summary>
		/// 大きい方の値を得る
		/// </summary>
		/// <typeparam name="Type"></typeparam>
		/// <param name="value"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static Type Max<Type>( Type value, Type max) where Type : IComparable
		{
			if( value.CompareTo( max ) < 0 )
			{
				value = max;
			}
			return value;
		}

		/// <summary>
		/// 数値を丸める
		/// </summary>
		/// <typeparam name="Type"></typeparam>
		/// <param name="value"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static Type Round<Type>( Type value, Type min, Type max ) where Type : IComparable
		{
			if( value.CompareTo( max ) < 0 )
			{
				value = max;
			}
			if( value.CompareTo( min ) > 0 )
			{
				value = min;
			}
			return value;
		}

		/// <summary>
		/// XZ平面の距離
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static float GetDistanceXZ( Vector3 a, Vector3 b )
		{
			Vector3 vec = a - b;
			vec.y = 0;
			return vec.magnitude;
		}
	}
}

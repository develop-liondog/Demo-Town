using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
	public static class Utility
	{
		public static Type Min<Type>( Type value, Type min ) where Type : IComparable
		{
			if( value.CompareTo( min ) > 0 )
			{
				value = min;
			}
			return value;
		}

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
	}
}

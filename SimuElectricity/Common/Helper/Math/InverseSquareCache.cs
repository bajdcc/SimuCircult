using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuElectricity.Common.Helper
{
	/// <summary>
	/// 平方倒数快速缓存
	/// 1/((a^2+b^2)^(3/2))
	/// </summary>
	public class InverseSquareCache
	{
		static ConcurrentDictionary<string, double> cache = new ConcurrentDictionary<string, double>();

		public static double Calculate(int a, int b)
		{
			string s;
			if (a <= b)
			{
				s = a.ToString() + "#" + b.ToString();
			}
			else
			{
				s = b.ToString() + "#" + a.ToString();
			}
			double val;
			if (cache.TryGetValue(s, out val))
			{
				return val;
			}
			var k = 0.0 + a * a + b * b;
			var d = 1.0 / k / Math.Sqrt(k);
			cache.TryAdd(s, d);
			return d;
		}
	}
}

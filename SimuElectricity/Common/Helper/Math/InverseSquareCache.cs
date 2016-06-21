using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimuElectricity.Common.Simulator;

namespace SimuElectricity.Common.Helper
{
	/// <summary>
	/// 平方倒数快速缓存
	/// 做了静态优化
	/// 1/((a^2+b^2)^(3/2))
	/// </summary>
	public class InverseSquareCache
	{
		static Dictionary<int, double> cache = new Dictionary<int, double>();

	    public static void Init(int width, int height)
	    {
	        for (int i = 0; i < width; i++)
	        {
	            for (int j = 0; j < height; j++)
	            {
	                CalculateInternal(i, j);
	            }
	        }
        }

	    private static void CalculateInternal(int a, int b)
	    {
	        int n = (a << 16) + b;
            var k = 0.0 + a * a + b * b;
            var d = 1.0 / k / Math.Sqrt(k);
            cache.Add(n, d);
        }

        public static double Calculate(int a, int b)
        {
            return cache[(a << 16) + b];
		}
	}
}

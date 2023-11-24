using AhDai.LeetCode.Problems;
using System;

namespace AhDai.LeetCode
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");

			var array = new int[8] { 2, 6, 8, 4, 3, 7, 5, 1 };
			for (int i = 0; i < array.Length - 1; i++)
			{
				for (int j = 1; j < array.Length - i; j++)
				{
					if (array[j] < array[j - 1])
					{
						var temp = array[j - 1];
						array[j - 1] = array[j];
						array[j] = temp;
					}
				}
			}
			

			var problem = new Problem();
			problem.Run();
		}

	}
}
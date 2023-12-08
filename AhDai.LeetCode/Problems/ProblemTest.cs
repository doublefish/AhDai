using AhDai.LeetCode.Problems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.LeetCode.Problems
{
	/// <summary>
	/// ProblemTest
	/// </summary>
	public class ProblemTest
	{
		Problem Problem { get; set; }

		public ProblemTest()
		{
			Problem = new Problem();
		}

		public void RunBubbleSort()
		{
			var array = new int[] { 2, 6, 8, 4, 3, 7, 5, 1 };
			Problem.BubbleSort(array);
			Console.WriteLine($"BubbleSort=>{string.Join(",", array)}");
		}


		public void RunAddTwoNumbers()
		{
			/**
			 * Input: l1 = [2,4,3], l2 = [5,6,4]
			 * Output: [7,0,8]
			 * Explanation: 342 + 465 = 807.
			 */
			var l1 = new ListNode(2, new ListNode(4, new ListNode(3)));
			var l2 = new ListNode(5, new ListNode(6, new ListNode(4)));
			var l3 = Problem.AddTwoNumbers(l1, l2);
			var builder = new StringBuilder();
			var n3 = l3;
			do
			{
				builder.Append(n3.val);
				n3 = n3.next;
			}
			while (n3 != null);
			var res = builder.ToString();
			Console.WriteLine($"AddTwoNumbers=>{res}");
		}


		public void RunIsPalindrome()
		{
			Console.WriteLine($"RunIsPalindrome=>121 -> {Problem.IsPalindrome(121)}");
			Console.WriteLine($"RunIsPalindrome=>121 -> {Problem.IsPalindrome(-121)}");
			Console.WriteLine($"RunIsPalindrome=>121 -> {Problem.IsPalindrome(10)}");
		}

		public void RunLargestOddNumber()
		{
			Console.WriteLine($"LargestOddNumber=>52 -> {Problem.LargestOddNumber("52")}");
			Console.WriteLine($"LargestOddNumber=>4206 -> {Problem.LargestOddNumber("4206")}");
			Console.WriteLine($"LargestOddNumber=>35427 -> {Problem.LargestOddNumber("35427")}");
		}

		public void RunFindMedianSortedArrays()
		{
			Console.WriteLine($"FindMedianSortedArrays=>52 -> {Problem.FindMedianSortedArrays(new int[] { 1, 3 }, new int[] { 2 })}");
			Console.WriteLine($"FindMedianSortedArrays=>4206 -> {Problem.FindMedianSortedArrays(new int[] { 1, 2 }, new int[] { 3, 4 })}");
		}

		public void Run()
		{
			RunBubbleSort();
			RunAddTwoNumbers();
			RunIsPalindrome();
			RunLargestOddNumber();
			RunFindMedianSortedArrays();
		}

	}
}

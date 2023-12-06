using AhDai.LeetCode.Problems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AhDai.LeetCode.Problems
{
	/// <summary>
	/// Problem
	/// </summary>
	public class Problem : IProblem
	{
		/// <summary>
		/// 冒泡排序
		/// </summary>
		/// <param name="input"></param>
		public void BubbleSort(int[] input)
		{
			for (var i = 0; i < input.Length - 1; i++)
			{
				for (var j = 1; j < input.Length - i; j++)
				{
					if (input[j] < input[j - 1])
					{
						var temp = input[j - 1];
						input[j - 1] = input[j];
						input[j] = temp;
					}
				}
			}
		}

		public void RunBubbleSort()
		{
			var array = new int[] { 2, 6, 8, 4, 3, 7, 5, 1 };
			BubbleSort(array);
			Console.WriteLine($"BubbleSort=>{string.Join(",", array)}");
		}


		/// <summary>
		/// AddTwoNumbers
		/// </summary>
		/// <param name="l1"></param>
		/// <param name="l2"></param>
		/// <returns></returns>
		public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
		{
			var l3 = new ListNode(0);
			var n1 = l1;
			var n2 = l2;
			var n3 = l3;
			while (true)
			{
				if (n1 != null)
				{
					n3.val += n1.val;
					if (n3.val > 9)
					{
						n3.val %= 10;
						n3.next = new ListNode(1);
					}
					n1 = n1.next;
				}
				if (n2 != null)
				{
					n3.val += n2.val;
					if (n3.val > 9)
					{
						n3.val %= 10;
						n3.next = new ListNode(1);
					}
					n2 = n2.next;
				}
				if (n1 == null && n2 == null)
				{
					break;
				}
				n3.next ??= new ListNode(0);
				n3 = n3.next;
			}
			return l3;
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
			var l3 = AddTwoNumbers(l1, l2);
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


		public bool IsPalindrome(int x)
		{
			if (x < 0) return false;
			else if (x < 10) return true;
			else
			{
				var str0 = x.ToString();
				var str1 = new string(str0.ToCharArray().Reverse().ToArray());
				return string.Compare(str0, str1) == 0;
			}
		}

		public void RunIsPalindrome()
		{
			Console.WriteLine($"RunIsPalindrome=>121 -> {IsPalindrome(121)}");
			Console.WriteLine($"RunIsPalindrome=>121 -> {IsPalindrome(-121)}");
			Console.WriteLine($"RunIsPalindrome=>121 -> {IsPalindrome(10)}");
		}


		public void Run()
		{
			RunBubbleSort();
			RunAddTwoNumbers();
			RunIsPalindrome();
		}

	}
}

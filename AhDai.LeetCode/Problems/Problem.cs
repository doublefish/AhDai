using AhDai.LeetCode.Problems.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.LeetCode.Problems
{
	/// <summary>
	/// Problem
	/// </summary>
	public class Problem
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

		/// <summary>
		/// 是否回文数
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 最大奇数
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public string LargestOddNumber(string num)
		{
			var cs = num.ToCharArray();
			for (var i = cs.Length - 1; i >= 0; i--)
			{
				if (Convert.ToInt32(cs[i]) % 2 != 0)
				{
					return num[..(i + 1)];
				}
			}
			return "";
		}

		/// <summary>
		/// 找出中位数
		/// </summary>
		/// <param name="nums1"></param>
		/// <param name="nums2"></param>
		/// <returns></returns>
		public double FindMedianSortedArrays(int[] nums1, int[] nums2)
		{
			var nums = new int[nums1.Length + nums2.Length];
			var j = 0;
			var k = 0;
			for (var i = 0; i < nums.Length; i++)
			{
				if (j == nums1.Length)
				{
					nums[i] = nums2[k++];
				}
				else if (k == nums2.Length)
				{
					nums[i] = nums1[j++];
				}
				else
				{
					var num1 = nums1[j];
					var num2 = nums2[k];
					if (num1 <= num2)
					{
						nums[i] = num1;
						j++;
					}
					else
					{
						nums[i] = num2;
						k++;
					}
				}
			}
			var medianIndex = nums.Length / 2;
			if (nums.Length % 2 == 0)
			{
				return (nums[medianIndex - 1] + nums[medianIndex]) / 2D;
			}
			else
			{
				return nums[medianIndex];
			}
		}

	}
}

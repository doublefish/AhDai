using AhDai.Base.Utils;
using System;
using System.Linq.Expressions;

namespace AhDai.Core
{
	/// <summary>
	/// Accessor
	/// </summary>
	/// <typeparam name="S"></typeparam>
	public class Accessor<S>
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public Accessor()
		{
		}

		/// <summary>
		/// Create
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="memberSelector"></param>
		/// <returns></returns>
		public static Accessor<S, T> Create<T>(Expression<Func<S, T>> memberSelector)
		{
			return new GetterSetter<T>(memberSelector);
		}

		/// <summary>
		/// Get
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="memberSelector"></param>
		/// <returns></returns>
		public Accessor<S, T> Get<T>(Expression<Func<S, T>> memberSelector)
		{
			return Create(memberSelector);
		}

		/// <summary>
		/// GetterSetter
		/// </summary>
		/// <typeparam name="T"></typeparam>
		class GetterSetter<T> : Accessor<S, T>
		{
			/// <summary>
			/// GetterSetter
			/// </summary>
			/// <param name="memberSelector"></param>
			public GetterSetter(Expression<Func<S, T>> memberSelector) : base(memberSelector)
			{

			}
		}
	}

	/// <summary>
	/// Accessor
	/// </summary>
	/// <typeparam name="S"></typeparam>
	/// <typeparam name="T"></typeparam>
	public class Accessor<S, T> : Accessor<S>
	{
		readonly Func<S, T> getter;
		readonly Action<S, T> setter;

		/// <summary>
		/// Getter
		/// </summary>
		public Func<S, T> Getter
		{
			get
			{
				if (getter == null)
				{
					throw new ArgumentException("Property get method not found.");
				}
				return getter;
			}
		}
		/// <summary>
		/// Setter
		/// </summary>
		public Action<S, T> Setter
		{
			get
			{
				if (setter == null)
				{
					throw new ArgumentException("Property set method not found.");
				}
				return setter;
			}
		}
		/// <summary>
		/// IsReadable
		/// </summary>
		public bool CanRead { get; private set; }
		/// <summary>
		/// IsWritable
		/// </summary>
		public bool CanWrite { get; private set; }
		/// <summary>
		/// T
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public T this[S instance]
		{
			get
			{
				if (!CanRead)
				{
					throw new ArgumentException("Property get method not found.");
				}
				return Getter(instance);
			}
			set
			{
				if (!CanWrite)
				{
					throw new ArgumentException("Property set method not found.");
				}
				Setter(instance, value);
			}
		}

		/// <summary>
		/// Accessor
		/// access not given to outside world
		/// </summary>
		/// <param name="memberSelector"></param>
		protected Accessor(Expression<Func<S, T>> memberSelector)
		{
			var prop = memberSelector.GetPropertyInfo();
			CanRead = prop.CanRead;
			CanWrite = prop.CanWrite;
			if (CanRead)
			{
				getter = prop.GetGetMethod().CreateDelegate<Func<S, T>>();
			}
			if (CanWrite)
			{
				setter = prop.GetSetMethod().CreateDelegate<Action<S, T>>();
			}
		}
	}
}

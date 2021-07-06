using System;
using System.Linq.Expressions;

namespace Adai.Standard
{
	/// <summary>
	/// Accessor<S>
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
	/// Accessor<S, T>
	/// </summary>
	/// <typeparam name="S"></typeparam>
	/// <typeparam name="T"></typeparam>
	public class Accessor<S, T> : Accessor<S>
	{
		readonly Func<S, T> _Getter;
		readonly Action<S, T> _Setter;

		/// <summary>
		/// Getter
		/// </summary>
		public Func<S, T> Getter
		{
			get
			{
				if (_Getter == null)
				{
					throw new ArgumentException("Property get method not found.");
				}
				return _Getter;
			}
		}
		/// <summary>
		/// Setter
		/// </summary>
		public Action<S, T> Setter
		{
			get
			{
				if (_Setter == null)
				{
					throw new ArgumentException("Property set method not found.");
				}
				return _Setter;
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
				_Getter = prop.GetGetMethod().CreateDelegate<Func<S, T>>();
			}
			if (CanWrite)
			{
				_Setter = prop.GetSetMethod().CreateDelegate<Action<S, T>>();
			}
		}
	}
}

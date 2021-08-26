using System.Resources;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// ResourceHelper
	/// </summary>
	public static class ResourceHelper
	{
		/// <summary>
		/// _SharedLocalizer
		/// </summary>
		private static ResourceManager _SharedLocalizer;

		/// <summary>
		/// 共享本地语言
		/// </summary>
		public static ResourceManager SharedLocalizer
		{
			get
			{
				if (_SharedLocalizer == null)
				{
					//var type = typeof(Language.Shared);
					var type = typeof(ResourceHelper);
					_SharedLocalizer = new ResourceManager(type.FullName, type.Assembly);
				}
				return _SharedLocalizer;
			}
		}
	}
}

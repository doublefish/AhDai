using System.Resources;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// ResourceHelper
	/// </summary>
	public static class ResourceHelper
	{
		static ResourceManager sharedLocalizer;

		/// <summary>
		/// 共享本地语言
		/// </summary>
		public static ResourceManager SharedLocalizer
		{
			get
			{
				if (sharedLocalizer == null)
				{
					//var type = typeof(Language.Shared);
					var type = typeof(ResourceHelper);
					sharedLocalizer = new ResourceManager(type.FullName, type.Assembly);
				}
				return sharedLocalizer;
			}
		}
	}
}

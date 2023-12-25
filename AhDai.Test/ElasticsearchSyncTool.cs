using Microsoft.Extensions.Logging;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Test
{
	internal class ElasticsearchSyncTool
	{
		readonly static ILogger Logger = Core.Utils.LoggerUtil.GetLogger<ElasticsearchSyncTool>();

		public static void Start()
		{
			var eventId = new EventId(1);
			try
			{
				var index = "sup8-bugsell-131631012886";
				var indices = Indices.Parse(index);

				var sourceClient = CreateClient("http://elastic.ccn.local:9200/", "", "", index);
				var targetClient = CreateClient("http://10.20.31.190:9200/", "elastic", "3uHfCUE7ZvP1aGGviH8s", index);

				var request = new SearchRequest<Models.Bugsell>(indices);
				var idRange = new LongRangeQuery()
				{
					Field = "id"
				};

				var isExists = targetClient.Indices.Exists(indices).Exists;
				if (!isExists)
				{
					var createIndexDescriptor = new CreateIndexDescriptor(index)
						.Map<Models.Bugsell>(m => m.AutoMap())
						.Settings(s => s.NumberOfShards(5));
					var createIndexResponse = targetClient.Indices.Create(createIndexDescriptor);
					request.Size = 1;
					request.Sort = new List<ISort>() {
						new FieldSort(){ Field = new Field("id"), Order= SortOrder.Descending }
					};
					var response = targetClient.Search<Models.Bugsell>(request);
					if (response.Documents.Count > 0)
					{
						var list = response.Documents.ToList();
						idRange.GreaterThan = list.First().Id;
					}
				}

				request.Query = idRange;
				request.Size = 1000;
				request.Sort = new List<ISort>() {
					new FieldSort(){ Field = new Field("id"), Order= SortOrder.Ascending }
				};

				while (true)
				{
					var time = DateTime.Now;
					var response = sourceClient.Search<Models.Bugsell>(request);
					Logger.LogInformation(eventId, $"查询数据：{response.Documents.Count}条，耗时：{DateTime.Now.Subtract(time).TotalMilliseconds}ms");
					if (response.Documents.Count == 0)
					{
						break;
					}
					var list = response.Documents.ToList();
					idRange.GreaterThan = list.Last().Id;

					time = DateTime.Now;
					var indexManyResponse = targetClient.IndexMany(list);
					if (!indexManyResponse.IsValid)
					{
						Console.WriteLine($"Failed to index document");
						break;
					}
					if (indexManyResponse.Errors)
					{
						foreach (var itemWithError in indexManyResponse.ItemsWithErrors)
						{
							Console.WriteLine($"Failed to index document {itemWithError.Id}: {itemWithError.Error}");
						}
					}
					Logger.LogInformation(eventId, $"写入耗时：{DateTime.Now.Subtract(time).TotalMilliseconds}ms");
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

		}



		public static ElasticClient CreateClient(string host, string username, string password, string defaultIndex = "")
		{
			var settings = new ConnectionSettings(new Uri(host)).DefaultIndex(defaultIndex);
			settings.BasicAuthentication(username, password);
			return new ElasticClient(settings);
		}

	}
}

using AhDai.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Test
{
    /// <summary>
    /// Worker
    /// </summary>
    public class Worker : BackgroundService
    {
        readonly ILogger<Worker> _logger;
        readonly IConfiguration _configuration;
        readonly IBaseRedisService _redisService;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IServiceProvider serviceProvider)//, IBaseRedisService redisService)
        {
            _logger = logger;
            _configuration = configuration;
            //_redisService = redisService;
            _redisService = serviceProvider.GetRequiredService<IBaseRedisService>();
            var dict = Core.Utils.ConfigurationUtil.GetAll(configuration);
            foreach (var kv in dict)
            {
                Console.WriteLine($"{kv.Key}={kv.Value}");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("开始执行：{time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    //ComplexPing();
                    //TestKafka();
                    //TestDb();
                    await TestRedisAsync();
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, "发生异常=>{Message}", ex.Message);
                    _logger.LogError("发生异常=>{Message}", ex.Message);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }

        protected virtual async Task TestRedisAsync()
        {
            var rdb = _redisService.GetDatabase();
            await rdb.HashSetAsync("test", "time", DateTime.Now.ToString());
            var res = await rdb.HashGetAsync("test", "time");
            _logger.LogInformation("读取=>{res}", res);
        }

        protected virtual void TestEs()
        {
            var esConfig = _configuration.GetSection("Elasticsearch");
            ElasticsearchSyncTool.Start();
        }

        protected virtual void TestKafka()
        {

        }

        protected virtual void TestRabbitMQ()
        {
            // 设置连接参数
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "guest", Password = "guest" };

            // 建立连接和通道
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // 声明 Exchange
                channel.ExchangeDeclare(exchange: "myexchange", ExchangeType.Direct);

                // 声明队列
                channel.QueueDeclare(queue: "myqueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                // 绑定 Exchange 和 Queue
                channel.QueueBind(queue: "myqueue", exchange: "myexchange", routingKey: "myroutingkey");

                // 发送消息
                for (int i = 0; i < 5; i++)
                {
                    string message = i.ToString();
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "myexchange", routingKey: "myroutingkey", basicProperties: null, body: body);
                }

                // 接收消息
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"第{message}次开始{DateTime.Now}");

                    Thread.Sleep(3000);

                    Console.WriteLine($"第{message}次结束{DateTime.Now}");

                };
                channel.BasicConsume(queue: "myqueue", autoAck: true, consumer: consumer);

                Console.ReadLine();
                //日志输出
                //第0次开始2023 / 6 / 28 11:29:46
                //第0次结束2023 / 6 / 28 11:29:49
                //第1次开始2023 / 6 / 28 11:29:49
                //第1次结束2023 / 6 / 28 11:29:52
                //第2次开始2023 / 6 / 28 11:29:52
                //第2次结束2023 / 6 / 28 11:29:55
                //第3次开始2023 / 6 / 28 11:29:55
                //第3次结束2023 / 6 / 28 11:29:58
                //第4次开始2023 / 6 / 28 11:29:58
                //第4次结束2023 / 6 / 28 11:30:01
            }
        }


        protected virtual void ComplexPing()
        {
            var pingSender = new Ping();
            var data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var buffer = Encoding.ASCII.GetBytes(data);

            var options = new PingOptions(64, true);

            var reply = pingSender.Send("www.contoso.com", 5000, buffer, options);

            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine($"来自 {reply.Address} 的回复: 字节={reply.Buffer.Length} 时间={reply.RoundtripTime}ms TTL={reply.Options?.Ttl}");

                //Console.WriteLine("Address: {0}", reply.Address.ToString());
                //Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                //Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                //Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                //Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
            }
            else
            {
                Console.WriteLine(reply.Status);
            }
        }
    }
}
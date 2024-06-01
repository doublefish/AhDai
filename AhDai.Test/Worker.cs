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
            _logger.LogInformation("��ʼִ�У�{time}", DateTimeOffset.Now);

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
                    //_logger.LogError(ex, "�����쳣=>{Message}", ex.Message);
                    _logger.LogError("�����쳣=>{Message}", ex.Message);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }

        protected virtual async Task TestRedisAsync()
        {
            var rdb = _redisService.GetDatabase();
            await rdb.HashSetAsync("test", "time", DateTime.Now.ToString());
            var res = await rdb.HashGetAsync("test", "time");
            _logger.LogInformation("��ȡ=>{res}", res);
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
            // �������Ӳ���
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "guest", Password = "guest" };

            // �������Ӻ�ͨ��
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // ���� Exchange
                channel.ExchangeDeclare(exchange: "myexchange", ExchangeType.Direct);

                // ��������
                channel.QueueDeclare(queue: "myqueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                // �� Exchange �� Queue
                channel.QueueBind(queue: "myqueue", exchange: "myexchange", routingKey: "myroutingkey");

                // ������Ϣ
                for (int i = 0; i < 5; i++)
                {
                    string message = i.ToString();
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "myexchange", routingKey: "myroutingkey", basicProperties: null, body: body);
                }

                // ������Ϣ
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"��{message}�ο�ʼ{DateTime.Now}");

                    Thread.Sleep(3000);

                    Console.WriteLine($"��{message}�ν���{DateTime.Now}");

                };
                channel.BasicConsume(queue: "myqueue", autoAck: true, consumer: consumer);

                Console.ReadLine();
                //��־���
                //��0�ο�ʼ2023 / 6 / 28 11:29:46
                //��0�ν���2023 / 6 / 28 11:29:49
                //��1�ο�ʼ2023 / 6 / 28 11:29:49
                //��1�ν���2023 / 6 / 28 11:29:52
                //��2�ο�ʼ2023 / 6 / 28 11:29:52
                //��2�ν���2023 / 6 / 28 11:29:55
                //��3�ο�ʼ2023 / 6 / 28 11:29:55
                //��3�ν���2023 / 6 / 28 11:29:58
                //��4�ο�ʼ2023 / 6 / 28 11:29:58
                //��4�ν���2023 / 6 / 28 11:30:01
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
                Console.WriteLine($"���� {reply.Address} �Ļظ�: �ֽ�={reply.Buffer.Length} ʱ��={reply.RoundtripTime}ms TTL={reply.Options?.Ttl}");

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
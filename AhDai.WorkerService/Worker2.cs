using AhDai.Core.Configs;
using AhDai.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.WorkerService;

public class Worker2(IConfiguration configuration, ILogger<Worker2> logger, IHttpClientFactory httpClientFactory) : BackgroundService
{
    private readonly ILogger<Worker2> _logger = logger;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly MailConfig _mailConfig = configuration.GetMailConfig();
    readonly string[] _apiUrls = ["https://ipinfo.io/ip", "https://ifconfig.me/ip"];
    string _ipAddress = "";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            try
            {
                var ip = "";
                foreach (var url in _apiUrls)
                {
                    try
                    {
                        ip = await GetExternalIpAddressAsync(url);
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }
                if (ip != _ipAddress)
                {
                    _ipAddress = ip;
                    SendEmail(_ipAddress);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "�����쳣=>{time}", ex.Message);
            }
            await Task.Delay(1000 * 3, stoppingToken);
        }
    }

    async Task<string> GetExternalIpAddressAsync(string apiUrl)
    {
        using var client = _httpClientFactory.CreateClient();
        client.Timeout = TimeSpan.FromSeconds(5);
        var response = await client.GetStringAsync(apiUrl);
        return response.Trim();
    }

    void SendEmail(string ipAddress)
    {
        var to = "doublefish1989@live.com";
        var subject = "��������IP��ַ";
        var body = $"����������IP��ַ��: {ipAddress}";
        var mail = new MailMessage(_mailConfig.SmtpUsername, to, subject, body);

        var smtpClient = new SmtpClient(_mailConfig.SmtpHost, _mailConfig.SmtpPort)
        {
            Credentials = new NetworkCredential(_mailConfig.SmtpUsername, _mailConfig.SmtpPassword),
            EnableSsl = true
        };
        smtpClient.Send(mail);
    }
}

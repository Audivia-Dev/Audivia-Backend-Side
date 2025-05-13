using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Payment;
using Audivia.Domain.ModelRequests.PaymentTransaction;
using Audivia.Domain.ModelResponses.Payment;
using Audivia.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static MongoDB.Driver.WriteConcern;

namespace Audivia.Application.Services.Implemetation
{
    public class PayOSService : IPayOSService
    {
        private readonly string _webhookUrl = "https://audivia-backend.azurewebsites.net/webhook-url"; 
        private readonly PayOSOptions _options;

        public PayOSService(IOptions<PayOSOptions> options)
        {
            _options = options.Value;
        }

        public string ClientId => _options.ClientId;
        public string ApiKey => _options.ApiKey;
        public string ChecksumKey => _options.ChecksumKey;


        public async Task<object> CreateVietQR(CreatePaymentTransactionRequest transaction, string cancelUrl, string returnUrl)
        {
            var orderCode = transaction.OrderCode;
            var amount = transaction.Amount;
            var desc = transaction.Description;
            //var cancelUrl = requestPayOS.CancelUrl;
            //var returnUrl = requestPayOS.ReturnUrl;
            var raw = $"amount={amount}&cancelUrl={cancelUrl}&description={desc}&orderCode={orderCode}&returnUrl={returnUrl}";
            var sig = GenerateSignature(raw);
            var body = new
            {
                orderCode,
                amount,
                description = desc,
                returnUrl,
                cancelUrl,
                signature = sig,
            };
            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-client-id", ClientId);
            client.DefaultRequestHeaders.Add("x-api-key", ApiKey);

            var res = await client.PostAsync("https://api-merchant.payos.vn/v2/payment-requests", content);
            res.EnsureSuccessStatusCode();

            var responseJson = await res.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(responseJson);

            return doc.RootElement.GetProperty("data");
        }


        public async Task ConfirmWebhookAsync()
        {
            var payload = new
            {
                webhookUrl = _webhookUrl,
            };
            var json = JsonSerializer.Serialize(payload);
            Console.WriteLine(json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-client-id", ClientId);
            client.DefaultRequestHeaders.Add("x-api-key", ApiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var res = await client.PostAsync("https://api-merchant.payos.vn/confirm-webhook", content);
            var responseBody = await res.Content.ReadAsStringAsync();

            Console.WriteLine($"PayOS response: {responseBody}");
            if (!res.IsSuccessStatusCode)
            {
                Console.WriteLine($"Status code: {res.StatusCode}");
                throw new HttpRequestException($"Cannot register Webhook: {responseBody}");
            }
            res.EnsureSuccessStatusCode();
        }

        public  bool VerifyBankWebhook(JsonElement payload)
        {
            var data = payload.GetProperty("data");

            // Tạo dictionary chứa tất cả các key-value trong "data"
            var dict = new SortedDictionary<string, string>();
            foreach (var prop in data.EnumerateObject())
            {
                dict[prop.Name] = prop.Value.GetRawText().Trim('"');
            }

            // Build raw data string: key1=value1&key2=value2&...
            var rawBuilder = new StringBuilder();
            foreach (var kvp in dict)
            {
                rawBuilder.Append($"{kvp.Key}={kvp.Value}&");
            }
            rawBuilder.Length--; // Bỏ dấu '&' cuối cùng

            var rawData = rawBuilder.ToString();
            var generatedSignature = GenerateSignature(rawData);
            var providedSignature = payload.GetProperty("signature").GetString();

            return generatedSignature == providedSignature;
        }

        public string GenerateSignature(string rawData)
        {
            var key = Encoding.UTF8.GetBytes(ChecksumKey);
            var data = Encoding.UTF8.GetBytes(rawData);
            using var hmac = new HMACSHA256(key);
            var hash = hmac.ComputeHash(data);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }

}

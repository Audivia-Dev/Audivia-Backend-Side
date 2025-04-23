using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Payment;
using Audivia.Domain.ModelRequests.PaymentTransaction;
using Audivia.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static MongoDB.Driver.WriteConcern;

namespace Audivia.Application.Services.Implemetation
{
    public class PayOSService : IPayOSService
    {
        private readonly PayOSOptions _options;

        public PayOSService(IOptions<PayOSOptions> options)
        {
            _options = options.Value;
        }

        public string ClientId => _options.ClientId;
        public string ApiKey => _options.ApiKey;
        public string ChecksumKey => _options.ChecksumKey;

        public string GenerateSignature(string rawData)
        {
            var key = Encoding.UTF8.GetBytes(ChecksumKey);
            var data = Encoding.UTF8.GetBytes(rawData);
            using var hmac = new HMACSHA256(key);
            var hash = hmac.ComputeHash(data);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
        public async Task<string> CreateVietQR(CreatePaymentTransactionRequest transaction, string cancelUrl, string returnUrl)
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
           
            return doc.RootElement.GetProperty("data").GetProperty("checkoutUrl").GetString();
        }

        public bool VerifyWebhook(PayOSWebhookRequest req)
        {
            var raw = $"{req.OrderCode}{req.Amount}{req.Description}{req.Status}{req.PaymentTime}";
            return GenerateSignature(raw) == req.Signature;
        }

        //public async Task ConfirmWebhookAsync()
        //{

        //    var payload = new
        //    {
        //        webhookUrl = "https://audivia-backend.azurewebsites.net/api/payment/webhook"
        //    };
        //    var json = JsonSerializer.Serialize(payload);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    using var client = new HttpClient();
        //    client.DefaultRequestHeaders.Add("x-client-id", ClientId);
        //    client.DefaultRequestHeaders.Add("x-api-key", ApiKey);
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    var res = await client.PostAsync("https://api-merchant.payos.vn/confirm-webhook", content);
        //    res.EnsureSuccessStatusCode();
        //}
        public async Task ConfirmWebhookAsync()
        {
            using var client = new HttpClient();

            // 1. Thiết lập headers CHUẨN
            client.DefaultRequestHeaders.Add("x-client-id", ClientId);
            client.DefaultRequestHeaders.Add("x-api-key", ApiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // 2. Tạo payload CHUẨN
            var payload = new
            {
                webhookUrl = "https://audivia-backend.azurewebsites.net/api/v1/payment/webhook" // Thay bằng URL thực tế
            };
            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            try
            {
                // 3. Gửi request đến URL CHÍNH XÁC
                var response = await client.PostAsync(
                    "https://api-merchant.payos.vn/confirm-webhook", // ĐÚNG endpoint
                    content
                );

                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response: {responseBody}");

                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"LỖI: {ex.Message}");
                if (ex.StatusCode.HasValue)
                    Console.WriteLine($"HTTP Status: {ex.StatusCode}");
            }
        }

    }

}

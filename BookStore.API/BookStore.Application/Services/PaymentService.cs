using BookStore.API.Contracts;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BookStore.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> MakeTransfer(TransferRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/account/transfer", request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Ошибка перевода: {response.StatusCode}, {error}");
        }
    }
}

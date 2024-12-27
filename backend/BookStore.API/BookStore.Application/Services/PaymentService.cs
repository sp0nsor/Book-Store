using BookStore.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace BookStore.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private Guid BookStoreBankNumber = Guid.Parse("574FDDD8-671C-4C25-B2CA-BCE107D1E8A8");

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult> MakeTransfer(Transfer trasfer)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/account/transfer", trasfer);

            return response.IsSuccessStatusCode ? Results.Ok(response) : Results.BadRequest(response);
        }
    }
}

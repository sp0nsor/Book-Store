using BookStore.API.Contracts;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace BookStore.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private Guid BookStroreBankNumber = Guid.Parse("574FDDD8-671C-4C25-B2CA-BCE107D1E8A8");

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult> MakeTransfer(TransferRequest request)
        {
            var newRequest = new TransferRequest(
                request.SenderAccountNumber,
                request.SenderSecretKey,
                request.BookPrice,
                BookStroreBankNumber);

            var response = await _httpClient.PutAsJsonAsync("/api/account/transfer", newRequest);

            return response.IsSuccessStatusCode ? Results.Ok(response) : Results.BadRequest(response);
        }
    }
}

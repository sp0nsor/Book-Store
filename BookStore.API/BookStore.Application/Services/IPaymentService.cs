using BookStore.API.Contracts;
using Microsoft.AspNetCore.Http;

namespace BookStore.Application.Services
{
    public interface IPaymentService
    {
        Task<IResult> MakeTransfer(TransferRequest request);
    }
}
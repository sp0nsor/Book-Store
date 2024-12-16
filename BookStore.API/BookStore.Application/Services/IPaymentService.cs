using BookStore.API.Contracts;

namespace BookStore.Application.Services
{
    public interface IPaymentService
    {
        Task<bool> MakeTransfer(TransferRequest request);
    }
}
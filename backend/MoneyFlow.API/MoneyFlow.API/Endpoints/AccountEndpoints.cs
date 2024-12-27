using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.API.Contracts;
using MoneyFlow.Core.Models;
using MoneyFlow.Core.Services;
using MoneyFlow.Infrastructure;

namespace MoneyFlow.API.Endpoints
{
    public static class AccountEndpoints
    {
        public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("api/account");

            group.MapGet("/info", GetAccounts);
            group.MapPost("/create", CreateAccount);
            group.MapPost("/transfer", MakeTransfer);

            return builder;
        }

        private static async Task<IResult> CreateAccount(
            [FromBody] AccountRequest request,
            IAccountService accountService,
            IKeyHasher keyHasher)
        {
            var secretKey = keyHasher.Generate(request.ScretKey);

            var (account, error) = Account.Create(Guid.NewGuid(), secretKey,
                request.FirstName, request.LastName, request.MoneyAmount);

            if(!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var response = await accountService.CreateAccount(account);

            return Results.Ok(response);
        }

        private static async Task<IResult> GetAccounts(IAccountService accountService)
        {
            var accounts = await accountService.GetAccounts();

            var response = accounts.Select(a => new AccountResponse(
                a.AccountNumber,
                a.OwnerFirstName,
                a.OwnerLastName,
                a.MoneyAmount)).ToList();

            return Results.Ok(response);
        }

        private static async Task<IResult> MakeTransfer(
            [FromBody] TransferRequest request,
            IAccountService accountService)
        {
            var (transfer, error) = Transfer.Create(
                request.SenderAccountNumber, request.RecipientAccountNumber,
                request.SenderSecretKey, request.MoneyAmount);

            if(!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            try
            {
                await accountService.MakeTranfer(transfer);

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }
    }
}

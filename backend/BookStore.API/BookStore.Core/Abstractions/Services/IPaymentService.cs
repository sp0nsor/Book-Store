﻿using BookStore.Core.Models;
using Microsoft.AspNetCore.Http;

namespace BookStore.Application.Services
{
    public interface IPaymentService
    {
        Task<IResult> MakeTransfer(Transfer transfer);
    }
}
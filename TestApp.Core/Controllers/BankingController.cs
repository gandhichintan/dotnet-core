using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Core.Services.Banking;
using TestApp.Core.ViewModels;
using TestApp.Utils.enums;
using TestApp.Utils.HttpReponse;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TestApp.Core.Controllers
{
    public class BankingController : BaseController
    {
        private IBankingService _bankingService;

        public BankingController(IBankingService bankingService, IHttpResponseResult httpResponseResult)
        {
            _bankingService = bankingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentBalance()
        {
            try
            {
                return Ok(await _bankingService.GetCurrentBalance(BankAccount.UserName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(TransactionVM transaction)
        {
            try
            {
                var bankAccount = await _bankingService.GetBankAccountInfo(BankAccount.UserName);
                await _bankingService.Deposit(bankAccount, transaction.Amount);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(TransactionVM transaction)
        {
            try
            {
                var bankAccount = await _bankingService.GetBankAccountInfo(BankAccount.UserName);

                if (bankAccount.Balance <= 0)
                {
                    return BadRequest("You have insufficient balance.");
                }

                await _bankingService.Withdraw(bankAccount, transaction.Amount);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> TransferFund(TransactionVM transaction)
        {
            try
            {
                var destinationBankAccount = await _bankingService.GetBankAccountInfo(transaction.Perticulars);

                if (destinationBankAccount == null)
                {
                    return BadRequest("bank account not exists.");
                }

                var sourceBankAccount = await _bankingService.GetBankAccountInfo(BankAccount.UserName);

                if (sourceBankAccount.Balance <= 0)
                {
                    return BadRequest("You have insufficient balance.");
                }
                await _bankingService.TransferToAnotherAccount(sourceBankAccount, destinationBankAccount, transaction.Amount);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStatement()
        {
            try
            {
                var bankAccount = await _bankingService.GetBankAccountInfo(BankAccount.UserName);
                var transactions = await _bankingService.GetTransactions(bankAccount);
                var transactionVmCollection = new List<TransactionVM>();

                foreach (var transaction in transactions)
                {
                    transactionVmCollection.Add(new TransactionVM
                    {
                        Number = transaction.Id,
                        Amount = transaction.Amount,
                        Perticulars = transaction.Username,
                        IsDebit = transaction.Type == TransactionType.Debit,
                        Balance = transaction.BalanceAfterTransaction
                    });
                }

                return Ok(JsonConvert.SerializeObject(transactionVmCollection));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

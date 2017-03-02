using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.DomainModel.Entity;
using TestApp.Repository.BankAccount;
using TestApp.Repository.Transaction;
using TestApp.Utils.enums;
using Microsoft.Extensions.Logging;

namespace TestApp.Core.Services.Banking
{
    public class BankingService : IBankingService
    {
        #region "Private Member(s)"
        private IBankAccountRepository _bankAccountRepository;
        private ITransactionRepository _transactionRepository;
        private ILogger _logger;
        #endregion

        public BankingService(IBankAccountRepository bankAccountRepository,
            ITransactionRepository transactionRepository,
            ILoggerFactory logger)
        {
            _bankAccountRepository = bankAccountRepository;
            _transactionRepository = transactionRepository;
            _logger = logger.CreateLogger<IBankingService>();
        }

        /// <summary>
        /// Deposit amount to account
        /// </summary>
        /// <param name="bankAccount">bank account object</param>
        public async Task Deposit(BankAccount bankAccount, double amount)
        {
            try
            {
                bankAccount = await _bankAccountRepository.GetBankAccountByName(bankAccount.UserName);
                if (bankAccount != null)
                {
                    var transaction = await CreateTransationEntry(bankAccount, amount, TransactionType.Credit);

                    bankAccount.Balance = transaction.BalanceAfterTransaction;

                    await UpdateBalance(bankAccount);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                throw ex;
            }
        }

        /// <summary>
        /// Transfer amount to another bank account
        /// </summary>
        /// <param name="sourceAccount">account to be debited</param>
        /// <param name="destinationAccout">account to be credited</param>
        public async Task TransferToAnotherAccount(BankAccount sourceAccount, BankAccount destinationAccount, double amount)
        {
            try
            {
                destinationAccount = await _bankAccountRepository.GetBankAccountByName(destinationAccount.UserName);
                if (destinationAccount != null)
                {
                    sourceAccount = await _bankAccountRepository.GetBankAccountByName(sourceAccount.UserName);

                    //create debit entry for source account
                    var transaction = await CreateTransationEntry(sourceAccount, amount, TransactionType.Debit);

                    //update source account balance
                    sourceAccount.Balance = transaction.BalanceAfterTransaction;
                    await UpdateBalance(sourceAccount);

                    //create credit entry for destination account
                    transaction = await CreateTransationEntry(destinationAccount, amount, TransactionType.Credit);

                    //update destination account balance
                    destinationAccount.Balance = transaction.BalanceAfterTransaction;
                    await UpdateBalance(destinationAccount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                throw ex;
            }
        }

        /// <summary>
        /// Withdraw amount from bank account
        /// </summary>
        /// <param name="bankAccount">bank account object</param>
        public async Task Withdraw(BankAccount bankAccount, double amount)
        {
            try
            {
                bankAccount = await _bankAccountRepository.GetBankAccountByName(bankAccount.UserName);
                if (bankAccount != null)
                {
                    var transaction = await CreateTransationEntry(bankAccount, amount, TransactionType.Debit);

                    bankAccount.Balance = transaction.BalanceAfterTransaction;

                    await UpdateBalance(bankAccount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                throw ex;
            }
        }

        /// <summary>
        /// Retrive account information
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<BankAccount> GetBankAccountInfo(string username)
        {
            var bankAccount = await _bankAccountRepository.GetBankAccountByName(username);
            return bankAccount;
        }

        /// <summary>
        /// Retrive balance for account
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<double> GetCurrentBalance(string username)
        {
            var balance = await _bankAccountRepository.GetCurrentBalance(username);
            return balance;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(BankAccount bankAccount)
        {
            var transactions = await _transactionRepository.GetTransactionsByBankAccountName(bankAccount.UserName);
            return transactions;
        }

        #region "Private Metnod(s)"
        private async Task UpdateBalance(BankAccount bankAccount)
        {
            try
            {
                await _bankAccountRepository.Update(bankAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                throw ex;
            }
        }

        private async Task<Transaction> CreateTransationEntry(BankAccount bankAccount, double amount, TransactionType transactionType)
        {
            try
            {
                var transaction = new Transaction
                {
                    Username = bankAccount.UserName,
                    BalanceAfterTransaction = GetNewBalanceBasedOnTransactionType(bankAccount.Balance, amount, transactionType),
                    Amount = amount,
                    Type = transactionType
                };
                transaction = await _transactionRepository.Create(transaction);

                return transaction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                throw ex;
            }
        }

        private double GetNewBalanceBasedOnTransactionType(double balance, double amount, TransactionType transactionType)
        {
            switch (transactionType)
            {
                case TransactionType.Debit:
                    balance = balance - amount;
                    break;
                case TransactionType.Credit:
                    balance = balance + amount;
                    break;
                default:
                    break;
            }
            return balance;
        }
        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        
    }
}

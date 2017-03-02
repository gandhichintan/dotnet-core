using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.DomainModel.Entity;

namespace TestApp.Core.Services.Banking
{
    /// <summary>
    /// Banking service infrastructure
    /// </summary>
    public interface IBankingService : IDisposable
    {
        /// <summary>
        /// Deposit amount to account
        /// </summary>
        /// <param name="account">bank account object</param>
        Task Deposit(BankAccount bankAccount, double amount);

        /// <summary>
        /// Withdraw amount from bank account
        /// </summary>
        /// <param name="account">bank account object</param>
        Task Withdraw(BankAccount bankAccount, double amount);

        /// <summary>
        /// Transfer amount to another bank account
        /// </summary>
        /// <param name="sourceAccount">account to be debited</param>
        /// <param name="destinationAccout">account to be credited</param>
        Task TransferToAnotherAccount(BankAccount sourceAccount, BankAccount destinationAccount, double amount);

        /// <summary>
        /// Retrive account information
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<BankAccount> GetBankAccountInfo(string username);

        /// <summary>
        /// Retrive balance for account
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<double> GetCurrentBalance(string username);

        /// <summary>
        /// Retrive all transactions
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        Task<IEnumerable<Transaction>> GetTransactions(BankAccount bankAccount);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Models = TestApp.DomainModel.Entity;


namespace TestApp.Repository.Transaction
{
    /// <summary>
    /// TransactionRepository infrastructure
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Amount deposit transaction
        /// </summary>
        /// <param name="transaction">transaction entity</param>
        /// <returns>transaction object with id</returns>
        Task<Models.Transaction> Create(Models.Transaction transaction);

        /// <summary>
        /// Retrive all transaction based on provided username(bank account name)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<IEnumerable<Models.Transaction>> GetTransactionsByBankAccountName(string username);
    }
}

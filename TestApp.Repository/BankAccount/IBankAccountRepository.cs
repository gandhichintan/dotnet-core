using Models = TestApp.DomainModel.Entity;
using System.Threading.Tasks;

namespace TestApp.Repository.BankAccount
{
    /// <summary>
    /// BankAccountRepository infrastructure
    /// </summary>
    public interface IBankAccountRepository
    {
        /// <summary>
        /// Create new bank account
        /// </summary>
        /// <param name="bankAccount">bank account entity</param>
        /// <returns>identity result</returns>
        Task Create(Models.BankAccount bankAccount);

        /// <summary>
        /// Create new bank account
        /// </summary>
        /// <param name="bankAccount">bank account entity</param>
        /// <returns>identity result</returns>
        Task Update(Models.BankAccount bankAccount);

        /// <summary>
        /// Get bank account based on provided username/ account name
        /// </summary>
        /// <param name="username">bank account username</param>
        /// <returns>bank account object</returns>
        Task<Models.BankAccount> GetBankAccountByName(string username);

        /// <summary>
        /// Gets the current balance of account
        /// </summary>
        /// <param name="username">bank account username</param>
        /// <returns></returns>
        Task<double> GetCurrentBalance(string username);

        /// <summary>
        /// Checks whether bank account exists in database or not
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        bool IsBankAccountExists(Models.BankAccount bankAccount);
    }
}

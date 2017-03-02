using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Models = TestApp.DomainModel.Entity;

namespace TestApp.Repository.BankAccount
{
    /// <summary>
    /// BankAccountRepository implementation
    /// </summary>
    public class BankAccountRepository : IBankAccountRepository
    {

        #region "Private Member(s)"
        private IDataRepository<Models.BankAccount> _bankAccountDataRepository;
        private ILogger _logger;
        #endregion

        public BankAccountRepository(IDataRepository<Models.BankAccount> bankAccountDataRepository,
                                    ILoggerFactory logger)
        {
            _bankAccountDataRepository = bankAccountDataRepository;
            _logger = logger.CreateLogger<BankAccountRepository>();
        }

        /// <summary>
        /// Create new bank account
        /// </summary>
        /// <param name="bankAccount">bank account entity</param>
        /// <returns>identity result</returns>
        public async Task Create(Models.BankAccount bankAccount)
        {
            try
            {
                var account = await _bankAccountDataRepository.FirstOrDefaultAsync(x => x.UserName == bankAccount.UserName);
                if (account == null)
                {
                    var result = await _bankAccountDataRepository.AddAsync(bankAccount);
                    await _bankAccountDataRepository.SaveChangesAsync();
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
        /// Update account balance information
        /// </summary>
        /// <param name="bankAccount">bank account entity</param>
        /// <returns></returns>
        public async Task Update(Models.BankAccount bankAccount)
        {
            await Task.Run(() => _bankAccountDataRepository.Update(bankAccount));
            await _bankAccountDataRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Get bank account based on provided username/ account name
        /// </summary>
        /// <param name="username">bank account username</param>
        /// <returns>bank account object</returns>
        public async Task<Models.BankAccount> GetBankAccountByName(string username)
        {
            return await _bankAccountDataRepository.FirstOrDefaultAsync(x => x.UserName == username);
        }

        /// <summary>
        /// Gets the current balance of account
        /// </summary>
        /// <param name="username">bank account username</param>
        /// <returns></returns>
        public async Task<double> GetCurrentBalance(string username)
        {
            var account = await GetBankAccountByName(username);

            if (account == null)
            {
                return 0;
            }

            return account.Balance;
        }

        /// <summary>
        /// Checks whether bank account exists in database or not
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        public bool IsBankAccountExists(Models.BankAccount bankAccount)
        {
            return _bankAccountDataRepository.Contains(x => x.UserName == bankAccount.UserName);
        }
    }
}

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models = TestApp.DomainModel.Entity;

namespace TestApp.Repository.Transaction
{
    /// <summary>
    /// TransactionRepository implementation
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {

        #region "Private Member(s)"
        private IDataRepository<Models.Transaction> _transactionDataRepository;
        private IDataRepository<Models.BankAccount> _bankAccountDataRepository;
        private ILogger _logger;
        #endregion

        public TransactionRepository(IDataRepository<Models.Transaction> transactionDataRepository,
            IDataRepository<Models.BankAccount> bankAccountDataRepository,
            ILoggerFactory logger)
        {
            _transactionDataRepository = transactionDataRepository;
            _bankAccountDataRepository = bankAccountDataRepository;
            _logger = logger.CreateLogger<TransactionRepository>();
        }

        /// <summary>
        /// Amount deposit transaction
        /// </summary>
        /// <param name="transaction">transaction entity</param>
        /// <returns>transaction object with id</returns>
        public async Task<Models.Transaction> Create(Models.Transaction transaction)
        {
            try
            {
                transaction = await _transactionDataRepository.AddAsync(transaction);
                await _transactionDataRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                throw ex;
            }
            return transaction;
        }

        /// <summary>
        /// Retrive all transaction based on provided username(bank account name)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByBankAccountName(string username)
        {
            IEnumerable<Models.Transaction> transactions = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(username))
                {
                    transactions = await _transactionDataRepository.FetchAsync(x => x.Username == username);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogTrace(ex.StackTrace);
                throw ex;
            }
            return transactions;
        }
    }
}

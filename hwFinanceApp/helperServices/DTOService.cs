using hwFinanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwFinanceApp.helperServices
{
    public class DTOService
    {
        public BankAccountDTO CreateDTO(BankAccount bankAccount)
        {
            var bankAccountDTO = new BankAccountDTO()
            {
                ID = bankAccount.ID,
                AccountDescription = bankAccount.AccountDescription,
                AccountType = bankAccount.AccountType,
                AccountOwnerId = bankAccount.AccountOwnerId,
                AccountBalance = bankAccount.AccountBalance
            };
            return bankAccountDTO;
        }

        public BankAccount Convert(BankAccountDTO bankAccountDTO)
        {
            var bankAccount = new BankAccount()
            {
                ID = bankAccountDTO.ID,
                AccountDescription = bankAccountDTO.AccountDescription,
                AccountType = bankAccountDTO.AccountType,
                AccountOwnerId = bankAccountDTO.AccountOwnerId,
                AccountBalance = bankAccountDTO.AccountBalance
            };
            return bankAccount;
        }




    }
}

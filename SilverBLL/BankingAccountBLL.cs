using SilverEntities;
using SilverDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBLL
{
    public class BankingAccountBLL
    {
        private BankingAccountDAL bankingAccountDAL;

        public BankingAccountDAL BankingAccountDAL
        {
            get
            {
                if (bankingAccountDAL == null)
                    bankingAccountDAL = new BankingAccountDAL();
                return bankingAccountDAL;
            }
        }

        public int InsertBankingAccount(BankingAccount bankingAccount)
        {
            return BankingAccountDAL.InsertBankingAccount(bankingAccount);
        }

        public bool UpdateBankingAccount(BankingAccount bankingAccount)
        {
            return BankingAccountDAL.UpdateBankingAccount(bankingAccount);
        }

        public BankingAccount getBankingAccountByID(int id)
        {
            return BankingAccountDAL.getBankingAccountByID(id);
        }

        public bool DeleteBankingAccountByID(int id)
        {
            return BankingAccountDAL.DeleteBankingAccountByID(id);
        }

        public List<BankingAccount> ListBankingAccountsByPartialName(string partialName)
        {
            return BankingAccountDAL.ListBankingAccountsByPartialBankname(partialName);
        }

        public List<BankingAccount> ListBankingAccounts()
        {
            return BankingAccountDAL.ListBankingAccounts();
        }
    }
}

using Newtonsoft.Json;
using SilverBLL;
using SilverEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace SilverAPI.Controllers
{
    public class BankingAccountsController : ApiController
    {
        private BankingAccountBLL bankingAccountBLL;

        public BankingAccountBLL BankingAccountBLL
        {
            get
            {
                if (bankingAccountBLL == null)
                    bankingAccountBLL = new BankingAccountBLL();
                return bankingAccountBLL;
            }
        }

        // GET api/values
        public JsonResult<List<BankingAccount>> Get()
        {
            List<BankingAccount> bankingAccounts = BankingAccountBLL.ListBankingAccounts();
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(bankingAccounts, serializerSettings);
        }

        public JsonResult<List<BankingAccount>> Get(string partialName)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(BankingAccountBLL.ListBankingAccountsByPartialName(partialName),serializerSettings);
        }

        // GET api/values/5
        public JsonResult<BankingAccount> Get(int id)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(BankingAccountBLL.getBankingAccountByID(id), serializerSettings);
        }

        // POST api/values
        public int Post([FromBody]string bankingAccount)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            BankingAccount u = JsonConvert.DeserializeObject<BankingAccount>(bankingAccount, serializerSettings);
            return BankingAccountBLL.InsertBankingAccount(u);
        }
        // PUT api/values/5
        public Object Put(int id, [FromBody]string bankingAccount)
        {
            BankingAccount u = JsonConvert.DeserializeObject<BankingAccount>(bankingAccount);
            u.ID = id;
            return new { success = BankingAccountBLL.UpdateBankingAccount(u) };
        }

        // DELETE api/values/5
        public Object Delete(int id)
        {
            return new { success = BankingAccountBLL.DeleteBankingAccountByID(id) };
        }
    }
}

using Akb.Base.Schema;
using System.Text.Json.Serialization;

namespace Akb.Schema
{
    public class AccountRequest : BaseRequest
    {
        [JsonIgnore]
        public int AccountNumber { get; set; }

        public int CustomerId { get; set; }

        public string IBAN { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyType { get; set; }
        public string Name { get; set; }




    }
    public class AccountResponse : BaseResponse
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int AccountNumber { get; set; }
        public string IBAN { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyType { get; set; }
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }


        public virtual List<AccountTransactionResponse> AccountTransactions { get; set; }

        public virtual List<EftTransactionResponse> EftTransactions { get; set; }
    }
}

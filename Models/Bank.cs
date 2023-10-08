namespace LiperFrontend.Models
{
    public class Bank
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nameAR { get; set; }
        public Country country { get; set; }
        public int countryId { get; set; }
    }
    public class GetBank
    {
        public Bank bank { get; set; }
        public responseMessage responseMessage { get; set; }
    }

    public class Banks
    {
        public List<Bank> banks { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class BankAccount
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nameAR { get; set; }
        public Bank bank { get; set; }
        public int bankId { get; set; }
        public int accountNumber { get; set; }
        public bool isDefault { get; set; }
    }
    public class GetBankAccount
    {
        public BankAccount account { get; set; }
        public responseMessage responseMessage { get; set; }
    }
    public class BankAccounts
    {
        public List<BankAccount> accounts { get; set; }
        public responseMessage responseMessage { get; set; }
    }

}

namespace DemoApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string?  FirstName { get; set; }
        public string? LastName { get; set; }
        public string LastName2 { get; set; }


        public virtual UserAddress? UserAddres { get; set; }
        public virtual ICollection<BankAccount>? BankAccounts { get; set;}=new List<BankAccount>() { };



    }
}

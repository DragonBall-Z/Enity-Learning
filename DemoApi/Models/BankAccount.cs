namespace DemoApi.Models
{
    public class BankAccount
    {
        public Guid Id { get; set; }
        public string? BankName { get; set; }
        public string? AccountId { get; set; }
        public string? AccountType { get; set; }

        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}

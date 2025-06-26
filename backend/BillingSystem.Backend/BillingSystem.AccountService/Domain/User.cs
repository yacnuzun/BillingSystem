using BillingSystem.Shared.Persistance.Entity;

namespace BillingSystem.AccountService.Domain
{
    public class User:TEntity,IEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime RecordDate { get; set; }
    }
}

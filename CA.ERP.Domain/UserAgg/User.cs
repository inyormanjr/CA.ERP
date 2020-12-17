using CA.ERP.Domain.BranchAgg;
using CA.ERP.Lib.Domain.UserAgg;

namespace CA.ERP.Domain.UserAgg
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public void SetHashAndSalt(byte[] hash, byte[] salt)
        {
            this.PasswordHash = hash;
            this.PasswordSalt = salt;
        }
    }
}

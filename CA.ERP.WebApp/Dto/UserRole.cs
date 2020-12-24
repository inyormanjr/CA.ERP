namespace CA.ERP.WebApp.Dto
{
    /// <summary>
    /// User roles. Can have multiple values using enum flags
    /// </summary>
    public enum UserRole: int
    {
        Admin = 1,
        Moderator = 2,
        Marketing = 4,
        Cashier = 8,
        Stocks = 16,
        CC = 32,
        Encoder = 64
    }
}
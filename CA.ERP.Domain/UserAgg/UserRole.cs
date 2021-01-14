using System;

namespace CA.ERP.Domain.UserAgg
{
    /// <summary>
    /// None = 0,
    ///Admin = 1, 
    ///Moderator = 2, 
    ///Marketing =4, 
    ///Cashier = 8, 
    ///Stocks = 16,
    ///CC = 32,
    ///Encoder = 64,
    ///Accounting = 128,
    ///Manager = 256
    /// </summary>
    [Flags]
    public enum UserRole : int
    {
        None = 0,
        Admin = 1, 
        Moderator = 2, 
        Marketing =4, 
        Cashier = 8, 
        Stocks = 16,
        CC = 32,
        Encoder = 64,
        Accounting = 128,
        Manager = 256
    }
}
﻿using System;

namespace CA.ERP.WebApp.Dto.User
{
    /// <summary>
    /// User roles. Can have multiple values using enum flags
    ///  Admin = 1,
    ///  Moderator = 2,
    ///  Marketing = 4,
    ///  Cashier = 8,
    ///  Stocks = 16,
    ///  CC = 32,
    ///  Encoder = 64
    /// </summary>
    [Flags]
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
namespace CA.ERP.Domain.PaymentAgg
{
    /// <summary>
    /// The payment method use.
    /// Cash = 1,
    /// Card = 2,
    /// Cheque = 3,
    /// </summary>
    public enum PaymentMethod : int
    {
        Cash = 1,
        CashOnDelivery = 2,
        Card = 3,
        Cheque = 4,
    }
}
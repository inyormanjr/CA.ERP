namespace CA.ERP.Domain.PaymentAgg
{
    /// <summary>
    /// OnSite = 1,
    /// OnDelivery = 2
    /// </summary>
    public enum PaymentType : int
    {
        /// <summary>
        /// When a payment is made on the site
        /// </summary>
        OnSite = 1,
        /// <summary>
        /// When a payment is made on delivery
        /// </summary>
        OnDelivery = 2
    }
}
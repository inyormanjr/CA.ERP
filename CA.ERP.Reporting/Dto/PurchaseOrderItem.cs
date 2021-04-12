namespace CA.ERP.Reporting.Dto
{
    public class PurchaseOrderItem
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public decimal OrderedQuantity { get; set; }

        public decimal FreeQuantity { get; set; }

        public decimal CostPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalCostPrice { get; set; }
    }
}

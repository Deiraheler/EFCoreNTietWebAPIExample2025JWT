namespace RAD302Week6Lab2025.DataModel.S00243021
{
    public class Product
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int ReorderLevel { get; set; }
        public int ReorderQuantity { get; set; }
        public double UnitPrice { get; set; }
        public int StockOnHand { get; set; }
    }
}

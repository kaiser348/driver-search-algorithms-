namespace DriverSearch.Models
{
    public class Order
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public Order(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

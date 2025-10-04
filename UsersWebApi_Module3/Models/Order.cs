namespace UsersWebApi_Module3.Models
{
    public class Order
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public List<string> Products { get; set; }
        public string UserId { get; set; }
    }
}
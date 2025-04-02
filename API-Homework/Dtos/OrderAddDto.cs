namespace API_Homework.Dtos
{
    public class OrderAddDto
    {
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
    }
}

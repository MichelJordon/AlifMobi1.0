using Domain.Entities;
namespace Domain.Dtos;
public class AddTransacitionDto{
    public int TransacitionId { get; set; }
    public string Recipient {get; set;}
    public DateTime CreatedAt {get; set;}
    public decimal Amount {get; set;}
    public PaymentMethod PaymentMethod {get; set;}
    public string Sender {get; set;}
    public PaymentType PaymentType {get; set;}
    public AddTransacitionDto()
    {
        CreatedAt  = DateTime.UtcNow;
    }
}
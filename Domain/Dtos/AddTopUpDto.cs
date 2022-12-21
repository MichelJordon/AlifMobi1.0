using Domain.Entities;
namespace Domain.Dtos;
public class AddTopUpDto{
    public int TransacitionId { get; set; }
    public string PhoneNumber {get; set;}
    public decimal Amount{get; set;}
    public PaymentMethod PaymentMethod {get; set;}
}
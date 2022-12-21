using Domain.Entities;
namespace Domain.Dtos;
public class GetTransacitionsDto{
    public int TransacitionId { get; set; }
    public string Recipient {get; set;}
    public DateTime CreatedAt {get; set;}
    public Status Status {get; set;}
    public decimal Amount {get; set;}
    public PaymentMethod PaymentMethod {get; set;}
    public string Sender {get; set;}
    public PaymentType PaymentType {get; set;}
    public string AcountId {get; set;}
}
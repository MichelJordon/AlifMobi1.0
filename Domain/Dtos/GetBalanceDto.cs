using Domain.Entities;
namespace Domain.Dtos;
public class GetBalanceDto{
    public PaymentType PaymentType {get; set;}
    public decimal Amount {get; set;}
}
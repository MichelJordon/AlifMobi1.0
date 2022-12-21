using Domain.Entities;
namespace Domain.Dtos;
public class GetTopUpDto{
    
    public string Name {get; set;}
    public string Surname {get; set;}
    public string PhoneNumber {get; set;}
    public decimal Amount{get; set;}

}
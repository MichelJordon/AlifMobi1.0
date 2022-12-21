using Domain.Entities;
namespace Domain.Dtos;
public class GetAcountDto{
    public string AcountId { get; set; }
    public string Name {get; set;}
    public string Surname {get; set;}
    public bool Authenticated {get; set;}
    public string PhoneNumber {get; set;}
}
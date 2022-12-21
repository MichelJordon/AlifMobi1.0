namespace Domain.Entities;
public class Acount
{
    public string AcountId { get; set; }
    public string Name {get; set;}
    public string Surname {get; set;}
    public bool Authenticated {get; set;}
    public string PhoneNumber {get; set;}
    public List<Transacition> Transacitions {get; set;}
}
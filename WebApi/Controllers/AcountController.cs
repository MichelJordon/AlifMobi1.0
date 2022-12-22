using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Infrastructure.Services;
using Domain.Dtos;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class AcountController{
    public readonly AcountService _acountService;
    public AcountController(AcountService acountService)
    {
        _acountService = acountService;
    }

    [HttpGet("GetAll")]
    public async Task<Response<List<GetAcountDto>>> GetAcounts(){
        return await _acountService.GetAcounts();
    }
    //TopUpBalance
    //GeAuthenticated
    [HttpGet("GeAuthenticated")]
    public async Task<Response<string>> GeAuthenticated( string phoneNumber ){
        return await _acountService.GeAuthenticated( phoneNumber );
    }
    [HttpGet("GetBalance")]
    public async Task<Response<decimal>> GetAccountBalance( string phoneNumber ){
        return await _acountService.GetAccountBalance( phoneNumber );
    }
    [HttpPost("Add")]
    public async Task<Response<GetAcountDto>> InsertJobHistory(AddAcountDto acount){
        return await _acountService.InsertAcount(acount);
    }
    [HttpPut("Update")]
    public async Task<Response<AddAcountDto>> Update(AddAcountDto acount){
        return await _acountService.Update(acount);
    }
    [HttpDelete("Delete")]
    public async Task<Response<string>> Delete(string id){
        return await _acountService.Delete(id);
    }
}
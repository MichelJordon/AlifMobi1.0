using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Infrastructure.Services;
using Domain.Dtos;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class TransacitionController{
    public readonly TransacitionService _transacitionService;
    public TransacitionController(TransacitionService transacitionService)
    {
        _transacitionService = transacitionService;
    }

    [HttpGet("GetAll")]
    public async Task<Response<List<GetTransacitionsDto>>> Get(){
        return await _transacitionService.Get();
    }
    [HttpPost("Add")]
    public async Task<Response<GetTransacitionsDto>> Insert(AddTransacitionDto transacition){
        return await _transacitionService.Insert(transacition);
    }
    [HttpPost("AddBalance")]
    public async Task<Response<GetTopUpDto>> TopUpBalance(AddTopUpDto ball){
        return await _transacitionService.TopUpBalance(ball);
    }

}
using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;
public class TransacitionService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public TransacitionService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetTransacitionsDto>>> Get()
    {
        try
        {
            var list = _mapper.Map<List<GetTransacitionsDto>>(_context.Transacitions.ToList());
            return new Response<List<GetTransacitionsDto>>(list.ToList());
        }
        catch (Exception ex)
        {
            return new Response<List<GetTransacitionsDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<GetTopUpDto>> TopUpBalance(AddTopUpDto ball)
    {
        var balance = _mapper.Map<Transacition>(ball);
        var acc = _context.Acounts.FirstOrDefault(x=>x.PhoneNumber == ball.PhoneNumber);
        if(acc == null) return new Response<GetTopUpDto>(HttpStatusCode.NotFound,"Sender not found");
        balance.AcountId = acc.AcountId;
        balance.Recipient = acc.PhoneNumber;
        balance.Sender = ball.PhoneNumber;
        balance.PaymentType = PaymentType.TopUp;
        _context.Transacitions.Add(balance);
        var get = new GetTopUpDto(){
            Name = acc.Name,
            Surname = acc.Surname,
            PhoneNumber = ball.PhoneNumber,
            Amount = ball.Amount
        };
        await _context.SaveChangesAsync();

        return new Response<GetTopUpDto>(_mapper.Map<GetTopUpDto>(get));
    }
    //top up balance
    public async Task<Response<GetTransacitionsDto>> Insert(AddTransacitionDto transacition)
    {
        try
        {
            var newTransacition = _mapper.Map<Transacition>(transacition);  
            var acc = _context.Acounts.FirstOrDefault(x=>x.PhoneNumber == transacition.Sender);
            if(acc == null) return new Response<GetTransacitionsDto>(HttpStatusCode.NotFound,"Sender not found");
            newTransacition.AcountId = acc.AcountId;
            if ( GetAccountBalance(transacition.Sender) <= transacition.Amount )
            {
                return new Response<GetTransacitionsDto>(HttpStatusCode.NotFound,"Not enough balance");
            }
            _context.Transacitions.AddAsync(newTransacition);
            await _context.SaveChangesAsync();
            return new Response<GetTransacitionsDto>(_mapper.Map<GetTransacitionsDto>(newTransacition));
        }
        catch (Exception ex)
        {
            return new Response<GetTransacitionsDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public decimal GetAccountBalance( string phoneNumber )
    {
            var get = (
            from t in _context.Transacitions
            where t.Recipient == phoneNumber || t.Recipient == t.Sender
            select new GetBalanceDto
            {
                PaymentType = t.PaymentType,
                Amount = t.Amount
            }
            ).ToList();
             var put = (
            from t in _context.Transacitions
            where t.Sender == phoneNumber && t.Recipient != t.Sender
            select new GetBalanceDto
            {
                PaymentType = t.PaymentType,
                Amount = t.Amount
            }
            ).ToList();
            decimal balance = 0;
            foreach (var method in get)
                balance += method.Amount;
            foreach (var method in put)
                balance -= method.Amount;
            return balance;
    }
    /*
    public async Task<Response<AddTransacitionDto>> UpdateJob(AddTransacitionDto transacition)
    {
        try
        {
            var find = await _context.Transacitions.FindAsync(transacition.);
            find.JobTitle = job.JobTitle;
            find.MinSalary = job.MinSalary;
            find.MaxSalary = job.MaxSalary;
            var updated = await _context.SaveChangesAsync();
            return new Response<AddJobDto>(job);
        }
        catch (Exception ex)
        {
            return new Response<AddJobDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    */
    /*
    public async Task<Response<string>> DeleteJob(string id)
    {
        try
        {
            var find = await _context.Jobs.FindAsync(id);
            _context.Remove(find);
            var response = await _context.SaveChangesAsync();
            if (response > 0)
                return new Response<string>("Object deleted successfully");
            return new Response<string>(HttpStatusCode.BadRequest, "Object not found");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    */
}
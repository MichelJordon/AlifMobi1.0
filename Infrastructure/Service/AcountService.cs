using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;
public class AcountService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public AcountService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetAcountDto>>> GetAcounts()
    {
        try
        {
            var list = _mapper.Map<List<GetAcountDto>>(_context.Acounts.ToList());
            return new Response<List<GetAcountDto>>(list.ToList());
        }
        catch (Exception ex)
        {
            return new Response<List<GetAcountDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<decimal>> GetAccountBalance( string phoneNumber )
    {
        try
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
            /*
            var list = _context.Transacitions.Select(s => new GetBalanceDto()
            {
                PaymentType = s.PaymentType,
                Amount = s.Amount
            }).ToList();
            */
            decimal balance = 0;
            foreach (var method in get)
                balance += method.Amount;
            foreach (var method in put)
                balance -= method.Amount;
            return new Response<decimal>(balance);
        }
        catch (Exception ex)
        {
            return new Response<decimal>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<GetAcountDto>> InsertAcount(AddAcountDto account)
    {
        try
        {
            var newAcc = _mapper.Map<Acount>(account);
            _context.Acounts.Add(newAcc);
            await _context.SaveChangesAsync();
            return new Response<GetAcountDto>(_mapper.Map<GetAcountDto>(newAcc));
        }
        catch (Exception ex)
        {
            return new Response<GetAcountDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<AddAcountDto>> Update(AddAcountDto acount)
    {
        try
        {
            var find = await _context.Acounts.FindAsync(acount.AcountId);
            find.AcountId = acount.AcountId;
            find.Name = acount.Name;
            find.Surname = acount.Surname;
            find.Authenticated = acount.Authenticated;
            find.PhoneNumber = acount.PhoneNumber;
            var updated = await _context.SaveChangesAsync();
            return new Response<AddAcountDto>(acount);
        }
        catch (Exception ex)
        {
            return new Response<AddAcountDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<string>> Delete(string id)
    {
        try
        {
            var find = await _context.Acounts.FindAsync(id);
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

}
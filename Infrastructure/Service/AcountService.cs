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
    public readonly TransacitionService _transacitionService;
    private readonly IMapper _mapper;

    public AcountService(DataContext context, IMapper mapper, TransacitionService transacitionService)
    {
        _context = context;
        _mapper = mapper;
        _transacitionService = transacitionService;
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
    public async Task<Response<string>> GeAuthenticated( string phoneNumber )
    {
        try
        {
            return new Response<string>(  _transacitionService.GetAuth(phoneNumber) ? "Authenticated" : "Not Authenticated" );
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<decimal>> GetAccountBalance( string phoneNumber )
    {
        try
        {
            if ( _transacitionService.FindAccount( phoneNumber ) == false )
                return new Response<decimal>(HttpStatusCode.NotFound,"Phone Number not found!");
            return new Response<decimal>(_transacitionService.GetAccountBalance(phoneNumber));
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
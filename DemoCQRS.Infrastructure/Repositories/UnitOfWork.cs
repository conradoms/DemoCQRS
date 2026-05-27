using DemoCQRS.Domain.Abstractions;
using DemoCQRS.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCQRS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private IMemberRepository? _memberRepo;
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IMemberRepository MemberRepository
    {
        get
        {
            return _memberRepo = _memberRepo ??
                new MemberRepository(_context);
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

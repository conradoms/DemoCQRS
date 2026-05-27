using DemoCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCQRS.Domain.Abstractions;

public interface IMemberDapperRepository
{
    Task<IEnumerable<Member>> GetMembers();
    Task<Member> GetMemberById(int id);
}

using DemoCQRS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCQRS.Application.Members.Commands;

public abstract class MemberCommandBase : IRequest<Member>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Gender { get; set; }
    public string? Email { get; set; }
    public bool? IsActive { get; set; }
}

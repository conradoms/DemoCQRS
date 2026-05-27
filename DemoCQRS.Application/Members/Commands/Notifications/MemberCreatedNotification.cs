using DemoCQRS.Domain.Entities;
using MediatR;

namespace DemoCQRS.Application.Members.Commands.Notifications;

public class MemberCreatedNotification : INotification
{
    public Member Member { get; }

    public MemberCreatedNotification(Member member)
    {
        Member = member;
    }
}

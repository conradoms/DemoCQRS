namespace DemoCQRS.Domain.Abstractions;

public interface IUnitOfWork
{
    IMemberRepository MemberRepository { get; }

    Task CommitAsync();
}

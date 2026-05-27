using DemoCQRS.Domain.Abstractions;
using DemoCQRS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCQRS.Application.Members.Commands
{
    public sealed class DeleteMemberCommand : MemberCommandBase
    {
        public int Id { get; set; }

        public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Member>
        {
            private readonly IUnitOfWork _unitOfWork;
            public DeleteMemberCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<Member> Handle(DeleteMemberCommand request,
                         CancellationToken cancellationToken)
            {
                var deletedMember = await _unitOfWork.MemberRepository.DeleteMember(request.Id);

                if (deletedMember is null)
                    throw new InvalidOperationException("Member not found");

                await _unitOfWork.CommitAsync();
                return deletedMember;
            }
        }
    }
}

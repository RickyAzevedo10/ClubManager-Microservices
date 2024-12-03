using Contracts;
using Financial.Domain.Entities;
using Financial.Domain.Interfaces.Identity;
using Financial.Domain.Interfaces.Repositories;
using MassTransit;

namespace Financial.Application.Consumers
{
    public class ClubMemberDeleteConsumer : IConsumer<DeleteClubMember>
    {
        private readonly IMembersService _memberService;
        private readonly IUnitOfWork _unitOfWork;

        public ClubMemberDeleteConsumer(IMembersService memberService, IUnitOfWork unitOfWork)
        {
            _memberService = memberService;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<DeleteClubMember> context)
        {
            ClubMember? clubMember = await _unitOfWork.ClubMemberRepository.GetById(context.Message.ExternalClubMemberId);

            if (clubMember != null)
            {
                //delete
                await _memberService.Delete(clubMember);
                await _unitOfWork.CommitAsync();
            }

            return;
        }
    }
}

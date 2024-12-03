using Contracts;
using Financial.Domain.Entities;
using Financial.Domain.Interfaces.Identity;
using Financial.Domain.Interfaces.Repositories;
using MassTransit;

namespace Financial.Application.Consumers
{
    public class ClubMemberCreateUpdateConsumer : IConsumer<CreateUpdateClubMember>
    {
        private readonly IMembersService _clubMemberService;
        private readonly IUnitOfWork _unitOfWork;

        public ClubMemberCreateUpdateConsumer(IMembersService clubMemberService, IUnitOfWork unitOfWork)
        {
            _clubMemberService = clubMemberService;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<CreateUpdateClubMember> context)
        {
            ClubMember? clubMember = await _unitOfWork.ClubMemberRepository.GetById(context.Message.ExternalClubMemberId);

            if (clubMember == null)
            {
                //create
                await _clubMemberService.Create(context.Message);
            }
            else
            {
                //update
                await _clubMemberService.Update(context.Message, clubMember);
            }
            await _unitOfWork.CommitAsync();
            return;
        }
    }
}

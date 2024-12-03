using Contracts;
using Financial.Domain.Entities;
using Financial.Domain.Interfaces.Identity;
using Financial.Domain.Interfaces.Repositories;

namespace Financial.Domain.Services.Identity
{
    public class MemberService : IMembersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClubMember?> Create(CreateUpdateClubMember createClubMember)
        {
            ClubMember clubMember = new();
            clubMember.SetFirstName(createClubMember.FirstName);
            clubMember.SetLastName(createClubMember.LastName);
            clubMember.SetPartner(createClubMember.Partner);
            clubMember.SetEmail(createClubMember.Email);
            clubMember.SetPhoneNumber(createClubMember.PhoneNumber);

            return await _unitOfWork.ClubMemberRepository.AddAsync(clubMember);
        }

        public async Task<ClubMember?> Update(CreateUpdateClubMember clubMemberToUpdate, ClubMember clubMember)
        {
            clubMember.SetFirstName(clubMemberToUpdate.FirstName);
            clubMember.SetLastName(clubMemberToUpdate.LastName);
            clubMember.SetPartner(clubMemberToUpdate.Partner);
            clubMember.SetEmail(clubMemberToUpdate.Email);
            clubMember.SetPhoneNumber(clubMemberToUpdate.PhoneNumber);

            _unitOfWork.ClubMemberRepository.Update(clubMember);
            return clubMember;
        }

        public async Task<ClubMember?> Delete(ClubMember clubMember)
        {
            _unitOfWork.ClubMemberRepository.Delete(clubMember);
            return clubMember;
        }
    }
}

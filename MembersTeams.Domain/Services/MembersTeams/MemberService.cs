using MembersTeams.Domain.Interfaces;
using MembersTeams.Domain.Interfaces.Identity;
using MembersTeams.Domain.Interfaces.Repositories;
using FluentValidation;
using MembersTeams.Domain.DTOs;
using MembersTeams.Domain.Entities;
using static MembersTeams.Domain.Constants.Constants;

namespace MembersTeams.Domain.Services.Identity
{
    public class MemberService : IMembersService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService _entityValidationService;
        private readonly IValidator<CreateClubMemberDTO> _clubMemberCreateValidator;
        private readonly IValidator<UpdateClubMemberDTO> _clubMemberUpdateValidator;
        private readonly IValidator<CreateMinorClubMemberDTO> _clubMinorMemberCreateValidator;
        private readonly IValidator<UpdateMinorClubMemberDTO> _clubMinorMemberUpdateValidator;

        public MemberService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IEntityValidationService entityValidationService, IValidator<CreateClubMemberDTO> clubMemberCreateValidator, IValidator<CreateMinorClubMemberDTO> clubMinorMemberCreateValidator, IValidator<UpdateMinorClubMemberDTO> clubMinorMemberUpdateValidator, IValidator<UpdateClubMemberDTO> clubMemberUpdateValidator)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _clubMemberCreateValidator = clubMemberCreateValidator;
            _clubMinorMemberCreateValidator = clubMinorMemberCreateValidator;
            _clubMinorMemberUpdateValidator = clubMinorMemberUpdateValidator;
            _clubMemberUpdateValidator = clubMemberUpdateValidator;
        }

        public async Task<ClubMember?> Create(CreateClubMemberDTO createClubMember)
        {
            bool validationResult = _entityValidationService.Validate(_clubMemberCreateValidator, createClubMember);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.ClubMemberNotifications.ERROR_CLUBMEMBER_CREATED, string.Empty);
                return null;
            }

            ClubMember clubMember = new();
            clubMember.SetFirstName(createClubMember.FirstName);
            clubMember.SetLastName(createClubMember.LastName);
            clubMember.SetPartner(createClubMember.Partner);
            clubMember.SetEducationOfficer(createClubMember.EducationOfficer);
            clubMember.SetDateOfJoining(createClubMember.DateOfJoining);
            clubMember.SetDateOfBirth(createClubMember.DateOfBirth);
            clubMember.SetEmail(createClubMember.Email);
            clubMember.SetPhoneNumber(createClubMember.PhoneNumber);
            clubMember.SetAddress(createClubMember.Address);

            return await _unitOfWork.ClubMemberRepository.AddAsync(clubMember);
        }

        public async Task<ClubMember?> Update(UpdateClubMemberDTO clubMemberToUpdate, ClubMember clubMember)
        {
            bool validationResult = _entityValidationService.Validate(_clubMemberUpdateValidator, clubMemberToUpdate);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.ERROR_USER_CREATED, string.Empty);
                return null;
            }

            clubMember.SetFirstName(clubMemberToUpdate.FirstName);
            clubMember.SetLastName(clubMemberToUpdate.LastName);
            clubMember.SetPartner(clubMemberToUpdate.Partner);
            clubMember.SetEducationOfficer(clubMemberToUpdate.EducationOfficer);
            clubMember.SetDateOfJoining(clubMemberToUpdate.DateOfJoining);
            clubMember.SetDateOfBirth(clubMemberToUpdate.DateOfBirth);
            clubMember.SetEmail(clubMemberToUpdate.Email);
            clubMember.SetPhoneNumber(clubMemberToUpdate.PhoneNumber);
            clubMember.SetAddress(clubMemberToUpdate.Address);

            _unitOfWork.ClubMemberRepository.Update(clubMember);
            return clubMember;
        }

        public async Task<ClubMember?> Delete(long id)
        {
            ClubMember? clubMember = await _unitOfWork.ClubMemberRepository.GetByIdAsync(id);

            if (clubMember == null)
            {
                _notificationContext.AddNotification(NotificationKeys.ClubMemberNotifications.CLUBMEMBER_DONT_EXITS, string.Empty);
                return null;
            }

            _unitOfWork.ClubMemberRepository.Delete(clubMember);
            return clubMember;
        }

        public async Task<MinorClubMember?> DeleteMinorClubMember(long id)
        {
            MinorClubMember? minorClubMember = await _unitOfWork.MinorClubMemberRepository.GetById(id);

            if (minorClubMember == null)
            {
                _notificationContext.AddNotification(NotificationKeys.MinorClubMemberNotifications.MINORCLUBMEMBER_DONT_EXIST, string.Empty);
                return null;
            }

            _unitOfWork.MinorClubMemberRepository.Delete(minorClubMember);
            return minorClubMember;
        }

        public async Task<MinorClubMember?> CreateMinorClubMember(CreateMinorClubMemberDTO createMinorClubMember)
        {
            bool validationResult = _entityValidationService.Validate(_clubMinorMemberCreateValidator, createMinorClubMember);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.ClubMemberNotifications.ERROR_CLUBMEMBER_CREATED, string.Empty);
                return null;
            }

            MinorClubMember minorClubMember = new();
            minorClubMember.SetFirstName(createMinorClubMember.FirstName);
            minorClubMember.SetLastName(createMinorClubMember.LastName);
            minorClubMember.SetPartner(createMinorClubMember.Partner);
            minorClubMember.SetDateOfJoining(createMinorClubMember.DateOfJoining);
            minorClubMember.SetDateOfBirth(createMinorClubMember.DateOfBirth);
            minorClubMember.SetEmail(createMinorClubMember.Email);
            minorClubMember.SetPhoneNumber(createMinorClubMember.PhoneNumber);
            minorClubMember.SetAddress(createMinorClubMember.Address);
            minorClubMember.SetGuardianId(createMinorClubMember.MemberId);

            return await _unitOfWork.MinorClubMemberRepository.AddAsync(minorClubMember);
        }

        public async Task<MinorClubMember?> UpdateMinorClubMember(UpdateMinorClubMemberDTO minorClubMemberToUpdate, MinorClubMember minorClubMember)
        {
            bool validationResult = _entityValidationService.Validate(_clubMinorMemberUpdateValidator, minorClubMemberToUpdate);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.ERROR_USER_CREATED, string.Empty);
                return null;
            }

            minorClubMember.SetFirstName(minorClubMemberToUpdate.FirstName);
            minorClubMember.SetLastName(minorClubMemberToUpdate.LastName);
            minorClubMember.SetPartner(minorClubMemberToUpdate.Partner);
            minorClubMember.SetDateOfJoining(minorClubMemberToUpdate.DateOfJoining);
            minorClubMember.SetDateOfBirth(minorClubMemberToUpdate.DateOfBirth);
            minorClubMember.SetEmail(minorClubMemberToUpdate.Email);
            minorClubMember.SetPhoneNumber(minorClubMemberToUpdate.PhoneNumber);
            minorClubMember.SetAddress(minorClubMemberToUpdate.Address);

            _unitOfWork.MinorClubMemberRepository.Update(minorClubMember);
            return minorClubMember;
        }

        public async Task<UserClubMember?> Create(long userId, long clubMemberId)
        {
            UserClubMember userClubMember = new();
            userClubMember.SetClubMemberId(clubMemberId);
            userClubMember.SetUserId(userId);

            return await _unitOfWork.UserClubMemberRepository.AddAsync(userClubMember);
        }

        public async Task<UserClubMember?> UpdateUserClubMember(long userId, long clubMemberId)
        {
            UserClubMember? userClubMember = await _unitOfWork.UserClubMemberRepository.GetByUserIdAsync(userId);
            if (userClubMember != null)
            {
                return userClubMember;
            }
            UserClubMember newUserClubMember = new();
            newUserClubMember.SetClubMemberId(clubMemberId);
            newUserClubMember.SetUserId(userId);

            return await _unitOfWork.UserClubMemberRepository.AddAsync(newUserClubMember);
        }

    }
}

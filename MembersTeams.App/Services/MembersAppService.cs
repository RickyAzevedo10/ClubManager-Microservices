using AutoMapper;
using MembersTeams.Application.Interfaces;
using MembersTeams.Application.Interfaces.Infrastructure;
using MembersTeams.Application.Interfaces.Producers;
using MembersTeams.Domain.DTOs;
using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces;
using MembersTeams.Domain.Interfaces.Identity;
using MembersTeams.Domain.Interfaces.Repositories;
using static MembersTeams.Domain.Constants.Constants;

namespace MembersTeams.Application.Services
{
    public class MembersAppService : IMembersAppService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMembersService _membersService;
        private readonly IMapper _mapper;
        private readonly IClubMemberProducer _clubMemberProducer;
        private readonly IUserPermissionsCachedRepository _userPermissionsCachedRepository;

        public MembersAppService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IClubMemberProducer clubMemberProducer, IUserPermissionsCachedRepository userPermissionsCachedRepository,
            IMembersService membersService, IMapper mapper)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _membersService = membersService;
            _mapper = mapper;
            _clubMemberProducer = clubMemberProducer;
            _userPermissionsCachedRepository = userPermissionsCachedRepository;
        }

        #region ClubMember
        public async Task<ClubMemberResponseDTO?> Create(CreateClubMemberDTO memberToCreate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            ClubMember? clubMember = await _unitOfWork.ClubMemberRepository.GetByEmailAsync(memberToCreate.Email);

            if (clubMember != null)
            {
                _notificationContext.AddNotification(NotificationKeys.ClubMemberNotifications.CLUBMEMBER_ALREADY_EXITS, string.Empty);
                return null;
            }

            clubMember = await _membersService.Create(memberToCreate);

            if (memberToCreate.IsUser && memberToCreate.UserId != 0)
            {
                clubMember.UserClubMember = await _membersService.Create(memberToCreate.UserId, clubMember.Id);
            }

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            if(clubMember != null)
                await _clubMemberProducer.CreateUpdateTeamProducer(clubMember);

            return _mapper.Map<ClubMemberResponseDTO>(clubMember);
        }

        public async Task<ClubMemberResponseDTO?> Delete(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            ClubMember? clubMemberDeleted = await _membersService.Delete(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            if (clubMemberDeleted != null)
                await _clubMemberProducer.DeleteClubMemberProducer(clubMemberDeleted.Id);

            return _mapper.Map<ClubMemberResponseDTO>(clubMemberDeleted);
        }

        public async Task<List<ClubMemberResponseDTO>?> GetAllClubMembers()
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            IEnumerable<ClubMember>? clubMembers = await _unitOfWork.ClubMemberRepository.GetAllAsync();

            return _mapper.Map<List<ClubMemberResponseDTO>>(clubMembers);
        }

        public async Task<List<ClubMemberResponseDTO>?> SearchClubMembersAsync(string? firstName, string? lastName)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            IEnumerable<ClubMember>? clubMembers = await _unitOfWork.ClubMemberRepository.SearchClubMembersAsync(firstName, lastName);

            return _mapper.Map<List<ClubMemberResponseDTO>>(clubMembers);
        }

        public async Task<ClubMemberResponseDTO?> Update(UpdateClubMemberDTO clubMemberToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            ClubMember? clubMember = null;

            if (clubMemberToUpdate.Id != 0)
                clubMember = await _unitOfWork.ClubMemberRepository.GetById(clubMemberToUpdate.Id);

            if (clubMember == null)
            {
                _notificationContext.AddNotification(NotificationKeys.ClubMemberNotifications.CLUBMEMBER_DONT_EXITS, string.Empty);
                return null;
            }

            clubMember = await _membersService.Update(clubMemberToUpdate, clubMember);

            if (clubMemberToUpdate.IsUser && clubMemberToUpdate.UserId != 0)
            {
                clubMember.UserClubMember = await _membersService.UpdateUserClubMember(clubMemberToUpdate.UserId, clubMember.Id);
            }

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            if (clubMember != null)
                await _clubMemberProducer.CreateUpdateTeamProducer(clubMember);

            return _mapper.Map<ClubMemberResponseDTO>(clubMember);
        }
        #endregion

        #region MinorClubMember
        public async Task<MinorClubMemberResponseDTO?> CreateMinorClubMembers(CreateMinorClubMemberDTO minorMemberToCreate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            MinorClubMember? minorClubMember = await _unitOfWork.MinorClubMemberRepository.GetByEmailAsync(minorMemberToCreate.Email);

            if (minorClubMember != null)
            {
                _notificationContext.AddNotification(NotificationKeys.MinorClubMemberNotifications.MINORCLUBMEMBER_ALREADY_EXISTS, string.Empty);
                return null;
            }

            minorClubMember = await _membersService.CreateMinorClubMember(minorMemberToCreate);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<MinorClubMemberResponseDTO>(minorClubMember);
        }

        public async Task<MinorClubMemberResponseDTO?> DeleteMinorClubMember(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            MinorClubMember? minorClubMemberDeleted = await _membersService.DeleteMinorClubMember(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<MinorClubMemberResponseDTO>(minorClubMemberDeleted);
        }

        public async Task<List<MinorClubMemberResponseDTO>?> GetAllMinorClubMembers()
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            IEnumerable<MinorClubMember>? minorClubMembers = await _unitOfWork.MinorClubMemberRepository.GetAllAsync();

            return _mapper.Map<List<MinorClubMemberResponseDTO>>(minorClubMembers);
        }

        public async Task<MinorClubMemberResponseDTO?> UpdateMinorMembers(UpdateMinorClubMemberDTO minorClubMemberToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            MinorClubMember? minorClubMember = null;

            if (minorClubMemberToUpdate.Id != null)
                minorClubMember = await _unitOfWork.MinorClubMemberRepository.GetById((long)minorClubMemberToUpdate.Id);

            if (minorClubMember == null)
            {
                _notificationContext.AddNotification(NotificationKeys.MinorClubMemberNotifications.MINORCLUBMEMBER_DONT_EXIST, string.Empty);
                return null;
            }

            minorClubMember = await _membersService.UpdateMinorClubMember(minorClubMemberToUpdate, minorClubMember);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<MinorClubMemberResponseDTO>(minorClubMember);
        }

        public async Task<List<MinorClubMemberResponseDTO>?> SearchMinorMembersAsync(string? firstName, string? lastName)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            IEnumerable<MinorClubMember>? minorClubMembers = await _unitOfWork.MinorClubMemberRepository.SearchMinorClubMemberAsync(firstName, lastName);

            return _mapper.Map<List<MinorClubMemberResponseDTO>>(minorClubMembers);
        }
        #endregion
    }
}

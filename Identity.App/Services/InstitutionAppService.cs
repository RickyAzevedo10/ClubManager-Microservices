using AutoMapper;
using Identity.App.Interfaces.Identity;
using Identity.App.Interfaces.Infrastructure;
using Identity.Application.Interfaces.Producers;
using Identity.Domain.DTOs;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Domain.Interfaces.Identity;
using Identity.Domain.Interfaces.Repositories;
using static Identity.Domain.Constants.Constants;

namespace Identity.Application.Services
{
    public class InstitutionAppService : IInstitutionAppService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorizationService _authorizationService;
        private readonly IInstitutionService _institutionService;
        private readonly IUserAppService _userAppService;
        private readonly IMapper _mapper;
        private readonly IInstitutionProducer _institutionProducer;

        public InstitutionAppService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IAuthorizationService authorizationService, IInstitutionService institutionService,
            IUserAppService userAppService, IMapper mapper, IInstitutionProducer institutionProducer)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _authorizationService = authorizationService;
            _institutionService = institutionService;
            _userAppService = userAppService;
            _mapper = mapper;
            _institutionProducer = institutionProducer;
        }

        public async Task<InstitutionResponseDTO?> Get(long id)
        {
            bool canConsult = await _authorizationService.CanConsult();

            if (!canConsult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            Institution? institution = await _institutionService.Get(id);
            return _mapper.Map<InstitutionResponseDTO>(institution);
        }

        public async Task<List<InstitutionResponseDTO>?> GetAll()
        {
            bool canConsult = await _authorizationService.CanConsult();
            if (!canConsult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            List<Institution>? institution = await _institutionService.GetAll();
            return _mapper.Map<List<InstitutionResponseDTO>>(institution);
        }

        public async Task<InstitutionResponseDTO?> Create(CreateInstitutionDTO institutionBody)
        {
            Institution? institution = await _unitOfWork.InstitutionRepository.GetByNameAsync(institutionBody.Name);

            if (institution != null)
            {
                _notificationContext.AddNotification(NotificationKeys.InstitutionNotifications.INSTITUTION_ALREADY_EXITS, string.Empty);
                return null;
            }

            institution = await _institutionService.Create(institutionBody);

            User? userAdmin = await _userAppService.CreateUserAdmin(institutionBody.Abbreviation);

            if (userAdmin != null && institution != null)
            {
                institution.User.Add(userAdmin);
            }

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            if (institution != null)
                await _institutionProducer.CreateUpdateInstitutionProducer(institution);

            return _mapper.Map<InstitutionResponseDTO>(institution);
        }

        public async Task<InstitutionResponseDTO?> Update(UpdateInstitutionDTO institutionToUpdate)
        {
            bool canEdit = await _authorizationService.CanEdit();
            if (!canEdit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            Institution? institution = await _unitOfWork.InstitutionRepository.GetById(institutionToUpdate.Id);

            if (institution == null)
            {
                _notificationContext.AddNotification(NotificationKeys.InstitutionNotifications.INSTITUTION_NOT_FOUND, string.Empty);
                return null;
            }

            institution = await _institutionService.Update(institutionToUpdate, institution);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            if (institution != null)
                await _institutionProducer.CreateUpdateInstitutionProducer(institution);

            return _mapper.Map<InstitutionResponseDTO>(institution);
        }

        public async Task<InstitutionResponseDTO?> Delete(long id)
        {
            bool canDelete = await _authorizationService.CanDelete();
            if (!canDelete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            Institution? institution = await _institutionService.Delete(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            if (institution != null)
                await _institutionProducer.DeleteInstitutionProducer(institution.Id);

            return _mapper.Map<InstitutionResponseDTO>(institution);
        }
    }
}

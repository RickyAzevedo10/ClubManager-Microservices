using Identity.Domain.Interfaces;
using Identity.Domain.Interfaces.Identity;
using Identity.Domain.Interfaces.Repositories;
using FluentValidation;
using Identity.Domain.DTOs;
using Identity.Domain.Entities;
using static Identity.Domain.Constants.Constants;

namespace Identity.Domain.Services.Identity
{
    public class InstitutionService : IInstitutionService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService _entityValidationService;
        private readonly IValidator<CreateInstitutionDTO> _institutionValidator;

        public InstitutionService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IEntityValidationService entityValidationService, IValidator<CreateInstitutionDTO> institutionValidator)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _institutionValidator = institutionValidator;
        }

        public async Task<Institution?> Get(long id)
        {
            Institution? institution = await _unitOfWork.InstitutionRepository.GetByIdAsync(id);

            if (institution == null)
            {
                _notificationContext.AddNotification(NotificationKeys.InstitutionNotifications.INSTITUTION_NOT_FOUND, string.Empty);
                return null;
            }
            return institution;
        }

        public async Task<List<Institution>?> GetAll()
        {
            IEnumerable<Institution>? institutions = await _unitOfWork.InstitutionRepository.GetAllAsync();

            if (institutions == null)
            {
                _notificationContext.AddNotification(NotificationKeys.InstitutionNotifications.INSTITUTION_NOT_FOUND, string.Empty);
                return null;
            }
            return institutions.ToList();
        }

        public async Task<Institution?> Create(CreateInstitutionDTO institutionBody)
        {
            bool validationResult = _entityValidationService.Validate(_institutionValidator, institutionBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.InstitutionNotifications.ERROR_INSTITUTION_CREATED, string.Empty);
                return null;
            }

            Institution institution = new();
            institution.SetAbbreviation(institutionBody.Abbreviation);
            institution.SetName(institutionBody.Name);
            institution.SetLogo(institutionBody.Logo);
            institution.SetFoundationDate(institutionBody.FoundationDate);
            institution.SetAddress(institutionBody.Address);
            institution.SetColors(institutionBody.Colors);
            institution.SetStadiumName(institutionBody.StadiumName);
            institution.SetStadiumCapacity(institutionBody.StadiumCapacity);
            institution.SetStadiumInauguration(institutionBody.StadiumInauguration);
            institution.SetHaveTrainingCenter(institutionBody.HaveTrainingCenter);
            institution.SetTrainingCenterAddress(institutionBody.TrainingCenterAddress);
            institution.SetOfficialWebsiteUrl(institutionBody.OfficialWebsiteUrl);
            institution.SetSocialMediaLinks(institutionBody.SocialMediaLinks);

            return await _unitOfWork.InstitutionRepository.AddAsync(institution);
        }

        public async Task<Institution?> Update(UpdateInstitutionDTO institutionToUpdate, Institution institution)
        {
            CreateInstitutionDTO institutionBody = new() 
            { 
                Name = institutionToUpdate.Name,
                Abbreviation = institutionToUpdate.Abbreviation,
                Logo = institutionToUpdate.Logo,
                FoundationDate = institutionToUpdate.FoundationDate,
                Address = institutionToUpdate.Address,
                Colors = institutionToUpdate.Colors,
                StadiumName = institutionToUpdate.StadiumName,
                StadiumCapacity = institutionToUpdate.StadiumCapacity,
                SocialMediaLinks = institutionToUpdate.SocialMediaLinks,
                StadiumInauguration = institutionToUpdate.StadiumInauguration,
                HaveTrainingCenter = institutionToUpdate.HaveTrainingCenter,
                TrainingCenterAddress = institutionToUpdate.TrainingCenterAddress,
                OfficialWebsiteUrl = institutionToUpdate.OfficialWebsiteUrl
            };
            bool validationResult = _entityValidationService.Validate(_institutionValidator, institutionBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.InstitutionNotifications.ERROR_INSTITUTION_CREATED, string.Empty);
                return null;
            }

            institution.SetAbbreviation(institutionBody.Abbreviation);
            institution.SetName(institutionBody.Name);
            institution.SetLogo(institutionBody.Logo);
            institution.SetFoundationDate(institutionBody.FoundationDate);
            institution.SetAddress(institutionBody.Address);
            institution.SetColors(institutionBody.Colors);
            institution.SetStadiumName(institutionBody.StadiumName);
            institution.SetStadiumCapacity(institutionBody.StadiumCapacity);
            institution.SetStadiumInauguration(institutionBody.StadiumInauguration);
            institution.SetHaveTrainingCenter(institutionBody.HaveTrainingCenter);
            institution.SetTrainingCenterAddress(institutionBody.TrainingCenterAddress);
            institution.SetOfficialWebsiteUrl(institutionBody.OfficialWebsiteUrl);
            institution.SetSocialMediaLinks(institutionBody.SocialMediaLinks);

            _unitOfWork.InstitutionRepository.Update(institution);
            return institution;
        }

        public async Task<Institution?> Delete(long id)
        {
            Institution? institution = await _unitOfWork.InstitutionRepository.GetByIdAsync(id);

            if (institution == null)
            {
                _notificationContext.AddNotification(NotificationKeys.InstitutionNotifications.INSTITUTION_NOT_FOUND, string.Empty);
                return null;
            }

            _unitOfWork.InstitutionRepository.Delete(institution);

            return institution;
        }
    }
}

using Financial.Domain.Interfaces;
using Financial.Domain.Interfaces.Identity;
using Financial.Domain.Interfaces.Repositories;
using Financial.Domain.DTOs;
using Financial.Domain.Entities;
using FluentValidation;
using static Financial.Domain.Constants.Constants;

namespace Financial.Domain.Services.Identity
{
    public class EntityService : IEntityService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService _entityValidationService;
        private readonly IValidator<CreateEntityDTO> _entityValidator;

        public EntityService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IEntityValidationService entityValidationService, IValidator<CreateEntityDTO> entityValidator)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _entityValidator = entityValidator;
        }

        public async Task<Entity?> CreateEntity(CreateEntityDTO entityBody)
        {
            bool validationResult = _entityValidationService.Validate(_entityValidator, entityBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.EntityNotifications.ERROR_ENTITY_CREATED, string.Empty);
                return null;
            }
            Entity entity = new();
            entity.SetClubMemberId(entityBody.ClubMemberId);
            entity.SetPlayerId(entityBody.PlayerId);
            entity.SetInternal(entityBody.Internal);
            entity.SetExternal(entityBody.External);
            entity.SetEntityName(entityBody.EntityName);
            entity.SetEntityType(entityBody.EntityType);

            await _unitOfWork.EntityRepository.AddAsync(entity);
               
            return entity;
        }

        public async Task<Entity?> UpdateEntity(UpdateEntityDTO entityToUpdate, Entity entity)
        {
            CreateEntityDTO entityBody = new()
            {
                Internal = entityToUpdate.Internal,
                External = entityToUpdate.External,
                ClubMemberId = entityToUpdate.ClubMemberId,
                PlayerId = entityToUpdate.PlayerId,
                EntityName = entityToUpdate.EntityName,
                EntityType = entityToUpdate.EntityType
            };

            bool validationResult = _entityValidationService.Validate(_entityValidator, entityBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.EntityNotifications.ERROR_ENTITY_UPDATED, string.Empty);
                return null;
            }

            entity.SetClubMemberId(entityBody.ClubMemberId);
            entity.SetPlayerId(entityBody.PlayerId);
            entity.SetInternal(entityBody.Internal);
            entity.SetExternal(entityBody.External);
            entity.SetEntityName(entityBody.EntityName);
            entity.SetEntityType(entityBody.EntityType);


            _unitOfWork.EntityRepository.Update(entity);
            return entity;
        }

        public async Task<Entity?> DeleteEntity(long id)
        {
            Entity? entity = await _unitOfWork.EntityRepository.GetById(id);

            if (entity == null)
            {
                _notificationContext.AddNotification(NotificationKeys.EntityNotifications.ENTITY_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            _unitOfWork.EntityRepository.Delete(entity);
            return entity;
        }
    }
}

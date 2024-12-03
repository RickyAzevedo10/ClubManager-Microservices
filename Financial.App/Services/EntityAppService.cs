using AutoMapper;
using Financial.App.Services.Infrastructures;
using Financial.Application.Interfaces.Infrastructure;
using Financial.Domain.DTOs;
using Financial.Domain.Entities;
using Financial.Domain.Interfaces;
using Financial.Domain.Interfaces.Identity;
using Financial.Domain.Interfaces.Repositories;
using static Financial.Domain.Constants.Constants;

namespace Financial.Application.Services
{
    public class EntityAppService : IEntityAppService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityService _entityService;
        private readonly IMapper _mapper;
        private readonly IUserPermissionsCachedRepository _userPermissionsCachedRepository;

        public EntityAppService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IEntityService entityService, IMapper mapper, 
            IUserPermissionsCachedRepository userPermissionsCachedRepository)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _entityService = entityService;
            _mapper = mapper;
            _userPermissionsCachedRepository = userPermissionsCachedRepository;
        }

        public async Task<EntityResponseDTO?> CreateEntity(CreateEntityDTO entityBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();
            
            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            Entity? entity = await _entityService.CreateEntity(entityBody);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<EntityResponseDTO>(entity);
        }

        public async Task<EntityResponseDTO?> DeleteEntity(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            Entity? entityDeleted = await _entityService.DeleteEntity(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<EntityResponseDTO>(entityDeleted);
        }

        public async Task<EntityResponseDTO?> UpdateEntity(UpdateEntityDTO entityToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            Entity? entity = null;

            if (entityToUpdate != null && entityToUpdate.Id != null)
                entity = await _unitOfWork.EntityRepository.GetById(entityToUpdate.Id);

            if (entity == null)
            {
                _notificationContext.AddNotification(NotificationKeys.EntityNotifications.ENTITY_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            entity = await _entityService.UpdateEntity(entityToUpdate, entity);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<EntityResponseDTO>(entity);
        }

        public async Task<EntityResponseDTO?> GetEntity(long entityId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            Entity? entity = await _unitOfWork.EntityRepository.GetEntityByID(entityId);

            return _mapper.Map<EntityResponseDTO>(entity);
        }

        public async Task<List<EntityResponseDTO>?> GetAllEntity()
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            IEnumerable<Entity>? allEntity = await _unitOfWork.EntityRepository.GetAllAsync();

            return _mapper.Map<List<EntityResponseDTO>>(allEntity);
        }
    }
}

using AutoMapper;
using Identity.Domain.DTOs;
using Identity.Domain.Entities;

namespace Identity.Application.Mappings
{
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<Institution, InstitutionResponseDTO>();
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserRoles, UserRoleResponseDTO>();
            CreateMap<UserPermissions, UserPermissionResponseDTO>();
        }
    }
}

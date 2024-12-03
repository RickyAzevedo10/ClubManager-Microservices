using AutoMapper;
using Financial.Domain.DTOs;
using Financial.Domain.Entities;

namespace Financial.Application.Mappings
{
    public class FinancialMappingProfile : Profile
    {
        public FinancialMappingProfile()
        {
            CreateMap<Entity, EntityResponseDTO>();
            CreateMap<Expense, ExpenseResponseDTO>();
            CreateMap<Revenue, RevenueResponseDTO>();
        }
    }
}

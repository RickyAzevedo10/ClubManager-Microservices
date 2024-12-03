using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configuration
{
    internal class UserRolesConfiguration : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> entity)
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(u => u.Name).IsUnique();
            SeedData(entity);
        }

        public void SeedData(EntityTypeBuilder<UserRoles> entity)
        {
            // Verificar se já existem dados
            if (!entity.Metadata.GetSeedData().Any())
            {
                entity.HasData(
                    new UserRoles { Id = 1, Name = "Admin", Description = "Função administrativa para gerenciamento da instituição. Acesso total a toda a informação dentro da instituição." },
                    new UserRoles { Id = 2, Name = "Presidente", Description = "Supervisiona todas as operações do clube, toma decisões estratégicas e tem acesso a todos as operações e funcionalidades da aplicação." },
                    new UserRoles { Id = 3, Name = "Diretor Desportivo", Description = "Encarregado das operações futebolísticas, incluindo gestão de treinadores, jogadores, transferências e contratações." },
                    new UserRoles { Id = 4, Name = "Treinador", Description = "Gere a equipe técnica, planeia treinos, táticas de jogo, escolhe a equipa para os jogos e monitoriza o desempenho dos jogadores." },
                    new UserRoles { Id = 5, Name = "Diretor Financeiro", Description = "Gere as finanças do clube, incluindo orçamento, salários, receitas de bilheteira, patrocínios e outras fontes de receita." },
                    new UserRoles { Id = 6, Name = "Gestor de Infraestruturas", Description = "Gere as instalações do clube, incluindo estádios, campos de treino e outras infraestruturas." },
                    new UserRoles { Id = 7, Name = "Secretário", Description = "Trata de toda a documentação e administração necessária para o funcionamento do clube." }
                );
            }
        }
    }
}

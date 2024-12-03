using Financial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financial.Infrastructure.Configuration
{
    internal class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
    {
        public void Configure(EntityTypeBuilder<ExpenseCategory> entity)
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(u => u.Name).IsUnique();
            SeedData(entity);
        }

        public void SeedData(EntityTypeBuilder<ExpenseCategory> entity)
        {
            // Verificar se já existem dados
            if (!entity.Metadata.GetSeedData().Any())
            {
                entity.HasData(
                    new ExpenseCategory { Id = 1, Name = "Salários", Description = "Despesas relacionadas com o pagamento de salários a jogadores, treinadores e funcionários." },
                    new ExpenseCategory { Id = 2, Name = "Material Desportivo", Description = "Custos associados à compra de equipamento e material desportivo necessário para a equipa." },
                    new ExpenseCategory { Id = 3, Name = "Custos de Viagem", Description = "Despesas com viagens e alojamento para jogos fora de casa, incluindo transporte e estadia." },
                    new ExpenseCategory { Id = 4, Name = "Manutenção do Estádio", Description = "Custos associados à manutenção e reparação do estádio e outras infraestruturas do clube." },
                    new ExpenseCategory { Id = 5, Name = "Marketing e Publicidade", Description = "Despesas com campanhas de marketing e publicidade para promover o clube e atrair patrocinadores e adeptos." },
                    new ExpenseCategory { Id = 6, Name = "Despesas Administrativas", Description = "Custos gerais de administração, incluindo despesas com escritório e administração do clube." },
                    new ExpenseCategory { Id = 7, Name = "Seguros", Description = "Despesas com seguros para proteger o clube, jogadores e infraestruturas contra riscos e danos." },
                    new ExpenseCategory { Id = 8, Name = "Honorários e Consultoria", Description = "Custos com serviços de consultoria e honorários profissionais, como advogados e contabilistas." },
                    new ExpenseCategory { Id = 9, Name = "Despesas com Eventos", Description = "Custos relacionados com a organização de eventos especiais, como receções, eventos de angariação de fundos e outros eventos promocionais." },
                    new ExpenseCategory { Id = 10, Name = "Despesas com Formação", Description = "Custos associados com a formação e desenvolvimento contínuo de jogadores e equipa técnica." },
                    new ExpenseCategory { Id = 11, Name = "Despesas de Licenciamento", Description = "Custos relacionados com licenças e autorizações necessárias para operar o clube e participar em competições." },
                    new ExpenseCategory { Id = 12, Name = "Despesas de Utilidades", Description = "Custos com serviços essenciais, como eletricidade, água e gás para as instalações do clube." },
                    new ExpenseCategory { Id = 13, Name = "Despesas de Reparação e Manutenção", Description = "Custos com a reparação e manutenção de equipamentos e infraestruturas do clube." },
                    new ExpenseCategory { Id = 14, Name = "Despesas de Empréstimo", Description = "Custos relacionados com empréstimos e financiamentos, incluindo juros e amortizações." },
                    new ExpenseCategory { Id = 15, Name = "Despesas Variáveis", Description = "Custos não categorizados especificamente, mas que podem incluir diversas despesas operacionais e imprevistos." }
                );
            }
        }
    }
}

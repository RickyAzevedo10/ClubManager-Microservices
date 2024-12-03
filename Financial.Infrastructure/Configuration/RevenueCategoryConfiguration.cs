using Financial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financial.Infrastructure.Configuration
{
    internal class RevenueCategoryConfiguration : IEntityTypeConfiguration<RevenueCategory>
    {
        public void Configure(EntityTypeBuilder<RevenueCategory> entity)
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(u => u.Name).IsUnique();
            SeedData(entity);
        }

        public void SeedData(EntityTypeBuilder<RevenueCategory> entity)
        {
            // Verificar se já existem dados
            if (!entity.Metadata.GetSeedData().Any())
            {
                entity.HasData(
                    new RevenueCategory { Id = 1, Name = "Patrocínios", Description = "Receitas provenientes de acordos com empresas que patrocinam o clube, como patrocínios de camisas ou nomeação do estádio." },
                    new RevenueCategory { Id = 2, Name = "Receitas de Dia de Jogo", Description = "Renda gerada com a venda de bilhetes, merchandising e alimentos e bebidas nos dias de jogo." },
                    new RevenueCategory { Id = 3, Name = "Direitos de Transmissão", Description = "Receitas recebidas pela venda dos direitos de transmissão dos jogos para televisão ou plataformas de streaming." },
                    new RevenueCategory { Id = 4, Name = "Merchandising", Description = "Receita gerada pela venda de produtos relacionados ao clube, como camisas, cachecóis e outros artigos." },
                    new RevenueCategory { Id = 5, Name = "Prémios em Dinheiro", Description = "Dinheiro recebido como prémio por desempenho em competições, como torneios nacionais ou internacionais." },
                    new RevenueCategory { Id = 6, Name = "Parcerias Comerciais", Description = "Receitas provenientes de parcerias comerciais com marcas e empresas para eventos especiais ou produtos conjuntos." },
                    new RevenueCategory { Id = 7, Name = "Taxas de Associação", Description = "Receitas das taxas pagas pelos associados do clube para acesso a benefícios exclusivos, como bilhetes preferenciais ou eventos especiais." },
                    new RevenueCategory { Id = 8, Name = "Direitos de Nomeação do Estádio", Description = "Receitas geradas pela venda dos direitos de nomeação do estádio do clube." },
                    new RevenueCategory { Id = 9, Name = "Transferências de Jogadores", Description = "Receitas provenientes da venda ou empréstimo de jogadores para outros clubes." },
                    new RevenueCategory { Id = 10, Name = "Publicidade", Description = "Receitas geradas pela publicidade dentro do estádio ou em outras plataformas relacionadas ao clube." },
                    new RevenueCategory { Id = 11, Name = "Hospedagem Corporativa", Description = "Receitas obtidas com a venda de pacotes de hospitalidade corporativa, que incluem bilhetes para jogos e serviços adicionais." },
                    new RevenueCategory { Id = 12, Name = "Receitas de Eventos", Description = "Receitas provenientes da realização de eventos especiais no estádio, como concertos ou eventos corporativos." },
                    new RevenueCategory { Id = 13, Name = "Receitas da Academia de Jovens", Description = "Receitas geradas por taxas de inscrição ou desenvolvimento de jovens talentos e futuras transferências." },
                    new RevenueCategory { Id = 14, Name = "Renda de Aluguel", Description = "Receitas obtidas pelo aluguel de instalações do clube, como campos de treino ou áreas do estádio para eventos externos." },
                    new RevenueCategory { Id = 15, Name = "Subsídios e Doações", Description = "Dinheiro recebido de subsídios governamentais, fundações ou doações privadas para apoiar o clube." }
                );


            }
        }
    }
}

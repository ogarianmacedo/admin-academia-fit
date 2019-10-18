using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Mapeamentos
{
    public class ObjetivoMap : IEntityTypeConfiguration<Objetivo>
    {
        public void Configure(EntityTypeBuilder<Objetivo> builder)
        {
            builder.HasKey(o => o.ObjetivoId);

            builder.Property(o => o.Nome).IsRequired();
            builder.Property(o => o.Descricao).HasMaxLength(500).IsRequired();

            builder.HasMany(o => o.Alunos).WithOne(o => o.Objetivo);

            builder.ToTable("Objetivos");
        }
    }
}

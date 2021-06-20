using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Mapeamentos
{
    public class ProfessorMap : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.HasKey(p => p.ProfessorId);
            builder.Property(p => p.Nome).IsRequired();
            builder.Property(p => p.Telefone).IsRequired();
            builder.Property(p => p.Turno).IsRequired();
            builder.Property(p => p.Foto);

            builder.HasMany(p => p.Alunos).WithOne(p => p.Professor);

            builder.ToTable("Professores");
        }
    }
}

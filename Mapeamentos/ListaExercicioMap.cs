using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Mapeamentos
{
    public class ListaExercicioMap : IEntityTypeConfiguration<ListaExercicio>
    {
        public void Configure(EntityTypeBuilder<ListaExercicio> builder)
        {
            builder.HasKey(l => l.ListaExercicioId);

            builder.Property(l => l.Frequencia).IsRequired();
            builder.Property(l => l.Repeticoes).IsRequired();
            builder.Property(l => l.Carga).IsRequired();

            builder.HasOne(l => l.Exercicio).WithMany(l => l.ListaExercicios).HasForeignKey(l => l.ExercicioId);
            builder.HasOne(l => l.Ficha).WithMany(l => l.ListaExercicios).HasForeignKey(l => l.FichaId);

            builder.ToTable("ListasExercicios");
        }
    }
}

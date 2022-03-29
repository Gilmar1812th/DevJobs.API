using devjobs.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace devjobs.api.Persistence
{
    public class DevJobsContext : DbContext
    {
        public DevJobsContext(DbContextOptions<DevJobsContext> context) : base(context)
        {
            
        }
        public DbSet<JobVacancy> JobVacancies { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }

        // Configurar as classes para virarem tabelas
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Definindo chave primária
            builder.Entity<JobVacancy>(e => {
                e.HasKey(jv => jv.Id);
                // configurar o nome da tabela
                //e.ToTable("tb_JobVacancies");
                // Relacionamento (1 vaga de emprego tem muitas aplicações)
                e.HasMany(jv => jv.Applications)
                    // 1 aplicação tem apenas uma vaga
                    .WithOne()
                    // e tem uma chave estrangeira 
                    .HasForeignKey(ja => ja.IdJobVacancy)
                    // exclui registros relacionados - cascade deleta tudo que esta relacionado
                    // Restrict restringe exclusão da tabela relacionada
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<JobApplication>(e => {
                e.HasKey(ja => ja.Id);
            });
        }
    }
}
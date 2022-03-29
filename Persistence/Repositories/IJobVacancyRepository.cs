using devjobs.api.Entities;

namespace DevJobs.API.Persistence.Repositories
{
    public interface IJobVacancyRepository
    {
         List<JobVacancy> GetAll();
         JobVacancy GetById(int id);
         void add(JobVacancy jobVacancy);
         void Update(JobVacancy jobVacancy);
         // JobApplication esta relacionado com JobVacancy
         void addApplication(JobApplication jobApplication);
    }
}
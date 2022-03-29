namespace DevJobs.API.Controllers
{
    using devjobs.api.Entities;
    using devjobs.api.Models;
    using devjobs.api.Persistence;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/job-vacancies/{id}/applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobApplicationsController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        // post api/job-vacancies/1/applications
        [HttpPost]
        public IActionResult Post(int id, AddJobApplicationInputModel model)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            var application = new JobApplication(
                model.ApplicantName,
                model.ApplicantEmail,
                id
            );

            _repository.addApplication(application);

            return NoContent();
        }
    }
}
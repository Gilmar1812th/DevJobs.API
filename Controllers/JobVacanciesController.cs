namespace DevJobs.API.Controllers
{
    using devjobs.api.Entities;
    using devjobs.api.Persistence;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Serilog;

    [Route("api/job-vacancies")]    
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        // injeçao de dependência
        private readonly IJobVacancyRepository _repository;
        public JobVacanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        // GET api/job-vacancies
        [HttpGet]
        public IActionResult GetAll()
        {            
            var jobVacancies = _repository.GetAll();

            return Ok(jobVacancies);
        }

        // GET api/job-vacancies/1        
        [HttpGet("{id}")]        
        public IActionResult GetById(int id)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            return Ok(jobVacancy);
        }

        // POST api/job-vacancies
        /// <summary>
        /// Cadastrar uma vaga de emprego.
        /// </summary>
        /// <remarks>
        /// {
        ///"title": "Dev .NET Jr",
        ///"description": "Vaga para sustenção de aplicações .NET Core.",
        ///"company": "GilmarDev",
        ///"isRemote": true,
        ///"salaryRange": "3000 - 5000"
        ///}
        /// </remarks>
        /// <param name="model">Dados da vaga.</param>
        /// <returns>Retorna o objeto recém criado.</returns>
        /// <response code="201">Sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            Log.Information("POST JobVacancy chamado");

            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );
            
            // para persistir a informação no banco de dados
            _repository.add(jobVacancy);

            // vai retornar o código 201 com as informações inseridas
            return CreatedAtAction(
                "GetById", 
                new { id = jobVacancy.Id },
                jobVacancy);                
        }

        // PUT api/job-vacancies/1
        [HttpPut("{id}")]        
        public IActionResult Put(int id, UpdateJobVacancyInputModel model)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            jobVacancy.Update(model.Title, model.Description);

            _repository.Update(jobVacancy);

            return NoContent();
        }

    }
}
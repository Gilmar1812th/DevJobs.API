namespace devjobs.api.Entities
{
    public class JobVacancy
    {
        public JobVacancy(string title, string description, string company, bool isRemote, string salaryRange)        
        {
            this.Title       = title;
            this.Description = description;
            this.Company     = company;
            this.IsRemote    = isRemote;
            this.SalaryRange = salaryRange;

            CreatedAt        = DateTime.Now;
            Applications     = new List<JobApplication>();
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Company { get; private set; }
        public bool IsRemote { get; private set; }
        public string SalaryRange { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<JobApplication> Applications { get; private set; }

        public void Update(string title, string description){
            Title       = title;
            Description = description;
        }
    }
}
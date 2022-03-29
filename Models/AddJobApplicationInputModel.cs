namespace devjobs.api.Models
{
    public record AddJobApplicationInputModel(
        string ApplicantName, 
        string ApplicantEmail, 
        int IdJobVacancy)
    {}
}
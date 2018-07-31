namespace VacationsBLL.Interfaces
{
    public interface IValidateService
    {
        bool CheckEmail(string email);
        bool CheckEmailOwner(string email, string id);
        bool CheckTeamName(string teamName);
        bool CheckTeam(string teamName, string id);
    }
}
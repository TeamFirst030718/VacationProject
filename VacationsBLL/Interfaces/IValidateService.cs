namespace VacationsBLL.Interfaces
{
    public interface IValidateService
    {
        bool CheckEmailForExisting(string email);
        bool CheckEmailOwner(string email, string id);
    }
}
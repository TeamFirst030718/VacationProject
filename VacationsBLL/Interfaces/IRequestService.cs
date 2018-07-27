using VacationsBLL.DTOs;
namespace VacationsBLL.Interfaces
{
    public interface IAdminRequestService
    {
        RequestProcessDTO GetRequestDataById(string id);
        void SetAdminID(string email);
        RequestDTO[] GetRequests();
        void DenyVacation(RequestProcessResultDTO result);
        void ApproveVacation(RequestProcessResultDTO result);
    }
}
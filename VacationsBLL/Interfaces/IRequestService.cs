using VacationsBLL.DTOs;
namespace VacationsBLL.Interfaces
{
    public interface IRequestService
    {
        RequestProcessDTO GetRequestDataById(string id);
        void SetReviewerID(string email);
        RequestDTO[] GetRequestsForAdmin(string searchKey = null);
        RequestDTO[] GetRequestsForTeamLeader(string searchKey = null);
        void DenyVacation(RequestProcessResultDTO result);
        void ApproveVacation(RequestProcessResultDTO result);
    }
}
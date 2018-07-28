using System.Web;

namespace VacationsBLL.Interfaces
{
    public interface IPhotoUploadService
    {
        void UploadPhoto(HttpPostedFileBase photo, string id);
    }
}
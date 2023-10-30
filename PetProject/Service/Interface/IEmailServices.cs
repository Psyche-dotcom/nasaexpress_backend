using PetProject.Model.DTO;

namespace PetProject.Service.Interface
{
    public interface IEmailServices
    {
        void SendEmail(Message message);
    }
}

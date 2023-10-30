using PetProject.Model.Entities;

namespace PetProject.Service.Interface
{
    public interface IGenerateJwt
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}

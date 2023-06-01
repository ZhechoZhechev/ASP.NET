using ForumApp.Core.Models;

namespace ForumApp.Core.Contracts;

public interface IPostService
{
    Task<IEnumerable<PostViewModel>> GetAllAsync();

    Task AddAsync(PostFormModel postFormModel);

    Task UpdateAsync(int id, PostFormModel postFormModel);

    Task DeleteAsync(int id);

    Task<PostFormModel> GetByIdAsync(int id);
}

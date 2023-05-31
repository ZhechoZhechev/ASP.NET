namespace ForumApp.Core.Services;

using ForumApp.Infrastructure.Common;
using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using ForumApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class PostService : IPostService
{
    private readonly IRepository repo;

    public PostService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task AddAsync(PostFormModel postFormModel)
    {
        var entityToAdd = new Post()
        {
            Title = postFormModel.Title,
            Content = postFormModel.Content
        };

        await repo.AddAsync(entityToAdd);
        await repo.SaveChangesAsync();
    }

    public Task DeleteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PostViewModel>> GetAllAsync()
    {
        return await repo.AllReadonly<Post>()
            .Select(p => new PostViewModel()
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content
            })
            .OrderBy(p => p.Title)
            .ToListAsync();
    }

    public async Task<PostFormModel> GetByIdAsync(int id)
    {
        var postToUpdate = await repo.GetByIdAsync<Post>(id);

        return new PostFormModel()
        {
            Title = postToUpdate.Title,
            Content = postToUpdate.Content
        };
    }

    public async Task UpdateAsync(int id, PostFormModel postFormModel)
    {
        var postToUpdate = await repo.GetByIdAsync<Post>(id);

        postToUpdate.Title = postFormModel.Title;
        postToUpdate.Content = postFormModel.Content;

        await repo.SaveChangesAsync();
    }
}

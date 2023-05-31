namespace ForumApp.Infrastructure.Data.Configure;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    //private readonly IConfiguration config;

    //public PostConfiguration()
    //{
    //}
    //public PostConfiguration(IConfiguration config)
    //{
    //    this.config = config;
    //}
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        List<Post> posts = GetPosts();

        builder.HasData(posts);
    }

    private List<Post> GetPosts()
    {
        //string dataPath = config.GetValue<string>("DataFiles:PostRawData")!;
        //string data = File.ReadAllText(dataPath);
        //var desData = JsonConvert.DeserializeObject<List<Post>>(data);

        //return desData!;

        var data = new List<Post>()
        {
            new Post()
            {
                Id= 1,
                Title = "My first post",
                Content = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit."
            },
            new Post()
            {
                Id= 2,
                Title = "My second post",
                Content = "Integer vitae libero ac risus egestas placerat."
            },
            new Post()
            {
                Id= 3,
                Title = "My second post",
                Content = "Donec quis dui at dolor tempor interdum."
            }
        };

        return data;
    }
}

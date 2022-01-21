using PostsApiDatabase;
using System.Collections.Concurrent;

namespace PostsApi.Services
{
    public interface IPostsAddingService
    {
        public ConcurrentQueue<Post> Cq { get; }
    }
}

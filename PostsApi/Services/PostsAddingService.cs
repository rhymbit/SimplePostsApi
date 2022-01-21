using PostsApiDatabase;
using System.Collections.Concurrent;

namespace PostsApi.Services
{
    public class PostsAddingService : IPostsAddingService
    {
        public ConcurrentQueue<Post>? Cq { get; }

        public PostsAddingService()
        {
            Cq = new ConcurrentQueue<Post>();
        }
    }
}

using PostsApiDatabase;
using System.Linq;

namespace PostsApi.Services.Worker
{
    public class DbSaveWorker : BackgroundService
    {
        private readonly TimeSpan TimeDelay = TimeSpan.FromSeconds(30);
        private PostsDbContext _db;
        private readonly IPostsAddingService _postsAddService;

        public DbSaveWorker(IPostsAddingService postAddingService)
        {
            _db = new PostsDbContext();
            _postsAddService = postAddingService;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!_postsAddService.Cq.IsEmpty)
                {
                    _db.Posts.AddRange(_postsAddService.Cq);
                    var affected = await _db.SaveChangesAsync();
                    if (affected > 0)
                        _postsAddService.Cq.Clear();
                }

                try
                {
                    await Task.Delay(TimeDelay);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}

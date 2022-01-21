using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostsApiDatabase;
using System.ComponentModel.DataAnnotations;
using PostsApi.Services;

namespace PostsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostsDbContext _db;
        private readonly IPostsAddingService _postAddService;
        public PostsController(PostsDbContext dbContext, IPostsAddingService postsAddingService)
        {
            _db = dbContext;
            _postAddService = postsAddingService;
        }

        [HttpGet("/get-posts")]
        public IActionResult GetPosts([FromQuery] int? numberOfPosts)
        {
            var posts = new List<PostDto>();

            if (_postAddService.Cq.Count > 0)
            {
                var queuedPosts = _postAddService.Cq.ToList();
                queuedPosts.Sort((post1, post2) =>
                {
                    if (post1 == null || post2 == null) return 0;
                    TimeSpan? diff = post1.Added - post2.Added;
                    return diff > TimeSpan.Zero ? 1 : -1;
                });

                queuedPosts.ForEach(post =>
                {
                    if (numberOfPosts == 0)
                        return;
                    posts.Add(new PostDto
                    {
                        Title = post.Title,
                        Content = post.Content
                    });

                    numberOfPosts--;
                });
            }

            if (numberOfPosts == 0) return Ok(posts);

            var allPosts = _db.Posts.ToList();
            allPosts.Sort((post1, post2) =>
                {
                    if (post1 == null || post2 == null) return 0;
                    TimeSpan? diff = post1.Added - post2.Added;
                    return diff > TimeSpan.Zero ? 1 : -1;
                });

            allPosts.ForEach(post =>
            {
                if (numberOfPosts == 0)
                    return;
                posts.Add(new PostDto
                {
                    Title = post.Title,
                    Content = post.Content
                });

                numberOfPosts--;
            });

            return Ok(posts);
        }

        [HttpPost("/create-post")]
        public IActionResult CreatePost(PostDto post)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _postAddService.Cq?.Enqueue(new Post
            {
                Title = post.Title,
                Content = post.Content
            });

            return Created("post created", null);

    }

    public class PostDto
    {
        [Required]
        [FromBody]
        [MaxLength(40)]
        public string? Title { get; set; }

        [Required]
        [FromBody]
        [MaxLength(200)]
        public string? Content { get; set; }

    }
}

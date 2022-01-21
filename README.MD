# A simple API to Create and Fetch Posts

- API is Designed to handle multiple concurrent post requests. Data is written 
in bulk rather than with every request. Time delay before writing data could be set in `WorkerService`, default time delay is set to 30 seconds.

### API Endpoints
- #### `/get-posts`
  - Returns json payload with posts data
  - Takes an optional query int parameter `numberOfPosts` to get a desired number of posts. If not provided then returns all the posts
  - Latest posts are always added at the top of the return payload

- ### `/create-post`
  - Requires a json payload in request body, for example: -
  ```
    title: "MyPost"
    content: "lorem-ipsum"
  ```
  - Returns Status-Code `201` with message `"post-created"` if post is created successfully
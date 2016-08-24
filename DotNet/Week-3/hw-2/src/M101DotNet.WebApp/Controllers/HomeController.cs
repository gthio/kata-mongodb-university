using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using M101DotNet.WebApp.Models;
using M101DotNet.WebApp.Models.Home;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace M101DotNet.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var blogContext = new BlogContext();
            // XXX WORK HERE
            // find the most recent 10 posts and order them
            // from newest to oldest

            var connectionString = "mongodb://localhost:27017";

            var client = new MongoClient(connectionString);

            var db = client.GetDatabase("blog");
            var col = db.GetCollection<Post>("posts").Find(x => true == true)
                .SortByDescending(x => x.CreatedAtUtc)
                .Limit(10)
                .Skip(0)
                .ToListAsync();

            col.Wait();

            var model = new IndexModel
            {
                RecentPosts = col.Result
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult NewPost()
        {
            return View(new NewPostModel());
        }

        [HttpPost]
        public async Task<ActionResult> NewPost(NewPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var blogContext = new BlogContext();

            var post = new Post()
            {
                Title = model.Title,
                Content = model.Content,
                Tags = new string[] { model.Tags },
                Author = this.User.Identity.Name,
                Comments = new List<Comment>()
            };

            post.Comments.Add(new Comment() { Content = model.Content,
                Author = this.User.Identity.Name});

            var connectionString = "mongodb://localhost:27017";

            var client = new MongoClient(connectionString);

            var db = client.GetDatabase("blog");
            var col = db.GetCollection<Post>("posts");
            await col.InsertOneAsync(post);

            return RedirectToAction("Post", new { id = post.Id.ToString() });
        }

        [HttpGet]
        public async Task<ActionResult> Post(string id)
        {
            var blogContext = new BlogContext();

            var connectionString = "mongodb://localhost:27017";

            var client = new MongoClient(connectionString);

            var db = client.GetDatabase("blog");
            var col = db.GetCollection<Post>("posts")
                .Find(x => x.Id == ObjectId.Parse(id))
                .ToListAsync();

            col.Wait();

            if (col.Result == null)
            {
                return RedirectToAction("Index");
            }

            var model = new PostModel
            {
                Post = col.Result[0],
                NewComment = new NewCommentModel()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Posts(string tag = null)
        {
            var blogContext = new BlogContext();

            // XXX WORK HERE
            // Find all the posts with the given tag if it exists.
            // Otherwise, return all the posts.
            // Each of these results should be in descending order.

            var connectionString = "mongodb://localhost:27017";

            var client = new MongoClient(connectionString);

            var db = client.GetDatabase("blog");
            var col = db.GetCollection<Post>("posts")
                .Find(x => tag == null || x.Tags.Contains(tag))
                .SortByDescending(x => x.CreatedAtUtc)
                .ToListAsync();

            col.Wait();

            return View(col.Result);
        }

        [HttpPost]
        public async Task<ActionResult> NewComment(NewCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Post", new { id = model.PostId });
            }

            var blogContext = new BlogContext();
            // XXX WORK HERE
            // add a comment to the post identified by model.PostId.
            // you can get the author from "this.User.Identity.Name"

            var connectionString = "mongodb://localhost:27017";

            var client = new MongoClient(connectionString);

            var db = client.GetDatabase("blog");
            var col = db.GetCollection<Post>("posts");
            var docs = col.Find<Post>(x => x.Id == ObjectId.Parse(model.PostId));
            var doc = docs.ToListAsync();

            var post = doc.Result[0];
            var comments = post.Comments;

            var newComment = new Comment()
            {
                Content = model.Content,
                Author = this.User.Identity.Name
            };

            comments.Add(newComment);

            await col.ReplaceOneAsync<Post>(s => s.Id == ObjectId.Parse(model.PostId),
                post);

            return RedirectToAction("Post", new { id = model.PostId });
        }
    }
}
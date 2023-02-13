using DHCore.Models;
using DHData.Models;
using DHMiniSite.Operations.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DHMiniSite.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentOperations commentOperations;

        public CommentsController(ICommentOperations commentOperations)
        {
            this.commentOperations = commentOperations;
        }

        public async Task<IActionResult> Index()
        {
            return View(await commentOperations.GetAsync());
        }

        public async Task<DataList<Comment>> Get(string id)
        {
            return await commentOperations.GetByPostId(id);
        }

        [HttpPost]
        public async Task<string> Add(Comment comment)
        {
            return await commentOperations.Add(comment.PostId, comment);
        }

        public async Task<IActionResult> ChangeApprove(string id)
        {
            var commentDetail = await commentOperations.GetAsync(id);
            if (commentDetail != null)
            {
                await commentOperations.ChangeApprove(commentDetail);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

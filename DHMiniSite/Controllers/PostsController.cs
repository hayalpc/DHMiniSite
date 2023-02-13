using DHData.Models;
using DHMiniSite.Operations.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DHMiniSite.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostOperations postOperations;

        public PostsController(IPostOperations postOperations)
        {
            this.postOperations = postOperations;
        }

        public async Task<IActionResult> Index()
        {
            return View(await postOperations.GetAsync());
        }

        public async Task<IActionResult> Add()
        {
            return View(new Post());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(Post model)
        {
            if (ModelState.IsValid)
            {
                await postOperations.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var postDetail = await postOperations.GetAsync(id);
            if (postDetail != null)
            {
                return View("Add", postDetail);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(string id, Post post)
        {
            if (ModelState.IsValid)
            {
                var postDetail = await postOperations.GetAsync(id);
                if (postDetail != null)
                {
                    postDetail.Title = post.Title;
                    postDetail.Content = post.Content;
                    postDetail.UserName = post.UserName;
                    postDetail.UserSurname = post.UserSurname;
                    postDetail.Email = post.Email;
                    await postOperations.UpdateAsync(id, postDetail);
                }
                return RedirectToAction(nameof(Index));
            }
            return View("Add", post);
        }

        public async Task<IActionResult> ChangeStatus(string id)
        {
            var postDetail = await postOperations.GetAsync(id);
            if (postDetail != null)
            {
                await postOperations.ChangeStatus(postDetail);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(string id)
        {
            var postDetail = await postOperations.GetAsync(id);
            if (postDetail != null)
            {
                return View(postDetail);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

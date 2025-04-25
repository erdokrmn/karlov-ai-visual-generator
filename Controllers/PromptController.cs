using Microsoft.AspNetCore.Mvc;
using SendingPrompt.Services;

namespace SendingPrompt.Controllers
{
    public class PromptController : Controller
    {
        private readonly PromptService _promptService;

        public PromptController(PromptService promptService)
        {
            _promptService = promptService;
        }

        [HttpGet]
        public IActionResult ImagePrompt()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImagePrompt(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                ModelState.AddModelError("prompt", "Prompt boş olamaz.");
                return View();
            }

            var imageBytes = await _promptService.GenerateImageAsync(prompt);
            ViewBag.ImageBase64 = Convert.ToBase64String(imageBytes);
            ViewBag.Prompt = prompt;
            return View();
        }
    }
}

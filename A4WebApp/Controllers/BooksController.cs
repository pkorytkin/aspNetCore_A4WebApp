using A4WebApp.Interfaces;
using A4WebApp.Models;
using A4WebApp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace A4WebApp.Controllers
{
    public class BooksController : Controller
    {
        readonly IConfiguration _configuration;
        readonly BookRepository repository;
        readonly ILogger<BooksController> logger;
        public BooksController(IConfiguration configuration, IDatabaseConnectionFactory dbFactory,ILogger<BooksController> logger)
        {
            _configuration=configuration;
            repository = new BookRepository(configuration,dbFactory.CreateConnection(), logger);
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await repository.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await repository.Add(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }
        [HttpGet]
        public async Task<IActionResult> Get(int bookid)
        {
            return View(await repository.Get(bookid));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await repository.Get(id));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                await repository.Update(book);
            }
            return await Get(book.Id);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            return View(await repository.Get(Id));
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            await repository.Delete(Id);
            return RedirectToAction("Index");
        }
        public new void Dispose()
        {
            base.Dispose();
        }
        bool disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (disposing&&!disposed)
            {
                repository.Dispose();
                disposed = true;
            }
            base.Dispose(disposing);
        }
    }
}

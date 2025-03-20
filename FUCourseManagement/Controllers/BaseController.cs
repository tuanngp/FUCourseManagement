using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace FUCourseManagement.Controllers
{
    public abstract class BaseController<TEntity, TId> : Controller 
        where TEntity : class
    {
        protected readonly IBaseRepository<TEntity, TId> _repository;
        protected readonly string _entityName;

        protected BaseController(IBaseRepository<TEntity, TId> repository)
        {
            _repository = repository;
            _entityName = typeof(TEntity).Name;
        }

        // GET: Entity
        public virtual async Task<IActionResult> Index()
        {
            var entities = await _repository.GetAllAsync();
            ViewData["Title"] = _entityName;
            return View("~/Views/Shared/Generic/Index.cshtml", entities);
        }

        // GET: Entity/Details/5
        public virtual async Task<IActionResult> Details(TId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            ViewData["Title"] = _entityName;
            return View("~/Views/Shared/Generic/Details.cshtml", entity);
        }

        // GET: Entity/Create
        public virtual IActionResult Create()
        {
            ViewData["Title"] = _entityName;
            var entity = Activator.CreateInstance<TEntity>();
            return View("~/Views/Shared/Generic/Create.cshtml", entity);
        }

        // POST: Entity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(entity);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Title"] = _entityName;
            return View("~/Views/Shared/Generic/Create.cshtml", entity);
        }

        // GET: Entity/Edit/5
        public virtual async Task<IActionResult> Edit(TId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            ViewData["Title"] = _entityName;
            return View("~/Views/Shared/Generic/Edit.cshtml", entity);
        }

        // POST: Entity/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(TId id, TEntity entity)
        {
            // Kiểm tra id của entity có khớp với id trong route không
            var entityId = entity.GetType().GetProperty("Id")?.GetValue(entity);
            if (!entityId?.ToString().Equals(id?.ToString()) ?? true)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(entity);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    if (!await EntityExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }

            ViewData["Title"] = _entityName;
            return View("~/Views/Shared/Generic/Edit.cshtml", entity);
        }

        // GET: Entity/Delete/5
        public virtual async Task<IActionResult> Delete(TId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            ViewData["Title"] = _entityName;
            return View("~/Views/Shared/Generic/Delete.cshtml", entity);
        }

        // POST: Entity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> DeleteConfirmed(TId id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        protected virtual async Task<bool> EntityExists(TId id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity != null;
        }
    }
}

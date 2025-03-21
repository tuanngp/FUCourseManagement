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
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] =
                    "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại thông tin nhập.";
                TempData["IsCreateRetry"] = true;
                ViewData["Title"] = _entityName;
                return View("~/Views/Shared/Generic/Create.cshtml", entity);
            }

            try
            {
                await _repository.AddAsync(entity);
                TempData["SuccessMessage"] = $"{_entityName} đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Có lỗi xảy ra khi tạo {_entityName}: {ex.Message}");
                TempData["IsCreateRetry"] = true;
                ViewData["Title"] = _entityName;
                return View("~/Views/Shared/Generic/Create.cshtml", entity);
            }
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
            var entityId = entity
                .GetType()
                .GetProperties()
                .FirstOrDefault(p => p.Name == "Id" || p.Name.EndsWith("Id"))
                ?.GetValue(entity);
            if (!entityId?.ToString().Equals(id?.ToString()) ?? true)
            {
                TempData["ErrorMessage"] = "Không tìm thấy bản ghi với ID tương ứng.";
                return View("~/Views/Shared/Generic/Edit.cshtml", entity);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(entity);
                    TempData["SuccessMessage"] = "Cập nhật bản ghi thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (!await EntityExists(id))
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy bản ghi để cập nhật.";
                        return View("~/Views/Shared/Generic/Edit.cshtml", entity);
                    }
                    TempData["ErrorMessage"] = $"Đã xảy ra lỗi khi cập nhật bản ghi: {ex.Message}";
                }
            }
            else
            {
                TempData["ErrorMessage"] =
                    "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại thông tin nhập.";
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
            try
            {
                var result = await _repository.DeleteAsync(id);
                if (!result)
                {
                    TempData["ErrorMessage"] = $"Không tìm thấy {_entityName} với ID: {id} để xóa.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["SuccessMessage"] = $"{_entityName} đã được xóa thành công.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Có lỗi xảy ra khi xóa {_entityName}: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        protected virtual async Task<bool> EntityExists(TId id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity != null;
        }
    }
}

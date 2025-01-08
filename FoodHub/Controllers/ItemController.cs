using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FoodHub.DAL;
using FoodHub.Models;
using FoodHub.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodHub.Controllers
{
    // Controller for handling item-related requests
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemController> _logger;

        // Constructor accepting the item repository and logger
        public ItemController(IItemRepository itemRepository, ILogger<ItemController> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        // Action method to display items in a table view
        public async Task<IActionResult> Table()
        {
            var items = await _itemRepository.GetAll();
            if (items == null)
            {
                _logger.LogError("[ItemController] Item list not found while executing _itemRepository.GetAll()");
                return NotFound("Item list not found");
            }
            var itemsViewModel = new ItemsViewModel(items, "Table");
            return View(itemsViewModel);
        }

        // Action method to display details of a specific item
        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemRepository.GetItemByIdWithAllergen(id);
            if (item == null)
            {
                _logger.LogError("[ItemController] Item not found for the ItemId {ItemId:0000}", id);
                return NotFound("Item not found for the ItemId");
            }
            // Include allergens when fetching the item
            return View(item);
        }

        // GET: Action method to display the create item form
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var categories = await _itemRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "ItemCategoryId", "Name");
            return View();
        }

        // POST: Action method to handle the creation of a new item
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Item item, IFormFile? ImagePath)
        {
            // Added the total amount of fat must be equal to the sum of saturated and unsaturated fat
            if (item.TotalFat < item.SaturatedFat + item.UnsaturatedFat)
            {
                ModelState.AddModelError("TotalFat", "Total fat must be equal to the sum of saturated fat and unsaturated fat");
            }

            if (ModelState.IsValid)
            {
                if (ImagePath != null && ImagePath.Length > 0)
                {
                    var fileName = Path.GetFileName(ImagePath.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImagePath.CopyToAsync(fileStream);
                    }

                    item.ImagePath = "/images/" + fileName;
                }
                else {
                    ModelState.AddModelError("ImagePath", "Image must be included");
                }

                try
                {
                    bool returnOk = await _itemRepository.Create(item);
                    if (returnOk)
                        return RedirectToAction(nameof(Table));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "[ItemController] Error occurred while creating item {@item}", item);
                    ModelState.AddModelError("Error", "An error occurred while creating the item. Please try again.");
                }
            }
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Validation failed for item {@item}. Errors: {@Errors}", item, ModelState.Values.SelectMany(e => e.Errors));
            }

            var categories = await _itemRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "ItemCategoryId", "Name");
            _logger.LogWarning("[ItemController] Item creation failed {@item}", item);
            return View(item);
        }

        // GET: Action method to display the update item form
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _itemRepository.GetItemById(id);
            if (item == null)
            {
                _logger.LogError("[ItemController] Item not found when updating the ItemId {ItemId:0000}", id);
                return BadRequest("Item not found for the ItemId");
            }

            var categories = await _itemRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "ItemCategoryId", "Name");

            return View(item);
        }

        // POST: Action method to handle the update of an existing item
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(int id, Item item, IFormFile? NewImage)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingItem = await _itemRepository.GetItemById(item.ItemId);
                    if (existingItem == null)
                    {
                        _logger.LogError("[ItemController] Item not found when updating the ItemId {ItemId:0000}", item.ItemId);
                        return NotFound("Item not found for the ItemId");
                    }

                    // Handle image update
                    if (NewImage != null && NewImage.Length > 0)
                    {
                        // Delete old image if it exists
                        if (!string.IsNullOrEmpty(existingItem.ImagePath))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingItem.ImagePath.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Save new image
                        var fileName = Path.GetFileName(NewImage.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await NewImage.CopyToAsync(fileStream);
                        }

                        item.ImagePath = "/images/" + fileName;
                    }

                    bool returnOk = await _itemRepository.Update(item);
                    if (returnOk)
                        return RedirectToAction(nameof(Table));
                    else
                        ModelState.AddModelError("", "Failed to update the item. Please try again.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "[ItemController] Error occurred while updating item {ItemId:0000}", item.ItemId);
                    ModelState.AddModelError("", "An error occurred while updating the item. Please try again.");
                }
            }
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Validation failed for item {@item}. Errors: {@Errors}", item, ModelState.Values.SelectMany(e => e.Errors));
            }

            // If we got this far, something failed, redisplay form
            var categories = await _itemRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "ItemCategoryId", "Name");
            _logger.LogWarning("[ItemController] Item update failed {@item}", item);
            return View(item);
        }

        // GET: Action method to display the delete item confirmation form
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _itemRepository.GetItemById(id);
            if (item == null)
            {
                _logger.LogError("[ItemController] Item not found for the ItemId {ItemId:0000}", id);
                return BadRequest("Item not found for the ItemId");
            }
            return View(item);
        }

        // POST: Action method to handle the deletion of an item
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool returnOk = await _itemRepository.Delete(id);
            if (!returnOk)
            {
                _logger.LogError("[ItemController] Item deletion failed for the ItemId {ItemId:0000}", id);
                return BadRequest("Item deletion failed");
            }
            return RedirectToAction(nameof(Table));
        }
    }
}
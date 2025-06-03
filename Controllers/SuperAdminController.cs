using Microsoft.AspNetCore.Mvc;  // This is required for Controller and IActionResult
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pizza_Shop_.ViewModels;
using Pizza_Shop_.Models;
using Pizza_Shop_.Services;
using ClosedXML.Excel;
using System.Text.RegularExpressions;
using Pizza_Shop_.Services.Interfaces;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
namespace Pizza_Shop_.Controllers
{
    public class SuperAdminController : Controller
    {
        private readonly IMenuItemService _menuItemService;
        private readonly ICategoryService _categoryService;
        private readonly ITableSectionService _tableSectionService;
        private readonly IViewRenderService _viewRenderService;
        public SuperAdminController(IMenuItemService menuItemService, ICategoryService categoryService, ITableSectionService tableSectionService, IViewRenderService viewRenderService)
        {
            _menuItemService = menuItemService;
            _categoryService = categoryService;
            _tableSectionService = tableSectionService;
            _viewRenderService = viewRenderService;
        }

        public IActionResult Dashboard()
        {
            var dashboardViewModel = new DashboardViewModel
            {
                TotalSales = 1630, // Example static value; Replace with dynamic value
                TotalOrders = 1,   // Example static value; Replace with dynamic value
                AvgOrderValue = 1630,  // Example static value; Replace with dynamic value
                AvgWaitingTime = 6.96m, // Example static value; Replace with dynamic value
                RevenueData = new List<decimal> { 100, 200, 300 },  // Example static values; Replace with dynamic data
                CustomerGrowthData = new List<decimal> { 1, 2, 3 },  // Example static values; Replace with dynamic data
                TopSellingItems = new List<SellingItem>
        {
            new SellingItem { Name = "Grilled burger", OrdersCount = 1 },
            new SellingItem { Name = "Margherita", OrdersCount = 1 }
        },  // Example static values; Replace with dynamic data
                LeastSellingItems = new List<SellingItem>
        {
            new SellingItem { Name = "Grilled burger", OrdersCount = 1 },
            new SellingItem { Name = "Margherita", OrdersCount = 1 }
        },  // Example static values; Replace with dynamic data
                WaitingListCount = 5,
                NewCustomersCount = 0
            };
            return View(dashboardViewModel);
        }
        public IActionResult Menu(string searchTerm, int page = 1)
        {
            int pageSize = 5;
            var items = _menuItemService.GetAllMenuItems();
            var categories = _categoryService.GetAllCategories();
            var modifierGroups = _menuItemService.GetAllModifierGroups();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                items = items.Where(m =>
                    m.Name.ToLower().Contains(searchTerm) ||
                    m.Category.ToLower().Contains(searchTerm)).ToList();
            }
            //Added Code
            string? categoryIdStr = Request.Query["SelectedCategory"];
            int categoryId = int.TryParse(categoryIdStr, out var parsedCategoryId) ? parsedCategoryId : 0; //
            string? groupIdStr = Request.Query["SelectedGroup"];
            int groupId = int.TryParse(groupIdStr, out var parsedId) ? parsedId : 0;
            var modifiers = groupId > 0
            ? _menuItemService.GetModifiersByGroupId(groupId).Select(m => new ModifierViewModel.ModifierDetailViewModel
            {
                Name = m.Name,
                Price = m.Price
            }).ToList()
            : new List<ModifierViewModel.ModifierDetailViewModel>();
            if (categoryId > 0)
            {
                items = items.Where(m => m.CategoryId == categoryId).ToList();
            }
            var paginatedItems = items.Skip((page - 1) * pageSize).Take(pageSize)
            .Select(m => new MenuItemViewModel
            {
                MenuItemId = m.MenuItemId,
                CategoryId = m.CategoryId,
                Category = m.Category,
                Name = m.Name,
                ItemTypeIcon = m.ItemTypeIcon,
                Rate = m.Rate,
                Quantity = m.Quantity,
                Available = m.Available
            }).ToList();

            var viewModel = new MenuPageViewModel
            {
                MenuItems = paginatedItems,
                Categories = categories,
                ModifierGroups = modifierGroups,
                Modifiers = modifiers,
                CurrentPage = page,
                TotalItems = items.Count
            };
            return View(viewModel);
        }

        public IActionResult Modifiers(string searchTerm, int page = 1)
        {
            int pageSize = 5;
            var modifierGroups = _menuItemService.GetAllModifierGroups();
            var allModifiers = _menuItemService.GetAllModifierDetails();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                allModifiers = allModifiers.Where(m => m.Name.ToLower().Contains(searchTerm)).ToList();
            }
            var paginatedModifiers = allModifiers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var viewModel = new MenuPageViewModel
            {
                Modifiers = paginatedModifiers,
                ModifierGroups = modifierGroups,
                CurrentPage = page,
                TotalItems = allModifiers.Count
            };
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult AddCategory(AddCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(model);
            }
            return RedirectToAction("Menu");
        }
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _categoryService.GetAllCategories().FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            var viewModel = new EditCategoryViewModel
            {
                Id = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };
            return Json(viewModel);
        }
        [HttpPost]
        public IActionResult EditCategory(EditCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _categoryService.EditCategory(model);
            }
            return RedirectToAction("Menu");
        }
        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction("Menu");
        }
        [HttpGet]
        public IActionResult GetModifiersByGroup(int groupId)
        {
            var modifiers = _menuItemService.GetModifiersByGroupId(groupId);
            return Json(modifiers);
        }
        [HttpPost]
        public IActionResult AddMenuItem(AddMenuItemViewModel model)
        {
            Console.WriteLine("Model is valid: " + ModelState.IsValid);
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("Error: " + error.ErrorMessage);
            }
            if (ModelState.IsValid)
            {
                var menuItem = new MenuItem
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = model.CategoryId,
                    ItemType = model.ItemType,
                    Quantity = model.Quantity,
                    Unit = model.Unit,
                    IsAvailable = model.IsAvailable,
                    TaxPercentage = model.TaxPercentage,
                    DefaultTax = model.DefaultTax,
                    ShortCode = model.ShortCode,
                    Rate = model.Rate
                };
                _menuItemService.InsertMenuItem(menuItem);
                var selectedModifierIdString = Request.Form["SelectedModifierId"].ToString();

                // if (!string.IsNullOrWhiteSpace(selectedModifierIdString))
                // {
                //     int selectedModifierId = int.Parse(selectedModifierIdString);
                //     _menuItemService.InsertMenuItemModifiers(menuItem.MenuItemId, selectedModifierId);
                // }
                // if (model.SelectedModifiers != null && model.SelectedModifiers.Any())
                // {
                //     _menuItemService.InsertMenuItemModifiers(menuItem.MenuItemId, model.SelectedModifiers);
                // }
            }
            return RedirectToAction("Menu");
        }
        [HttpGet]
        public IActionResult GetMenuItem(int id)
        {
            var menuItem = _menuItemService.GetMenuItemById(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            var result = new
            {
                MenuItemId = menuItem.MenuItemId,
                Name = menuItem.Name,
                CategoryId = menuItem.CategoryId,
                ItemType = menuItem.ItemType,
                Price = menuItem.Price,
                Rate = menuItem.Rate,
                Quantity = menuItem.Quantity,
                Unit = menuItem.Unit,
                IsAvailable = menuItem.IsAvailable,
                TaxPercentage = menuItem.TaxPercentage,
                DefaultTax = menuItem.DefaultTax,
                ShortCode = menuItem.ShortCode,
                Description = menuItem.Description
            };
            return Json(result);
        }

        [HttpPost]
        public IActionResult EditMenuItem(AddMenuItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingItem = _menuItemService.GetMenuItemById(model.MenuItemId);
                if (existingItem == null)
                {
                    return NotFound();
                }
                existingItem.Name = model.Name;
                existingItem.CategoryId = model.CategoryId;
                existingItem.ItemType = model.ItemType;
                existingItem.Price = model.Price;
                existingItem.Rate = model.Rate;
                existingItem.Quantity = model.Quantity;
                existingItem.Unit = model.Unit;
                existingItem.IsAvailable = model.IsAvailable;
                existingItem.TaxPercentage = model.TaxPercentage;
                existingItem.DefaultTax = model.DefaultTax;
                existingItem.ShortCode = model.ShortCode;
                existingItem.Description = model.Description;
                _menuItemService.UpdateMenuItem(existingItem);
                return RedirectToAction("Menu");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteItem(string itemIds)
        {
            if (!string.IsNullOrWhiteSpace(itemIds))
            {
                var ids = itemIds.Split(',').Select(int.Parse).ToList();
                foreach (var id in ids)
                {
                    _menuItemService.SoftDeleteMenuItem(id);
                }
            }
            return RedirectToAction("Menu");
        }
        [HttpPost]
        public IActionResult AddModifierGroup(string name, string description, string selectedModifierIds)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {
                return RedirectToAction("Modifiers");
            }
            List<int> modifierIds = new List<int>();
            if (!string.IsNullOrWhiteSpace(selectedModifierIds))
            {
                modifierIds = selectedModifierIds.Split(',').Select(int.Parse).ToList();
            }
            _menuItemService.AddModifierGroup(name, description, modifierIds);
            return RedirectToAction("Modifiers");
        }
        [HttpGet]
        public IActionResult GetModifierSelectionPartial(int page = 1)
        {
            int pageSize = 5;
            var allModifiers = _menuItemService.GetAllModifierDetails();
            var paginatedModifiers = allModifiers
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
            var viewModel = new MenuPageViewModel
            {
                Modifiers = paginatedModifiers,
                CurrentPage = page,
                TotalItems = allModifiers.Count
            };
            return PartialView("_ModifierSelectionPartial", viewModel);
        }
        [HttpGet]
        public IActionResult TablesAndSections(string searchTerm, int page = 1, int pageSize = 5)
        {
            var sections = _tableSectionService.GetAllSections();
            // if (!string.IsNullOrWhiteSpace(searchTerm))
            // {
            //     tables = tables
            //         .Where(s => s.Name != null && s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            //         .ToList();
            // }
            var tables = _tableSectionService.GetAllTables();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                tables = tables
                    .Where(s => s.Table_number != null && s.Table_number.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            var totalItems = tables.Count();
            var paginatedTables = tables
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
            var viewModel = new SectionPageViewModel
            {
                Sections = sections,
                Tables = paginatedTables,
                CurrentPage = page,
                TotalItems = totalItems
            };
            return View("TablesAndSections", viewModel);
        }
        [HttpPost]
        public IActionResult AddSection(AddSectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                _tableSectionService.AddSection(model);
            }
            return RedirectToAction("TablesandSections");
        }

        [HttpGet]
        public IActionResult EditSection(int id)
        {
            var section = _tableSectionService.GetAllSections().FirstOrDefault(s => s.SectionId == id);
            if (section == null)
            {
                return NotFound();
            }
            var viewModel = new EditSectionViewModel
            {
                Id = section.SectionId,
                Name = section.Name,
                Description = section.Description
            };
            return Json(viewModel);
        }


        [HttpPost]
        public IActionResult EditSection(EditSectionViewModel model)
        {
            Console.WriteLine("ModelState.IsValid: " + ModelState.IsValid);
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state != null)
                    {
                        foreach (var error in state.Errors)
                        {
                            Console.WriteLine($"Error in '{key}': {error.ErrorMessage}");
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                Console.WriteLine($"Editing Section - ID: {model.Id}, Name: {model.Name}, Description: {model.Description}");
                _tableSectionService.UpdateSection(model);
            }
            return RedirectToAction("TablesAndSections");
        }

        [HttpPost]
        public IActionResult DeleteSection(int sectionId)
        {
            _tableSectionService.SoftDeleteSection(sectionId);
            return RedirectToAction("TablesandSections");
        }
        [HttpPost]
        public IActionResult AddTable(TableViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    var key = entry.Key;
                    var errors = entry.Value?.Errors;
                    if (errors != null && errors.Count > 0)
                    {
                        foreach (var error in errors)
                        {
                            Console.WriteLine($"Model Error - Key: {key}, Error: {error.ErrorMessage}");
                        }
                    }
                }
                return BadRequest("Invalid data.");
            }
            _tableSectionService.AddTable(model);
            return RedirectToAction("TablesandSections");
        }
        // [HttpPost]
        // public IActionResult DeleteTable(int tableId)
        // {
        //     _tableSectionService.SoftDeleteTables(tableId);
        //     return RedirectToAction("TablesandSections");
        // }
        [HttpPost]
        public IActionResult DeleteTable(string tableIds)
        {
            if (!string.IsNullOrWhiteSpace(tableIds))
            {
                var ids = tableIds.Split(',').Where(id => int.TryParse(id, out _)).Select(int.Parse).ToList();
                foreach (var id in ids)
                {
                    _tableSectionService.SoftDeleteTables(id);
                }
            }
            return RedirectToAction("TablesandSections");
        }
        [HttpGet]
        public IActionResult UpdateTable(int id)
        {
            var table = _tableSectionService.GetAllTables().FirstOrDefault(t => t.TableId == id);
            if (table == null)
            {
                return NotFound();
            }
            var viewModel = new EditTableViewModel
            {
                Id = table.TableId,
                Table_number = table.Table_number,
                Capacity = table.Capacity,
                Status = table.Status,
                SectionId = table.SectionId
            };
            return Json(viewModel);
        }
        [HttpPost]
        public IActionResult EditTable(EditTableViewModel model)
        {
            Console.WriteLine("ModelState.IsValid: " + ModelState.IsValid);
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state != null)
                    {
                        foreach (var error in state.Errors)
                        {
                            Console.WriteLine($"Error in '{key}': {error.ErrorMessage}");
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                Console.WriteLine($"Editing Section - ID: {model.Id}, Table_number: {model.Table_number}, Capacity: {model.Capacity}, Status: {model.Status}");
                _tableSectionService.UpdateTable(model);
            }
            return RedirectToAction("TablesandSections");
        }
        public IActionResult TaxesandFees()
        {
            var taxes = _tableSectionService.GetAllTaxes();
            return View(taxes);
        }

        [HttpPost]
        public IActionResult CreateTax(CreateTaxViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    Console.WriteLine($"Field: {state.Key}");
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($" - Error: {error.ErrorMessage}");
                    }
                }

                return RedirectToAction("TaxesandFees");
            }
            Console.WriteLine($"Is_enabled: {model.Is_enabled}, Is_default: {model.Is_default}");
            _tableSectionService.CreateTax(model);
            return RedirectToAction("TaxesandFees");
        }
        [HttpGet]
        public IActionResult GetTaxById(int id)
        {
            var tax = _tableSectionService.GetAllTaxes().FirstOrDefault(t => t.Id == id);
            if (tax == null) return NotFound();
            var model = new EditTaxViewModel
            {
                Id = tax.Id,
                Name = tax.Name,
                Type = tax.Type,
                Value = tax.Value,
                Is_enabled = tax.Is_enabled,
                Is_default = tax.Is_default
            };
            return Json(model);
        }
        [HttpPost]
        public IActionResult UpdateTax(EditTaxViewModel model)
        {
            Console.WriteLine("ModelState.IsValid: " + ModelState.IsValid);
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state != null)
                    {
                        foreach (var error in state.Errors)
                        {
                            Console.WriteLine($"Error in '{key}': {error.ErrorMessage}");
                        }
                    }
                }
                return RedirectToAction("TaxesandFees");
            }
            Console.WriteLine($"Editing Tax - ID: {model.Id}, Name: {model.Name}, Type: {model.Type}, Value: {model.Value}, Is_enabled: {model.Is_enabled}, Is_default: {model.Is_default}");
            _tableSectionService.UpdateTax(model);
            return RedirectToAction("TaxesandFees");
        }
        // [HttpPost]
        // public IActionResult DeleteTax(int id)
        // {
        // _tableSectionService.SoftDeleteTaxes(id);
        // return RedirectToAction("TaxesandFees");
        // }
        [HttpPost]
        public IActionResult DeleteTax(int id)
        {
            Console.WriteLine($"[DeleteTax] ModelState.IsValid: {ModelState.IsValid}");

            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state != null && state.Errors.Count > 0)
                    {
                        foreach (var error in state.Errors)
                        {
                            Console.WriteLine($"[ModelState Error] Field: {key}, Error: {error.ErrorMessage}");
                        }
                    }
                }
            }
            Console.WriteLine($"[DeleteTax] Received ID: {id}");
            _tableSectionService.SoftDeleteTaxes(id);
            return RedirectToAction("TaxesandFees");
        }
        public IActionResult Orders(string searchTerm, string fromDate, string toDate, string status, string sortBy, string sortOrder, int page = 1)
        {
            Console.WriteLine("🔍 Received Status: " + status);
            int pageSize = 5;
            var orders = _tableSectionService.GetAllOrders();
            if (!string.IsNullOrEmpty(status) && status != "All Status")
            {
                orders = orders.Where(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                orders = orders.Where(o =>
                    o.CustomerName.ToLower().Contains(searchTerm) ||
                    o.OrderNumber.ToLower().Contains(searchTerm) ||
                    o.Status.ToLower().Contains(searchTerm) ||
                    o.PaymentMode.ToLower().Contains(searchTerm)
                ).ToList();
            }
            if (DateTime.TryParseExact(fromDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var from))
            {
                orders = orders.Where(o => o.OrderDate >= from).ToList();
            }
            if (DateTime.TryParseExact(toDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var to))
            {
                orders = orders.Where(o => o.OrderDate <= to).ToList();
            }
            sortBy = (sortBy ?? "").ToLower();
            sortOrder = (sortOrder ?? "").ToLower();
            orders = sortBy switch
            {
                "ordernumber" => sortOrder == "desc" ? orders.OrderByDescending(o => o.OrderNumber).ToList() : orders.OrderBy(o => o.OrderNumber).ToList(),
                "orderdate" => sortOrder == "desc" ? orders.OrderByDescending(o => o.OrderDate).ToList() : orders.OrderBy(o => o.OrderDate).ToList(),
                "customername" => sortOrder == "desc" ? orders.OrderByDescending(o => o.CustomerName).ToList() : orders.OrderBy(o => o.CustomerName).ToList(),
                "totalamount" => sortOrder == "desc" ? orders.OrderByDescending(o => o.TotalAmount).ToList() : orders.OrderBy(o => o.TotalAmount).ToList(),
                _ => orders
            };
            var paginatedOrders = orders
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var viewModel = new PaginatedOrderListViewModel
            {
                Orders = paginatedOrders,
                CurrentPage = page,
                TotalItems = orders.Count
            };
            return View(viewModel);
        }
        public IActionResult ExportToExcel(string status, string searchTerm, string fromDate, string toDate)
        {
            // Parse date range
            DateTime? from = DateTime.TryParseExact(fromDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var f) ? f : null;
            DateTime? to = DateTime.TryParseExact(toDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var t) ? t : null;
            // Get all orders
            var orders = _tableSectionService.GetAllOrders();

            // Apply filters
            if (!string.IsNullOrEmpty(status) && status != "All Status")
                orders = orders.Where(o => o.Status == status).ToList();

            if (!string.IsNullOrEmpty(searchTerm))
                orders = orders.Where(o => o.CustomerName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            if (from.HasValue)
                orders = orders.Where(o => o.OrderDate >= from.Value).ToList();

            if (to.HasValue)
                orders = orders.Where(o => o.OrderDate <= to.Value).ToList();

            // Create Excel
            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add("Orders_Report");
            // Write header
            var row = 1;
            sheet.Cell(row, 1).Value = "Order ID";
            sheet.Cell(row, 2).Value = "Date";
            sheet.Cell(row, 3).Value = "Customer";
            sheet.Cell(row, 4).Value = "Status";
            sheet.Cell(row, 5).Value = "Payment Mode";
            sheet.Cell(row, 6).Value = "Rating";
            sheet.Cell(row, 7).Value = "Total Amount";
            sheet.Range(row, 1, row, 7).Style.Font.Bold = true;
            // Write data
            foreach (var o in orders)
            {
                row++;
                sheet.Cell(row, 1).Value = o.OrderNumber;
                sheet.Cell(row, 2).Value = o.OrderDate.ToString("dd-MM-yyyy");
                sheet.Cell(row, 3).Value = o.CustomerName;
                sheet.Cell(row, 4).Value = o.Status;
                sheet.Cell(row, 5).Value = o.PaymentMode;
                sheet.Cell(row, 6).Value = o.Rating;
                sheet.Cell(row, 7).Value = o.TotalAmount;
            }
            sheet.Columns().AdjustToContents();
            // Return file
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "Orders_Report.xlsx");
        }

        public async Task<IActionResult> DownloadInvoice(string orderNumber)
        {
            // Step 1: Get only the items from service (List<InvoiceDummyData>)
            var items = _tableSectionService.GetInvoiceItems(orderNumber);
            if (items == null || !items.Any())
                return NotFound();

            // Step 2: Manually wrap into OrderInvoiceViewModel
            var model = new OrderInvoiceViewModel
            {
                OrderNumber = items.First().OrderNumber,
                OrderDate = DateTime.Now, // Or load from DB later
                CustomerName = "Demo User", // Static or from DB
                PaymentMode = "Cash",       // Static or from DB
                Section = "Ground Floor",   // Optional
                TableName = "T1",           // Optional
                InvoiceItems = items,
                SubTotal = items.Sum(x => x.TotalPrice),
                CGST = 10,
                SGST = 10,
                GST = 0,
                Other = 0
            };

            // Step 3: Render Razor view
            string htmlContent = await _viewRenderService.RenderToStringAsync("SuperAdmin/ExportPdf", model);

            // Step 4: Convert to PDF using iTextSharp
            using var stream = new MemoryStream();
            var doc = new iTextSharp.text.Document();
            var writer = PdfWriter.GetInstance(doc, stream);
            doc.Open();
            using (var stringReader = new StringReader(htmlContent))
            {
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, stringReader);
            }
            doc.Close();
            // Step 5: Return the PDF file
            byte[] bytes = stream.ToArray();
            return File(bytes, "application/pdf", $"Invoice_{model.OrderNumber}.pdf");
        }

        public IActionResult ViewInvoice(string orderNumber)
        {
            // Step 1: Get only the items
            var items = _tableSectionService.GetInvoiceItems(orderNumber);
            if (items == null || !items.Any())
                return NotFound();
            // Step 2: Wrap them into the full view model
            var model = new OrderInvoiceViewModel
            {
                OrderNumber = items.First().OrderNumber,
                OrderDate = DateTime.Now, // You can update this later
                CustomerName = "Demo User",
                PaymentMode = "Cash",
                Section = "Ground Floor",
                TableName = "T1",
                InvoiceItems = items,
                SubTotal = items.Sum(x => x.TotalPrice),
                CGST = 10,
                SGST = 10,
                GST = 0,
                Other = 0
            };
            // Step 3: Return the View
            return View("ViewInvoice", model);
        }
        public IActionResult Customers(string searchTerm, string fromDate, string toDate, string sortBy, string sortOrder, int page = 1)
        {
            Console.WriteLine($"[Customers] Received FromDate: {fromDate}, ToDate: {toDate}");
            var customers = _tableSectionService.GetAllCustomers();
            // Filter by searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                customers = customers.Where(c =>
                    c.Name.ToLower().Contains(searchTerm) ||
                    c.Email.ToLower().Contains(searchTerm) ||
                    c.PhoneNumber.ToLower().Contains(searchTerm)
                ).ToList();
            }
            //Date filter 
            if (DateTime.TryParseExact(fromDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var from))
            {
                customers = customers.Where(c => c.CreatedDate >= from).ToList();
            }
            if (DateTime.TryParseExact(toDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var to))
            {
                customers = customers.Where(c => c.CreatedDate <= to).ToList();
            }
            // Sorting logic
            sortBy = (sortBy ?? "").ToLower();
            sortOrder = (sortOrder ?? "").ToLower();
            customers = sortBy switch
            {
                "name" => sortOrder == "desc"
                    ? customers.OrderByDescending(c => c.Name).ToList()
                    : customers.OrderBy(c => c.Name).ToList(),

                "createddate" => sortOrder == "desc"
                    ? customers.OrderByDescending(c => c.CreatedDate).ToList()
                    : customers.OrderBy(c => c.CreatedDate).ToList(),

                "totalorders" => sortOrder == "desc"
                    ? customers.OrderByDescending(c => c.TotalOrders).ToList()
                    : customers.OrderBy(c => c.TotalOrders).ToList(),

                _ => customers
            };
            int pageSize = 5;
            var paginatedList = customers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var viewModel = new PaginatedCustomerListViewModel
            {
                Customers = paginatedList.Select(c => new CustomerListViewModel
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    CreatedDate = c.CreatedDate,
                    TotalOrders = c.TotalOrders
                }).ToList(),
                CurrentPage = page,
                TotalItems = customers.Count
            };
            return View(viewModel);
        }
        public IActionResult ExportCustomersToExcel(string searchTerm, string fromDate, string toDate)
        {
            Console.WriteLine($"[Export] Received FromDate: {fromDate}, ToDate: {toDate}");
            var customers = _tableSectionService.GetAllCustomers();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                customers = customers.Where(c =>
                c.Name.ToLower().Contains(searchTerm) ||
                c.Email.ToLower().Contains(searchTerm) ||
                c.PhoneNumber.ToLower().Contains(searchTerm)
                ).ToList();
            }
            if (DateTime.TryParseExact(fromDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var from))
            {
                customers = customers.Where(c => c.CreatedDate >= from).ToList();
            }
            if (DateTime.TryParseExact(toDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var to))
            {
                customers = customers.Where(c => c.CreatedDate <= to).ToList();
            }
            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add("Customers_Report");
            // Header Row
            var row = 1;
            sheet.Cell(row, 1).Value = "Name";
            sheet.Cell(row, 2).Value = "Email";
            sheet.Cell(row, 3).Value = "Phone Number";
            sheet.Cell(row, 4).Value = "Date";
            sheet.Cell(row, 5).Value = "Total Orders";
            sheet.Range(row, 1, row, 5).Style.Font.Bold = true;
            // Data Rows
            foreach (var c in customers)
            {
                row++;
                sheet.Cell(row, 1).Value = c.Name;
                sheet.Cell(row, 2).Value = c.Email;
                sheet.Cell(row, 3).Value = c.PhoneNumber;
                sheet.Cell(row, 4).Value = c.CreatedDate.ToString("dd-MM-yyyy");
                sheet.Cell(row, 5).Value = c.TotalOrders;
            }
            sheet.Columns().AdjustToContents();
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "Customers_Report.xlsx");
        }
        [HttpGet]
        public IActionResult GetCustomerHistory(int customerId)
        {
        Console.WriteLine("Received customerId: " + customerId); 
        var result = _tableSectionService.GetCustomerHistoryById(customerId);
        if (result == null)
        return NotFound();
        return Json(result);
        }
    }
}

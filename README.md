ntroduction

Design patterns are proven solutions to common problems in software design. In ASP.NET MVC applications, implementing these patterns can help improve code maintainability, scalability, and testability. This article explores key design patterns used in ASP.NET MVC projects and provides practical examples of their implementation.

1. Repository Pattern
The repository pattern abstracts data access logic, providing a clean separation between the business logic and the data layer.

Benefits:

Promotes loose coupling between the application and data storage.
Simplifies unit testing by allowing mocking of the data layer.
Implementation:

// IRepository Interface
public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(int id);
}

// Repository Implementation
public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private DbSet<T> entities;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        entities = context.Set<T>();
    }

    public IEnumerable<T> GetAll() => entities.ToList();
    public T GetById(int id) => entities.Find(id);
    public void Insert(T entity) => entities.Add(entity);
    public void Update(T entity) => _context.Entry(entity).State = EntityState.Modified;
    public void Delete(int id) => entities.Remove(GetById(id));
}
C#
2. Unit of Work Pattern
The unit of work pattern helps manage transactions by coordinating changes across multiple repositories in a single transaction.

Benefits:

Ensures consistency across repositories.
Reduces redundant calls to the database.
Implementation:

public class UnitOfWork : IDisposable
{
    private readonly ApplicationDbContext _context;
    private IRepository<Product> _productRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IRepository<Product> ProductRepository
    {
        get
        {
            return _productRepository ??= new Repository<Product>(_context);
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
C#
3. Dependency Injection (DI)
Dependency injection (DI) injects dependencies into controllers or classes instead of creating them directly.

Benefits

Reduces tight coupling.
Simplifies testing by allowing dependency substitution.
Implementation

Configure DI in Startup.cs:

public void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IRepository<Product>, Repository<Product>>();
    services.AddScoped<UnitOfWork>();
    services.AddControllersWithViews();
}
C#
Inject Dependencies in Controller:

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
C#
Products Controller

using System.Linq;
using System.Web.Mvc;
using MVCDesignPatternsApp.Models;
using MVCDesignPatternsApp.Repositories;

public class ProductsController : Controller
{
    private readonly UnitOfWork _unitOfWork;

    public ProductsController(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: Products
    public ActionResult Index()
    {
        var products = _unitOfWork.ProductRepository.GetAll().ToList();
        return View(products);
    }

    // GET: Products/Details/5
    public ActionResult Details(int id)
    {
        var product = _unitOfWork.ProductRepository.GetById(id);
        if (product == null)
        {
            return HttpNotFound();
        }
        return View(product);
    }

    // GET: Products/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.ProductRepository.Insert(product);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        return View(product);
    }

    // GET: Products/Edit/5
    public ActionResult Edit(int id)
    {
        var product = _unitOfWork.ProductRepository.GetById(id);
        if (product == null)
        {
            return HttpNotFound();
        }
        return View(product);
    }

    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        return View(product);
    }

    // GET: Products/Delete/5
    public ActionResult Delete(int id)
    {
        var product = _unitOfWork.ProductRepository.GetById(id);
        if (product == null)
        {
            return HttpNotFound();
        }
        return View(product);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        _unitOfWork.ProductRepository.Delete(id);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _unitOfWork.Dispose();
        }
        base.Dispose(disposing);
    }
}
C#
Views
Ensure you have corresponding views in the Views/Products/ folder:

Index.cshtml: Display the product list.
Details.cshtml: Show product details.
Create.cshtml: Form to add a new product.
Edit.cshtml: Form to update product information.
Delete.cshtml: Confirm product deletion.
View Example for Index.cshtml:
@model IEnumerable<YourNamespace.Models.Product>

<h2>Product List</h2>

<p>
    @Html.ActionLink("Create New Product", "Create")
</p>

<table class="table">
    <thead>
        <tr>
            <th>Name</th>
            <th>Price</th>
            <th>Stock Quantity</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
    @foreach (var item in Model) {
        <tr>
            <td>@item.Name</td>
            <td>@item.Price</td>
            <td>@item.StockQuantity</td>
            <td>
                @Html.ActionLink("Edit", "Edit", new { id = item.Id }) |
                @Html.ActionLink("Details", "Details", new { id = item.Id }) |
                @Html.ActionLink("Delete", "Delete", new { id = item.Id })
            </td>
        </tr>
    }
    </tbody>
</table>
C#
4. Factory Pattern
The factory pattern centralizes object creation logic.

Benefits

Decouples object creation from usage.
Promotes flexibility for varying object requirements.
Implementation

Factory Implementation:

public interface IProductService
{
    void ProcessOrder();
}

public class PhysicalProductService : IProductService
{
    public void ProcessOrder() => Console.WriteLine("Processing physical product order.");
}

public class DigitalProductService : IProductService
{
    public void ProcessOrder() => Console.WriteLine("Processing digital product order.");
}

public class ProductServiceFactory
{
    public IProductService GetProductService(string productType)
    {
        return productType switch
        {
            "Physical" => new PhysicalProductService(),
            "Digital" => new DigitalProductService(),
            _ => throw new ArgumentException("Invalid product type")
        };
    }
}
C#
Usage in Controller:

public class OrdersController : Controller
{
    private readonly ProductServiceFactory _factory;

    public OrdersController(ProductServiceFactory factory)
    {
        _factory = factory;
    }

    public void CreateOrder(string productType)
    {
        var service = _factory.GetProductService(productType);
        service.ProcessOrder();
    }
}
C#
5. Singleton Pattern
The singleton pattern ensures only one instance of a class is created and shared.

Benefits

Ideal for shared resources like logging or caching.
Ensures a single point of control.
Implementation

Singleton Class:

public sealed class LogManager
{
    private static readonly Lazy<LogManager> instance = new(() => new LogManager());

    private LogManager() { }

    public static LogManager Instance => instance.Value;

    public void Log(string message) => Console.WriteLine($"Log: {message}");
}
C#
6. Command Pattern
The command pattern encapsulates a request as an object, allowing for more complex request handling.

Benefits

Supports undoable operations.
Decouples request handling from request execution.
Implementation

Command Interface and Implementation:

public interface ICommand
{
    void Execute();
}

public class SaveOrderCommand : ICommand
{
    public void Execute() => Console.WriteLine("Order saved.");
}

public class CancelOrderCommand : ICommand
{
    public void Execute() => Console.WriteLine("Order canceled.");
}
C#
Invoker Class:

public class CommandInvoker
{
    private readonly List<ICommand> _commands = new();

    public void AddCommand(ICommand command) => _commands.Add(command);

    public void ExecuteCommands()
    {
        foreach (var command in _commands)
        {
            command.Execute();
        }
        _commands.Clear();
    }
}
C#
GitHub Project Link
https://github.com/SardarMudassarAliKhan/MVCDesignPatternsAp

Conclusion
Design patterns enhance the development of ASP.NET MVC applications by promoting best practices, reducing redundancy, and improving maintainability. By implementing patterns such as Repository, Unit of work, dependency injection, factory, singleton, and command, you can build scalable and testable applications.
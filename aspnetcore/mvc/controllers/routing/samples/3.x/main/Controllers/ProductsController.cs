﻿// #define First
//#define Second
//#define Third
//#define Fourth
//#define Five
//#define Six
//#define Seven
#define Eight


using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMvcRouting.Controllers
{
#if First
    #region snippet
    [Route("[controller]/[action]")]
    public class ProductsController : Controller
    {
    #region snippet10
        [HttpGet]
        public IActionResult List() {
    #endregion
            return View("Generic");
        }

    #region snippet11
        [HttpGet("{id}")]
        public IActionResult Edit(int id) {
    #endregion
            ViewData["Message"] = id.ToString();
            return View("Generic");
        }
    }
    #endregion
    // Was products2 controller
#elif Second
    #region snippet20
    public class ProductsController : Controller
    {
    #region snippet21
        [HttpGet("[controller]/[action]")]  // Matches '/Products/List'
        public IActionResult List()
        {
    #endregion
            return View("Generic");
        }

        [HttpGet("[controller]/[action]/{id}")]   // Matches '/Products/Edit/{id}'
        public IActionResult Edit(int id)
        {
            ViewData["Message"] = id.ToString();
            return View("Generic");
        }
    }
    #endregion
#elif Third
     [Route("[controller]")]
    public class ProductsController : Controller
    {
        [Route("")] // Matches 'Products'
        [Route("Index")] // Matches 'Products/Index'
        public IActionResult Index()
        {
            // ...
            return View("Generic");
        }
    }
#elif Fourth
    // Test with StartupAPI
    #region snippet4
    [ApiController]
    [Route("api/[controller]")]
    public abstract class MyBaseController : Controller
    {
    }

    public class ProductsController : MyBaseController
    {
        [HttpGet] // Matches '/api/Products'
        public IActionResult List()
        {
            return Content("Using BaseController List");
        }

        [HttpPut("{id}")] // Matches '/api/Products/{id}'
        public IActionResult Edit(int id)
        {
            return Content($"Using BaseController Edit/{id}");
        }
    }
    #endregion
#elif Five
    // Test with StartupDefaultMVC
    #region snippet5
    [ApiController]
    [Route("api/[controller]/[action]", Name = "[controller]_[action]")]
    public abstract class MyBaseController : Controller
    {
    }

    public class ProductsController : MyBaseController
    {
        [HttpGet]
        public IActionResult List()
        {
            var routeName = ControllerContext.ActionDescriptor.AttributeRouteInfo.Name;
            return Content($"List- route name:{routeName}");
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var routeName = ControllerContext.ActionDescriptor.AttributeRouteInfo.Name;
            return Content($"Edit - Route name: {routeName}, ID = {id.ToString()}");
        }
    }
    #endregion
#elif Six
    #region snippet6
    [Route("Store")]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        [HttpPost("Buy")]       // Matches 'Products/Buy' and 'Store/Buy'
        [HttpPost("Checkout")]  // Matches 'Products/Checkout' and 'Store/Checkout'
        public IActionResult Buy()
        {
            var template = ControllerContext.ActionDescriptor.AttributeRouteInfo.Template;
            return Content($"Buy- template:{template}");
        }
    }
    #endregion
#elif Seven
    #region snippet7
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        [HttpPut("Buy")]        // Matches PUT 'api/Products/Buy'
        [HttpPost("Checkout")]  // Matches POST 'api/Products/Checkout'
        public IActionResult Buy()
        {
            var path = Request.Path.Value;
            return Content($"Buy- Path:{path}");
        }
    }
    #endregion
#elif Eight
    // test with POST /product/3
    #region snippet8
    public class ProductsController : Controller
    {
        [HttpPost("product/{id:int}")]
        public IActionResult ShowProduct(int id)
        {
            return Content($"ShowProduct- id:{id}");
        }
    }
    #endregion
#endif
}

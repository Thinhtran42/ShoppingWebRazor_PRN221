using System;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingWeb.Razor.Controllers
{
	public class BaseController<T> : Controller
	{
        protected ILogger<T> _logger;

        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}


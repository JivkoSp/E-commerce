using BooksPlace.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Controllers.ApiControllers
{
    [ApiController]
    [Route("Admin/api/user")]
    [Route("api/user")]
    public class UserSearchController : ControllerBase
    {
        private IUnitOfWork UnitOfWork;

        public UserSearchController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public IActionResult Search()
        {
            try
            {
                string searchTerm = HttpContext.Request.Query["term"].ToString();
                var response = UnitOfWork.User.SearchUserNames(searchTerm);

                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

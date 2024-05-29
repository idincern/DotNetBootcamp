using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.DTOs;
using System.Net;

namespace NetBootcamp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        // GET, PUT, DELETE
        public IActionResult CreateActionResult<T>(ResponseModelDto<T> response)
        {
            if (response.StatusCodes == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = 204 }; // PUT, DELETE: Response bodysi boş
            }
            return new ObjectResult(response) { StatusCode = (int)response.StatusCodes }; // GET
        }

        // POST
        public IActionResult CreateActionResult<T>(ResponseModelDto<T> response, string methodName,
            object routeValues)
        {
            if (response.StatusCodes == HttpStatusCode.Created)
            {
                return CreatedAtAction(methodName, routeValues, response); // POST
            }
            return new ObjectResult(response) { StatusCode = (int)response.StatusCodes }; 
        }
    }
}
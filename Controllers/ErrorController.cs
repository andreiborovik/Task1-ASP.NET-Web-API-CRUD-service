using Microsoft.AspNetCore.Mvc;


namespace Task1.Controllers
{
    
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error() => Problem();
        
    }
}

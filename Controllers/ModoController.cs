using binks_forum_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace binks_forum_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModoController : ControllerBase
    {
        private readonly IModoService _modoService;
        public ModoController(IModoService modoService)
        {
            _modoService = modoService;
        }
    }
}
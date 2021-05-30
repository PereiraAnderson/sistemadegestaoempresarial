using Microsoft.AspNetCore.Mvc;
using SGE.Infra.Enums;

namespace SGE.Controllers.Utils
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public EnumUsuarioPerfil UserPerfil { get; set; }
    }
}
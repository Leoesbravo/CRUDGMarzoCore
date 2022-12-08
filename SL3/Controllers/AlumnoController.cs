using Microsoft.AspNetCore.Mvc;

namespace SL3.Controllers
{
    public class AlumnoController : Controller
    {
        [HttpGet]
        [Route("api/Alumno/GetAll")]
        public IActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            var result = BL.Materia.GetAll(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        [Route("api/Materia/Add")]
        public IActionResult Post([FromBody] ML.Materia materia)
        {

            ML.Result result = BL.Materia.Add(materia);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }
    }
}

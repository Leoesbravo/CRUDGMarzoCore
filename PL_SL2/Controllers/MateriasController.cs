using Microsoft.AspNetCore.Mvc;

namespace PL_SL2.Controllers
{
    public class MateriasController : Controller
    {
        [HttpGet]
        [Route("api/Materia/GetAll")]
        public IActionResult GetAll()
        {

            ML.Materia materia = new ML.Materia();
            materia.Semestre = new ML.Semestre();
            materia.Grupo = new ML.Grupo();

            ML.Result result = BL.Materia.GetAll(materia);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }
        [HttpPost] 
        [Route("api/Materia/Add")]
        public IActionResult Post([FromBody]ML.Materia materia)
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
        [HttpGet]
        [Route("api/Materia/GetById/{IdMateria}")]
        public IActionResult GetById(int IdMateria)
        {

            ML.Result result = BL.Materia.GetById(IdMateria);

            if (result.Correct)
            {
                return Ok (result);
            }
            else
            {
                return NotFound (result);
            }

        }
        //[HttpPost]
        //[Route("api/Usuario/Authenticate")]
        //public IActionResult Authenticate([FromBody] ML.Materia materia)
        //{
        //    ML.Materia usuariologin = new ML.Materia();
        //    ML.Result result = BL.Materia.GetById(materia.IdMateria.Value);
        //    usuariologin = (ML.Materia)result.Object;

        //    if (result.Correct)
        //    {

        //        if (materia.UserName == usuariologin.UserName)
        //        {
        //            if (materia.Password == materia.Password)
        //            {
        //                var token =  TokenGenerator.GenerateTokenJwt(materia.UserName);
        //                return Ok(result);

        //            }
        //            else
        //            {
        //                result.ErrorMessage = "Contraseña incorrecta";
        //                return Content(HttpStatusCode.NotFound, result.ErrorMessage);
        //            }
        //        }
        //        else
        //        {
        //            result.ErrorMessage = "User Name incorrecto";
        //            return Content(HttpStatusCode.NotFound, result.ErrorMessage);
        //        }
        //    }
        //    else
        //    {
        //        result.ErrorMessage = "No encontrado";
        //        return Content(HttpStatusCode.NotFound, result.ErrorMessage);
        //    }
        //}
    }
}

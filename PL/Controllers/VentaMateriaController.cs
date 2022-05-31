using Microsoft.AspNetCore.Mvc;
using Microsoft.Xrm.Sdk.Messages;
using System.IdentityModel.Tokens.Jwt;

namespace PL.Controllers
{
    public class VentaMateriaController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            materia.Semestre = new ML.Semestre();
            materia.Grupo = new ML.Grupo();

            ML.Result result = BL.Materia.GetAll(materia);
            ML.Result resultSemestre = BL.Semestre.GetAll();

            materia.Semestre.Semestres = resultSemestre.Objects;
            materia.Materias = result.Objects;

            return View(materia);
        }
        [HttpPost]
        public ActionResult GetAll(ML.Materia materia)
        {
            materia.Nombre = (materia.Nombre == null) ? "" : materia.Nombre;
            //materia.Semestre.IdSemestre = (materia.Semestre.IdSemestre == null) ? "" : empleado.ApellidoPaterno;
            materia.Grupo.Horario = materia.Grupo.Horario == null ? "" : materia.Grupo.Horario;

            ML.Result result = BL.Materia.GetAll(materia);
            materia.Materias = result.Objects;

            ML.Result resultSemestre = BL.Semestre.GetAll();
            materia.Semestre.Semestres = resultSemestre.Objects;


            return View(materia);
        }
        public ActionResult Cart()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CartPost(ML.Materia materia)
        {
            ML.VentaMateria ventaMateria = new ML.VentaMateria();
     
            if (HttpContext.Session.GetString("Producto") == null)
            {
                ventaMateria.VentaMaterias = new List<object>();
                ventaMateria.VentaMaterias.Add(materia);
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaMateria.VentaMaterias));
                var session = HttpContext.Session.GetString("Producto");
            }
            else
            {
                //foreach (var resultItem in readTask.Result.Objects)
                //{
                //    ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                //    usuario.Usuarios.Add(resultItemList);
                //}
                var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));
                ML.Materia test = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(ventaSession[0].ToString());

                ventaMateria.VentaMaterias.Add(test);
                

            }

            return GetAll();
        }
        //public ActionResult Serializar(List<object> Object)
        //{
        //    ISerializer
        //    return View();
        //}
    }
}

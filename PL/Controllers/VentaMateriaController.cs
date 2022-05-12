using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        public ActionResult Cart(ML.Materia materia)
        {
            ML.VentaMateria ventaMateria = new ML.VentaMateria();

            ventaMateria.VentaMaterias = new List<object>();
            ventaMateria.VentaMaterias.Add(materia);

            return View();
        }
        //public ActionResult Serializar(List<object> Object)
        //{
        //    ISerializer
        //    return View();
        //}
    }
}

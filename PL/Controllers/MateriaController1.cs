using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MateriaController1 : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Materia.GetAll();
            ML.Materia materia = new ML.Materia();

            materia.Materias = result.Objects;

            return View(materia);
        }
        [HttpGet]
        public ActionResult Form(int? IdMateria)
        {

            ML.Materia materia = new ML.Materia();
            ML.Result resultSemestre = BL.Semestre.GetAll();

            materia.Plantel = new ML.Plantel();
            materia.Plantel.Grupo = new ML.Grupo();

            ML.Result resultPlantel = BL.Plantel.GetAll();


            if (resultSemestre.Correct)
            {
                if (IdMateria == null)
                {
                    materia.Semestre = new ML.Semestre();

                    materia.Semestre.Semestres = resultSemestre.Objects;
                    materia.Plantel.Planteles = resultPlantel.Objects;

                    return View(materia);
                }
                else
                {
                    ML.Result result = new ML.Result();
                    result = BL.Materia.GetById(IdMateria.Value);

                    if (result.Correct)
                    {
                        materia = ((ML.Materia)result.Object);
                        return View(materia);
                    }
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            if (materia.IdMateria == 0)
            {
                result = BL.Materia.Add(materia);
                if (result.Correct)
                {
                    ViewBag.Message = "La materia se ha registrado correctamente";
                }
                else
                {
                    ViewBag.Message = "La materia no se ha registrado correctamente " + result.ErrorMessage;
                }
            }
            else
            {
                //result = BL.Materia.Update(materia);

                if (result.Correct)
                {
                    ViewBag.Message = "El registro se ha actualizado correctamente";
                }
                else
                {
                    ViewBag.Message = "El registro no se ha actualizado correctamente " + result.ErrorMessage;
                }


            }

            return PartialView("Modal");
        }

        public JsonResult GrupoGetByIdPlantel(int IdPlantel)
        {
            ML.Result result = BL.Grupo.GrupoGetByIdPlantel(IdPlantel);

            return Json(result.Objects);
        }

    }
}

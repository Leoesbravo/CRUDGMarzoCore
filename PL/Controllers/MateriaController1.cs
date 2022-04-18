using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MateriaController1 : Controller
    {
        [HttpGet]
        public ActionResult Form(int? IdMateria)
            {
            ML.Materia materia = new ML.Materia();
            materia.Semestre = new ML.Semestre();

            if (IdMateria == null)
            {
                //add 
                return View(materia);
            }
            else
            {
                //Update
                //ML.Result result = BL.Materia.GetById(IdMateria.Value);

                //materia = ((ML.Materia)result.Object);

                return View(materia);
            }
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
                    ViewBag.Message = "La materia se ha agregado";
                }
                else
                {

                    ViewBag.Message = "La materia no se agrego";
                }
            }
            else
            {
               // result = BL.Materia.Update(materia);
                if (result.Correct)
                {
                    ViewBag.Message = "La materia se ha agregado";
                }
                else
                {

                    ViewBag.Message = "La materia no se agrego";
                }
            }

            return PartialView("Modal");
        }
    }
}

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
            

            ML.Result resultPaises = BL.Pais.GetAll();
            materia.Direccion = new ML.Direccion();
            materia.Direccion.Colonia = new ML.Colonia();
            materia.Direccion.Colonia.Municipio = new ML.Municipio();
            materia.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            materia.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
            materia.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

            if (resultSemestre.Correct)
            {
                if (IdMateria == null)
                {
                    materia.Semestre = new ML.Semestre();
                    materia.Semestre.Semestres = resultSemestre.Objects;
                    return View(materia);
                }
                else
                {
                    ML.Result result = new ML.Result();
                    result = BL.Materia.GetById(IdMateria.Value);

                    if (result.Correct)
                    {
                        materia = ((ML.Materia)result.Object);
                        materia.Semestre.Semestres = resultSemestre.Objects;
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
        public JsonResult GetEstado(int IdPais)
        {
            var result = BL.Estado.GetByIdPais(IdPais);

            return Json(result.Objects);
        }
        public JsonResult GetMunicipio(int IdEstado)
        {
            var result = BL.Municipio.MunicipioGetByIdEstado(IdEstado);

            return Json(result.Objects);
        }
        public JsonResult GetColonia(int IdMunicipio)
        {
            var result = BL.Colonia.ColoniaGetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }
    }
}

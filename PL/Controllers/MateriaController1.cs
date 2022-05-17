using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MateriaController1 : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            //ML.Materia materia = new ML.Materia();
            //materia.Semestre = new ML.Semestre();
            //materia.Grupo = new ML.Grupo();

            //ML.Result result = BL.Materia.GetAll(materia);
            //ML.Result resultSemestre = BL.Semestre.GetAll();

            //materia.Semestre.Semestres = resultSemestre.Objects;
            //materia.Materias = result.Objects;

            //return View(materia);
            ML.Materia resultmateria = new ML.Materia();
            resultmateria.Materias = new List<Object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5017/api/");

                var responseTask = client.GetAsync("Materia/GetAll ");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Materia resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultItem.ToString());
                        resultmateria.Materias.Add(resultItemList);
                    }
                    ML.Result resultSemestre = BL.Semestre.GetAll();
                    resultmateria.Semestre = new ML.Semestre();
                    resultmateria.Semestre.Semestres = resultSemestre.Objects;
                }
            }
            return View(resultmateria);
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
        [HttpGet]
        public ActionResult Form(int? IdMateria)
        {

            ML.Materia materia = new ML.Materia();
            ML.Result resultSemestre = BL.Semestre.GetAll();

            materia.Grupo = new ML.Grupo();
            materia.Grupo.Plantel = new ML.Plantel();

            ML.Result resultPlantel = BL.Plantel.GetAll();


            if (resultSemestre.Correct)
            {
                if (IdMateria == null)
                {
                    materia.Semestre = new ML.Semestre();

                    materia.Semestre.Semestres = resultSemestre.Objects;
                    materia.Grupo.Plantel.Planteles = resultPlantel.Objects;

                    return View(materia);
                }
                else
                {
                    ML.Result result = new ML.Result();
                    result = BL.Materia.GetById(IdMateria.Value);

                    if (result.Correct)
                    {
                        materia = ((ML.Materia)result.Object);

                        ML.Result resultGrupo = BL.Grupo.GrupoGetByIdPlantel(materia.Grupo.Plantel.IdPlantel.Value);

                        materia.Grupo.Grupos = resultGrupo.Objects;
                        materia.Grupo.Plantel.Planteles = resultPlantel.Objects;
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

            IFormFile file = Request.Form.Files["fuImage"];

            if (file != null)
            {
                byte[] ImagenBytes = ConvertToBytes(file);
                materia.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if (ModelState.IsValid)
            {


                if (materia.IdMateria == null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5017/api/");

                        var postTask = client.PostAsJsonAsync<ML.Materia>("Materia/Add", ateria);
                        postTask.Wait();

                        var resultAseguradora = postTask.Result;

                        //result = BL.Materia.Add(materia);
                        if (resultAseguradora.IsSuccessStatusCode)
                        //if (result.Correct)
                        {
                            ViewBag.Message = "La materia se ha registrado correctamente";
                        }
                        else
                        {
                            ViewBag.Message = "La materia no se ha registrado correctamente " + result.ErrorMessage;
                        }
                    }
                }
                else
                {
                    result = BL.Materia.Update(materia);

                    if (result.Correct)
                    {
                        ViewBag.Message = "El registro se ha actualizado correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "El registro no se ha actualizado correctamente " + result.ErrorMessage;
                    }


                }
            }
            else
            {
                ML.Result resultSemestre = BL.Semestre.GetAll();
                ML.Result resultPlantel = BL.Plantel.GetAll();

                materia.Semestre.Semestres = resultSemestre.Objects;
                materia.Grupo.Plantel.Planteles = resultPlantel.Objects;

                return View(materia);
            }
            return PartialView("Modal");
        }
        public JsonResult GrupoGetByIdPlantel(int IdPlantel)
        {
            ML.Result result = BL.Grupo.GrupoGetByIdPlantel(IdPlantel);

            return Json(result.Objects);
        }
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
        public ActionResult UpdateStatus(int idMateria)
        {
            ML.Materia materia = new ML.Materia();
            ML.Result result = BL.Materia.GetById(idMateria);

            if (result.Correct)
            {
                materia = ((ML.Materia)result.Object);
                materia.Status = materia.Status.Value ? false : true;
                ML.Result resultUpdate = BL.Materia.Update(materia);
            }
            else
            {

            }

            return PartialView("Modal");
        }
    }
}

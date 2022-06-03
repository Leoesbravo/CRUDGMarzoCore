﻿using Microsoft.AspNetCore.Mvc;
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
            ventaMateria.VentaMaterias = new List<object>();

            if (HttpContext.Session.GetString("Producto") == null)
            {
                materia.Cantidad = materia.Cantidad = 1;
                ventaMateria.VentaMaterias.Add(materia);
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaMateria.VentaMaterias));
                var session = HttpContext.Session.GetString("Producto");
            }
            else
            {
                var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));
               
                foreach (var obj in ventaSession)
                {
                   ML.Materia objMateria = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(obj.ToString());
                   ventaMateria.VentaMaterias.Add(objMateria);
                }
                //var indice = ventaMateria.VentaMaterias.IndexOf(materia);
                foreach (ML.Materia venta in ventaMateria.VentaMaterias.ToList())
                {
                    if (materia.IdMateria == venta.IdMateria)
                    {
                        venta.Cantidad = venta.Cantidad + 1;   //True                
                    }
                    else
                    {
                        materia.Cantidad = materia.Cantidad = 1; //False
                        ventaMateria.VentaMaterias.Add(materia);
                    }
                }       
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaMateria.VentaMaterias));
            }
            if (HttpContext.Session.GetString("Producto") != null)
            {
                ViewBag.Message = "Se ha agregado la materia a tu carrito!";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "No se pudo agregar tu producto ):";
                return PartialView("Modal");
            }
            
        }
        [HttpGet]
        public ActionResult ResumenCompra(ML.VentaMateria ventaMateria)
        {
            if (HttpContext.Session.GetString("Producto") == null)
            {
                return View();
            }
            else
            {
                var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));
                ventaMateria.VentaMaterias = new List<object>();
                foreach (var obj in ventaSession)
                {
                    ML.Materia objMateria = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(obj.ToString());
                    ventaMateria.VentaMaterias.Add(objMateria);
                }

            }

            return View(ventaMateria);
        }
    }
}

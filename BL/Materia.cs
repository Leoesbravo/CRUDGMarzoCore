using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {
        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LEscogidoMarzoContext context = new DL.LEscogidoMarzoContext())
                {

                    //Stored con .Net Framework
                    // var query = context.MateriaAdd(materia.Nombre, materia.Costo, materia.Creditos, materia.Semestre.IdSemestre);

                    //Stored con .Net core
                    var query = context.Database.ExecuteSqlRaw($"MateriaAdd '{materia.Nombre}', {materia.Costo}, {materia.Creditos}, {materia.Semestre.IdSemestre}, '{materia.Direccion.Calle}', '{materia.Direccion.NumeroExterior}', '{materia.Direccion.NumeroInterior}', {materia.Direccion.Colonia.IdColonia}" );

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetById(int  IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LEscogidoMarzoContext context = new DL.LEscogidoMarzoContext())
                {
                    var obj = context.Materia.FromSqlRaw($"MateriaGetById {IdMateria}").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {
                        ML.Materia materia = new ML.Materia();
                        materia.IdMateria = obj.IdMateria;
                        materia.Nombre = obj.Nombre;
                        materia.Costo = obj.Costo;
                        materia.Creditos = obj.Creditos;

                        materia.Semestre = new ML.Semestre();
                        materia.Semestre.IdSemestre = obj.IdSemestre.Value;


                        result.Object = materia;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo realizar la consulta";
                    }

                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.LEscogidoMarzoContext context = new DL.LEscogidoMarzoContext())
                
                {
                    var query = context.Materia.FromSqlRaw("MateriaGetAll").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Materia usuario = new ML.Materia();
                            usuario.IdMateria = obj.IdMateria;
                            usuario.Nombre = obj.Nombre;
                            usuario.Creditos = obj.Creditos;
                            usuario.Costo = obj.Costo;

                            usuario.Semestre = new ML.Semestre();
                            usuario.Semestre.IdSemestre = obj.IdSemestre.Value;

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
                    var query = context.Database.ExecuteSqlRaw($"MateriaAdd '{materia.Nombre}', {materia.Creditos}, {materia.Costo}, {materia.Semestre.IdSemestre}, '{materia.Grupo.Horario}', {materia.Grupo.Plantel.IdPlantel}, '{materia.Imagen}', {materia.Status}");

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
        public static ML.Result GetById(int IdMateria)
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
                        materia.Creditos = obj.Creditos.Value;
                        materia.Imagen = obj.Imagen;
                        materia.Status = obj.Status;

                        materia.Semestre = new ML.Semestre();
                        materia.Semestre.IdSemestre = obj.IdSemestre.Value;

                        materia.Grupo = new ML.Grupo();
                        materia.Grupo.IdGrupo = obj.IdGrupo.Value;
                        materia.Grupo.Horario = obj.Horario;

                        materia.Grupo.Plantel = new ML.Plantel();
                        materia.Grupo.Plantel.IdPlantel = obj.IdPlantel;
                        materia.Grupo.Plantel.Nombre = obj.Plantel;

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
        public static ML.Result GetAll(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.LEscogidoMarzoContext context = new DL.LEscogidoMarzoContext())

                {
                    materia.Semestre.IdSemestre = (materia.Semestre.IdSemestre == null) ? 0 : materia.Semestre.IdSemestre;
                    var query = context.Materia.FromSqlRaw($"MateriaGetAll {materia.Semestre.IdSemestre}, '{materia.Nombre}', '{materia.Grupo.Horario}'").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Materia usuario = new ML.Materia();
                            usuario.IdMateria = obj.IdMateria;
                            usuario.Nombre = obj.Nombre;
                            usuario.Creditos = obj.Creditos.Value;
                            usuario.Costo = obj.Costo;

                            usuario.Semestre = new ML.Semestre();
                            usuario.Semestre.IdSemestre = obj.IdSemestre.Value;

                            usuario.Grupo = new ML.Grupo();
                            usuario.Grupo.Horario = obj.Horario;
                            usuario.Grupo.IdGrupo = obj.IdGrupo.Value;

                            usuario.Grupo.Plantel = new ML.Plantel();
                            usuario.Grupo.Plantel.IdPlantel = obj.IdPlantel;
                            usuario.Grupo.Plantel.Nombre = obj.Plantel;

                            usuario.Imagen = obj.Imagen;
                            usuario.Status = obj.Status;

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
        public static ML.Result Update(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LEscogidoMarzoContext context = new DL.LEscogidoMarzoContext())
                {
                    var query = context.Database.ExecuteSqlRaw($" MateriaUpdate '{materia.Nombre}', {materia.Creditos}, {materia.Costo}, {materia.Semestre.IdSemestre}, 'vespertino', {materia.Grupo.Plantel.IdPlantel}, '{materia.Imagen}', {materia.Status}, {materia.IdMateria}");


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
        public static ML.Result CargaMasiva(string root)
        {
            ML.Result result = new ML.Result();

            //StreamReader archivo = new StreamReader(@"C:\Users\digis\Documents\LayoutMateria.txt");
            StreamReader archivo = new StreamReader(root);

            string line;

            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();

            line = archivo.ReadLine();
            while ((line = archivo.ReadLine()) != null)
            {
                string[] datos = line.Split('|');

                ML.Materia materia = new ML.Materia();
                materia.Nombre = datos[0];
                materia.Creditos = Convert.ToByte(datos[1]);
                materia.Costo = decimal.Parse(datos[2]);

                materia.Semestre = new ML.Semestre();
                materia.Semestre.IdSemestre = Convert.ToInt32(datos[3]);

                materia.Grupo = new ML.Grupo();
                materia.Grupo.Horario = datos[4];

                materia.Grupo.Plantel = new ML.Plantel();
                materia.Grupo.Plantel.IdPlantel = Convert.ToInt32(datos[5]);

                materia.Status = Convert.ToBoolean(datos[6]);

                result = BL.Materia.Add(materia);

                if (result.Correct == false)
                {
                    resultErrores.Objects.Add(
                        "No se inserto el Nombre: " + materia.Nombre + " " +
                        "No se inserto los Creditos: " + materia.Creditos + " " +
                        "No se inserto el Costo: " + materia.Costo + " " +
                        "No se inserto el Semestre: " + materia.Semestre.IdSemestre + " " +
                        "No se inserto el Horario:" + materia.Grupo.Horario + " " +
                        "No se inserto el Plantel:" + materia.Grupo.Plantel.IdPlantel + "" +
                        "No se inserto el Status: " + materia.Status + " ");
                }
            }
            archivo.Close();

            TextWriter tw = new StreamWriter(@"C:\Users\digis\Documents\ErroresCargaMasiva.txt");
            foreach (string error in resultErrores.Objects)
            {
                tw.WriteLine(error);
                Console.WriteLine(error);
            }
            tw.Close();

            return result;
        }

        //Carga Masiva Excel
        public static ML.Result ConvertXSLXtoDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;


                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableMateria = new DataTable();

                        da.Fill(tableMateria);

                        if (tableMateria.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableMateria.Rows)
                            {
                                ML.Materia materia = new ML.Materia();
                                materia.Nombre = row[0].ToString();
                                //row[1] = (row[1] == null) ? 0 : materia.Creditos;
                                //materia.Nombre = (materia.Nombre == null) ? "" : materia.Nombre;
                                materia.Creditos = byte.Parse(row[1].ToString());
                                materia.Costo = decimal.Parse(row[2].ToString());

                                materia.Semestre = new ML.Semestre();
                                materia.Semestre.IdSemestre = int.Parse(row[3].ToString());

                                materia.Grupo = new ML.Grupo();
                                materia.Grupo.Horario = row[4].ToString();

                                materia.Grupo.Plantel = new ML.Plantel();
                                materia.Grupo.Plantel.IdPlantel = int.Parse(row[5].ToString());

                                materia.Status = bool.Parse(row[6].ToString());

                                result.Objects.Add(materia);
                            }

                            result.Correct = true;

                        }

                        result.Object = tableMateria;

                        if (tableMateria.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
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
        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Materia materia in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if (materia.Nombre == "")
                    {
                        error.Mensaje += "Ingresar el nombre  ";
                    }
                    if (materia.Creditos.ToString() == "")
                    {
                        error.Mensaje += "Ingresar los creditos ";
                    }
                    if (materia.Costo.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el Costo ";
                    }
                    if (materia.Semestre.IdSemestre.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el semestre ";
                    }
                    if (materia.Grupo.Horario == "")
                    {
                        error.Mensaje += "Ingresar el horario ";
                    }
                    if (materia.Grupo.Plantel.IdPlantel.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el plantel ";
                    }
                    if (materia.Status.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el status ";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
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

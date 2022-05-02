using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLC
{
    internal class Materia
    {
        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();

            StreamReader archivo = new StreamReader(@"C:\Users\digis\Documents\LayoutMateria.txt");

            string line;

            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();
            //leo la primera linea
            line = archivo.ReadLine();
            //
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
    }
}

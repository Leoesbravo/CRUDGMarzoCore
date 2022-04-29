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
            
            while ((line = archivo.ReadLine()) != null)
            {
                string[] datos = line.Split('|');

                ML.Materia materia = new ML.Materia();
                materia.Nombre = datos[0];
                materia.Creditos = Convert.ToByte(datos[1]);
                materia.Costo = decimal.Parse(datos[2]);

                materia.Semestre.IdSemestre = Convert.ToInt32(datos[3]);

                materia.Grupo.Horario = datos[4];

                materia.Grupo.Plantel.IdPlantel = Convert.ToInt32(datos[5]);

                materia.Status = Convert.ToBoolean(datos[6]);


                if (result.Correct == false)
                {
                    resultErrores.Objects.Add(
                                                ); 
                }
            }
            archivo.Close();

            TextWriter tw = new StreamWriter(@"C:\Users\digis\Documents\ErroresCargaMasiva.txt");
            foreach ()
            {
                tw.WriteLine(error);
                Console.WriteLine(error);
            }

            tw.Close();

            return result;

        }
    }
}

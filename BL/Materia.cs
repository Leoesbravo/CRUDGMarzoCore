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
                    //Llamamos al SP en NETCORE
                    //ExecuteSQLRaw
                    //FromSQLRaw

                    //Stored con .Net Framework
                    // var query = context.MateriaAdd(materia.Nombre, materia.Costo, materia.Creditos, materia.Semestre.IdSemestre);

                    //Stored con .Net core
                    var query = context.Database.ExecuteSqlRaw($"MateriaAdd '{materia.Nombre}', {materia.Costo}, {materia.Creditos}, {materia.Semestre.IdSemestre}" );

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
    }
}

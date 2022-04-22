using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Grupo
    {
        public static ML.Result GrupoGetByIdPlantel(int idPlantel)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LEscogidoMarzoContext context = new DL.LEscogidoMarzoContext())

                {
                    var query = context.Grupos.FromSqlRaw($"GrupoGetByIdPlantel {idPlantel}").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Grupo grupo = new ML.Grupo();
                            grupo.IdGrupo = obj.IdGrupo;
                            grupo.Horario = obj.Horario;

                            grupo.Materia = new ML.Materia();
                            grupo.Materia.IdMateria = obj.IdMateria.Value;

                            grupo.Plantel = new ML.Plantel();
                            grupo.Plantel.IdPlantel = obj.IdPlantel.Value;
                            //grupo.  

                            result.Objects.Add(grupo);
                        }
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
            }
            return result;
        }
    }
}

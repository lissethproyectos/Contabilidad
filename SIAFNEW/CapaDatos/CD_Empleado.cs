using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace CapaDatos
{
    public class CD_Empleado
    {
        public void ConsultarEmpleados(Empleado objEmpleado, ref List<Empleado> lstEmpleados)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand cmm = null;
            try
            {

                OracleDataReader dr = null;
                String[] Parametros = { "p_Nombre", "p_Paterno", "p_Materno", "p_tipo_personal" };
                Object[] Valores = { objEmpleado.Nombre, objEmpleado.Paterno, objEmpleado.Materno, objEmpleado.Tipo_Personal };

                cmm = CDDatos.GenerarOracleCommandCursor("PKG_CONTABILIDAD.Obt_Grid_Empleados", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    objEmpleado = new Empleado();
                    objEmpleado.Nombre = Convert.ToString(dr[0]);
                    objEmpleado.Dependencia = Convert.ToString(dr[1]);
                    objEmpleado.Numero_Plaza = Convert.ToString(dr[2]);
                    objEmpleado.Tipo_Personal = Convert.ToString(dr[3]);
                    objEmpleado.Correo_UNACH = Convert.ToString(dr[5]);
                    objEmpleado.Nomina = Convert.ToString(dr[6]);
                    lstEmpleados.Add(objEmpleado);

                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref cmm);
            }
        }
        public void ConsultarCatEmpleados(Empleado objEmpleado, ref List<Empleado> lstEmpleados)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand cmm = null;
            try
            {

                OracleDataReader dr = null;
                String[] Parametros = { "p_Nombre" };
                Object[] Valores = { objEmpleado.Nombre };

                cmm = CDDatos.GenerarOracleCommandCursor("PKG_CONTABILIDAD.Obt_Grid_Empleados2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    objEmpleado = new Empleado();
                    objEmpleado.Nombre = Convert.ToString(dr[0]);
                    lstEmpleados.Add(objEmpleado);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref cmm);
            }
        }

    }
}

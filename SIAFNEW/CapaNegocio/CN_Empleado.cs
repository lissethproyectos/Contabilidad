using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaNegocio
{
    public class CN_Empleado
    {
        public void ConsultarEmpleados(Empleado objEmpleado, ref List<Empleado> lstEmpleados)
        {
            try
            {

                CD_Empleado CDEmpleado = new CD_Empleado();
                CDEmpleado.ConsultarEmpleados(objEmpleado, ref lstEmpleados);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ConsultarCatEmpleados(Empleado objEmpleado, ref List<Empleado> lstEmpleados)
        {
            try
            {

                CD_Empleado CDEmpleado = new CD_Empleado();
                CDEmpleado.ConsultarCatEmpleados(objEmpleado, ref lstEmpleados);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

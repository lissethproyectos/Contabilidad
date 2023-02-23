using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAF.Contabilidad.Form
{
    public partial class frmCatEmpleados : System.Web.UI.Page
    {
        Sesion SesionUsu = new Sesion();
        CN_Empleado CNEmpleado = new CN_Empleado();
        CN_Comun CNComun = new CN_Comun();
        List<Empleado> ListEmpleados = new List<Empleado>();
        string Verificador = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            SesionUsu = (Sesion)Session["Usuario"];
            //if (!IsPostBack)
            //    inicializar();

            ScriptManager.RegisterStartupScript(this, GetType(), "Grid", "Empleados();", true);
        }

        protected void linkBttnBuscarEmpleado_Click(object sender, EventArgs e)
        {
            CargarGridEmpleados();
        }

        private void CargarGridEmpleados()
        {
            //lblError.Text = string.Empty;
            grdCatEmpleados.DataSource = null;
            grdCatEmpleados.DataBind();
            try
            {
                DataTable dt = new DataTable();
                grdCatEmpleados.DataSource = dt;
                grdCatEmpleados.DataSource = GetListEmpleados();
                grdCatEmpleados.DataBind();
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        private List<Empleado> GetListEmpleados()
        {
            Empleado objEmpleado = new Empleado();
            try
            {
                List<Empleado> List = new List<Empleado>();
                objEmpleado.Nombre = txtNombre.Text.ToUpper();
                objEmpleado.Paterno = txtPaterno.Text.ToUpper();
                objEmpleado.Materno = txtMaterno.Text.ToUpper();
                objEmpleado.Tipo_Personal = ddlTipoPersonal.SelectedValue;
                CNEmpleado.ConsultarEmpleados(objEmpleado, ref ListEmpleados);
                return ListEmpleados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
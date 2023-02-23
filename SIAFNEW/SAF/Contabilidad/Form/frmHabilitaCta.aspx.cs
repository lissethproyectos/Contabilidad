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
    public partial class frmHabilitaCta : System.Web.UI.Page
    {
        #region <Variables>
        Int32[] Celdas = new Int32[] { 0, 15, 16, 17, 18, 19, 20 };
        string Verificador = string.Empty;
        CN_Comun CNComun = new CN_Comun();
        Centros_Contables objCentroContable = new Centros_Contables();
        CN_Centros_Contables CNCentroContable = new CN_Centros_Contables();
        Sesion SesionUsu = new Sesion();
        Centros_Contables objCC = new Centros_Contables();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            SesionUsu = (Sesion)Session["Usuario"];
            if (!IsPostBack)
                Inicializar();

            ScriptManager.RegisterStartupScript(this, GetType(), "GridDisp", "MayorDisponibles();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "GridAsig", "MayorAsignados();", true);

        }
        private void Inicializar()
        {
            
                CargarGrid();
                CargarGridAsig();
           
        }
        private void CargarGrid()
        {
            Verificador = string.Empty;

            grdCCDisponibles.DataSource = null;
            grdCCDisponibles.DataBind();
            try
            {
                DataTable dt = new DataTable();
                grdCCDisponibles.DataSource = dt;
                grdCCDisponibles.DataSource = GetList();
                grdCCDisponibles.DataBind();
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        private List<Centros_Contables> GetList()
        {
            Verificador = string.Empty;
            try
            {
                List<Centros_Contables> List = new List<Centros_Contables>();
                Centros_Contables objCCDisp = new Centros_Contables();
                objCCDisp.Ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                CNCentroContable.ConsultaGrid_CCDisp_1123(objCCDisp, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void CargarGridAsig()
        {
            Verificador = string.Empty;

            grdCCAsignados.DataSource = null;
            grdCCAsignados.DataBind();
            try
            {
                DataTable dt = new DataTable();
                grdCCAsignados.DataSource = dt;
                grdCCAsignados.DataSource = GetListAsig();
                grdCCAsignados.DataBind();
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        private List<Centros_Contables> GetListAsig()
        {
            Verificador = string.Empty;
            try
            {
                List<Centros_Contables> List = new List<Centros_Contables>();
                Centros_Contables objCCDisp = new Centros_Contables();
                objCCDisp.Ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                CNCentroContable.ConsultaGrid_CCDispAsig_1123(objCCDisp, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void grdCCDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            try
            {
                objCC.Ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                objCC.Centro_Contable = Convert.ToString(grdCCDisponibles.SelectedRow.Cells[0].Text);
                CNCentroContable.Agregar_Mayor(objCC, ref Verificador);
                if (Verificador == "0")
                {
                    CargarGrid();
                    CargarGridAsig();
                }
                else
                {
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                }
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void grdCCAsignados_SelectedIndexChanged(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            try
            {
                objCC.Ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                objCC.Centro_Contable = Convert.ToString(grdCCAsignados.SelectedRow.Cells[1].Text);
                CNCentroContable.Eliminar_Mayor(objCC, ref Verificador);
                if (Verificador == "0")
                {
                    CargarGrid();
                    CargarGridAsig();
                }
                else
                {
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                }
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
    }
}
using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAF
{
    public partial class Inicio : System.Web.UI.Page
    {
        #region <Variables>
        string Verificador = string.Empty;
        CN_Usuario CNUsuario = new CN_Usuario();
        Usuario Usuario = new Usuario();
        Sesion SesionUsu = new Sesion();
        Comun Comun = new Comun();
        List<Comun> lstComun = new List<Comun>();
        Mnu mnu = new Mnu();
        CN_Mnu CNMnu = new CN_Mnu();
        CN_Comun CNMonitor = new CN_Comun();
        CN_Comun CNComun = new CN_Comun();
        CN_Informativa CNInformativa = new CN_Informativa();
        cuentas_contables Objinformativa = new cuentas_contables();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            SesionUsu = (Sesion)Session["Usuario"];
            if (!IsPostBack)
            {
                busca_informativa();
                Cargarcombos();
                //MonitorConsultaGrid();
            }
        }
        private void busca_informativa()
        {
            Verificador = string.Empty;
            try
            {
                //lblMensaje.Text = string.Empty;
                Objinformativa.usuario = SesionUsu.Usu_Nombre;
                Objinformativa.ejercicio = SesionUsu.Usu_Ejercicio;
                CNInformativa.Consultar_Mensajes(SesionUsu.Usu_Nombre, 15361, ref lstComun);

                if (lstComun.Count >= 1)
                {
                    lblMsg_Observaciones.Text = string.Empty;
                    foreach (Comun lst in lstComun)
                    {
                        lblMsg_Observaciones.Text = lblMsg_Observaciones.Text + "<br />" + lst.Descripcion;
                    }
                    //ModalPopupExtender.Show();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupAviso", "$('#modalAviso').modal('show')", true);
                }

            }
            catch (Exception ex)
            {
                string Msj = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Msj);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "modal", "mostrar_modal( 0, '" + Msj + "');", true);  //lblMsjFam.Text = Verificador;

                //lblMensaje.Text = ex.Message;
            }

        }
        private void Cargarcombos()
        {
            Verificador = string.Empty;
            try
            {
                CNMonitor.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref DDLCentro_Contable, "p_usuario", "p_ejercicio", SesionUsu.Usu_Nombre, SesionUsu.Usu_Ejercicio);
                //this.DDLCentro_Contable.Items.Insert(0, new ListItem("-- Seleccionar --", "X"));
                DDLCentro_Contable_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

            }
        }
        private void MonitorConsultaGrid()
        {
            //lblError.Text = string.Empty;
            Verificador = string.Empty;
            grvMonitorCont.DataSource = null;
            grvMonitorCont.DataBind();
            try
            {
                DataTable dt = new DataTable();
                grvMonitorCont.DataSource = dt;
                grvMonitorCont.DataSource = GetList();
                grvMonitorCont.DataBind();

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }

        }
        private List<Comun> GetList()
        {
            try
            {
                List<Comun> List = new List<Comun>();
                //CNMonitor.Monitor(SesionUsu.Usu_Nombre, "15830", DDLCentro_Contable.SelectedValue, ref List);
                CNMonitor.MonitorContabilidad(SesionUsu.Usu_Nombre, "15830", DDLCentro_Contable.SelectedValue, SesionUsu.Usu_Ejercicio, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnSi_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender.Hide();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupAviso", "$('#modalAviso').modal('show')", true);
        }
        protected void DDLCentro_Contable_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonitorConsultaGrid();
        }
        protected void grvMonitorCont_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvMonitorCont.PageIndex = 0;
            grvMonitorCont.PageIndex = e.NewPageIndex;
            MonitorConsultaGrid();
        }
    }
}
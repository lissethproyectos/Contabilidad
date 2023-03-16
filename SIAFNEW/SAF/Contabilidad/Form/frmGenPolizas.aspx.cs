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

    public partial class frmGenPolizas : System.Web.UI.Page
    {

       //Cambios lis 16
        #region <Variables>
        Int32[] Celdas = new Int32[] { 0, 15, 16, 17, 18, 19, 20 };
        string Verificador = string.Empty;
        string MsjError = string.Empty;
        Sesion SesionUsu = new Sesion();
        CN_Comun CNComun = new CN_Comun();
        CN_Poliza CNPoliza = new CN_Poliza();
        Poliza ObjPoliza = new Poliza();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            SesionUsu = (Sesion)Session["Usuario"];
            if (!IsPostBack)            
                Inicializar();

            ScriptManager.RegisterStartupScript(this, GetType(), "GridPolizas", "Polizas();", true);


        }
        private void Inicializar()
        {
            CargarGrid();

            //Se va a validar monto en partidas 1
        }
        protected void linkBttnGenPolizas_Click(object sender, EventArgs e)
        {
            int TotalPolizasGen = 0;
            //LinkButton cbi = (LinkButton)(sender);
            //GridViewRow row = (GridViewRow)cbi.NamingContainer;

            //LinkButton linkBttnAgregarReg = (LinkButton)(grvPolizas .HeaderRow.Cells[11].FindControl("linkBttnAgregarReg0"));


            //grvPolizas.SelectedIndex = row.RowIndex; 
            Verificador = string.Empty;
            string ss=SesionUsu.Usu_Ejercicio.Substring(2, 2);
            string MesAnio =  ddlMes.SelectedValue+SesionUsu.Usu_Ejercicio.Substring(2, 2);
            try
            {
                CNPoliza.GenPolizasAuto(Convert.ToInt32(SesionUsu.Usu_Ejercicio), MesAnio, ddlTipo.SelectedValue, ref TotalPolizasGen, ref Verificador);

                if (Verificador == "0")
                {
                    lblMsjError.Text = "Se han generado " + TotalPolizasGen + " pólizas.";
                    CargarGrid();
                }
                else
                {
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    //lblMsjError.Text = Verificador;
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
        private void CargarGrid()
        {
            Verificador = string.Empty;
            grvPolizas.DataSource = null;
            grvPolizas.DataBind();
            try
            {
                DataTable dt = new DataTable();
                grvPolizas.DataSource = dt;
                grvPolizas.DataSource = GetList();
                grvPolizas.DataBind();
                //if(grvPolizas.Rows.Count>1)

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        private List<Poliza> GetList()
        {
            Verificador = string.Empty;
            try
            {
                List<Poliza> List = new List<Poliza>();
                ObjPoliza.Ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                ObjPoliza.Mes_anio = ddlMes.SelectedValue + SesionUsu.Usu_Ejercicio.Substring(2);
                
                CNPoliza.PolizasCjaConsultaGrid(ObjPoliza, ddlTipo.SelectedValue,  ref List);

                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            try
            {
                CargarGrid();
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void grvPolizas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //lblMjErrorOficio.Text = string.Empty;
            Verificador = string.Empty;
            try
            {
                int fila = e.RowIndex;
                ObjPoliza.IdPoliza = Convert.ToInt32(grvPolizas.Rows[fila].Cells[0].Text);
                CNPoliza.PolizaEliminar(ObjPoliza, ref Verificador);

                if (Verificador == "0")
                    CargarGrid();
                else
                {
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    //lblMjErrorOficio.Text = Verificador;
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

        protected void linkBttnVerPolizas_Click(object sender, EventArgs e)
        {
            try
            {
                CargarGrid();
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void linkBttnEliminarTodo_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            string MesAnio = ddlMes.SelectedValue + SesionUsu.Usu_Ejercicio.Substring(2, 2);
            try
            {
                CNPoliza.EliminarPolizasAuto(Convert.ToInt32(SesionUsu.Usu_Ejercicio), MesAnio, ddlTipo.SelectedValue, ref Verificador);

                if (Verificador == "0")
                {
                    CargarGrid();
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

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void grvPolizas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "VerPoliza('RP-005'," + SesionUsu.Usu_Ejercicio + ", '" + grvPolizas.SelectedRow.Cells[0].Text + "');", true);
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

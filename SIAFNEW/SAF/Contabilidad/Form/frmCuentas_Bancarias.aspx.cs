using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocio;

namespace SAF.Contabilidad.Form
{   
    public partial class frmCuentas_Bancarias : System.Web.UI.Page
    {
        #region <Variables>
        string Verificador = string.Empty;
        Sesion SesionUsu = new Sesion();
        Cuentas_Bancarias ObjCuentas_Bancarias = new Cuentas_Bancarias();
        CN_Cuentas_Bancarias CNCuentas_Bancarias = new CN_Cuentas_Bancarias();
        CN_Comun CNComun = new CN_Comun();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            SesionUsu = (Sesion)Session["Usuario"];
            if (!IsPostBack)
            {
                Inicializar();
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "GridCuentas", "Ctas_Bancarias();", true);

            
        }

        #region <Funciones y Sub>
        private void Inicializar()
        {
            
           
            //txtFecha_Apertura.Text = string.Empty;            
            Cargarcombos();
            CargarGrid();
            MultiView1.ActiveViewIndex = 0;
            SesionUsu.Editar = -1;
           
        }
        private void CargarGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                grvCuentas_Bancarias.DataSource = dt;
                grvCuentas_Bancarias.DataSource = GetList();
                grvCuentas_Bancarias.DataBind();

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        private List<Cuentas_Bancarias> GetList()
        {
            try
            {
                List<Cuentas_Bancarias> List = new List<Cuentas_Bancarias>();
                ObjCuentas_Bancarias.Ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                ObjCuentas_Bancarias.Centro_Contable = ddlCentros_Contables0.SelectedValue;
                ObjCuentas_Bancarias.Dependencia= ddlDependencia0.SelectedValue;
                ObjCuentas_Bancarias.Cuenta_Bancaria = string.Empty; //txtBuscar.Text.ToUpper();
                CNCuentas_Bancarias.Cuentas_BancariasConsultaGrid(ref ObjCuentas_Bancarias, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void Cargarcombos()
        {
            //lblError.Text = string.Empty;            
            try
            {
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Ejercicio_Ctas", ref ddlEjercicio);                           
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref ddlCentros_Contables0, "p_usuario", "p_ejercicio", SesionUsu.Usu_Nombre, SesionUsu.Usu_Ejercicio);                
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref ddlCentros_Contables, "p_usuario", "p_ejercicio", SesionUsu.Usu_Nombre, SesionUsu.Usu_Ejercicio);
                ddlCentros_Contables0_SelectedIndexChanged(null, null);
                ddlCentros_Contables_SelectedIndexChanged(null, null);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Bancos", ref ddlBancos);
                
                //CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Cuentas_contables_Id", ref ddlCuentas_Contables0, "p_ejercicio", "p_centro_contable", SesionUsu.Usu_Ejercicio, Convert.ToString(DDLCentro_Contable.SelectedValue));
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        #endregion

       

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            //CNComun.Limpiador_controles(this);            
            SesionUsu.Editar = -1;
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            //lblError.Text = string.Empty;
            try
            {
                ObjCuentas_Bancarias.Ejercicio =Convert.ToInt32(ddlEjercicio.SelectedValue); // Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                ObjCuentas_Bancarias.Clave = txtClave.Text;
                ObjCuentas_Bancarias.Centro_Contable = ddlCentros_Contables.SelectedValue;
                ObjCuentas_Bancarias.Dependencia = ddlDependencia.SelectedValue;
                ObjCuentas_Bancarias.Banco = ddlBancos.SelectedValue;
                ObjCuentas_Bancarias.Cuenta_Bancaria = txtCuenta_Bancaria.Text;
                ObjCuentas_Bancarias.Cuenta_Contable = ddlCuentas_Contables.SelectedValue;
                ObjCuentas_Bancarias.Descripcion = txtDescripcion.Text.ToUpper();
                ObjCuentas_Bancarias.Fecha_Apertura = txtFecha_Apertura.Text;
                ObjCuentas_Bancarias.Localidad = txtLocalidad.Text.ToUpper();
                ObjCuentas_Bancarias.Status=Convert.ToChar(rdoBttnStatus.SelectedValue);
                ObjCuentas_Bancarias.Alta_Usuario = SesionUsu.Usu_Nombre;
                ObjCuentas_Bancarias.Tipo_Subsidio = ddlSubsidio.SelectedValue;
                if (SesionUsu.Editar == 0)
                    CNCuentas_Bancarias.Cuentas_BancariasInsertar(ObjCuentas_Bancarias, ref Verificador);
                else
                {
                    ObjCuentas_Bancarias.IdCuenta_Bancaria =Convert.ToInt32(grvCuentas_Bancarias.SelectedRow.Cells[0].Text);
                    CNCuentas_Bancarias.Cuentas_BancariasEditar(ObjCuentas_Bancarias, ref Verificador);
                }

                if (Verificador == "0")
                {
                    //lblError.Text = "Los datos han sido agregados correctamente";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(1, 'Los datos han sido agregados correctamente.');", true);

                    btnCancelar_Click(null, null);                   
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

        protected void ddlCentros_Contables_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblError.Text = string.Empty;
            try
            {
                //if(SesionUsu.Editar==0||SesionUsu.Editar==1)
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Dependencias", ref ddlDependencia, "p_centro_contable", "p_ejercicio", ddlCentros_Contables.SelectedValue, ddlEjercicio.SelectedValue);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Cuentas_Bancos", ref ddlCuentas_Contables, "p_ejercicio", "p_centro_contable", ddlEjercicio.SelectedValue, Convert.ToString(ddlCentros_Contables.SelectedValue));                
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void grvCuentas_Bancarias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvCuentas_Bancarias.PageIndex = 0;
            grvCuentas_Bancarias.PageIndex = e.NewPageIndex;
            CargarGrid();
        }

       
        protected void grvCuentas_Bancarias_SelectedIndexChanged(object sender, EventArgs e)
        {            
            Verificador = string.Empty;
            try
            {
                ObjCuentas_Bancarias.IdCuenta_Bancaria = Convert.ToInt32(grvCuentas_Bancarias.SelectedRow.Cells[0].Text);
                CNCuentas_Bancarias.Cuentas_BancariasSelect(ref ObjCuentas_Bancarias, ref Verificador);
                ddlEjercicio.SelectedValue = Convert.ToString(ObjCuentas_Bancarias.Ejercicio);
                txtClave.Text = ObjCuentas_Bancarias.Clave;
                ddlCentros_Contables.SelectedValue = ObjCuentas_Bancarias.Centro_Contable;
                ddlCentros_Contables_SelectedIndexChanged(null, null);
                ddlDependencia.SelectedValue = ObjCuentas_Bancarias.Dependencia;
                ddlSubsidio.SelectedValue= ObjCuentas_Bancarias.Tipo_Subsidio;
                if (ObjCuentas_Bancarias.Cuenta_Contable != "0")                                    
                    ddlCuentas_Contables.SelectedValue = ObjCuentas_Bancarias.Cuenta_Contable;
                

                ddlBancos.SelectedValue = ObjCuentas_Bancarias.Banco;
                txtCuenta_Bancaria.Text = ObjCuentas_Bancarias.Cuenta_Bancaria;
                txtDescripcion.Text = ObjCuentas_Bancarias.Descripcion;
                txtFecha_Apertura.Text = ObjCuentas_Bancarias.Fecha_Apertura;
                txtLocalidad.Text = ObjCuentas_Bancarias.Localidad;
                SesionUsu.Editar = 1;
                MultiView1.ActiveViewIndex = 1;
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void ddlCentros_Contables0_SelectedIndexChanged(object sender, EventArgs e)
        {
            CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Dependencias", ref ddlDependencia0, "p_centro_contable", "p_ejercicio", ddlCentros_Contables0.SelectedValue, ddlEjercicio.SelectedValue);
            ddlDependencia0_SelectedIndexChanged(null, null);
        }

        protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
        {
            CNComun.Limpiador_controles(this);
            txtFecha_Apertura.Text = System.DateTime.Now.ToString("dd/MM/") + SesionUsu.Usu_Ejercicio;
            ddlCentros_Contables.SelectedValue = ddlCentros_Contables0.SelectedValue;
            ddlCentros_Contables_SelectedIndexChanged(null, null);
            SesionUsu.Editar = 0;
            MultiView1.ActiveViewIndex = 1;
        }

        protected void grvCuentas_Bancarias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            Verificador = string.Empty;
            try
            {
                int fila = e.RowIndex;
                //int fila = e.RowIndex;
                int pagina = grvCuentas_Bancarias.PageSize * grvCuentas_Bancarias.PageIndex;
                fila = pagina + fila;

                ObjCuentas_Bancarias.IdCuenta_Bancaria = Convert.ToInt32(grvCuentas_Bancarias.Rows[fila].Cells[0].Text);
                CNCuentas_Bancarias.Cuentas_BancariasEliminar(ObjCuentas_Bancarias, ref Verificador);

                if (Verificador == "0")
                    CargarGrid();
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

        protected void imgbtnBuscar1_Click(object sender, ImageClickEventArgs e)
        {
            CargarGrid();

        }

        protected void ddlEjercicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if(SesionUsu.Editar==0||SesionUsu.Editar==1)
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Cuentas_Contables_N4", ref ddlCuentas_Contables, "p_ejercicio", "p_centro_contable", ddlEjercicio.SelectedValue, Convert.ToString(ddlCentros_Contables.SelectedValue));
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void ddlDependencia0_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void linkBttnNuevo_Click(object sender, EventArgs e)
        {
            CNComun.Limpiador_controles(this);
            txtFecha_Apertura.Text = System.DateTime.Now.ToString("dd/MM/") + SesionUsu.Usu_Ejercicio;
            ddlCentros_Contables.SelectedValue = ddlCentros_Contables0.SelectedValue;
            ddlCentros_Contables_SelectedIndexChanged(null, null);
            SesionUsu.Editar = 0;
            MultiView1.ActiveViewIndex = 1;
        }

        protected void linBttnClonar_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grvCuentas_Bancarias.SelectedIndex = row.RowIndex;
            grvCuentas_Bancarias_SelectedIndexChanged(null, null);
            ddlDependencia.SelectedValue = "00000";
            SesionUsu.Editar = 0;
            txtClave.Text = string.Empty;
        }
    }
}
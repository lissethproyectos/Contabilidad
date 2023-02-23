using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAF.Contabilidad.Form
{
    public partial class frmRegistroFacturas : System.Web.UI.Page
    {
        #region <Variables>
        Usuario Usuario = new Usuario();
        Sesion SesionUsu = new Sesion();
        CN_Comun CNComun = new CN_Comun();
        string Verificador = string.Empty;
        Poliza_CFDI ObjPolizaCFDI = new Poliza_CFDI();
        Poliza ObjPoliza = new Poliza();
        CN_Poliza_CFDI CNPolizaCFDI = new CN_Poliza_CFDI();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SesionUsu = (Sesion)Session["Usuario"];
            if (!IsPostBack)
            {
                Cargarcombos();
                CargarGridPolizas();
                MultiView1.ActiveViewIndex = 0;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "FiltroProveedor", "Autocomplete();", true);

        }

        private List<Poliza> GetList()
        {

            try
            {
                List<Poliza> List = new List<Poliza>();
                ObjPoliza.Centro_contable = DDLCentro_Contable.SelectedValue;
                ObjPoliza.Mes_anio = ddlMes.SelectedValue;
                CNPolizaCFDI.PolizasSinComprobar(ObjPoliza, ref List/*, txtBuscar.Text*/);
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //private void CargarGridPolizas()
        //{
        //    grvPolizaCFDI.DataSource = null;
        //    grvPolizaCFDI.DataBind();
        //    Int32[] Columnas = new Int32[] { 0 };
        //    decimal GranTotPorComprobar = 0;
        //    decimal GranTotComprobado = 0;

        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        grvPolizas.DataSource = GetList();
        //        grvPolizas.DataBind();
        //        double TotPorComprobar = 0;
        //        if (grvPolizas.Rows.Count > 0)
        //        {

        //            foreach (GridViewRow row in grvPolizas.Rows)
        //            {

        //                Label lblPorComprobar = row.FindControl("lblPorComprobar") as Label;
        //                GranTotPorComprobar = GranTotPorComprobar + Convert.ToDecimal(lblPorComprobar.Text);

        //                Label lblComprobado = row.FindControl("lblComprobado") as Label;
        //                GranTotComprobado = GranTotComprobado + Convert.ToDecimal(lblComprobado.Text);


        //            }

        //            Label lblTotPorComprobar = (Label)grvPolizas.FooterRow.FindControl("lblTotPorComprobar");
        //            Label lblTotComprobado = (Label)grvPolizas.FooterRow.FindControl("lblTotComprobado");

        //            lblTotPorComprobar.Text = Convert.ToString(GranTotPorComprobar.ToString("C"));
        //            lblTotComprobado.Text = Convert.ToString(GranTotComprobado.ToString("C"));

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Verificador = ex.Message;
        //        CNComun.VerificaTextoMensajeError(ref Verificador);
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
        //    }
        //}
        private void CargarGridPolizas()
        {
            grvPolizaCFDI.DataSource = null;
            grvPolizaCFDI.DataBind();
            Int32[] Columnas = new Int32[] { 0 };
            List<Poliza> lstPolizas = new List<Poliza>();

            try
            {

                ObjPoliza.Centro_contable = DDLCentro_Contable.SelectedValue;
                ObjPoliza.Mes_anio = ddlMes.SelectedValue;
                CNPolizaCFDI.PolizasSinComprobar(ObjPoliza, ref lstPolizas);
                CargarGridPolizas2(lstPolizas);

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        private void CargarGridPolizas2(List<Poliza> ListPolizas)
        {
            grvPolizas.DataSource = null;
            grvPolizas.DataBind();
            Int32[] Columnas = new Int32[] { 0 };
            try
            {
                double TotalPorComprobar;
                double TotalComprobado;

                DataTable dt = new DataTable();
                grvPolizas.DataSource = ListPolizas;
                grvPolizas.DataBind();
                if (ListPolizas.Count() > 0)
                {
                    TotalPorComprobar = ListPolizas.Sum(item => Convert.ToDouble(item.Tot_Cargo));
                    TotalComprobado = ListPolizas.Sum(item => Convert.ToDouble(item.Tot_Comprobado));

                    Label lblTot = (Label)grvPolizas.FooterRow.FindControl("lblTotPorComprobar");
                    Label lblTotComprobado = (Label)grvPolizas.FooterRow.FindControl("lblTotComprobado");

                    lblTot.Text = TotalPorComprobar.ToString("C");
                    lblTotComprobado.Text = TotalComprobado.ToString("C");

                    //lblTotInt.Text = Convert.ToString(TotalPorComprobar);
                }
                //CNComun.HideColumns(grvPolizas, Columnas);

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        private void Cargarcombos()
        {
            try
            {
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Proveedores", ref ddlProveedor);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Beneficiario", ref ddlTipoBeneficiario2);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Gasto", ref ddlTipoGasto2);


                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref DDLCentro_Contable, "p_usuario", "p_ejercicio", SesionUsu.Usu_Nombre, SesionUsu.Usu_Ejercicio);
                //DDLCentro_Contable.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

            }
        }



        protected void grvPolizaCFDI_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            lstPolizasCFDI = (List<Poliza_CFDI>)Session["PolizasCFDI"];
            try
            {

                grvPolizaCFDI.PageIndex = 0;
                grvPolizaCFDI.PageIndex = e.NewPageIndex;
                CargarGridPolizaCFDI(lstPolizasCFDI);
            }

            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProveedor.SelectedValue == "X")
                txtProveedor.Visible = true;
            else
                txtProveedor.Visible = false;

            txtRFC.Text = ddlProveedor.SelectedValue;
        }

        protected void bttnAgregaFactura_Click(object sender, EventArgs e)
        {
            string Ruta;
            string NombreArchivo;
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            string VerificadorCFDI = string.Empty;
            DateTime FechaActual = DateTime.Today;
            updPnlArchivos.Update();
            try
            {

                ObjPolizaCFDI.Beneficiario_Tipo = ddlTipoBeneficiario2.SelectedValue;
                ObjPolizaCFDI.Tipo_Gasto = ddlTipoGasto2.SelectedValue;
                ObjPolizaCFDI.Fecha_Captura = FechaActual.ToString("dd/MM/yyyy");
                ObjPolizaCFDI.Usuario_Captura = SesionUsu.Usu_Nombre;
                ObjPolizaCFDI.CFDI_Fecha = txtFecha.Text;
                ObjPolizaCFDI.CFDI_Total = Convert.ToDouble(txtImporte.Text);
                ObjPolizaCFDI.Tipo_Docto = ddlTipoDocto.SelectedValue;

                if (ddlProveedor.SelectedValue == "X")
                    ObjPolizaCFDI.CFDI_Nombre = txtProveedor.Text.ToUpper();
                else
                    ObjPolizaCFDI.CFDI_Nombre = ddlProveedor.SelectedValue;



                if (ddlTipoDocto.SelectedValue == "F")
                {
                    ObjPolizaCFDI.CFDI_UUID = txtFolio.Text;
                    ObjPolizaCFDI.CFDI_RFC = txtRFC.Text;
                }
                else
                {
                    ObjPolizaCFDI.CFDI_UUID = string.Empty;
                    //ObjPolizaCFDI.CFDI_Nombre = string.Empty;
                    ObjPolizaCFDI.CFDI_RFC = string.Empty;

                }


                if (FileFactura.HasFile)
                {
                    NombreArchivo = FileFactura.FileName.ToUpper();
                    if (NombreArchivo.Contains(".XML"))
                    {
                        Ruta = Path.Combine(Server.MapPath("~/AdjuntosExtras"), grvPolizas.SelectedRow.Cells[0].Text + "-" + grvPolizas.SelectedRow.Cells[1].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[4].Text + "-" + NombreArchivo);
                        FileFactura.SaveAs(Ruta);
                        ObjPolizaCFDI.NombreArchivoXML = grvPolizas.SelectedRow.Cells[0].Text + "-" + grvPolizas.SelectedRow.Cells[1].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[4].Text + "-" + NombreArchivo;
                        ObjPolizaCFDI.Ruta_XML = "~/AdjuntosExtras/" + ObjPolizaCFDI.NombreArchivoXML;
                    }
                }


                if (FileFacturaPDF.HasFile)
                {
                    NombreArchivo = FileFacturaPDF.FileName.ToUpper();
                    if (NombreArchivo.Contains(".PDF"))
                    {
                        Ruta = Path.Combine(Server.MapPath("~/AdjuntosExtras"), grvPolizas.SelectedRow.Cells[0].Text + "-" + grvPolizas.SelectedRow.Cells[1].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[4].Text + "-" + NombreArchivo);
                        FileFacturaPDF.SaveAs(Ruta);
                        ObjPolizaCFDI.NombreArchivoPDF = grvPolizas.SelectedRow.Cells[0].Text + "-" + grvPolizas.SelectedRow.Cells[1].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[4].Text + "-" + NombreArchivo;
                        ObjPolizaCFDI.Ruta_PDF = "~/AdjuntosExtras/" + ObjPolizaCFDI.NombreArchivoPDF;
                    }
                }

                if (Session["PolizasCFDI"] != null)
                    lstPolizasCFDI = (List<Poliza_CFDI>)Session["PolizasCFDI"];

                lstPolizasCFDI.Add(ObjPolizaCFDI);
                Session["PolizasCFDI"] = lstPolizasCFDI;
                CargarGridPolizaCFDI(lstPolizasCFDI);
                LimpiarCampos();

            }

            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        private void CargarGridPolizaCFDI(List<Poliza_CFDI> ListPolizaCFDI)
        {
            grvPolizaCFDI.DataSource = null;
            grvPolizaCFDI.DataBind();
            Int32[] Celdas = new Int32[] { 12, 13 };
            Int32[] Celdas2 = new Int32[] { 10, 11, 12 };
            try
            {
                double TotalPagos;
                DataTable dt = new DataTable();
                grvPolizaCFDI.DataSource = ListPolizaCFDI;
                grvPolizaCFDI.DataBind();
                if (ListPolizaCFDI.Count() > 0)
                {
                    TotalPagos = ListPolizaCFDI.Sum(item => Convert.ToDouble(item.CFDI_Total));

                    Label lblTot = (Label)grvPolizaCFDI.FooterRow.FindControl("lblGranTotal");
                    Label lblTotInt = (Label)grvPolizaCFDI.FooterRow.FindControl("lblGranTotalInt");

                    lblTot.Text = TotalPagos.ToString("C");
                    lblTotInt.Text = Convert.ToString(TotalPagos);
                }
                CNComun.HideColumns(grvPolizaCFDI, Celdas);

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void linkBttnAgregar_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grvPolizas.SelectedIndex = row.RowIndex;

            LimpiarCampos();
            grvPolizaCFDI.DataSource = null;
            grvPolizaCFDI.DataBind();
            Session["PolizasCFDI"] = null;
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            ObjPolizaCFDI.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
            CNPolizaCFDI.PolizaCFDIExtrasConsultaDatos(ObjPolizaCFDI, Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text), Convert.ToString(grvPolizas.SelectedRow.Cells[1].Text), ref lstPolizasCFDI, ref Verificador);
            if (lstPolizasCFDI.Count > 0)
            {
                Session["PolizasCFDI"] = lstPolizasCFDI;
                CargarGridPolizaCFDI(lstPolizasCFDI);
            }
            MultiView1.ActiveViewIndex = 1;
        }

        protected void LimpiarCampos()
        {
            ddlTipoBeneficiario2.SelectedIndex = 0;
            ddlTipoGasto2.SelectedIndex = 0;
            txtFecha.Text = string.Empty;
            txtFolio.Text = string.Empty;
            txtProveedor.Text = string.Empty;
            ddlProveedor.SelectedIndex = 0;
            ddlProveedor_SelectedIndexChanged(null, null);
            //grvPolizaCFDI.DataSource = null;
            //grvPolizaCFDI.DataBind();
        }


        protected void linkBttnBuscar_Click(object sender, EventArgs e)
        {
            CargarGridPolizas();
        }

        protected void btnCancelarCFDI_Click(object sender, EventArgs e)
        {
            grvPolizaCFDI.EditIndex = -1;
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnGuardarCFDI_Click(object sender, EventArgs e)
        {
            /*DATOS CFDI XML*/
            Verificador = string.Empty;
            grvPolizaCFDI.EditIndex = -1;
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            //Poliza objPolizas = new Poliza();
            //double total = 0;
            try
            {
                if (grvPolizaCFDI.Rows.Count >= 1)
                {
                    Label lblTot = (Label)grvPolizaCFDI.FooterRow.FindControl("lblGranTotalInt");
                    double lblTotInt = Convert.ToDouble(lblTot.Text);
                    lblTotInt = Math.Ceiling(lblTotInt);
                    ObjPoliza.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
                    ObjPoliza.Partida = Convert.ToString(grvPolizas.SelectedRow.Cells[1].Text);


                    if (Session["PolizasCFDI"] != null)
                        lstPolizasCFDI = (List<Poliza_CFDI>)Session["PolizasCFDI"];


                    //ObjPolizaCFDI.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);

                    CNPolizaCFDI.PolizaCFDIExtraEditar(ObjPoliza, lstPolizasCFDI, ref Verificador);

                    if (Verificador == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(1, 'Los datos han sido agregados correctamente.');", true);

                        SesionUsu.Editar = -1;
                        MultiView1.ActiveViewIndex = 0;
                        CargarGridPolizas();



                    }
                    else
                    {
                        //SesionUsu.Editar = -1;
                        //MultiView1.ActiveViewIndex = 0;
                        //CargarGrid(0);
                        CNComun.VerificaTextoMensajeError(ref Verificador);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

                    }
                }
                else
                {
                    ObjPoliza.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
                    ObjPoliza.Partida = Convert.ToString(grvPolizas.SelectedRow.Cells[1].Text);

                    CNPolizaCFDI.EliminarCFDIExtra(ObjPoliza, ref Verificador);
                    if (Verificador == "0")
                    {
                        SesionUsu.Editar = -1;
                        MultiView1.ActiveViewIndex = 0;
                        CargarGridPolizas();
                    }
                }
                /*FIN DATOS CFDI XML*/
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void grvPolizaCFDI_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            lstPolizasCFDI = (List<Poliza_CFDI>)Session["PolizasCFDI"];
            try
            {
                int fila = e.RowIndex;
                int pagina = grvPolizaCFDI.PageSize * grvPolizaCFDI.PageIndex;
                fila = pagina + fila;
                lstPolizasCFDI.RemoveAt(fila);
                Session["PolizasCFDI"] = lstPolizasCFDI;
                CargarGridPolizaCFDI(lstPolizasCFDI);
            }

            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }


        protected void ddlTipoDocto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoDocto.SelectedValue == "R")
            {
                fileXML.Visible = false;
                colUUID1.Visible = false;
                //colUUID2.Visible = false;
            }
            else
            {
                fileXML.Visible = true;
                colUUID1.Visible = true;
                //colUUID2.Visible = true;
            }
        }

        protected void grvPolizas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvPolizas.PageIndex = 0;
            grvPolizas.PageIndex = e.NewPageIndex;
            CargarGridPolizas();
        }

        protected void grvPolizaCFDI_RowEditing(object sender, GridViewEditEventArgs e)
        {
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            lstPolizasCFDI = (List<Poliza_CFDI>)Session["PolizasCFDI"];
            try
            {
                grvPolizaCFDI.EditIndex = e.NewEditIndex;
                Session["PolizasCFDI"] = lstPolizasCFDI;
                CargarGridPolizaCFDI(lstPolizasCFDI);

                //((DropDownList)grvInventarioAgregado.Rows[e.NewEditIndex].FindControl("DDLUnidadMedida")).Enabled = true;
                //((TextBox)grvInventarioAgregado.Rows[e.NewEditIndex].FindControl("txtCantidad")).Enabled = true;
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void grvPolizaCFDI_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            lstPolizasCFDI = (List<Poliza_CFDI>)Session["PolizasCFDI"];
            grvPolizaCFDI.EditIndex = -1;
            CargarGridPolizaCFDI(lstPolizasCFDI);
        }

        protected void grvPolizaCFDI_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            lstPolizasCFDI = (List<Poliza_CFDI>)Session["PolizasCFDI"];
            try
            {
                int fila = e.RowIndex;
                int pagina = grvPolizaCFDI.PageSize * grvPolizaCFDI.PageIndex;
                fila = pagina + fila;

                GridViewRow row = grvPolizaCFDI.Rows[e.RowIndex];
                lstPolizasCFDI[e.RowIndex].CFDI_UUID = Convert.ToString(((TextBox)(row.Cells[3].Controls[0])).Text);
                lstPolizasCFDI[e.RowIndex].CFDI_Fecha = Convert.ToString(((TextBox)(row.Cells[4].Controls[0])).Text);
                //lstPolizasCFDI[e.RowIndex].CFDI_Nombre = Convert.ToString(((TextBox)(row.Cells[5].Controls[0])).Text);
                //lstPolizasCFDI[e.RowIndex].CFDI_RFC = Convert.ToString(((TextBox)(row.Cells[6].Controls[0])).Text);
                TextBox txt = (TextBox)(row.Cells[7].FindControl("txtTotal"));


                lstPolizasCFDI[e.RowIndex].CFDI_Total = Convert.ToDouble(txt.Text);

                grvPolizaCFDI.EditIndex = -1;
                Session["PolizasCFDI"] = lstPolizasCFDI;
                CargarGridPolizaCFDI(lstPolizasCFDI);
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void imgBttnPdf(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgBttnExcel(object sender, ImageClickEventArgs e)
        {

        }
    }
}
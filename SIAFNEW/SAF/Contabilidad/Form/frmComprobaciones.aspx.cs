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
using System.Xml;

namespace SAF.Contabilidad.Form
{
    public partial class frmComprobaciones : System.Web.UI.Page
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
            //MultiView1.ActiveViewIndex = 1;
            SesionUsu = (Sesion)Session["Usuario"];
            if (!IsPostBack)
            {
                Cargarcombos();
                CargarGridPolizas();
                MultiView1.ActiveViewIndex = 0;
            }
            //ScriptManager.RegisterStartupScript(this, GetType(), "FiltroProveedor", "Autocomplete();", true);
        }

        protected void linkBttnBuscar_Click(object sender, EventArgs e)
        {
            CargarGridPolizas();

        }

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
                CNPolizaCFDI.PolizasPorComprobar(ObjPoliza, ref lstPolizas);
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


                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Beneficiario", ref ddlTipo_Beneficiario);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Gasto", ref ddlTipo_Gasto);


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



        protected void linkBttnAgregar_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grvPolizas.SelectedIndex = row.RowIndex;

            //LimpiarCampos();
            //grvPolizaCFDI.DataSource = null;
            //grvPolizaCFDI.DataBind();
            //Session["PolizasCFDI"] = null;
            //List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            //ObjPolizaCFDI.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
            //CNPolizaCFDI.PolizaCFDIExtrasConsultaDatos(ObjPolizaCFDI, Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text), Convert.ToString(grvPolizas.SelectedRow.Cells[1].Text), ref lstPolizasCFDI, ref Verificador);
            //if (lstPolizasCFDI.Count > 0)
            //{
            //    Session["PolizasCFDI"] = lstPolizasCFDI;
            //    CargarGridPolizaCFDI(lstPolizasCFDI);
            //}
            updPnlDoctos.Update();
            MultiView1.ActiveViewIndex = 1;

        }

        protected void bttnAgregaFactura_Click(object sender, EventArgs e)
        {
            string Ruta;
            string NombreArchivo;
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            string VerificadorCFDI = string.Empty;

            try
            {
                if (FileFactura.HasFile)
                {
                    NombreArchivo = FileFactura.FileName.ToUpper();
                    if (NombreArchivo.Contains(".XML"))
                    {
                        DateTime FechaActual = DateTime.Today;

                        XmlDocument xDoc = new XmlDocument();
                        Ruta = Path.Combine(Server.MapPath("~/AdjuntosTemp"), grvPolizas.SelectedRow.Cells[9].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[3].Text + "-" + NombreArchivo);
                        FileFactura.SaveAs(Ruta);
                        ObjPolizaCFDI.Tipo_Docto = "FACTURA";
                        ObjPolizaCFDI.Beneficiario_Tipo = ddlTipo_Beneficiario.SelectedValue;
                        ObjPolizaCFDI.Tipo_Gasto = ddlTipo_Gasto.SelectedValue;
                        ObjPolizaCFDI.NombreArchivoXML = grvPolizas.SelectedRow.Cells[9].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[3].Text + "-" + NombreArchivo;
                        ObjPolizaCFDI.Fecha_Captura = FechaActual.ToString("dd/MM/yyyy");
                        ObjPolizaCFDI.Usuario_Captura = SesionUsu.Usu_Nombre;
                        ObjPolizaCFDI.Ruta_XML = "~/AdjuntosTemp/" + ObjPolizaCFDI.NombreArchivoXML;

                        xDoc.Load(Ruta);
                        XmlNodeList datos = xDoc.GetElementsByTagName("cfdi:Comprobante");

                        foreach (XmlElement nodo in datos)
                        {
                            try
                            {
                                ObjPolizaCFDI.CFDI_Folio = string.Empty;

                                if (nodo.Attributes["Folio"] != null)
                                    ObjPolizaCFDI.CFDI_Folio = nodo.Attributes["Folio"].InnerText;

                                if (nodo.Attributes["folio"] != null)
                                    ObjPolizaCFDI.CFDI_Folio = nodo.Attributes["folio"].InnerText;

                                if (nodo.Attributes["FOLIO"] != null)
                                    ObjPolizaCFDI.CFDI_Folio = nodo.Attributes["FOLIO"].InnerText;


                            }
                            catch (Exception)
                            {
                                ObjPolizaCFDI.CFDI_Folio = string.Empty;
                            }

                            /*BUSCA CAMPO FECHA*/
                            if (nodo.Attributes["Fecha"] != null)
                                ObjPolizaCFDI.CFDI_Fecha = nodo.Attributes["Fecha"].InnerText;

                            if (nodo.Attributes["fecha"] != null)
                                ObjPolizaCFDI.CFDI_Fecha = nodo.Attributes["fecha"].InnerText;

                            if (nodo.Attributes["FECHA"] != null)
                                ObjPolizaCFDI.CFDI_Fecha = nodo.Attributes["FECHA"].InnerText;
                            /*FIN CAMPO FECHA*/

                            /*BUSCA CAMPO TOTAL*/
                            if (nodo.Attributes["Total"] != null)
                                ObjPolizaCFDI.CFDI_Total = Convert.ToDouble(nodo.Attributes["Total"].InnerText);

                            if (nodo.Attributes["total"] != null)
                                ObjPolizaCFDI.CFDI_Total = Convert.ToDouble(nodo.Attributes["total"].InnerText);

                            if (nodo.Attributes["TOTAL"] != null)
                                ObjPolizaCFDI.CFDI_Total = Convert.ToDouble(nodo.Attributes["TOTAL"].InnerText);
                            /*FIN CAMPO TOTAL*/


                            XmlNodeList listEmisor = nodo.GetElementsByTagName("cfdi:Emisor");
                            XmlNodeList listReceptor = nodo.GetElementsByTagName("cfdi:Receptor");
                            XmlNodeList listConcepto = nodo.GetElementsByTagName("cfdi:Conceptos");
                            XmlNodeList listImpuesto = nodo.GetElementsByTagName("cfdi:Impuestos");
                            XmlNodeList listComplemento = nodo.GetElementsByTagName("cfdi:Complemento");

                            if (listEmisor.Count >= 1)
                            {
                                /*BUSCA CAMPO NOMBRE*/
                                if (SesionUsu.Usu_TipoUsu == "3" || SesionUsu.Usu_TipoUsu == "2")
                                    ObjPolizaCFDI.CFDI_Nombre = string.Empty;

                                if (listEmisor[0].Attributes["Nombre"] != null)
                                    ObjPolizaCFDI.CFDI_Nombre = listEmisor[0].Attributes["Nombre"].InnerText;

                                if (listEmisor[0].Attributes["nombre"] != null)
                                    ObjPolizaCFDI.CFDI_Nombre = listEmisor[0].Attributes["nombre"].InnerText;

                                if (listEmisor[0].Attributes["NOMBRE"] != null)
                                    ObjPolizaCFDI.CFDI_Nombre = listEmisor[0].Attributes["NOMBRE"].InnerText;
                                /*FIN CAMPO NOMBRE*/

                                /*BUSCA CAMPO RFC*/
                                if (listEmisor[0].Attributes["Rfc"] != null)
                                    ObjPolizaCFDI.CFDI_RFC = listEmisor[0].Attributes["Rfc"].InnerText;

                                if (listEmisor[0].Attributes["rfc"] != null)
                                    ObjPolizaCFDI.CFDI_RFC = listEmisor[0].Attributes["rfc"].InnerText;

                                if (listEmisor[0].Attributes["RFC"] != null)
                                    ObjPolizaCFDI.CFDI_RFC = listEmisor[0].Attributes["RFC"].InnerText;
                                /*FIN CAMPO RFC*/


                                //ObjPolizaCFDI.CFDI_Nombre = listEmisor[0].Attributes["Nombre"].InnerText;
                                //ObjPolizaCFDI.CFDI_RFC = listEmisor[0].Attributes["Rfc"].InnerText;

                            }
                            else
                                VerificadorCFDI = "ERROR";

                            if (listComplemento.Count >= 1)
                            {
                                XmlNodeList listTimbreDigital =
                                ((XmlElement)listComplemento[0]).GetElementsByTagName("tfd:TimbreFiscalDigital");
                                ObjPolizaCFDI.CFDI_UUID = listTimbreDigital[0].Attributes["UUID"].InnerText;
                            }
                            else
                                VerificadorCFDI = "ERROR";
                        }

                    }
                }

                /*Archivo PDF*/
                if (FileFacturaPDF.HasFile)
                {
                    NombreArchivo = FileFacturaPDF.FileName.ToUpper();
                    if (NombreArchivo.Contains(".PDF"))
                    {

                        //Ruta = Path.Combine(Server.MapPath("~/AdjuntosTemp"), DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[3].Text + "-"+NombreArchivo);
                        Ruta = Path.Combine(Server.MapPath("~/AdjuntosTemp"), grvPolizas.SelectedRow.Cells[9].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[3].Text + "-" + NombreArchivo);
                        FileFacturaPDF.SaveAs(Ruta);
                        //ObjPolizaCFDI.NombreArchivoPDF = grvPolizas.SelectedRow.Cells[3].Text + DDLCentro_Contable.SelectedValue + "-" + NombreArchivo;
                        ObjPolizaCFDI.NombreArchivoPDF = grvPolizas.SelectedRow.Cells[9].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[3].Text + "-" + NombreArchivo;
                        ObjPolizaCFDI.Ruta_PDF = "~/AdjuntosTemp/" + ObjPolizaCFDI.NombreArchivoPDF;
                    }
                    else
                    {
                        Verificador = "Documento invalido, deber ser un PDF.";
                    }
                }
                /*Fin Archivo PDF*/

                if (VerificadorCFDI == string.Empty)
                {
                    if (Session["PolizasCFDI"] != null)
                        lstPolizasCFDI = (List<Poliza_CFDI>)Session["PolizasCFDI"];

                    lstPolizasCFDI.Add(ObjPolizaCFDI);
                    Session["PolizasCFDI"] = lstPolizasCFDI;
                    CargarGridPolizaCFDI(lstPolizasCFDI);
                    //LimpiaCamposFiscales();
                }
                else
                {
                    Verificador = "Error en el XML, faltan uno de los siguientes campos: fecha, total, nombre, rfc, UUID (principal)";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

                }

            }

            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "modal", "mostrar_modal( 0, '" + MsjError + "');", true);  //lblMsjFam.Text = Verificador;
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);


            }
        }
        private void CargarGridPolizaCFDI(List<Poliza_CFDI> ListPolizaCFDI)
        {
            Poliza_CFDI objPoliza = new Poliza_CFDI();
            List<Poliza_CFDI> lstPartidas = new List<Poliza_CFDI>();
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
                    //GridView grvPartidas = (GridView)grvPolizaCFDI.FooterRow.FindControl("grdPartidas");


                    lblTot.Text = TotalPagos.ToString("C");
                    lblTotInt.Text = Convert.ToString(TotalPagos);


                        CNComun.HideColumns(grvPolizaCFDI, Celdas);
                    //for (int i = 0; i < Columnas.Length; i++)
                    //{
                    //    grvPolizaCFDI.HeaderRow.Cells[Convert.ToInt32(Columnas.GetValue(i))].Visible = false;
                    //objPoliza.IdPoliza = 1204918;
                    //foreach (GridViewRow row in grvPolizaCFDI.Rows)
                    //{
                    //    CNPolizaCFDI.PolizaPartidasDatos(objPoliza, ref lstPartidas, ref Verificador);                     
                    //    GridView grvPartidas = (GridView)(row.Cells[0].FindControl("grdPartidas"));
                    //    DataTable dt2 = new DataTable();
                    //    grvPartidas.DataSource = lstPartidas;
                    //    grvPartidas.DataBind();
                    //}
                    //    grdView.FooterRow.Cells[Convert.ToInt32(Columnas.GetValue(i))].Visible = false;
                    //}

                    //if (grvPolizas.SelectedRow.Cells[16].Text == "S")
                    //{
                    //    CNComun.HideColumns(grvPolizaCFDI, Celdas);
                    //}
                    //else
                    //{
                    //    CNComun.HideColumns(grvPolizaCFDI, Celdas2);
                    //}

                }

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void linkBtnnAgregar_Click(object sender, EventArgs e)
        {
            Poliza_CFDI objPoliza = new Poliza_CFDI();
            List<Poliza_CFDI> lstPartidas = new List<Poliza_CFDI>();
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            GridView grvPartidas = (GridView)(row.Cells[0].FindControl("grdPartidas"));
            DataTable dt2 = new DataTable();

            if (cbi.Text == "- Ocultar")
            {
                grvPartidas.DataSource = null;
                grvPartidas.DataBind();
                cbi.Text = "+ Partida";
            }
            else
            {
                cbi.Text = "- Ocultar";
                grvPolizaCFDI.SelectedIndex = row.RowIndex;
                objPoliza.IdPoliza = 1204918;

                CNPolizaCFDI.PolizaPartidasDatos(objPoliza, ref lstPartidas, ref Verificador);
                grvPartidas.DataSource = lstPartidas;
                grvPartidas.DataBind();
            }
        }
    }
}
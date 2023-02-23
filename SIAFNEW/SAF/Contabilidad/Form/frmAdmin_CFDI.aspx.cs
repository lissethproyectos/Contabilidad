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
    public partial class frmAdmin_CFDI : System.Web.UI.Page
    {
        #region <Variables>
        Int32[] Celdas = new Int32[] { 0 };
        string Verificador = string.Empty;
        string MsjError = string.Empty;
        Usuario Usuario = new Usuario();
        Sesion SesionUsu = new Sesion();
        CN_Comun CNComun = new CN_Comun();
        CN_Poliza CNPoliza = new CN_Poliza();
        CN_Poliza_CFDI CNPolizaCFDI = new CN_Poliza_CFDI();
        CN_Usuario CNUsuario = new CN_Usuario();
        List<Poliza_CFDI> ListPolizaCFDI = new List<Poliza_CFDI>();

        Comun ObjCC = new Comun();
        Poliza ObjPoliza = new Poliza();
        Poliza_CFDI ObjPolizaCFDI = new Poliza_CFDI();
        #endregion
        private void Inicializar()
        {
            Cargarcombos();
        }

        private void Cargarcombos()
        {
            Verificador = string.Empty;
            try
            {
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Beneficiario", ref ddlTipo_Beneficiario);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Gasto", ref ddlTipo_Gasto);
                ddlTipo_Beneficiario.Items.RemoveAt(0);
                ddlTipo_Beneficiario.Items.Insert(0, new ListItem("-- TODOS --", "T"));

                ddlTipo_Gasto.Items.RemoveAt(0);
                ddlTipo_Gasto.Items.Insert(0, new ListItem("-- TODOS --", "T"));



                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref DDLCentro_Contable, "p_usuario", "p_ejercicio", SesionUsu.Usu_Nombre, SesionUsu.Usu_Ejercicio);
                DDLCentro_Contable.Items.Insert(0, new ListItem("-- TODOS --", "00000"));
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

            }
        }

        private void CargarGridPolizaCFDI()
        {
            grvPolizaCFDI.DataSource = null;
            grvPolizaCFDI.DataBind();
            //Int32[] Celdas = new Int32[] { 0, 2 };
            try
            {
                DataTable dt = new DataTable();
                grvPolizaCFDI.DataSource = GetList();
                grvPolizaCFDI.DataBind();

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        private List<Poliza_CFDI> GetList()
        {
            try
            {
                List<Poliza_CFDI> List = new List<Poliza_CFDI>();
                ObjPolizaCFDI.Tipo_Gasto = ddlTipo_Gasto.SelectedValue;
                ObjPolizaCFDI.Beneficiario_Tipo = ddlTipo_Beneficiario.SelectedValue;
                ObjPolizaCFDI.Centro_Contable = DDLCentro_Contable.SelectedValue;
                ObjPolizaCFDI.Ejercicio = SesionUsu.Usu_Ejercicio;
                ObjPolizaCFDI.Mes_anio = ddlMes.SelectedValue;
                CNPolizaCFDI.PolizaCFDIConsultaDatosAdmin(ObjPolizaCFDI, ref List, txtBuscar.Text);
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            SesionUsu = (Sesion)Session["Usuario"];

            if (!IsPostBack)
                Inicializar();

            ScriptManager.RegisterStartupScript(this, GetType(), "GridPolizasCFDI", "PolizasCFDI();", true);

        }

        protected void imgbtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CargarGridPolizaCFDI();
        }

        protected void grvPolizaCFDI_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvPolizaCFDI.PageIndex = 0;
            grvPolizaCFDI.PageIndex = e.NewPageIndex;
            CargarGridPolizaCFDI();

        }

        protected void imgBttnPDF_Click(object sender, ImageClickEventArgs e)
        {
            string ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=RP_CFDIS&parametro1=" + ddlTipo_Gasto.SelectedValue + "&parametro2=" + ddlTipo_Beneficiario.SelectedValue + "&parametro3=" + txtBuscar.Text + "&parametro4=" + SesionUsu.Usu_Ejercicio;
            string _open = "window.open('" + ruta + "', 'miniContenedor', 'toolbar=yes', 'location=no', 'menubar=yes', 'resizable=yes');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
        }

        protected void imgBttnExcel_Click(object sender, ImageClickEventArgs e)
        {
            string ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=RP_CFDISxls&parametro1=" + ddlTipo_Gasto.SelectedValue + "&parametro2=" + ddlTipo_Beneficiario.SelectedValue + "&parametro3=" + txtBuscar.Text + "&parametro4=" + SesionUsu.Usu_Ejercicio;
            string _open = "window.open('" + ruta + "', 'miniContenedor', 'toolbar=yes', 'location=no', 'menubar=yes', 'resizable=yes');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
        }

        protected void linkBttnBuscar_Click(object sender, EventArgs e)
        {
            CargarGridPolizaCFDI();
        }

        protected void grvPolizaCFDI_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Ruta;
            Verificador = string.Empty;
            string Concepto_Descripcion = string.Empty;
            int fila = 0;
            ObjPolizaCFDI.Id_CFDI = Convert.ToInt32(grvPolizaCFDI.SelectedRow.Cells[0].Text);
            CNPolizaCFDI.PolizaCFDIConsulta(ref ObjPolizaCFDI, ref Verificador);
            if (Verificador == "0")
            {
                Ruta = Path.Combine(Server.MapPath("~/AdjuntosTemp"), ObjPolizaCFDI.Ruta_XML);


                XmlDocument xDoc = new XmlDocument();


                xDoc.Load(Ruta);



                XmlNodeList datos = xDoc.GetElementsByTagName("cfdi:Comprobante");

                foreach (XmlElement nodo in datos)
                {
                    try
                    {
                        XmlNodeList listConcepto = nodo.GetElementsByTagName("cfdi:Conceptos");
                        if (listConcepto.Count >= 1)
                        {
                            XmlNodeList nodoLstConceptos =
                           ((XmlElement)listConcepto[0]).GetElementsByTagName("cfdi:Concepto");
                            for (int i = 0; i < nodoLstConceptos.Count; i++)
                            {
                                Poliza_CFDI_Det objCFDIDet = new Poliza_CFDI_Det();
                                fila = fila + 1;
                                Concepto_Descripcion = Concepto_Descripcion + "CONCEPTO " + fila + "----------------------\n";
                                if (nodoLstConceptos[i].Attributes["Descripcion"] != null)
                                    Concepto_Descripcion = Concepto_Descripcion + "Descripcion: " + nodoLstConceptos[i].Attributes["Descripcion"].InnerText + "\n";
                                if (nodoLstConceptos[i].Attributes["ClaveProdServ"] != null)
                                    Concepto_Descripcion = Concepto_Descripcion + "ClaveProdServ: " + nodoLstConceptos[i].Attributes["ClaveProdServ"].InnerText + "\n";
                                if (nodoLstConceptos[i].Attributes["Cantidad"] != null)
                                    Concepto_Descripcion = Concepto_Descripcion + "Cantidad: " + nodoLstConceptos[i].Attributes["Cantidad"].InnerText + "\n";
                                if (nodoLstConceptos[i].Attributes["ClaveUnidad"] != null)
                                    Concepto_Descripcion = Concepto_Descripcion + "ClaveUnidad: " + nodoLstConceptos[i].Attributes["ClaveUnidad"].InnerText + "\n";
                                if (nodoLstConceptos[i].Attributes["Unidad"] != null)
                                    Concepto_Descripcion = Concepto_Descripcion + "Unidad: " + nodoLstConceptos[i].Attributes["Unidad"].InnerText + "\n";
                                if (nodoLstConceptos[i].Attributes["ValorUnitario"] != null)
                                    Concepto_Descripcion = Concepto_Descripcion + "ValorUnitario: " + nodoLstConceptos[i].Attributes["ValorUnitario"].InnerText + "\n";
                                if (nodoLstConceptos[i].Attributes["Importe"] != null)
                                    Concepto_Descripcion = Concepto_Descripcion + "Importe: " + nodoLstConceptos[i].Attributes["Importe"].InnerText + "\n";




                            }
                            ObjPolizaCFDI.CFDI_Concepto_Descripcion = Concepto_Descripcion;
                        }

                        if (ObjPolizaCFDI.CFDI_Concepto_Descripcion != string.Empty)
                        {
                            Verificador = string.Empty;
                            ObjPolizaCFDI.Id_CFDI = Convert.ToInt32(grvPolizaCFDI.SelectedRow.Cells[0].Text);
                            CNPolizaCFDI.PolizaCFDIEditarConceptos(ObjPolizaCFDI, ref Verificador);
                            if (Verificador == "0")
                                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(1, 'El concepto fue actualizado correctamente.');", true);
                            else
                            {
                                CNComun.VerificaTextoMensajeError(ref Verificador);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        ObjPolizaCFDI.CFDI_Folio = string.Empty;
                    }
                }
            }
        }

        protected void linkBttnVer_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grvPolizaCFDI.SelectedIndex = row.RowIndex;
            ObjPolizaCFDI.Id_CFDI = Convert.ToInt32(grvPolizaCFDI.SelectedRow.Cells[0].Text);
            CNPolizaCFDI.PolizaCFDIConsultaConceptos(ObjPolizaCFDI, ref ListPolizaCFDI);
            if (ListPolizaCFDI.Count >= 1)
                lblConceptos.Text = ListPolizaCFDI[0].CFDI_Concepto_Descripcion;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupError", "$('#modalConceptos').modal('show')", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Ruta;
            Verificador = string.Empty;
            string Concepto_Descripcion = string.Empty;
            int fila;

            try
            {
                foreach (GridViewRow gvrow in grvPolizaCFDI.Rows)
                {
                    ObjPolizaCFDI.CFDI_Concepto_Descripcion = string.Empty;
                    Concepto_Descripcion = string.Empty;
                    fila = 0;
                    SesionUsu.Editar = 0;
                    ObjPolizaCFDI.Id_CFDI = Convert.ToInt32(gvrow.Cells[0].Text);
                    CNPolizaCFDI.PolizaCFDIConsulta(ref ObjPolizaCFDI, ref Verificador);
                    if (Verificador == "0")
                    {
                        Ruta = Path.Combine(Server.MapPath("~/AdjuntosTemp"), ObjPolizaCFDI.Ruta_XML);


                        XmlDocument xDoc = new XmlDocument();
                        try
                        {

                            xDoc.Load(Ruta);
                        }
                        catch
                        {
                            SesionUsu.Editar = 1;
                        }

                        if (SesionUsu.Editar == 0)
                        {
                            XmlNodeList datos = xDoc.GetElementsByTagName("cfdi:Comprobante");

                            foreach (XmlElement nodo in datos)
                            {
                                try
                                {
                                    XmlNodeList listEmisor = nodo.GetElementsByTagName("cfdi:Emisor");
                                    if (listEmisor.Count >= 1)
                                    {
                                        if (listEmisor[0].Attributes["RegimenFiscal"] != null)
                                            ObjPolizaCFDI.RegimenFiscal = listEmisor[0].Attributes["RegimenFiscal"].InnerText;

                                        if (listEmisor[0].Attributes["regimenfiscal"] != null)
                                            ObjPolizaCFDI.RegimenFiscal = listEmisor[0].Attributes["regimenfiscal"].InnerText;

                                        if (listEmisor[0].Attributes["REGIMENFISCAL"] != null)
                                            ObjPolizaCFDI.RegimenFiscal = listEmisor[0].Attributes["REGIMENFISCAL"].InnerText;


                                        if (ObjPolizaCFDI.RegimenFiscal != string.Empty)
                                        {
                                            Verificador = string.Empty;
                                            ObjPolizaCFDI.Id_CFDI = Convert.ToInt32(gvrow.Cells[0].Text);
                                            CNPolizaCFDI.PolizaCFDIEditar(ObjPolizaCFDI, ref Verificador);
                                            if (Verificador == "0")
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(1, 'El concepto fue actualizado correctamente.');", true);
                                            else
                                            {
                                                CNComun.VerificaTextoMensajeError(ref Verificador);
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                                            }


                                        }
                                    }
                                        //XmlNodeList listConcepto = nodo.GetElementsByTagName("cfdi:Conceptos");
                                        //if (listConcepto.Count >= 1)
                                        //{
                                        //    XmlNodeList nodoLstConceptos =
                                        //   ((XmlElement)listConcepto[0]).GetElementsByTagName("cfdi:Concepto");
                                        //    for (int i = 0; i < nodoLstConceptos.Count; i++)
                                        //    {
                                        //        Poliza_CFDI_Det objCFDIDet = new Poliza_CFDI_Det();
                                        //        fila = fila + 1;
                                        //        Concepto_Descripcion = Concepto_Descripcion + "CONCEPTO " + fila + "----------------------\n";
                                        //        if (nodoLstConceptos[i].Attributes["Descripcion"] != null)
                                        //            Concepto_Descripcion = Concepto_Descripcion + "DESCRIPCION: " + nodoLstConceptos[i].Attributes["Descripcion"].InnerText.ToUpper() + "\n";
                                        //        if (nodoLstConceptos[i].Attributes["ClaveProdServ"] != null)
                                        //            Concepto_Descripcion = Concepto_Descripcion + "CLAVEPRODSERV: " + nodoLstConceptos[i].Attributes["ClaveProdServ"].InnerText.ToUpper() + "\n";
                                        //        if (nodoLstConceptos[i].Attributes["Cantidad"] != null)
                                        //            Concepto_Descripcion = Concepto_Descripcion + "CANTIDAD: " + nodoLstConceptos[i].Attributes["Cantidad"].InnerText.ToUpper() + "\n";
                                        //        if (nodoLstConceptos[i].Attributes["ClaveUnidad"] != null)
                                        //            Concepto_Descripcion = Concepto_Descripcion + "CLAVEUNIDAD: " + nodoLstConceptos[i].Attributes["ClaveUnidad"].InnerText.ToUpper() + "\n";
                                        //        if (nodoLstConceptos[i].Attributes["Unidad"] != null)
                                        //            Concepto_Descripcion = Concepto_Descripcion + "UNIDAD: " + nodoLstConceptos[i].Attributes["Unidad"].InnerText.ToUpper() + "\n";
                                        //        if (nodoLstConceptos[i].Attributes["ValorUnitario"] != null)
                                        //            Concepto_Descripcion = Concepto_Descripcion + "VALORUNITARIO: " + nodoLstConceptos[i].Attributes["ValorUnitario"].InnerText.ToUpper() + "\n";
                                        //        if (nodoLstConceptos[i].Attributes["Importe"] != null)
                                        //            Concepto_Descripcion = Concepto_Descripcion + "IMPORTE: " + nodoLstConceptos[i].Attributes["Importe"].InnerText.ToUpper() + "\n";




                                        //    }
                                        //    ObjPolizaCFDI.CFDI_Concepto_Descripcion = Concepto_Descripcion;
                                        //}

                                        //if (ObjPolizaCFDI.CFDI_Concepto_Descripcion != string.Empty)
                                        //{
                                        //    Verificador = string.Empty;
                                        //    ObjPolizaCFDI.Id_CFDI = Convert.ToInt32(gvrow.Cells[0].Text);
                                        //    CNPolizaCFDI.PolizaCFDIEditarConceptos(ObjPolizaCFDI, ref Verificador);
                                        //    if (Verificador == "0")
                                        //        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(1, 'El concepto fue actualizado correctamente.');", true);
                                        //    else
                                        //    {
                                        //        CNComun.VerificaTextoMensajeError(ref Verificador);
                                        //        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                                        //    }
                                        //}
                                    }
                                catch (Exception)
                                {
                                    ObjPolizaCFDI.CFDI_Folio = string.Empty;
                                }
                            }
                        }
                    }
                }
                CargarGridPolizaCFDI();
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
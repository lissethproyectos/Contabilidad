
using AjaxControlToolkit;
using Aspose.Cells;
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
    public partial class frmConciliacionBancaria : System.Web.UI.Page
    {
        #region <Variables>
        string Verificador = string.Empty;
        string VerificadorDet = string.Empty;

        Sesion SesionUsu = new Sesion();
        CN_Comun CNComun = new CN_Comun();
        CN_Poliza_Conciliacion CNConciliacion = new CN_Poliza_Conciliacion();
        Poliza_Conciliacion objConciliacion = new Poliza_Conciliacion();
        Poliza_Conciliacion objAdjunto = new Poliza_Conciliacion();
        Poliza_Detalle objPolizaDet = new Poliza_Detalle();
        List<Poliza_Detalle> ListPDet = new List<Poliza_Detalle>();

        private static List<Comun> ListTipo = new List<Comun>();
        private static List<Comun> ListCentroContable = new List<Comun>();

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            //SesionUsu = (Sesion)Session["Usuario"];
            //if (!IsPostBack)
            //{
            //    MultiView1.ActiveViewIndex = 0;
            //    Cargarcombos();
            //}

            //ScriptManager.RegisterStartupScript(this, GetType(), "GridConciliacion", "ConciliacionBanc();", true);
            SesionUsu = (Sesion)Session["Usuario"];
            if (!IsPostBack)
                Inicializar();
            else
            {
                if ((Request.Params["__EVENTTARGET"] != null) && (Request.Params["__EVENTARGUMENT"] != null))
                {
                    if (Request.Params["__EVENTTARGET"] == this.txtNumPoliza.UniqueID)
                    {
                        if (ListTipo[ddlTipo.SelectedIndex].EtiquetaTres == "ANEXO_4")
                            this.txtNumCheque.Focus();
                        else
                            this.txtFecha.Focus();
                    }
                    else if (Request.Params["__EVENTTARGET"] == this.txtNumCheque.UniqueID)
                        this.txtFecha.Focus();
                    else if (Request.Params["__EVENTTARGET"] == this.txtFecha.UniqueID)
                        this.txtImporte.Focus();
                    else if (Request.Params["__EVENTTARGET"] == this.txtImporte.UniqueID)
                        this.txtConcepto.Focus();
                    else if (Request.Params["__EVENTTARGET"] == this.txtConcepto.UniqueID)
                        this.txtDescripcion.Focus();
                    //else if (Request.Params["__EVENTTARGET"] == this.txtDescripcion.UniqueID)
                    //    this.bttnAgregar.Focus();
                    else if (Request.Params["__EVENTTARGET"] == this.bttnAgregar.UniqueID)
                        this.ddlTipo.Focus();

                }
            }


            //ScriptManager.RegisterStartupScript(this, GetType(), "Cuentas_Contables", "Autocomplete();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "GridConciliacion", "Conciliacion();", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "GridDetalle", "Detalle();", true);

        }
        private void Inicializar()
        {
            MultiView1.ActiveViewIndex = 0;
            ddlFecha_Ini1.SelectedValue = System.DateTime.Now.ToString("MM");
            ddlFecha_Fin1.SelectedValue = System.DateTime.Now.ToString("MM");
            Cargarcombos();
            CargarGrid();
            //ScriptManager.RegisterStartupScript(this, GetType(), "Materias", "FiltMat();", true);
        }

        protected void grdConciliacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Verificador = string.Empty;
            try
            {
                int fila = e.RowIndex;
                //objConciliacion.Id = Convert.ToInt32(grdConciliacion.Rows[fila].Cells[0].Text);

                objConciliacion.IdEnc = Convert.ToInt32(grdConciliacion.Rows[fila].Cells[12].Text);
                CNConciliacion.ConciliacionEliminar(objConciliacion, ref Verificador);
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

        private void Cargarcombos()
        {
            try
            {
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref DDLCentro_Contable1, "p_usuario", "p_ejercicio", "p_sistema", SesionUsu.Usu_Nombre, SesionUsu.Usu_Ejercicio, "CONCILIACION");
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref DDLCentro_Contable, "p_usuario", "p_ejercicio", "p_sistema", SesionUsu.Usu_Nombre, SesionUsu.Usu_Ejercicio, "CONCILIACION", ref ListCentroContable);
                Session["CentrosContab"] = ListCentroContable;
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Conciliacion", ref ddlTipo, "p_ejercicio", SesionUsu.Usu_Ejercicio, ref ListTipo);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Conciliacion", ref ddlTipo2, "p_ejercicio", SesionUsu.Usu_Ejercicio, ref ListTipo);
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

            }
        }
        protected void linkBttnNuevo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupConciliacion", "$('#modalConciliacion').modal('show')", true);
            ddlTipo.Enabled = true;
            bttnModificar.Visible = false;
            bttnAgregar.Visible = true;
            ddlTipo.SelectedIndex = 0;
            ddlTipo_SelectedIndexChanged(null, null);
            LimpiarCamposDet();
        }
        protected void bttnCargarArchivo_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            string Ruta;
            List<Poliza_Conciliacion> lstConciliacion = new List<Poliza_Conciliacion>();
            try
            {
                if (FileFactura.HasFile)
                {
                    Ruta = Path.Combine(Server.MapPath("~/AdjuntosTemp"), FileFactura.FileName);
                    FileFactura.SaveAs(Ruta);
                    Workbook wb = new Workbook(Ruta);

                    // Obtener todas las hojas de trabajo
                    WorksheetCollection collection = wb.Worksheets;

                    // Recorra todas las hojas de trabajo
                    for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
                    {

                        // Obtener hoja de trabajo usando su índice
                        Worksheet worksheet = collection[worksheetIndex];

                        // Imprimir el nombre de la hoja de trabajo
                        Console.WriteLine("Worksheet: " + worksheet.Name);

                        // Obtener el número de filas y columnas
                        int rows = worksheet.Cells.MaxDataRow;
                        int cols = worksheet.Cells.MaxDataColumn;
                        if (Session["ConciliacionDet"] != null)
                            lstConciliacion = (List<Poliza_Conciliacion>)Session["ConciliacionDet"];
                        // Bucle a través de filas
                        for (int i = 0; i < rows; i++)
                        {

                            // Recorra cada columna en la fila seleccionada

                            objConciliacion.NumeroPoliza = Convert.ToString(worksheet.Cells[i, 0].Value);
                            objConciliacion.Numero_Cheque = Convert.ToString(worksheet.Cells[i, 1].Value);
                            string Fecha = Convert.ToString(worksheet.Cells[i, 2].Value);
                            objConciliacion.Fecha = Convert.ToString(worksheet.Cells[i, 2].Value).Substring(0,10);
                            objConciliacion.Importe = Convert.ToDouble(worksheet.Cells[i, 3].Value);
                            objConciliacion.Concepto = Convert.ToString(worksheet.Cells[i, 4].Value);
                            lstConciliacion.Add(objConciliacion);
                        }




                        CargarGridConcDet(lstConciliacion);
                        Session["ConciliacionDet"] = lstConciliacion;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0,'Adjuntar archivo xlsx.');", true);

                }

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

            }
        }
        protected void linkBttnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrid();
        }
        private void CargarGrid()
        {
            Verificador = string.Empty;
            Int32[] Celdas = new Int32[] { 8, 9, 10, 11, 12 };
            grdConciliacion.DataSource = null;
            grdConciliacion.DataBind();
            try
            {
                DataTable dt = new DataTable();
                grdConciliacion.DataSource = dt;
                grdConciliacion.DataSource = GetList();
                grdConciliacion.DataBind();



            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        private List<Poliza_Conciliacion> GetList()
        {
            try
            {
                List<Poliza_Conciliacion> List = new List<Poliza_Conciliacion>();
                objConciliacion.Ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                objConciliacion.Centro_contable = Convert.ToInt32(DDLCentro_Contable1.SelectedValue);
                objConciliacion.Fecha_inicial = ddlFecha_Ini1.SelectedValue;
                objConciliacion.Fecha_final = ddlFecha_Fin1.SelectedValue;
                //objConciliacion.Tipo = ddlTipo1.SelectedValue;
                CNConciliacion.ConciliacionConsultaGrid2(objConciliacion, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void linkBttnAdj_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            List<Poliza_Conciliacion> ListAdj = new List<Poliza_Conciliacion>();
            Verificador = string.Empty;
            grdConciliacion.SelectedIndex = row.RowIndex;
            Session["DoctosAdj"] = null;
            grdDoctos.DataSource = null;
            grdDoctos.DataBind();
            try
            {
                objConciliacion.IdEnc = Convert.ToInt32(grdConciliacion.SelectedRow.Cells[12].Text);
                CNConciliacion.ConciliacionAdjConsultaGrid(objConciliacion, ref ListAdj);
                Session["DoctosAdj"] = ListAdj;
                CargarGridAdjuntos(ListAdj);


                modalAdj.Show();
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

            }

        }
        protected void bttnAdjuntar_Click(object sender, EventArgs e)
        {
            modalAdj.Show();
            List<Poliza_Conciliacion> ListAdj = new List<Poliza_Conciliacion>();
            string NombreArchivo = string.Empty;


            if (FileUpload1.HasFile)
            {
                try
                {
                    string Nombre = "EDO_CTA_" + SesionUsu.Usu_Ejercicio + "-" + grdConciliacion.SelectedRow.Cells[0].Text + "-" + FileUpload1.FileName.ToUpper();
                    string Ruta = Path.Combine(Server.MapPath("~/AdjuntosTemp"), Nombre);
                    FileUpload1.SaveAs(Ruta);
                    //NombreArchivo= FileUpload1.FileName.ToUpper();
                    objAdjunto.NombreArchivoPDF = Nombre;
                    objAdjunto.Ruta_PDF = "~/AdjuntosTemp/" + Nombre;

                    if (Session["DoctosAdj"] != null)
                        ListAdj = (List<Poliza_Conciliacion>)Session["DoctosAdj"];


                    int count = (from dato in ListAdj
                                 where dato.NombreArchivoPDF == Nombre
                                 select dato).Count();

                    if (count == 0)
                    {
                        ListAdj.Add(objAdjunto);
                        Session["DoctosAdj"] = ListAdj;
                        CargarGridAdjuntos(ListAdj);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'Ya existe un archivo con ese nombre, verificar.');", true);
                }
                catch (Exception ex)
                {
                    Verificador = ex.Message;
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

                }
            }
        }
        protected void btnCancelarAdj_Click(object sender, EventArgs e)
        {
            modalAdj.Hide();
        }
        protected void btnGuardarAdj_Click(object sender, EventArgs e)
        {
            modalAdj.Show();
            Verificador = string.Empty;

            try
            {
                List<Poliza_Conciliacion> ListAdj = new List<Poliza_Conciliacion>();
                objConciliacion.IdEnc = Convert.ToInt32(grdConciliacion.SelectedRow.Cells[12].Text);
                ListAdj = (List<Poliza_Conciliacion>)Session["DoctosAdj"];
                CNConciliacion.PolizaAdjInsertar(objConciliacion, ListAdj, ref Verificador);
                if (Verificador == "0")
                {
                    Copiar_a_Adjuntos(ListAdj);
                    modalAdj.Hide();
                    CargarGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(1, 'Doctos. guardados correctamente.');", true);

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
        private void Copiar_a_Adjuntos(List<Poliza_Conciliacion> lstPolizasAdj)
        {
            string OrigenArchivo = string.Empty;
            string DestinoArchivo = string.Empty;

            try
            {
                for (int i = 0; i < lstPolizasAdj.Count; i++)
                {
                    OrigenArchivo = Path.Combine(Server.MapPath("~/AdjuntosTemp"), lstPolizasAdj[i].NombreArchivoPDF); //System.IO.Path.Combine(Origen, fileName);
                    DestinoArchivo = Path.Combine(Server.MapPath("~/Adjuntos"), lstPolizasAdj[i].NombreArchivoPDF);  //System.IO.Path.Combine(Destino, fileName);
                    System.IO.File.Copy(OrigenArchivo, DestinoArchivo, true);
                    //System.IO.File.Delete(OrigenArchivo); MODIFICADO DIA 26 / 03
                }
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }


        protected void grdDoctos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Poliza_Conciliacion> ListAdj = new List<Poliza_Conciliacion>();
            ListAdj = (List<Poliza_Conciliacion>)Session["DoctosAdj"];
            modalAdj.Show();
            try
            {
                int fila = e.RowIndex;
                int pagina = grdDoctos.PageSize * grdDoctos.PageIndex;
                fila = pagina + fila;
                ListAdj.RemoveAt(fila);
                Session["DoctosAdj"] = ListAdj;
                CargarGridAdjuntos(ListAdj);
            }

            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        private void CargarGridAdjuntos(List<Poliza_Conciliacion> lstAdjuntos)
        {
            grdDoctos.DataSource = null;
            grdDoctos.DataBind();
            try
            {
                DataTable dt = new DataTable();
                grdDoctos.DataSource = dt;
                grdDoctos.DataSource = lstAdjuntos;
                grdDoctos.DataBind();

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void btnGuardar_Continuar_Click(object sender, EventArgs e)
        {
            if (grdDetalle.Rows.Count >= 1)
            {
                if (Convert.ToDecimal(hddnTot113.Value) == Convert.ToDecimal(hddnTotBancos.Value))
                {
                    if (Convert.ToInt32(ddlFecha_Ini.SelectedValue) >= Convert.ToInt32(SesionUsu.MesActivo))
                    {
                        string conErrores = GuardarDatos();
                        if (conErrores == "0")
                        {
                            txtFecha.Text = string.Empty;
                            //txtFecha_Final.Text = string.Empty;
                            txtNumPoliza.Text = string.Empty;
                            txtImporte.Text = string.Empty;
                            //hddnIdPolizaDet.Value = "0";
                            MultiView1.ActiveViewIndex = 0;
                            CargarGrid();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(1, 'Registro guardado correctamente.');", true);
                        }
                        else
                        {
                            CNComun.VerificaTextoMensajeError(ref Verificador);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                        }
                    }

                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'El mes seleccionado ya esta cerrado, favor de verificar.');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'Conciliación descuadrada, favor de verificar.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'Falta agregar detalle, favor de verificar.');", true);
        }
        private string GuardarDatos()
        {
            Verificador = string.Empty;
            List<Poliza_Conciliacion> ListPDet = new List<Poliza_Conciliacion>();
            try
            {
                objConciliacion.Ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                objConciliacion.Centro_contable = Convert.ToInt32(DDLCentro_Contable.SelectedValue);
                objConciliacion.Cuenta_contable = Convert.ToInt32(DDLCuenta_Contable.SelectedValue);
                objConciliacion.Fecha_inicial = ddlFecha_Ini.SelectedValue; // +"/"+SesionUsu.Usu_Ejercicio;
                objConciliacion.Fecha_final = ddlFecha_Fin.SelectedValue;  //txtFechaFinal.Text;
                objConciliacion.Elaboro_nombre = txtElaboroNombre.Text.ToUpper();
                objConciliacion.Elaboro_puesto = txtElaboroPuesto.Text.ToUpper();
                objConciliacion.Vb_nombre = txtVB_Nombre.Text.ToUpper();
                objConciliacion.Vb_puesto = txtVB_Puesto.Text.ToUpper();
                objConciliacion.Nombre_archivo = string.Empty;
                ListPDet = (List<Poliza_Conciliacion>)Session["ConciliacionDet"];
                if (SesionUsu.Editar == 0)
                {
                    if (ListPDet != null)
                        CNConciliacion.ConciliacionInsertarEnc2(ref objConciliacion, ListPDet, ref Verificador);
                    else
                        CNConciliacion.ConciliacionInsertarEnc2(ref objConciliacion, null, ref Verificador);
                }
                else
                {
                    objConciliacion.IdEnc = Convert.ToInt32(grdConciliacion.SelectedRow.Cells[12].Text);
                    if (ListPDet != null)
                        CNConciliacion.ConciliacionEditarEnc2(ref objConciliacion, ListPDet, ref Verificador);
                    else
                        CNConciliacion.ConciliacionEditarEnc2(ref objConciliacion, null, ref Verificador);
                }
                return Verificador;


            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                return Verificador;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void linkReporteEnc_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            string ruta = string.Empty;
            grdConciliacion.SelectedIndex = row.RowIndex;
            int IdConciliacion = Convert.ToInt32(grdConciliacion.SelectedRow.Cells[12].Text);
            if (Convert.ToInt32(SesionUsu.Usu_Ejercicio) > 2020)
                ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=RP_Conciliacion2021&id=" + grdConciliacion.SelectedRow.Cells[12].Text;
            else
                ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=RP_Conciliacion&id=" + grdConciliacion.SelectedRow.Cells[12].Text;

            string _open = "window.open('" + ruta + "', 'miniContenedor', 'toolbar=yes', 'location=no', 'menubar=yes', 'resizable=yes');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

        }
        protected void grdConciliacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            SesionUsu.Editar = 1;
            MultiView1.ActiveViewIndex = 1;
            try
            {
                hddnTot113.Value = "0";
                hddnTotBancos.Value = "0";

                objConciliacion.IdEnc = Convert.ToInt32(grdConciliacion.SelectedRow.Cells[12].Text);
                CNConciliacion.ConsultarConciliacionEncSel2(ref objConciliacion, ref Verificador);
                if (Verificador == "0")
                {
                    DDLCentro_Contable.SelectedValue = Convert.ToString(objConciliacion.Centro_contable);
                    DDLCentro_Contable_SelectedIndexChanged(null, null);
                    ddlFecha_Ini.SelectedValue = Convert.ToString(objConciliacion.Fecha_inicial);
                    ddlFecha_Fin.SelectedValue = Convert.ToString(objConciliacion.Fecha_final);

                    DDLCuenta_Contable.SelectedValue = Convert.ToString(objConciliacion.Cuenta_contable);
                    txtElaboroNombre.Text = objConciliacion.Elaboro_nombre;
                    txtVB_Nombre.Text = objConciliacion.Vb_nombre;
                    txtElaboroPuesto.Text = objConciliacion.Elaboro_puesto;
                    txtVB_Puesto.Text = objConciliacion.Vb_puesto;
                    ddlTipo.SelectedIndex = 0;
                    ddlTipo_SelectedIndexChanged(null, null);
                    objConciliacion.IdEnc = Convert.ToInt32(grdConciliacion.SelectedRow.Cells[12].Text);
                    List<Poliza_Conciliacion> lstConciliacionDet = new List<Poliza_Conciliacion>();
                    CNConciliacion.ConciliacionDetConsultaGrid(objConciliacion, ref lstConciliacionDet);
                    if (lstConciliacionDet.Count > 0)
                    {
                        Session["ConciliacionDet"] = lstConciliacionDet;
                        CargarGridConcDet(lstConciliacionDet);

                    }
                    else
                        Session["ConciliacionDet"] = null;

                    MultiView1.ActiveViewIndex = 1;
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

        protected void grdDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Poliza_Conciliacion> lstConciliacionDet = new List<Poliza_Conciliacion>();
            lstConciliacionDet = (List<Poliza_Conciliacion>)Session["ConciliacionDet"];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupConciliacion", "$('#modalConciliacion').modal('show')", true);
            if (lstConciliacionDet.Count > 0)
            {
                try
                {
                    ddlTipo.SelectedValue = lstConciliacionDet[grdDetalle.SelectedIndex].Tipo;
                }
                catch
                {
                    ddlTipo.SelectedIndex = 0;
                }
                ddlTipo_SelectedIndexChanged(null, null);

                ddlTipo.Enabled = false;
                txtNumPoliza.Text = lstConciliacionDet[grdDetalle.SelectedIndex].Numero_Poliza;
                txtNumCheque.Text = lstConciliacionDet[grdDetalle.SelectedIndex].Numero_Cheque;
                txtFecha.Text = lstConciliacionDet[grdDetalle.SelectedIndex].Fecha;
                txtImporte.Text = Convert.ToString(lstConciliacionDet[grdDetalle.SelectedIndex].Importe);
                txtImporteBanco.Text = Convert.ToString(lstConciliacionDet[grdDetalle.SelectedIndex].ImporteBanco);
                txtConcepto.Text = lstConciliacionDet[grdDetalle.SelectedIndex].Concepto;
                txtDescripcion.Text = lstConciliacionDet[grdDetalle.SelectedIndex].Observaciones;

                bttnModificar.Visible = true;
                bttnAgregar.Visible = false;
            }




        }

        protected void DDLCentro_Contable_SelectedIndexChanged(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            try
            {
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Ctas_Bancos", ref DDLCuenta_Contable, "p_ejercicio", "p_centro_contable", SesionUsu.Usu_Ejercicio, Convert.ToString(DDLCentro_Contable.SelectedValue));
                string MesCC = VerificaMes();
                SesionUsu.MesActivo = MesCC;

                if (SesionUsu.Editar == 0)
                {
                    ddlFecha_Ini.SelectedValue = MesCC;
                    ddlFecha_Fin.SelectedValue = MesCC;
                }

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

            }
        }
        private string VerificaMes()
        {
            try
            {
                ListCentroContable = (List<Comun>)Session["CentrosContab"];
                string MesCC = ListCentroContable[DDLCentro_Contable.SelectedIndex].EtiquetaCuatro;
                MesCC = MesCC.PadLeft(2, '0');
                return MesCC;
            }
            catch (Exception ex)
            {
                return "function(VerificaMes) " + ex.Message;
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            grdDetalle.EditIndex = -1;
            MultiView1.ActiveViewIndex = 0;
        }
        protected void linkBttnNuevoConc_Click(object sender, EventArgs e)
        {
            List<Poliza_Conciliacion> ListPDet = new List<Poliza_Conciliacion>();
            Session["ConciliacionDet"] = null;
            grdDetalle.DataSource = null;
            grdDetalle.DataBind();
            SesionUsu.Editar = 0;
            MultiView1.ActiveViewIndex = 1;
            TabContainer2.ActiveTabIndex = 0;
            LimpiarCampos();
            ddlTipo.Enabled = true;
            txtNumPoliza.Text = string.Empty;
            txtNumCheque.Text = string.Empty;
            txtFecha.Text = string.Empty;
            txtImporte.Text = string.Empty;
            txtConcepto.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            hddnTot113.Value = "0";
            hddnTotBancos.Value = "0";
        }
        protected void bttnModificar_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            int Fila = -1;
            List<Poliza_Conciliacion> ListPDet = new List<Poliza_Conciliacion>();

            Fila = grdDetalle.SelectedIndex;
            try
            {
                if (Session["ConciliacionDet"] != null)
                    ListPDet = (List<Poliza_Conciliacion>)Session["ConciliacionDet"];


                ListPDet[Fila].NumeroPoliza = txtNumPoliza.Text;
                ListPDet[Fila].Numero_Cheque = txtNumPoliza.Text;
                ListPDet[Fila].Fecha = txtFecha.Text;
                ListPDet[Fila].Importe = Convert.ToDouble(txtImporte.Text);
                ListPDet[Fila].ImporteBanco = Convert.ToDouble(txtImporteBanco.Text);
                ListPDet[Fila].Concepto = txtConcepto.Text;
                ListPDet[Fila].Observaciones = txtDescripcion.Text;
                CargarGridConcDet(ListPDet);
                Session["ConciliacionDet"] = ListPDet;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupConciliacion", "$('#modalConciliacion').modal('hide')", true);

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupConciliacion", "$('#modalConciliacion').modal('hide')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

            }
        }
        protected void bttnAgregar_Click(object sender, EventArgs e)
        {
            List<Poliza_Conciliacion> ListPDet = new List<Poliza_Conciliacion>();
            try
            {

                objConciliacion.Fecha = txtFecha.Text;
                if (txtNumPoliza.Text == string.Empty)
                    objConciliacion.Numero_Poliza = "0";
                else
                    objConciliacion.Numero_Poliza = txtNumPoliza.Text;

                if (txtNumCheque.Text == string.Empty)
                    objConciliacion.Numero_Cheque = "0";
                else
                    objConciliacion.Numero_Cheque = txtNumCheque.Text;

                objConciliacion.Importe = Convert.ToDouble(txtImporte.Text);
                if (txtImporteBanco.Text == string.Empty)
                    objConciliacion.ImporteBanco = Convert.ToDouble("0");
                else
                    objConciliacion.ImporteBanco = Convert.ToDouble(txtImporteBanco.Text);

                objConciliacion.Concepto = txtConcepto.Text.ToUpper();
                objConciliacion.Observaciones = txtDescripcion.Text.ToUpper();
                objConciliacion.Tipo = ddlTipo.SelectedValue;
                objConciliacion.DescTipo = ddlTipo.SelectedItem.Text;
                objConciliacion.CveTipo = ListTipo[ddlTipo.SelectedIndex].EtiquetaTres;

                if (Session["ConciliacionDet"] != null)
                    ListPDet = (List<Poliza_Conciliacion>)Session["ConciliacionDet"];


                ListPDet.Add(objConciliacion);
                ListPDet = ListPDet.OrderBy(x => x.CveTipo).ToList();
                Session["ConciliacionDet"] = ListPDet;
                CargarGridConcDet(ListPDet);
                LimpiarCamposDet();
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        private void LimpiarCampos()
        {
            Verificador = string.Empty;
            try
            {
                txtElaboroNombre.Text = string.Empty;
                txtElaboroPuesto.Text = string.Empty;
                txtVB_Nombre.Text = string.Empty;
                txtVB_Puesto.Text = string.Empty;
                if (SesionUsu.Editar == 0)
                {
                    DDLCentro_Contable.SelectedValue = DDLCentro_Contable1.SelectedValue;
                    DDLCentro_Contable_SelectedIndexChanged(null, null);
                }

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        private void LimpiarCamposDet()
        {
            txtFecha.Text = string.Empty;
            txtNumPoliza.Text = string.Empty;
            txtNumCheque.Text = string.Empty;
            txtImporte.Text = string.Empty;
            txtConcepto.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtImporteBanco.Text = "0";
            //ddlTipo.Focus();
        }
        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtConcepto.Text = string.Empty;
            txtConcepto.Visible = true;
            lblConcepto.Visible = true;
            lblConcepto.Text = "Concepto";
            txtImporteBanco.Visible = false;
            lblImporteBanco.Visible = false;
            trowCheque.Visible = false;
            lblImporte.Text = "Importe";
            if (ListTipo[ddlTipo.SelectedIndex].EtiquetaTres == "ANEXO_3")
            {
                trowFecha.Visible = true;
                trowPoliza.Visible = true;
                lblObservaciones.Visible = true;
                txtDescripcion.Visible = true;

            }
            else if (ListTipo[ddlTipo.SelectedIndex].EtiquetaTres == "ANEXO_4" || ListTipo[ddlTipo.SelectedIndex].EtiquetaTres == "ANEXO_7" || ListTipo[ddlTipo.SelectedIndex].EtiquetaTres == "ANEXO_11")
            {
                trowFecha.Visible = true;
                trowPoliza.Visible = true;
                trowCheque.Visible = true;
                lblObservaciones.Visible = true;
                txtDescripcion.Visible = true;
                lblConcepto.Text = "Beneficiario";

            }
            else if (ListTipo[ddlTipo.SelectedIndex].EtiquetaTres == "ANEXO_1" || ListTipo[ddlTipo.SelectedIndex].EtiquetaTres == "ANEXO_2")
            {
                DateTime fecha;
                fecha = new DateTime(Convert.ToInt32(SesionUsu.Usu_Ejercicio), Convert.ToInt32(ddlFecha_Fin.SelectedValue), 1);
                DateTime ultimoDia = fecha.AddMonths(1).AddDays(-1);

                if (Convert.ToInt32(SesionUsu.Usu_Ejercicio) > 2020)
                {
                    lblImporte.Text = "Importe UNACH 1113";
                    txtImporteBanco.Visible = true;
                    lblImporteBanco.Visible = true;
                }
                trowPoliza.Visible = false;
                trowFecha.Visible = false;
                lblObservaciones.Visible = false;
                txtDescripcion.Visible = false;
                txtConcepto.Text = ddlTipo.SelectedItem.Text + " AL " + ultimoDia.ToShortDateString(); // txtFechaFinal.Text;

            }
            else if (ListTipo[ddlTipo.SelectedIndex].EtiquetaTres == "ANEXO_10")
            {
                trowPoliza.Visible = true;
                trowFecha.Visible = true;
                lblObservaciones.Visible = false;
                txtDescripcion.Visible = false;
                lblConcepto.Visible = true;
                txtConcepto.Visible = true;
            }
            else
            {
                trowFecha.Visible = true;
                trowPoliza.Visible = false;
                lblObservaciones.Visible = true;
                txtDescripcion.Visible = true;

            }
        }
        protected void grdDetalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Poliza_Conciliacion> ListPDet = new List<Poliza_Conciliacion>();
            ListPDet = (List<Poliza_Conciliacion>)Session["ConciliacionDet"];
            try
            {
                int fila = e.RowIndex;
                ListPDet.RemoveAt(fila);
                Session["ConciliacionDet"] = ListPDet;
                CargarGridConcDet(ListPDet);
            }

            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }

        }
        private void CargarGridConcDet(List<Poliza_Conciliacion> lstPolizasDet)
        {
            grdDetalle.DataSource = null;
            grdDetalle.DataBind();
            Int32[] Celdas = new Int32[] { 2, 12, 13 };
            try
            {
                DataTable dt = new DataTable();

                List<Poliza_Conciliacion> SortedList = lstPolizasDet.OrderBy(o => o.CveTipo).ToList();
                grdDetalle.DataSource = SortedList;
                grdDetalle.DataBind();
                CNComun.HideColumns(grdDetalle, Celdas);
                Sumatoria(grdDetalle, Celdas);
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        private void Sumatoria(GridView grdView, Int32[] Columnas)
        {
            hddnTot113.Value = "0";
            hddnTotBancos.Value = "0";
            decimal unach_1113 = 0;
            decimal bancos = 0;

            //for (int i = 0; i < Columnas.Length; i++)
            //{
            //grdView.HeaderRow.Cells[Convert.ToInt32(Columnas.GetValue(i))].Visible = false;
            foreach (GridViewRow row in grdView.Rows)
            {
                if (row.Cells[0].Text == "ANEXO_1" || row.Cells[0].Text == "ANEXO_11" || row.Cells[0].Text == "ANEXO_9")
                {
                    unach_1113 = unach_1113 + Convert.ToDecimal(row.Cells[12].Text);
                    bancos = bancos + Convert.ToDecimal(row.Cells[13].Text);
                }
                else if (row.Cells[0].Text == "ANEXO_1" || row.Cells[0].Text == "ANEXO_7" || row.Cells[0].Text == "ANEXO_8" || row.Cells[0].Text == "ANEXO_10")
                {
                    if (row.Cells[0].Text == "ANEXO_7")
                        bancos = bancos - Convert.ToDecimal(row.Cells[12].Text);
                    else
                        bancos = bancos + Convert.ToDecimal(row.Cells[12].Text);
                }

                //row.Cells[Convert.ToInt32(Columnas.GetValue(i))].Visible = false;
            }
            //grdView.FooterRow.Cells[Convert.ToInt32(Columnas.GetValue(i))].Visible = false;
            lblTot113.Text = string.Format("{0:C}", unach_1113);
            lblTotBancos.Text = string.Format("{0:C}", bancos);

            hddnTot113.Value = Convert.ToString(unach_1113);
            hddnTotBancos.Value = Convert.ToString(bancos);
            //}


        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocio;
using System.Xml;
using System.IO;
using System.Text;

namespace SAF.Form
{
    public partial class frmRegPolizas : System.Web.UI.Page
    {
        #region <Variables>
        Int32[] Celdas = new Int32[] { 0, 15, 16, 17, 18, 19, 20, 22, 23 };
        string Verificador = string.Empty;
        string Verificador2 = string.Empty;
        string MsjError = string.Empty;
        Usuario Usuario = new Usuario();
        Sesion SesionUsu = new Sesion();
        Empleado objEmpleado = new Empleado();
        CN_Comun CNComun = new CN_Comun();
        CN_Poliza CNPoliza = new CN_Poliza();
        CN_Empleado CNEmpleado = new CN_Empleado();
        CN_Poliza_Det CNPolizaDet = new CN_Poliza_Det();
        CN_Poliza_CFDI CNPolizaCFDI = new CN_Poliza_CFDI();
        CN_Poliza_Oficio CNPolizaOficio = new CN_Poliza_Oficio();
        CN_Usuario CNUsuario = new CN_Usuario();
        Comun ObjCC = new Comun();
        Poliza ObjPoliza = new Poliza();
        Poliza_Detalle ObjPolizaDet = new Poliza_Detalle();
        Poliza_CFDI ObjPolizaCFDI = new Poliza_CFDI();

        Poliza_Oficio ObjPolizaOficio = new Poliza_Oficio();
        List<Poliza_Detalle> ListPDet = new List<Poliza_Detalle>();
        List<cuentas_contables> ListCC = new List<cuentas_contables>();
        List<Comun> ListCuentas = new List<Comun>();
        List<Empleado> ListEmpleados = new List<Empleado>();
        List<Comun> ListCedulas = new List<Comun>();
        private static List<Comun> ListCentroContable = new List<Comun>();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SesionUsu = (Sesion)Session["Usuario"];



            if ((Request.Params["__EVENTTARGET"] != null) && (Request.Params["__EVENTARGUMENT"] != null))
            {
                //if ((Request.Params["__EVENTTARGET"] == this.ddlCuentas_Contables.UniqueID) /*+&& (Request.Params["__EVENTARGUMENT"] == "actualizar_label")*/)
                if ((Request.Params["__EVENTTARGET"] == this.txtSearch.UniqueID) /*+&& (Request.Params["__EVENTARGUMENT"] == "actualizar_label")*/)
                {
                    this.txtCargo.Focus();
                }
                else if ((Request.Params["__EVENTTARGET"] == this.bttnAgregar.ClientID))
                {
                    Request.Params.Clear();
                }
                else if ((Request.Params["__EVENTTARGET"] == this.txtBuscar.UniqueID))
                {
                    this.linkBttnNuevo.Focus();
                    //this.linkBttnBuscar.Focus(); //this.imgbtnBuscar.Focus();
                }

            }

            if (!IsPostBack)
            {
                //double importe = 518;
                //double pruebas = Math.Ceiling(importe);

                //double importe2 = 516.49;
                //double pruebas2 = Math.Ceiling(importe2);
                Inicializar();
            }

            //ScriptManager.RegisterStartupScript(this, GetType(), "CtasContables", "FiltCtasContables();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "GridPolizas", "Polizas();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "GridDetPolizas", "DetallePoliza();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "NumCedulas", "FiltNumCedulas();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ComboProveedores", "Proveedores();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ComboProveedores2", "Proveedores2();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "GridCFDIS", "PolizaCFDI();", true);


        }


        #region <Funciones y Sub>
        private void Inicializar()
        {
            SesionUsu.Editar = -1;
            MultiView1.ActiveViewIndex = 0;
            TabContainer1.ActiveTabIndex = 0;
            ddlFecha_Ini.SelectedValue = System.DateTime.Now.ToString("MM");
            ddlFecha_Fin.SelectedValue = System.DateTime.Now.ToString("MM");
            txtFecha.Text = string.Empty;
            Cargarcombos();
            DataTable dt = new DataTable();
            grvPolizas.DataSource = dt;
            grvPolizas.DataSource = null;
            grvPolizas.DataBind();
            //CargarGrid(0);
        }


        private void Cargarcombos()
        {
            //lblError.Text = string.Empty;
            Session["CentrosContab"] = null;
            try
            {
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref DDLCentro_Contable, "p_usuario", "p_ejercicio", SesionUsu.Usu_Nombre, SesionUsu.Usu_Ejercicio, ref ListCentroContable);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Beneficiario", ref ddlTipo_Beneficiario);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Gasto", ref ddlTipo_Gasto);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Clasificacion", ref ddlClasifica, "p_tipo_usuario", "p_centro_contable", SesionUsu.Usu_TipoUsu, DDLCentro_Contable.SelectedValue);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Clasificacion", ref ddlClasificaIni, "p_tipo_usuario", "p_centro_contable", SesionUsu.Usu_TipoUsu, DDLCentro_Contable.SelectedValue);
                //CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Clasificacion", ref ddlClasificaCopia, "p_tipo_usuario", "p_centro_contable", SesionUsu.Usu_TipoUsu, DDLCentro_Contable.SelectedValue);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Proveedores", ref ddlProveedor);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Proveedores", ref ddlProveedor2);
                //CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Docto", ref ddlTipoDocto, "p_tipo_usuario", "p_tipo", SesionUsu.Usu_TipoUsu,DDLCentro_Contable.SelectedValue);
                Session["CentrosContab"] = ListCentroContable;
                ddlClasificaIni.Items.Remove(new ListItem("--SELECCIONAR--", "X"));
                ddlClasificaIni.Items.Insert(0, new ListItem("-- TODOS --", "0"));


            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

            }
        }
        private void CargarGrid(int indexCopia)
        {
            //lblError.Text = string.Empty;
            grvPolizas.DataSource = null;
            grvPolizas.DataBind();
            try
            {
                DataTable dt = new DataTable();
                grvPolizas.DataSource = dt;
                grvPolizas.DataSource = GetList();
                grvPolizas.DataBind();

                //if (grvPolizas.Rows.Count > 0)
                //    CNComun.HideColumns(grvPolizas, Celdas);
                //{
                //    OcultaColumna(grvPolizas, Celdas, indexCopia);
                //}
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupEmp", "$('#modalEmp').modal('show')", true);
                //modalCatEmpleados.Show();

            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        public void OcultaColumna(GridView grdView, Int32[] Columnas, int indexCopia)
        {
            for (int i = 0; i < Columnas.Length; i++)
            {
                grdView.HeaderRow.Cells[Convert.ToInt32(Columnas.GetValue(i))].Visible = false;
                foreach (GridViewRow row in grdView.Rows)
                {
                    row.Cells[Convert.ToInt32(Columnas.GetValue(i))].Visible = false;
                    if (indexCopia != 0)
                    {
                        if (row.Cells[0].Text == Convert.ToString(indexCopia))
                        {
                            grdView.SelectedIndex = row.RowIndex;
                        }
                    }

                }
            }
        }

        private void CargarGridDetalle(List<Poliza_Detalle> ListPDet)
        {
            //lblError.Text = string.Empty;
            try
            {
                grvPolizas_Detalle.DataSource = ListPDet;
                grvPolizas_Detalle.DataBind();
                Sumatoria(grvPolizas_Detalle);
                grvPolizas_Detalle.DataBind();
                Celdas = new Int32[] { 0, 3, 4 };
                if (grvPolizas_Detalle.Rows.Count > 0)
                    CNComun.HideColumns(grvPolizas_Detalle, Celdas);
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        private void CargarGridPolizaCFDI(List<Poliza_CFDI> ListPolizaCFDI)
        {
            grvPolizaCFDI.DataSource = null;
            grvPolizaCFDI.DataBind();
            Int32[] Celdas = new Int32[] { 14, 15, 16 };
            Int32[] Celdas2 = new Int32[] { 13, 14, 15, 16 };
            try
            {
                double TotalPagos;
                DataTable dt = new DataTable();
                grvPolizaCFDI.DataSource = ListPolizaCFDI;
                grvPolizaCFDI.DataBind();
                if (ListPolizaCFDI.Count() > 0)
                {
                    TotalPagos = ListPolizaCFDI.Sum(item => Convert.ToDouble(item.CFDI_Total));
                    lblGranTotalInt.Text = Convert.ToString(TotalPagos);
                    lblGranTotal.Text = Convert.ToString(TotalPagos.ToString("C"));


                    foreach (GridViewRow row in grvPolizaCFDI.Rows)
                    {
                        GridView grd2 = row.FindControl("grdPartidas") as GridView;
                        DataTable dt2 = new DataTable();
                        grd2.DataSource = dt;
                        if (ListPolizaCFDI[row.RowIndex].listPolizaPartidas.Count > 0)
                        {
                            grd2.DataSource = ListPolizaCFDI[row.RowIndex].listPolizaPartidas;
                            grd2.DataBind();
                        }
                        else
                        {
                            grd2.DataSource = null;
                            grd2.DataBind();
                        }

                        DropDownList ddlCatPartidas = row.FindControl("ddlCatPartidas") as DropDownList;
                        ddlCatPartidas.DataSource = ListPolizaCFDI[row.RowIndex].lstPartidas;
                        ddlCatPartidas.DataValueField = "IdStr";
                        ddlCatPartidas.DataTextField = "Descripcion";
                        ddlCatPartidas.DataBind();

                        //DropDownList ddlCatPartidas = grd2.HeaderRow.FindControl("ddlCatPartidas") as DropDownList;
                        ////DropDownList ddlCatPartidas = row.FindControl("ddlCatPartidas") as DropDownList;
                        //ddlCatPartidas.DataSource = ListPolizaCFDI[row.RowIndex].lstPartidas;
                        //ddlCatPartidas.DataValueField = "IdStr";
                        //ddlCatPartidas.DataTextField = "Descripcion";
                        //ddlCatPartidas.DataBind();
                    }
                    //    DropDownList ddlCatPartidas = row.FindControl("ddlCatPartidas") as DropDownList;
                    //    ddlCatPartidas.DataSource = ListPolizaCFDI[row.RowIndex].lstPartidas;
                    //    ddlCatPartidas.DataValueField = "IdStr";
                    //    ddlCatPartidas.DataTextField = "Descripcion";
                    //    ddlCatPartidas.DataBind();
                    //    CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref ddlCatPartidas, "p_usuario", "p_ejercicio", SesionUsu.Usu_Nombre, SesionUsu.Usu_Ejercicio, ref ListCentroContable);


                    //    GridView grd2 = row.FindControl("grdPartidas") as GridView;
                    //    DataTable dt2 = new DataTable();
                    //    grd2.DataSource = dt;
                    //    grd2.DataSource = ListPolizaCFDI[row.RowIndex].lstPolizaPartidas;
                    //    grd2.DataBind();
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
                else
                {
                    TotalPagos = 0;
                    lblGranTotalInt.Text = Convert.ToString(TotalPagos);
                    lblGranTotal.Text = Convert.ToString(TotalPagos.ToString("C"));
                }


            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        private void CargarGridPolizaOficios(List<Poliza_Oficio> ListPoliza_Oficio)
        {
            grdOficios.DataSource = null;
            grdOficios.DataBind();
            Int32[] Celdas = new Int32[] { 10 };
            //Int32[] Celdas2 = new Int32[] { 10, 11, 12 };
            try
            {
                DataTable dt = new DataTable();
                grdOficios.DataSource = ListPoliza_Oficio;
                grdOficios.DataBind();
                if (ListPoliza_Oficio.Count() > 0)
                {
                    if (grvPolizas.SelectedRow.Cells[16].Text == "N")
                    {
                        CNComun.HideColumns(grdOficios, Celdas);
                    }
                }
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }


        private void LimpiaGrid()
        {
            //List<Poliza> dt = new List<Poliza>();
            //grvPolizas.DataSource = dt;
            grvPolizas.DataSource = null;
            grvPolizas.DataBind();
        }
        private List<Poliza> GetList()
        {
            divErrorTot.Visible = false;
            try
            {
                List<Poliza> List = new List<Poliza>();
                ObjPoliza.Ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                ObjPoliza.Centro_contable = DDLCentro_Contable.SelectedValue;
                ObjPoliza.Tipo = ddlTipo2.SelectedValue;
                ObjPoliza.Status = ddlStatus2.SelectedValue;
                ObjPoliza.Tipo_captura = ddlTipo_CapturaInicio.SelectedValue;
                ObjPoliza.Clasificacion = ddlClasificaIni.SelectedValue;
                string FechaInicial;
                string FechaFinal;
                if (ddlFecha_Ini.SelectedValue == "00")
                    FechaInicial = "01/01/" + SesionUsu.Usu_Ejercicio;
                else
                    FechaInicial = "01/" + ddlFecha_Ini.SelectedValue + "/" + SesionUsu.Usu_Ejercicio;



                if (ddlFecha_Ini.SelectedValue == "00")
                    FechaFinal = "31/01/" + SesionUsu.Usu_Ejercicio;
                else
                {
                    int DiaFinal = System.DateTime.DaysInMonth(Convert.ToInt32(SesionUsu.Usu_Ejercicio), Convert.ToInt32(ddlFecha_Fin.SelectedValue));
                    FechaFinal = DiaFinal + "/" + ddlFecha_Fin.SelectedValue + "/" + SesionUsu.Usu_Ejercicio;
                }

                if (Convert.ToInt32(SesionUsu.Usu_Ejercicio) >= 2022)
                    CNPoliza.PolizaConsultaGrid_Min(ref ObjPoliza, FechaInicial, FechaFinal, txtBuscar.Text.ToUpper(), SesionUsu.Usu_TipoUsu, ref List);
                else
                    CNPoliza.PolizaConsultaGrid(ref ObjPoliza, FechaInicial, FechaFinal, txtBuscar.Text.ToUpper(), SesionUsu.Usu_TipoUsu, ref List);
                if (List.Count >= 4000)
                {
                    List = null;
                    divErrorTot.Visible = true;

                }
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                CNEmpleado.ConsultarEmpleados(objEmpleado, ref ListEmpleados);
                return ListEmpleados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Sumatoria(GridView grdView)
        {
            //grdView.AllowPaging = false;
            lblTotal_Cargos.Text = string.Empty;
            lblTotal_Abonos.Text = string.Empty;
            //double cargos = 0;
            //double abonos = 0;
            decimal cargos = 0;
            decimal abonos = 0;
            cargos = grdView.Rows.Cast<GridViewRow>().Sum(x => Convert.ToDecimal(x.Cells[3].Text));
            abonos = grdView.Rows.Cast<GridViewRow>().Sum(x => Convert.ToDecimal(x.Cells[4].Text));
            lblTotal_Cargos.Text = Convert.ToString(cargos); // String.Format("{0:c}", Convert.ToDouble(cargos));
            lblTotal_Abonos.Text = Convert.ToString(abonos); //String.Format("{0:c}", Convert.ToDouble(abonos));
            lblFormatoTotal_Cargos.Text = String.Format("{0:C}", Convert.ToDouble(cargos));
            lblFormatoTotal_Abonos.Text = String.Format("{0:C}", Convert.ToDouble(abonos));
            //grdView.AllowPaging = true;

        }

        private void InicializaCamposGuardar()
        {
            filaFechasBusqueda.Visible = true;
            filaFechas.Visible = false;
            filaBusqueda.Visible = true;
            linkBttnNuevo.Visible = true;
            //linkBttnNuevo.Visible = true;
            DDLCentro_Contable.Enabled = true;
            DateTime fecha = Convert.ToDateTime(txtFecha.Text);
            //string Ini = fecha.ToString("MM");
            ddlFecha_Ini.SelectedValue = fecha.ToString("MM");
            ddlFecha_Fin.SelectedValue = fecha.ToString("MM");
        }
        private void VerificaFechas(TextBox txt)
        {
            //lblError.Text = string.Empty;
            try
            {
                DateTime fecha = Convert.ToDateTime(txt.Text);
                string Anio = fecha.ToString("yyyy");
                if (Anio != SesionUsu.Usu_Ejercicio)
                {
                    //txt.Text = string.Empty;
                    //lblMsj.Text = "Ejercicio incorrecto";
                    string MesCC = VerificaMes();
                    txt.Text = "01/" + MesCC + "/" + SesionUsu.Usu_Ejercicio;
                }
            }
            catch (Exception ex)
            {
                //lblError.Text = "function(VerificaFechas) " + ex.Message;
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
        private string ValidaMes(TextBox txt)
        {
            //lblError.Text = string.Empty;
            int band = 0;
            int Mes = 0;
            int NumMesCC = 0;
            try
            {
                string MesCC = VerificaMes();
                band = band + 1;
                DateTime fecha = Convert.ToDateTime(txt.Text);
                string MesFecha = fecha.ToString("MM");
                band = band + 1;
                Mes = Convert.ToInt32(MesFecha);
                band = band + 1;
                NumMesCC = Convert.ToInt32(MesCC);
                band = band + 1;
                if (Mes >= NumMesCC)
                    return "Z";
                else
                    return "Unicamente se puede capturar hasta el mes " + MesCC + ", verificar";

            }
            catch (Exception ex)
            {
                //return lblError.Text = band + "-" + Mes + "-" + NumMesCC + "function(ValidaMes) " + ex.Message;
                return band + "-" + Mes + "-" + NumMesCC + "function(ValidaMes) " + ex.Message;

            }

        }
        private void ValidateNumPoliza(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (args.Value.Contains("4999"));
        }
        private void Copiar_a_AdjuntosTemp
            (List<Poliza_CFDI> lstPolizasCFDI)
        {
            string OrigenArchivo = string.Empty;
            string DestinoArchivo = string.Empty;
            string OrigenArchivo2 = string.Empty;
            string DestinoArchivo2 = string.Empty;

            try
            {
                for (int i = 0; i < lstPolizasCFDI.Count; i++)
                {
                    OrigenArchivo = Path.Combine(Server.MapPath("~/Adjuntos"), lstPolizasCFDI[0].NombreArchivoXML);
                    DestinoArchivo = Path.Combine(Server.MapPath("~/AdjuntosTemp"), lstPolizasCFDI[0].NombreArchivoXML);
                    System.IO.File.Copy(OrigenArchivo, DestinoArchivo, true);


                    if (lstPolizasCFDI[0].NombreArchivoPDF != string.Empty)
                    {
                        OrigenArchivo2 = Path.Combine(Server.MapPath("~/Adjuntos"), lstPolizasCFDI[0].NombreArchivoPDF);
                        DestinoArchivo2 = Path.Combine(Server.MapPath("~/AdjuntosTemp"), lstPolizasCFDI[0].NombreArchivoPDF);
                        System.IO.File.Copy(OrigenArchivo2, DestinoArchivo2, true);
                    }
                }

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        private void Copiar_a_Adjuntos(List<Poliza_CFDI> lstPolizasCFDI)
        {
            string OrigenArchivo = string.Empty;
            string DestinoArchivo = string.Empty;
            string OrigenArchivo2 = string.Empty;
            string DestinoArchivo2 = string.Empty;

            try
            {
                for (int i = 0; i < lstPolizasCFDI.Count; i++)
                {
                    OrigenArchivo = Path.Combine(Server.MapPath("~/AdjuntosTemp"), lstPolizasCFDI[0].NombreArchivoXML); //System.IO.Path.Combine(Origen, fileName);
                    DestinoArchivo = Path.Combine(Server.MapPath("~/Adjuntos"), lstPolizasCFDI[0].NombreArchivoXML);  //System.IO.Path.Combine(Destino, fileName);
                    System.IO.File.Copy(OrigenArchivo, DestinoArchivo, true);
                    //System.IO.File.Delete(OrigenArchivo); //MODIFICADO DIA 26/03

                    OrigenArchivo2 = Path.Combine(Server.MapPath("~/AdjuntosTemp"), lstPolizasCFDI[0].NombreArchivoPDF); //System.IO.Path.Combine(Origen, fileName);
                    DestinoArchivo2 = Path.Combine(Server.MapPath("~/Adjuntos"), lstPolizasCFDI[0].NombreArchivoPDF);  //System.IO.Path.Combine(Destino, fileName);
                    System.IO.File.Copy(OrigenArchivo2, DestinoArchivo2, true);
                    //System.IO.File.Delete(OrigenArchivo2); //MODIFICADO DIA 26/03

                }
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        private void Guardar()
        {
            string Validado = ValidaMes(txtFecha);
            string Verificador = string.Empty;
            string Verificador2 = string.Empty;

            try
            {

                if (Validado == "Z")
                {
                    if ((lblIniPoliza.Text + txtNumero_Poliza.Text).Length == 7)
                    {
                        ObjPoliza.Ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                        ObjPoliza.Numero_poliza = lblIniPoliza.Text + txtNumero_Poliza.Text;
                        ObjPoliza.Centro_contable = DDLCentro_Contable.SelectedValue;
                        ObjPoliza.Tipo = ddlTipo0.SelectedValue;
                        ObjPoliza.Fecha = txtFecha.Text;
                        ObjPoliza.Status = (Math.Abs(Convert.ToDouble(lblTotal_Cargos.Text)) != Math.Abs(Convert.ToDouble(lblTotal_Abonos.Text))) ? "N" : "A"; //ddlStatus0.SelectedValue;
                        ObjPoliza.Tipo_captura = ddlTipo_Captura.SelectedValue;
                        ObjPoliza.Alta_usuario = SesionUsu.Usu_Nombre;
                        ObjPoliza.Modificacion_usuario = SesionUsu.Usu_Nombre;
                        ObjPoliza.Cheque_cuenta = ddlCheque_Cuenta.SelectedValue; //"00000"; //ddlCheque_Cuenta.SelectedValue;
                        ObjPoliza.Cheque_numero = txtCheque_Numero.Text;
                        ObjPoliza.Cheque_importe = (txtCheque_Importe.Text.Length > 0) ? Convert.ToDouble(txtCheque_Importe.Text) : Convert.ToDouble("0");
                        if (ddlTipo0.SelectedValue != "I")
                        {
                            ObjPoliza.Cedula_numero = ddlNumCedula.SelectedValue; //txtCedula_Numero.Text;
                            if (ObjPoliza.Cedula_numero.Length == 18)
                            {
                                ListCedulas = (List<Comun>)Session["Cedulas"];
                                ObjPoliza.IdCedula = Convert.ToInt32(ListCedulas[ddlNumCedula.SelectedIndex].EtiquetaDos);
                            }
                        }
                        ObjPoliza.Beneficiario = txtBeneficiario.Text;
                        ObjPoliza.Tipo_Documento = ddlTipoDocto.SelectedValue;
                        ObjPoliza.Clasificacion = ddlClasifica.SelectedValue;
                        ObjPoliza.CFDI = ddlTipoDocto.SelectedValue == "CFDI" ? "S" : "N";
                        //if (ddlTipoDocto.SelectedValue == "CFDI")
                        //    ObjPoliza.Validar_Total_CFDI = chkValidarTotCFDI.Checked;

                        ObjPoliza.Oficio_Autorizacion = ddlTipoDocto.SelectedValue == "OFICIO" ? "S" : "N";

                        if (DDLCentro_Contable.SelectedValue == "72103")
                        {
                            if (ddlTipo0.SelectedValue != "I")
                                ObjPoliza.Concepto = ddlprograma.SelectedValue + "»" + txtConcepto.Text;
                            else
                                ObjPoliza.Concepto = txtConcepto.Text;
                        }
                        else
                            ObjPoliza.Concepto = txtConcepto.Text;




                        if (SesionUsu.Editar == 0)
                        {

                            CNPoliza.PolizaInsertar(ref ObjPoliza, ref Verificador);
                            if (Verificador == "0")
                            {
                                ListPDet = (List<Poliza_Detalle>)Session["PolizaDet"];
                                CNPolizaDet.PolizaDetInsertar(ListPDet, ObjPoliza.IdPoliza, /*grvPolizas_Detalle,*/ ref Verificador);
                                if (Verificador == "0")
                                {
                                    SesionUsu.Editar = -1;
                                    MultiView1.ActiveViewIndex = 0;
                                    InicializaCamposGuardar();
                                    CargarGrid(0);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(1, 'Los datos han sido agregados correctamente.');", true);
                                }
                                else
                                {
                                    CNComun.VerificaTextoMensajeError(ref Verificador);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                                    //lblError.Text = Verificador;
                                }
                            }
                            else
                            {
                                CNComun.VerificaTextoMensajeError(ref Verificador);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "VerPoliza('RP-005'," + SesionUsu.Usu_Ejercicio + ", '" + grvPolizas.SelectedRow.Cells[0].Text + "');", true);

                                //lblError.Text = Verificador;

                            }
                        }

                        else if (SesionUsu.Editar == 1)
                        {
                            ObjPoliza.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
                            CNPoliza.PolizaEditar(ref ObjPoliza, ref Verificador);
                            if (Verificador == "0")
                            {
                                ListPDet = (List<Poliza_Detalle>)Session["PolizaDet"];
                                CNPolizaDet.PolizaDetInsertar(ListPDet, ObjPoliza.IdPoliza, /*grvPolizas_Detalle,*/ ref Verificador);
                                if (Verificador == "0")
                                {
                                    SesionUsu.Editar = -1;
                                    MultiView1.ActiveViewIndex = 0;
                                    InicializaCamposGuardar();
                                    CargarGrid(0);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(1, 'Los datos han sido modificados correctamente.');", true);
                                }
                                else
                                {
                                    CNComun.VerificaTextoMensajeError(ref Verificador);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                                }
                            }
                            else
                            {
                                CNComun.VerificaTextoMensajeError(ref Verificador);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "modal", "mostrar_modal(0, '" + Verificador + "');", true);
                                //lblError.Text = Verificador;
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'Error en el número de póliza, favor de verificar.');", true);
                    }
                }

                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Validado + "');", true);

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                //lblError.Text = ex.Message;
            }

        }
        #endregion

        #region <Botones y Eventos>
        protected void imgbtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            if (SesionUsu.Editar == -1)
            {
                CargarGrid(0);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtBuscar.Visible = true;
            linkBttnBuscar.Visible = true;
            linkBttnNuevo.Visible = true;
            //linkBttnNuevo.Visible = true;
            filaFechas.Visible = false;
            filaFechasBusqueda.Visible = true;
            filaBusqueda.Visible = true;
            lblTotal_Cargos.Text = string.Empty;
            lblTotal_Abonos.Text = string.Empty;
            lblFormatoTotal_Cargos.Text = string.Empty;
            lblFormatoTotal_Abonos.Text = string.Empty;
            DDLCentro_Contable.Enabled = true;
            SesionUsu.Editar = -1;
            MultiView1.ActiveViewIndex = 0;
            //CargarGrid(0);
        }
        protected void grvPolizas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvPolizas.PageIndex = 0;
            grvPolizas.PageIndex = e.NewPageIndex;
            CargarGrid(0);
        }

        protected void DDLCentro_Contable_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Clasificacion", ref ddlClasifica, "p_tipo_usuario", "p_centro_contable", SesionUsu.Usu_TipoUsu, DDLCentro_Contable.SelectedValue);
            //CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Clasificacion", ref ddlClasificaCopia, "p_tipo_usuario", "p_centro_contable", SesionUsu.Usu_TipoUsu, DDLCentro_Contable.SelectedValue);

            if (SesionUsu.Editar == 0 || SesionUsu.Editar == 1)
            {
                Select_Programa();
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Cheque_Cuenta", ref ddlCheque_Cuenta, "p_ejercicio", "p_centro_contable", SesionUsu.Usu_Ejercicio, DDLCentro_Contable.SelectedValue);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_List_Cuentas_Contables_Id", ref ddlCuentas_Contables, "p_ejercicio", "p_centro_contable", SesionUsu.Usu_Ejercicio, Convert.ToString(DDLCentro_Contable.SelectedValue), ref ListCuentas);


                Session["Cuentas"] = ListCuentas;
                DateTime fechaIni = new DateTime();
                DateTime fechaFin = new DateTime();
                string MesCC = VerificaMes();
                SesionUsu.MesActivo = MesCC;
                if (Convert.ToInt32(MesCC) > 12)
                {
                    fechaIni = Convert.ToDateTime("01/12/" + SesionUsu.Usu_Ejercicio);
                    fechaFin = Convert.ToDateTime("31/12/" + SesionUsu.Usu_Ejercicio);
                    SesionUsu.MesActivo = "12";
                }
                else
                {

                    DateTime fecha = Convert.ToDateTime("01/" + MesCC + "/" + SesionUsu.Usu_Ejercicio);
                    fechaIni = new DateTime(fecha.Year, fecha.Month, 1); // iniciar el primer día de mes
                                                                         // Establecer esta variable al Calendar
                    fechaFin = fechaIni.AddMonths(1).AddDays(-1); // este es el último día del mes en curso. 
                }

                CalendarExtenderFecha.StartDate = fechaIni;
                CalendarExtenderFecha.EndDate = fechaFin;

                CalendarExtenderFechaCopia.StartDate = fechaIni;
                CalendarExtenderFechaCopia.EndDate = fechaFin;

                if (SesionUsu.Editar == 0)
                {
                    txtFecha.Text = "01/" + MesCC + "/" + SesionUsu.Usu_Ejercicio;
                    lblIniPoliza.Text = MesCC;
                }
            }
            else
            {
                LimpiaGrid();

            }
        }
        protected void grvPolizas_Detalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int fila = e.RowIndex;
                int pagina = grvPolizas_Detalle.PageSize * grvPolizas_Detalle.PageIndex;
                fila = pagina + fila;
                List<Poliza_Detalle> ListPDet = new List<Poliza_Detalle>();
                ListPDet = (List<Poliza_Detalle>)Session["PolizaDet"];
                ListPDet.RemoveAt(fila);
                Session["PolizaDet"] = ListPDet;
                CargarGridDetalle(ListPDet);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void grvPolizas_Detalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvPolizas_Detalle.PageIndex = 0;
            grvPolizas_Detalle.PageIndex = e.NewPageIndex;
            //CargarGridDet(Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text));                    
            List<Poliza_Detalle> ListPDet = new List<Poliza_Detalle>();
            ListPDet = (List<Poliza_Detalle>)Session["PolizaDet"];
            Session["PolizaDet"] = ListPDet;
            grvPolizas_Detalle.DataSource = ListPDet;
            grvPolizas_Detalle.DataBind();
        }
        protected void grvPolizas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblError.Text = string.Empty;
            DDLCentro_Contable.Enabled = false;
            LimpiaCampos(); // linkBttnNuevo_Click(null, null);
            try
            {
                ObjPoliza.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);   // Convert.ToInt32(DataBinder.Eval(sender, "CommandArgument").ToString());
                CNPoliza.ConsultarPolizaSel(ref ObjPoliza, ref Verificador);
                if (Verificador == "0")
                {
                    SesionUsu.Editar = 1;
                    ddlTipo0.SelectedValue = ObjPoliza.Tipo;
                    ddlTipo0_SelectedIndexChanged(null, null);
                    ddlTipo_Captura.SelectedValue = ObjPoliza.Tipo_captura;
                    ddlStatus0.SelectedValue = ObjPoliza.Status;

                    txtConcepto.Text = ObjPoliza.Concepto;
                    txtFecha.Text = ObjPoliza.Fecha;
                    DDLCentro_Contable.SelectedValue = ObjPoliza.Centro_contable;
                    DDLCentro_Contable_SelectedIndexChanged(null, null);
                    ddlCheque_Cuenta.SelectedValue = ObjPoliza.Cheque_cuenta;
                    txtCheque_Numero.Text = ObjPoliza.Cheque_numero;
                    txtCheque_Importe.Text = Convert.ToString(ObjPoliza.Cheque_importe);


                    //if (ddlTipoDocto.SelectedValue=="CFDI" && SesionUsu.Usu_TipoUsu == "3")
                    //    chkValidarTotCFDI.Visible = true;

                    if (ObjPoliza.Cedula_numero.Length == 18)
                    {
                        ddlNumCedula.Visible = true;
                        txtCedula_Numero.Visible = false;
                        //CNComun.LlenaCombo("PKG_CONTABILIDAD.Obt_Combo_Num_Cedula", ref ddlNumCedula, "p_ejercicio", "p_centro_contable", "p_mes_anio", "p_editar", "p_num_cedula", SesionUsu.Usu_Ejercicio, DDLCentro_Contable.SelectedValue, txtFecha.Text.Substring(3, 2) + SesionUsu.Usu_Ejercicio.Substring(2), Convert.ToString(SesionUsu.Editar), ObjPoliza.Cedula_numero, ref ListCedulas);
                        CNComun.LlenaCombo("PKG_CONTABILIDAD.Obt_Combo_Num_Cedula", ref ddlNumCedula, "p_ejercicio", "p_centro_contable", "p_mes_anio", "p_editar", "p_num_cedula", "p_tipo", SesionUsu.Usu_Ejercicio, DDLCentro_Contable.SelectedValue, txtFecha.Text.Substring(3, 2) + SesionUsu.Usu_Ejercicio.Substring(2), Convert.ToString(SesionUsu.Editar), "", ddlTipo0.SelectedValue, ref ListCedulas);

                        Session["Cedulas"] = ListCedulas;
                        ddlNumCedula.SelectedValue = ObjPoliza.Cedula_numero;
                    }
                    else
                    {
                        ddlNumCedula.Visible = false;
                        txtCedula_Numero.Visible = true;
                        txtCedula_Numero.Text = ObjPoliza.Cedula_numero;
                    }
                    txtBeneficiario.Text = ObjPoliza.Beneficiario;

                    try
                    {
                        ddlClasifica.SelectedValue = ObjPoliza.Clasificacion;
                    }
                    catch
                    {
                        ddlClasifica.SelectedIndex = 0;
                    }



                    DateTime fecha = Convert.ToDateTime(txtFecha.Text);
                    //string Ini = fecha.ToString("MM");
                    SesionUsu.MesActivo = fecha.ToString("MM");

                    if (SesionUsu.MesActivo == ObjPoliza.Numero_poliza.Substring(0, 2))
                    {
                        lblIniPoliza.Text = ObjPoliza.Numero_poliza.Substring(0, 3);
                        txtNumero_Poliza.Text = ObjPoliza.Numero_poliza.Substring(3);
                    }
                    else
                    {
                        lblIniPoliza.Text = SesionUsu.MesActivo; //ObjPoliza.Numero_poliza.Substring(0, 3);
                        txtNumero_Poliza.Text = ObjPoliza.Numero_poliza.Substring(3);
                    }

                    try
                    {
                        ddlTipoDocto.SelectedValue = ObjPoliza.Tipo_Documento;
                    }
                    catch
                    {
                        ddlTipoDocto.SelectedValue = "X";
                    }


                    //chkValidarTotCFDI.Checked = ObjPoliza.Validar_Total_CFDI;

                    //chkIncluyeCFDI.Checked = ObjPoliza.CFDI == "S" ? true : false;
                    //if (ObjPoliza.CFDI == "S")
                    //    ddlTipoDocto.SelectedValue = "CFDI"; //rdoBtnnTipoDocto.SelectedValue = "CFDI";
                    //else if (ObjPoliza.Oficio_Autorizacion == "S")
                    //    ddlTipoDocto.SelectedValue = "OFICIO";  //rdoBtnnTipoDocto.SelectedValue = "OFICIO";
                    //else
                    //{

                    //    ddlTipoDocto.SelectedValue = ObjPoliza.Tipo_Documento;
                    //}


                    //txtFecha.Text = ObjPoliza.Fecha;
                    grvPolizas_Detalle.EditIndex = -1;
                    ObjPolizaDet.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
                    List<Poliza_Detalle> ListPDet = new List<Poliza_Detalle>();
                    CNPolizaDet.PolizaDetConsultaGrid(ref ObjPolizaDet, ref ListPDet);
                    DataTable dt = new DataTable();
                    Session["PolizaDet"] = ListPDet;
                    CargarGridDetalle(ListPDet);



                    //ValidatorNumPoliza.ValidationGroup = string.Empty;
                    //SesionUsu.Editar = 1;
                }
                else
                {
                    //lblError.Text = Verificador;                    
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                }

                if (DDLCentro_Contable.SelectedValue == "72103")
                {
                    if (ddlTipo0.SelectedValue != "I")
                    {
                        string[] resultado;
                        char[] delimiter = { '»' };
                        resultado = txtConcepto.Text.Split(delimiter);
                        if (resultado.Length > 1)
                        {
                            txtConcepto.Text = resultado[1];
                            ddlprograma.SelectedValue = resultado[0];
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }




        protected void grvPolizas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblMjErrorOficio.Text = string.Empty;
            Verificador = string.Empty;
            try
            {
                int fila = e.RowIndex;
                ObjPoliza.IdPoliza = Convert.ToInt32(grvPolizas.Rows[fila].Cells[0].Text);
                CNPoliza.PolizaEliminar(ObjPoliza, ref Verificador);

                if (Verificador == "0")
                    CargarGrid(0);
                else
                {
                    //lblError.Text = Verificador;                    
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    //lblMjErrorOficio.Text = Verificador;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

                }
                //modalOficios.Show();
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

                //lblMjErrorOficio.Text = Verificador;
                //modalOficios.Show();
            }
        }
        protected void imgBttnPdf(object sender, ImageClickEventArgs e)
        {
            string FechaInicial = "01/" + ddlFecha_Ini.SelectedValue + "/" + SesionUsu.Usu_Ejercicio;
            int DiaFinal = System.DateTime.DaysInMonth(Convert.ToInt32(SesionUsu.Usu_Ejercicio), Convert.ToInt32(ddlFecha_Fin.SelectedValue));
            string FechaFinal = DiaFinal + "/" + ddlFecha_Fin.SelectedValue + "/" + SesionUsu.Usu_Ejercicio;

            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "Verdiario_general('RP-004','" + DDLCentro_Contable.SelectedValue + "','" + FechaInicial + "','" + FechaFinal + "', '" + ddlTipo2.SelectedValue + "', '" + ddlStatus2.SelectedValue + "','" + txtBuscar.Text + "');", true);
        }
        protected void imgBttnExcel(object sender, ImageClickEventArgs e)
        {
            string FechaInicial = "01/" + ddlFecha_Ini.SelectedValue + "/" + SesionUsu.Usu_Ejercicio;
            int DiaFinal = System.DateTime.DaysInMonth(Convert.ToInt32(SesionUsu.Usu_Ejercicio), Convert.ToInt32(ddlFecha_Fin.SelectedValue));
            string FechaFinal = DiaFinal + "/" + ddlFecha_Fin.SelectedValue + "/" + SesionUsu.Usu_Ejercicio;
            string ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=RP-004exc&centro_contable=" + DDLCentro_Contable.SelectedValue + "&mes_inicial=" + FechaInicial + "&mes_final=" + FechaFinal + "&tipo_p=" + ddlTipo2.SelectedValue + "&status=" + ddlStatus2.SelectedValue + "&filtro_busca=" + txtBuscar.Text;
            string _open = "window.open('" + ruta + "', '_newtab');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

        }
        #endregion

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            //lblError.Text = string.Empty;
            lblIniPoliza.Text = Convert.ToString(txtFecha.Text).Substring(3, 2) + ddlClasifica.SelectedValue;
            VerificaFechas(txtFecha);
            string Validado = ValidaMes(txtFecha);
            if (Validado != "Z")
            {
                //lblError.Text = Validado;
                Verificador = Validado;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

            }
        }

        protected void ddlCuentas_Contables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            //ListCuentas.Find(TextBox2.Text);           

            //List<Comun> resultFindAll = ListCuentas.FindAll(
            //    delegate(string current)
            //    {
            //        return current.Contains(TextBox2.Text);
            //    }
            //);


            //string Busca = TextBox2.Text;
            //ListCuentas.Contains(Busca);


            //ListCuentas.Contains()

        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //txtSearch.Focus();
            ddlCuentas_Contables.DataSource = null;
            ddlCuentas_Contables.DataBind();
            ListCuentas = (List<Comun>)Session["Cuentas"];
            var filteredCuentas = from c in ListCuentas
                                  where c.Descripcion.Contains(txtSearch.Text.ToUpper()) //txtSearch.Text
                                  select c;

            var content = filteredCuentas.ToList<Comun>();
            ddlCuentas_Contables.DataSource = content;
            ddlCuentas_Contables.DataValueField = "IdStr";
            ddlCuentas_Contables.DataTextField = "Descripcion";
            ddlCuentas_Contables.DataBind();
            if (content.Count() >= 1)
            {
                ddlCuentas_Contables.SelectedIndex = 0;
            }
        }

        //protected void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    //txtSearch.Focus();
        //    ddlCuentas_Contables.DataSource = null;
        //    ddlCuentas_Contables.DataBind();
        //    ListCuentas = (List<Comun>)Session["Cuentas"];
        //    var filteredCuentas = from c in ListCuentas
        //                          where c.Descripcion.Contains(txtSearch.Text.ToUpper()) //txtSearch.Text
        //                          select c;

        //    var content = filteredCuentas.ToList<Comun>();

        //    //List<Comun> Lista = new List<Comun>();
        //    //Lista = filteredCuentas.ToList<Comun>();
        //    //ddlCuentas_Contables.DataSource = Lista;

        //    ddlCuentas_Contables.DataSource = content;
        //    ddlCuentas_Contables.DataValueField = "IdStr";
        //    ddlCuentas_Contables.DataTextField = "Descripcion";
        //    ddlCuentas_Contables.DataBind();
        //    if (content.Count() >= 1)
        //    {
        //        ddlCuentas_Contables.SelectedIndex = 0;
        //    }
        //    //ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", "Mensaje();", true);
        //    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "Mensaje();", true);

        //    //txtCargo.Focus();
        //    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + txtName.Text + "');", true);
        //}
        protected void ddlCuentas_Contables_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //txtSearch.Text = ddlCuentas_Contables.SelectedItem.Text;
            //txtSearch_TextChanged(null, null);
            //txtCargo.Focus();
        }
        protected void linkBttnImprimir_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grvPolizas.SelectedIndex = row.RowIndex;
            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "VerPoliza('RP-005'," + SesionUsu.Usu_Ejercicio + ", '" + grvPolizas.SelectedRow.Cells[0].Text + "');", true);

        }
        protected void linkBttnEliminar_Click(object sender, EventArgs e)
        {

        }
        protected void ddlFecha_Fin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlFecha_Ini_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlFecha_Ini.SelectedValue) >= Convert.ToInt32(ddlFecha_Fin.SelectedValue))
                {
                    ddlFecha_Fin.SelectedValue = ddlFecha_Ini.SelectedValue;
                }
                LimpiaGrid();
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        protected void ddlTipo0_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_Programa();
            rowCFDI.Visible = true;
            CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Docto", ref ddlTipoDocto, "p_tipo_usuario", "p_tipo", "p_centro_contable", SesionUsu.Usu_TipoUsu, ddlTipo0.SelectedValue, DDLCentro_Contable.SelectedValue);

            rowIngreso.Visible = true;
            ddlTipoDocto.SelectedIndex = 0;
            if (ddlTipo0.SelectedValue == "E")
            {
                rowEgreso.Visible = true;
                rowEgreso2.Visible = true;
                rowEgreso3.Visible = true;
            }
            else if (ddlTipo0.SelectedValue == "I")
            {
                rowIngreso.Visible = false;
                rowEgreso.Visible = false;
            }
            else
            {
                rowEgreso.Visible = false;
                rowEgreso2.Visible = false;
                rowEgreso3.Visible = false;
                ddlCheque_Cuenta.SelectedValue = "00000";
                txtCheque_Numero.Text = string.Empty;
                txtCheque_Importe.Text = string.Empty;
                txtCedula_Numero.Text = string.Empty;
                txtBeneficiario.Text = string.Empty;
            }

            if (txtFecha.Text != string.Empty)
            {
                CNComun.LlenaCombo("PKG_CONTABILIDAD.Obt_Combo_Num_Cedula", ref ddlNumCedula, "p_ejercicio", "p_centro_contable", "p_mes_anio", "p_editar", "p_num_cedula", "p_tipo", SesionUsu.Usu_Ejercicio, DDLCentro_Contable.SelectedValue, txtFecha.Text.Substring(3, 2) + SesionUsu.Usu_Ejercicio.Substring(2), Convert.ToString(SesionUsu.Editar), "", ddlTipo0.SelectedValue, ref ListCedulas);
                Session["Cedulas"] = ListCedulas;
            }

        }
        protected void btnSi_Click(object sender, EventArgs e)
        {
            Guardar();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCOG", "$('#modalMsgError').modal('hide')", true);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            //modalGuardar.Hide();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCOG", "$('#modalMsgError').modal('hide')", true);

        }

        protected void ddlFecha_Fin_SelectedIndexChanged1(object sender, EventArgs e)
        {
            LimpiaGrid();
        }

        protected void ddlTipo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiaGrid();
        }

        protected void ddlStatus2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiaGrid();
        }

        protected void ddl_server(object sender, ServerValidateEventArgs e)
        {
            if (ddlTipo0.SelectedValue == "E")
            {
                string d = e.Value;
            }

        }


        protected void linkBttnCopiar_Click(object sender, EventArgs e)
        {
            txtNumero_Poliza_Copia.Text = string.Empty;
            txtFecha_Copia.Text = string.Empty;

            //modalCopiaPoliza.Show();            
            //MultiView1.ActiveViewIndex = 2;
            modalCopia.Show();

            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grvPolizas.SelectedIndex = row.RowIndex;
            //pnlPrincipal.Visible = false;
            string MesCC = VerificaMes();
            txtFecha_Copia.Text = "01/" + MesCC + "/" + SesionUsu.Usu_Ejercicio;
            hddnMesCopia.Value = MesCC;
            lblMsjPolizaCopia.Text = "ESTAS COPIANDO LA PÓLIZA NÚMERO " + grvPolizas.SelectedRow.Cells[2].Text + " DEL CENTRO CONTABLE " + grvPolizas.SelectedRow.Cells[1].Text;

        }

        protected void btnCancelarCopia_Click(object sender, EventArgs e)
        {
            //modalCopiaPoliza.Hide();
            //pnlPrincipal.Visible = true;
            MultiView1.ActiveViewIndex = 0;
        }

        protected void txtFecha_Copia_TextChanged(object sender, EventArgs e)
        {
            hddnMesCopia.Value = "00";
            lblMsjErrPolizaCopia.Text = string.Empty;
            modalCopia.Show();
            try
            {
                VerificaFechas(txtFecha_Copia);
                string Validado = ValidaMes(txtFecha_Copia);
                if (Validado != "Z")
                    lblMsjErrPolizaCopia.Text = Validado;
                else
                    hddnMesCopia.Value = txtFecha_Copia.Text.Substring(3, 2);
            }
            catch (Exception ex)
            {
                modalCopia.Show();
                lblMsjErrPolizaCopia.Text = ex.Message;
            }

        }

        protected void btnCopiar_Click(object sender, EventArgs e)
        {
            lblMsjErrPolizaCopia.Text = string.Empty;
            Verificador = string.Empty;

            try
            {
                ObjPoliza.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
                ObjPoliza.Numero_poliza = txtNumero_Poliza_Copia.Text;
                ObjPoliza.Fecha = txtFecha_Copia.Text;
                ObjPoliza.Clasificacion = ddlClasificaCopia.SelectedValue;
                CNPoliza.PolizaCopiar(ref ObjPoliza, ref Verificador);
                if (Verificador == "0")
                {
                    //pnlPrincipal.Visible = true;
                    int newIdPoliza = Convert.ToInt32(ObjPoliza.IdPoliza);
                    CargarGrid(newIdPoliza);
                    //grvPolizas.SelectedRow.Cells[0].Text = newIdPoliza;

                    //int idx=Convert.ToInt32(grvPolizas.SelectedRow.RowIndex.ToString());


                    //int index = grvPolizas.SelectedIndex;
                    grvPolizas_SelectedIndexChanged(null, null);



                    //LinkButton cbi = (LinkButton)(sender);
                    //GridViewRow row = (GridViewRow)cbi.NamingContainer;
                    //grvPolizas.SelectedIndex = row.RowIndex;



                }
                else
                {
                    modalCopia.Show();
                    lblMsjErrPolizaCopia.Text = Verificador;
                }
            }
            catch (Exception ex)
            {
                modalCopia.Show();
                lblMsjErrPolizaCopia.Text = ex.Message;
            }
        }

        //protected void txtFecha_Copia_TextChanged(object sender, EventArgs e)
        //{
        //    lblMsjErrPolizaCopia.Text = string.Empty;
        //    VerificaFechas(txtFecha_Copia);
        //    string Validado = ValidaMes(txtFecha_Copia);
        //    if (Validado != "Z")
        //    {
        //        lblMsjErrPolizaCopia.Text = Validado;
        //    }
        //    //txtNumero_Poliza_Copia.Text=string.Empty;
        //}

        protected void grvPolizas_Detalle_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //int indice=e.NewEditIndex;
            grvPolizas_Detalle.EditIndex = e.NewEditIndex;
            List<Poliza_Detalle> ListPDet = new List<Poliza_Detalle>();
            ListPDet = (List<Poliza_Detalle>)Session["PolizaDet"];
            Session["PolizaDet"] = ListPDet;
            CargarGridDetalle(ListPDet);
            //CargarGridDetalle();            
        }

        protected void grvPolizas_Detalle_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            List<Poliza_Detalle> ListPDet = new List<Poliza_Detalle>();
            ListPDet = (List<Poliza_Detalle>)Session["PolizaDet"];
            grvPolizas_Detalle.EditIndex = -1;
            CargarGridDetalle(ListPDet);
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            string FechaInicial = "01/" + ddlFecha_Ini.SelectedValue + "/" + SesionUsu.Usu_Ejercicio;
            int DiaFinal = System.DateTime.DaysInMonth(Convert.ToInt32(SesionUsu.Usu_Ejercicio), Convert.ToInt32(ddlFecha_Fin.SelectedValue));
            string FechaFinal = DiaFinal + "/" + ddlFecha_Fin.SelectedValue + "/" + SesionUsu.Usu_Ejercicio;

            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "Verdiario_polizas_x_lote('RP-005-lote','" + SesionUsu.Usu_Ejercicio + "','" + DDLCentro_Contable.SelectedValue + "','" + FechaInicial + "','" + FechaFinal + "', '" + ddlTipo2.SelectedValue + "', '" + ddlStatus2.SelectedValue + "');", true);
        }



        protected void EditaRegistro(object sender, GridViewUpdateEventArgs e)
        {
            List<Poliza_Detalle> ListPDet = new List<Poliza_Detalle>();
            ListPDet = (List<Poliza_Detalle>)Session["PolizaDet"];
            GridViewRow row = grvPolizas_Detalle.Rows[e.RowIndex];
            ListPDet[e.RowIndex].Cargo = Convert.ToDouble(((TextBox)(row.Cells[5].Controls[0])).Text);
            ListPDet[e.RowIndex].Abono = Convert.ToDouble(((TextBox)(row.Cells[6].Controls[0])).Text);
            grvPolizas_Detalle.EditIndex = -1;
            Session["PolizaDet"] = ListPDet;
            CargarGridDetalle(ListPDet);
        }

        protected void grvPolizas_Detalle_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {


        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (grvPolizas_Detalle.Rows.Count > 0)
                if (Math.Abs(Convert.ToDouble(lblTotal_Cargos.Text)) != Math.Abs(Convert.ToDouble(lblTotal_Abonos.Text)))
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCOG", "$('#modalMsgError').modal('show')", true); //modalGuardar.Show();
                else
                    Guardar();
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'Se deben agregar cargos y abonos.');", true);

            //lblError.Text = "Se deben agregar cargos y abonos";           
        }

        protected void grvPolizas_Detalle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
        {
            string MesCC = VerificaMes();
            if (Convert.ToInt32(MesCC) > 12)
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'Ejercicio Cerrado.');", true);
            else
            {
                LimpiaCampos();
                DDLCentro_Contable.Enabled = false;
                DDLCentro_Contable_SelectedIndexChanged(null, null);
            }
        }

        protected void LimpiaCampos()
        {
            linkBttnNuevo.Visible = false;
            //linkBttnNuevo.Visible = false;
            filaFechas.Visible = true;
            filaFechasBusqueda.Visible = false;
            filaBusqueda.Visible = false;
            //ddlTipo0_SelectedIndexChanged(null, null);
            txtNumero_Poliza.Text = string.Empty;
            txtConcepto.Text = string.Empty;
            ddlTipo_Captura.SelectedValue = "MC";
            txtCargo.Text = string.Empty;
            txtAbono.Text = string.Empty;
            ddlCheque_Cuenta.SelectedValue = "00000";
            txtCheque_Numero.Text = string.Empty;
            txtCheque_Importe.Text = string.Empty;
            pnlPrincipal.Visible = true;
            txtCedula_Numero.Text = string.Empty;
            txtBeneficiario.Text = string.Empty;
            lblTotal_Cargos.Text = string.Empty;
            lblTotal_Abonos.Text = string.Empty;
            lblFormatoTotal_Cargos.Text = string.Empty;
            lblFormatoTotal_Abonos.Text = string.Empty;
            //if (SesionUsu.Usu_TipoUsu != "3" || SesionUsu.Usu_TipoUsu != "4")
            //{
            //    rdoBtnnTipoDocto.Items[0].Enabled = false;
            //    rdoBtnnTipoDocto
            //}

            //rdoBtnnTipoDocto.SelectedValue = "N";
            SesionUsu.Editar = 0;

            ddlTipo0_SelectedIndexChanged(null, null);
            ddlTipoDocto.SelectedIndex = 0;
            ddlClasifica.SelectedIndex = 0;

            Session["PolizaDet"] = null;
            Session["PolizasCFDI"] = null;
            lblNumPolizaCFDI.Text = string.Empty;
            lblNumCheque.Text = string.Empty;
            lblTotCheque.Text = string.Empty;
            //lblTotAbonoPol.Text = string.Empty;
            grvPolizas_Detalle.DataSource = null;
            grvPolizas_Detalle.DataBind();

            TabContainer1.ActiveTabIndex = 0;
            MultiView1.ActiveViewIndex = 1;
            //ValidatorNumPoliza.ValidationGroup = "Poliza";

        }
        protected void LimpiaCamposFiscales()
        {
            /*Datos Fiscales*/
            ddlTipo_Beneficiario.SelectedValue = "0";
            ddlTipo_Gasto.SelectedValue = "0";

            /*Fin Datos Fiscales*/
        }
        protected void Select_Programa()
        {
            if (DDLCentro_Contable.SelectedValue == "72103")
            {
                if (ddlTipo0.SelectedValue == "E" || ddlTipo0.SelectedValue == "D")
                {
                    if (ddlTipo0.SelectedValue == "E")
                    {

                        rowPrograma.Visible = true;
                        //ddlprograma.Visible = true;
                        //lbprograma.Visible = true;


                        ddlprograma.SelectedIndex = 0;
                        val_programa.ValidationGroup = "Poliza";
                        //RFVCheque_Importe.ValidationGroup = string.Empty;
                        //RFVCheque_Numero.ValidationGroup = string.Empty;
                        //RFVBeneficiario.ValidationGroup = string.Empty;
                    }
                    else
                    {
                        rowPrograma.Visible = true;
                        //ddlprograma.Visible = true;
                        //lbprograma.Visible = true;
                        ddlprograma.SelectedIndex = 0;
                        val_programa.ValidationGroup = "Poliza";
                    }
                }
                else
                {
                    val_programa.ValidationGroup = string.Empty;
                    rowPrograma.Visible = false;
                    //ddlprograma.Visible = false;
                    //lbprograma.Visible = false;
                    ddlprograma.SelectedIndex = 1;
                }
            }
            else
            {
                val_programa.ValidationGroup = string.Empty;
                rowPrograma.Visible = false;
                //ddlprograma.Visible = false;
                //lbprograma.Visible = false;
                ddlprograma.SelectedIndex = 1;
                RFVCheque_Importe.ValidationGroup = "Poliza";
                RFVCheque_Numero.ValidationGroup = "Poliza";
                RFVBeneficiario.ValidationGroup = "Poliza";
            }
        }
        protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            //lblError.Text = string.Empty;
            string Cargo = txtCargo.Text.Replace(",", "");
            double TotCargo = Convert.ToDouble(Cargo);
            string Abono = txtAbono.Text.Replace(",", "");
            double TotAbono = Convert.ToDouble(Abono);
            try
            {
                ObjPolizaDet.IdCuenta_Contable = Convert.ToInt32(ddlCuentas_Contables.SelectedValue);
                ObjPolizaDet.DescCuenta_Contable = ddlCuentas_Contables.SelectedItem.Text;
                ObjPolizaDet.Cargo = TotCargo; // Convert.ToDouble(txtCargo.Text);
                ObjPolizaDet.Abono = TotAbono; // Convert.ToDouble(txtAbono.Text);
                //ObjPolizaDet.Numero_Movimiento=((Label)grvPolizas_Detalle.Rows[0].FindControl("lblNumero_Movimiento_Aut")).
                //_Detalle.PageSize * grvPolizas_Detalle.PageIndex) + Container.DisplayIndex + 1 


                if (Session["PolizaDet"] == null)
                {
                    ListPDet = new List<Poliza_Detalle>();
                    ListPDet.Add(ObjPolizaDet);
                }
                else
                {
                    ListPDet = (List<Poliza_Detalle>)Session["PolizaDet"];
                    ListPDet.Add(ObjPolizaDet);
                }

                Session["PolizaDet"] = ListPDet;
                CargarGridDetalle(ListPDet);
                txtCargo.Text = string.Empty;
                txtAbono.Text = string.Empty;
                ddlCuentas_Contables.Focus();
                //txtSearch.Focus();
            }

            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void imgBttnExcelLotes_Click(object sender, ImageClickEventArgs e)
        {
            string FechaInicial = "01/" + ddlFecha_Ini.SelectedValue + "/" + SesionUsu.Usu_Ejercicio;
            int DiaFinal = System.DateTime.DaysInMonth(Convert.ToInt32(SesionUsu.Usu_Ejercicio), Convert.ToInt32(ddlFecha_Fin.SelectedValue));
            string FechaFinal = DiaFinal + "/" + ddlFecha_Fin.SelectedValue + "/" + SesionUsu.Usu_Ejercicio;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "Verdiario_polizas_x_lote_exc('RP-005-lote-exc','" + SesionUsu.Usu_Ejercicio + "','" + DDLCentro_Contable.SelectedValue + "','" + FechaInicial + "','" + FechaFinal + "', '" + ddlTipo2.SelectedValue + "', '" + ddlStatus2.SelectedValue + "');", true);
            //string ruta = "Reportes/VisualizadorCrystal.aspx?idFact=" + Convert.ToInt32(grdDatosFactura.SelectedRow.Cells[0].Text);
            string ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=RP-005-lote-exc&ejercicio=" + SesionUsu.Usu_Ejercicio + "&centro_contable=" + DDLCentro_Contable.SelectedValue + "&mes_inicial=" + FechaInicial + "&mes_final=" + FechaFinal + "&tipo_p=" + ddlTipo2.SelectedValue + "&status=" + ddlStatus2.SelectedValue;
            string _open = "window.open('" + ruta + "', '_newtab');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

        }

        protected void bttnAgregaFactura_Click(object sender, EventArgs e)
        {
            string Ruta;
            string NombreArchivo;


            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            Poliza_CFDI objCFDI = new Poliza_CFDI();

            string VerificadorCFDI = string.Empty;
            string Concepto_Descripcion = string.Empty;
            int fila = 0;
            lblErrorCFDI.Text = string.Empty;
            lblErrorCFDI.Visible = false;

            //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'PRUEBAS');", true);

            try
            {
                if (FileFactura.HasFile)
                {
                    NombreArchivo = FileFactura.FileName.ToUpper();
                    NombreArchivo = NombreArchivo.Replace("&", "");
                    NombreArchivo = NombreArchivo.Replace(" ", "_");
                    if (NombreArchivo.Contains(".XML"))
                    {
                        DateTime FechaActual = DateTime.Today;
                        XmlDocument xDoc = new XmlDocument();

                        //Ruta = Path.Combine(Server.MapPath("~/AdjuntosTemp"), grvPolizas.SelectedRow.Cells[0].Text + grvPolizas.SelectedRow.Cells[17].Text + "-" + grvPolizas.SelectedRow.Cells[4].Text+ "-"+ DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[2].Text + "-" + NombreArchivo);
                        Ruta = Path.Combine(Server.MapPath("~/AdjuntosTemp"), grvPolizas.SelectedRow.Cells[17].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[2].Text + "-" + grvPolizas.SelectedRow.Cells[4].Text.Substring(0, 1) + "-" + grvPolizas.SelectedRow.Cells[0].Text + "-" + NombreArchivo);



                        ObjPolizaCFDI.Beneficiario_Tipo = ddlTipo_Beneficiario.SelectedValue;
                        ObjPolizaCFDI.Tipo_Gasto = ddlTipo_Gasto.SelectedValue;
                        ObjPolizaCFDI.NombreArchivoXML = grvPolizas.SelectedRow.Cells[17].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[2].Text + "-" + grvPolizas.SelectedRow.Cells[4].Text.Substring(0, 1) + "-" + grvPolizas.SelectedRow.Cells[0].Text + "-" + NombreArchivo;
                        ObjPolizaCFDI.Fecha_Captura = FechaActual.ToString("dd/MM/yyyy");
                        ObjPolizaCFDI.Usuario_Captura = SesionUsu.Usu_Nombre;
                        ObjPolizaCFDI.Tipo_Docto = "CFDI";
                        ObjPolizaCFDI.Ruta_XML = "~/AdjuntosTemp/" + ObjPolizaCFDI.NombreArchivoXML;
                        FileFactura.SaveAs(Ruta);
                        try
                        {
                            xDoc.Load(Ruta);

                        }
                        catch
                        {
                            StreamReader sr = new StreamReader(Ruta, System.Text.Encoding.UTF8);
                            StreamWriter writer = new StreamWriter("UTF8-" + Ruta, false, Encoding.UTF8);
                        }





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

                                if (listEmisor[0].Attributes["RegimenFiscal"] != null)
                                    ObjPolizaCFDI.RegimenFiscal = listEmisor[0].Attributes["RegimenFiscal"].InnerText;

                                if (listEmisor[0].Attributes["regimenfiscal"] != null)
                                    ObjPolizaCFDI.RegimenFiscal = listEmisor[0].Attributes["regimenfiscal"].InnerText;

                                if (listEmisor[0].Attributes["REGIMENFISCAL"] != null)
                                    ObjPolizaCFDI.RegimenFiscal = listEmisor[0].Attributes["REGIMENFISCAL"].InnerText;
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
                                objCFDI.CFDI_UUID = listTimbreDigital[0].Attributes["UUID"].InnerText;
                                if (ObjPolizaCFDI.CFDI_UUID.Length < 36)
                                {
                                    VerificadorCFDI = "EL VALOR (" + ObjPolizaCFDI.CFDI_UUID + ") DEL CAMPO UUID ES INCORRECTO.";
                                    ObjPolizaCFDI.CFDI_UUID = string.Empty;
                                }
                            }
                            else
                                VerificadorCFDI = "ERROR";

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

                                    ObjPolizaCFDI.CFDI_Concepto_Descripcion = Concepto_Descripcion;

                                    //if (nodoLstConceptos[i].Attributes["ClaveProdServ"] != null)
                                    //    objCFDIDet.CFDI_Concepto_Cve = nodoLstConceptos[i].Attributes["ClaveProdServ"].InnerText;
                                    //if (nodoLstConceptos[0].Attributes["Cantidad"] != null)
                                    //    objCFDIDet.CFDI_Concepto_Cantidad = nodoLstConceptos[i].Attributes["Cantidad"].InnerText;
                                    //if (nodoLstConceptos[0].Attributes["ClaveUnidad"] != null)
                                    //    objCFDIDet.CFDI_Concepto_ClaveUnidad = nodoLstConceptos[i].Attributes["ClaveUnidad"].InnerText;
                                    //if (nodoLstConceptos[0].Attributes["Unidad"] != null)
                                    //    objCFDIDet.CFDI_Concepto_Unidad = nodoLstConceptos[i].Attributes["Unidad"].InnerText;
                                    //if (nodoLstConceptos[0].Attributes["ValorUnitario"] != null)
                                    //    objCFDIDet.CFDI_Concepto_ValorUnitario = Convert.ToDouble(nodoLstConceptos[i].Attributes["ValorUnitario"].InnerText);
                                    //if (nodoLstConceptos[0].Attributes["Importe"] != null)
                                    //    objCFDIDet.CFDI_Concepto_Importe = Convert.ToDouble(nodoLstConceptos[i].Attributes["Importe"].InnerText);
                                    //if (nodoLstConceptos[i].Attributes["Descripcion"] != null)
                                    //    objCFDIDet.CFDI_Concepto_Descripcion = nodoLstConceptos[i].Attributes["Descripcion"].InnerText;

                                    //ObjPolizaCFDI.lstDetCFDI.Add(objCFDIDet);

                                }
                            }

                        }

                    }
                }

                /*Archivo PDF*/
                if (FileFacturaPDF.HasFile)
                {
                    NombreArchivo = FileFacturaPDF.FileName.ToUpper();
                    if (NombreArchivo.Contains(".PDF"))
                    {

                        //Ruta = Path.Combine(Server.MapPath("~/AdjuntosTemp"), DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[2].Text + "-"+NombreArchivo);
                        Ruta = Path.Combine(Server.MapPath("~/AdjuntosTemp"), grvPolizas.SelectedRow.Cells[17].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[2].Text + "-" + grvPolizas.SelectedRow.Cells[4].Text.Substring(0, 1) + "-" + grvPolizas.SelectedRow.Cells[0].Text + "-" + NombreArchivo);
                        FileFacturaPDF.SaveAs(Ruta);
                        //ObjPolizaCFDI.NombreArchivoPDF = grvPolizas.SelectedRow.Cells[2].Text + DDLCentro_Contable.SelectedValue + "-" + NombreArchivo;
                        //ObjPolizaCFDI.NombreArchivoPDF = grvPolizas.SelectedRow.Cells[17].Text + "-" + grvPolizas.SelectedRow.Cells[4].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[2].Text + "-" + NombreArchivo;
                        ObjPolizaCFDI.NombreArchivoPDF = grvPolizas.SelectedRow.Cells[17].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[2].Text + "-" + grvPolizas.SelectedRow.Cells[4].Text.Substring(0, 1) + "-" + grvPolizas.SelectedRow.Cells[0].Text + "-" + NombreArchivo;
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
                    objCFDI.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
                    CNPolizaCFDI.PolizaCFDIValidar(ref objCFDI, ref Verificador);
                    if (objCFDI.CFDI_Existe == 0 && Verificador == "0")
                    {
                        if (Session["PolizasCFDI"] != null)
                            lstPolizasCFDI = (List<Poliza_CFDI>)Session["PolizasCFDI"];

                        lstPolizasCFDI.Add(ObjPolizaCFDI);
                        Session["PolizasCFDI"] = lstPolizasCFDI;
                        CargarGridPolizaCFDI(lstPolizasCFDI);
                        LimpiaCamposFiscales();
                    }
                    else
                    {
                        Verificador = objCFDI.CFDI_Observaciones;
                        CNComun.VerificaTextoMensajeError(ref Verificador);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'ESTE CFDI YA EXISTE EN EL " + Verificador  + "');", true);
                        lblErrorCFDI.Visible = true;
                        lblErrorCFDI.Text = "ESTE CFDI YA EXISTE EN EL " + Verificador;

                    }
                }
                else
                {
                    Verificador = "ERROR EN EL XML: " + VerificadorCFDI;
                    lblErrorCFDI.Visible = true;
                    lblErrorCFDI.Text = Verificador;

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

                }

            }

            catch (Exception ex)
            {
                Verificador = ex.Message;
                lblErrorCFDI.Visible = true;
                lblErrorCFDI.Text = Verificador;
                //CNComun.VerificaTextoMensajeError(ref Verificador);

                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "modal", "mostrar_modal( 0, '" + MsjError + "');", true);  //lblMsjFam.Text = Verificador;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupError", "$('#modalError').modal('hide')", true);



            }
        }


        protected void imgBttnEliminarXML_Click(object sender, ImageClickEventArgs e)
        {
            LimpiaCamposFiscales();
        }


        protected void bttnAgregar_Click(object sender, EventArgs e)
        {
            //var lstCtas1123 = new string[]{ "11122","12122","13122","11222" };
            //var lstCtas1123 = Array.FindAll(ConceptosPosgrado, s => s.Equals(Substring(6, 5)));
            string[] lstCtas1123 = { "11122", "12122", "13122", "11222" };
            //var ExisteCta1123=string.Empty;
            string CtaInvalida = "N";
            try
            {

                string Cargo = txtCargo.Text.Replace(",", "");
                double TotCargo = Convert.ToDouble(Cargo);
                string Abono = txtAbono.Text.Replace(",", "");
                double TotAbono = Convert.ToDouble(Abono);


                ObjPolizaDet.IdCuenta_Contable = Convert.ToInt32(ddlCuentas_Contables.SelectedValue);
                ObjPolizaDet.DescCuenta_Contable = ddlCuentas_Contables.SelectedItem.Text;
                ObjPolizaDet.Cargo = TotCargo; // Convert.ToDouble(txtCargo.Text);
                ObjPolizaDet.Abono = TotAbono; // Convert.ToDouble(txtAbono.Text);
                string nivel_2 = ddlCuentas_Contables.SelectedItem.Text.Substring(5, 5);
                string mayor = ddlCuentas_Contables.SelectedItem.Text.Substring(0, 4);
                //& ddlCuentas_Contables.SelectedItem.Text.Substring(0, 4) == "1123" & (nivel_2 == "11122" || nivel_2 == "12122" || nivel_2 == "13122" ||   nivel_2 == "11222" || nivel_2 == "12222" || nivel_2 == "13222" || nivel_2 == "11322" || nivel_2 == "12322" || nivel_2 == "13322" || nivel_2 == "11422" || nivel_2 == "12422" || nivel_2 == "13422" || nivel_2 == "11622" || nivel_2 == "12622" || nivel_2 == "13622"))

                if (mayor == "1123" & ddlTipo0.SelectedValue == "E" & TotCargo > 0)
                    CtaInvalida = CNPoliza.ValidarTotalCta(DDLCentro_Contable.SelectedValue, nivel_2, ref Verificador);

                else
                    CtaInvalida = "N";


                if (CtaInvalida == "N")
                {
                    if (Session["PolizaDet"] == null)
                    {
                        ListPDet = new List<Poliza_Detalle>();
                        ListPDet.Add(ObjPolizaDet);
                    }
                    else
                    {
                        ListPDet = (List<Poliza_Detalle>)Session["PolizaDet"];
                        ListPDet.Add(ObjPolizaDet);
                    }

                    Session["PolizaDet"] = ListPDet;
                    CargarGridDetalle(ListPDet);
                    txtCargo.Text = string.Empty;
                    txtAbono.Text = string.Empty;
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'No son permitidos los cargos en las ctas 11122, 12122, 13122, 11222, 12222, 13222, 11322, 12322, 13322, 11422, 12422, 13422, 11622, 12622 y 13622');", true);

            }

            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }


        protected void btnGuardarCFDI_Click(object sender, EventArgs e)
        {
            /*DATOS CFDI XML*/
            Verificador = string.Empty;
            Verificador2 = string.Empty;
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            Poliza objPolizas = new Poliza();
            double total = 0;
            double total_5131 = 0;
            try
            {
                if (grvPolizaCFDI.Rows.Count >= 1)
                {
                    //Label lblTot = (Label)grvPolizaCFDI.FooterRow.FindControl("lblGranTotalInt");
                    //double lblTotInt = Convert.ToDouble(lblTot.Text);
                    double lblTotInt = Convert.ToDouble(lblGranTotalInt.Text);
                    //lblTotInt = Math.Ceiling(lblTotInt);
                    objPolizas.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
                    CNPoliza.ValidarTotal(ref objPolizas, ref Verificador);

                    //grvPolizas.SelectedRow.Cells[19].Text
                    total = Convert.ToDouble(hddnTotCheque.Value);
                    total_5131 = Convert.ToDouble(hddnTotCheque.Value);
                    total_5131 = total_5131 - 10;
                    if (objPolizas.ValidaTotal == "S" && (grvPolizas.SelectedRow.Cells[4].Text == "Egreso" || grvPolizas.SelectedRow.Cells[4].Text == "Diario") && (lblTotInt < total_5131) && grvPolizas.SelectedRow.Cells[21].Text.ToUpper() == "FALSE")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'El total de los CFDI´s es menor al total del cheque, favor de verificar.');", true);

                    else if (objPolizas.ValidaTotal == "N" && (grvPolizas.SelectedRow.Cells[4].Text == "Egreso" || grvPolizas.SelectedRow.Cells[4].Text == "Diario") && (lblTotInt < total) && grvPolizas.SelectedRow.Cells[21].Text.ToUpper() == "FALSE")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'El total de los CFDI´s es menor al total del cheque, favor de verificar.');", true);

                    else
                    {

                        if (Session["PolizasCFDI"] != null)
                            lstPolizasCFDI = (List<Poliza_CFDI>)Session["PolizasCFDI"];


                        ObjPolizaCFDI.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
                        CNPolizaCFDI.PolizaCFDIEditar(ObjPolizaCFDI, lstPolizasCFDI, ref Verificador);
                        //CNPolizaCFDI.PolizaCFDIInsertar(ObjPolizaCFDI, lstPolizasCFDI, ref Verificador);

                        if (Verificador == "0")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(1, 'Los datos han sido agregados correctamente.');", true);
                            Copiar_a_Adjuntos(lstPolizasCFDI);
                            SesionUsu.Editar = -1;
                            MultiView1.ActiveViewIndex = 0;
                            CargarGrid(0);
                            filaCentroContable.Visible = true;
                            filaFechasBusqueda.Visible = true;
                            filaBusqueda.Visible = true;
                            pnlPrincipal.Visible = true;
                        }
                        else
                        {
                            CNPolizaCFDI.EliminarCFDIEditar(Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text), ref Verificador2);
                            CNComun.VerificaTextoMensajeError(ref Verificador);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + " " + Verificador2 + "');", true);

                        }

                    }
                }
                else
                {
                    CNPolizaCFDI.EliminarCFDIEditar(Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text), ref Verificador);
                    if (Verificador == "0")
                    {
                        SesionUsu.Editar = -1;
                        MultiView1.ActiveViewIndex = 0;
                        CargarGrid(0);
                        filaCentroContable.Visible = true;
                        filaFechasBusqueda.Visible = true;
                        filaBusqueda.Visible = true;
                        pnlPrincipal.Visible = true;
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
        protected void linkBtnnAnexo_Click(object sender, EventArgs e)
        {
            string Ruta = string.Empty;
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grvPolizas.SelectedIndex = row.RowIndex;
            DropDownList ddl = (DropDownList)(row.Cells[13].FindControl("ddlDoctosTrans"));

            if (ddl.SelectedValue == "Volante")
                Ruta = grvPolizas.SelectedRow.Cells[22].Text;
            else if (ddl.SelectedValue == "Reclasificacion")
                Ruta = grvPolizas.SelectedRow.Cells[23].Text;
            else if (ddl.SelectedValue == "Anexo")
                Ruta = grvPolizas.SelectedRow.Cells[24].Text;



            Ruta = Ruta.Replace("&amp;", "&");

            string _open = "window.open('" + Ruta + "', '_newtab');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
        }
        protected void linkBttnCFDI_Click(object sender, EventArgs e)
        {
            lblNumCheque.Visible = false;
            lblTotCheque.Visible = false;
            lblNumPolizaCFDI.Text = string.Empty;
            lblMsjOficios.Text = string.Empty;
            //lblTotAbonoPol.Text = string.Empty;
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grvPolizas.SelectedIndex = row.RowIndex;
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            List<Poliza_Oficio> lstPolizaOficios = new List<Poliza_Oficio>();



            Session["PolizasCFDI"] = null;
            Session["PolizaOficios"] = null;

            grvPolizaCFDI.DataSource = null;
            grvPolizaCFDI.DataBind();
            grdOficios.DataSource = null;
            grdOficios.DataBind();


            ddlDocto.SelectedIndex = 0;
            divDatosOficio.Visible = false;
            ddlProveedor2.SelectedIndex = 0;
            txtCFDI_RFC.Text = string.Empty;
            txtCFDI_Fecha.Text = string.Empty;
            txtCFDI_Total.Text = string.Empty;
            txtNumFactura.Text = string.Empty;

            chkDocto.Checked = true;
            chkDocto_CheckedChanged(null, null);

            try
            {


                if (grvPolizas.SelectedRow.Cells[20].Text == "CFDI" || grvPolizas.SelectedRow.Cells[20].Text == "AMBOS")
                {
                    pnlPrincipal.Visible = false;
                    hddnTotCheque.Value = "0";

                    CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Docto_CFDI", ref ddlDocto, "p_tipo_docto", grvPolizas.SelectedRow.Cells[20].Text);

                    lblNumPolizaCFDI.Text = "# DE PÓLIZA: " + grvPolizas.SelectedRow.Cells[2].Text;
                    if (grvPolizas.SelectedRow.Cells[4].Text == "Egreso" || grvPolizas.SelectedRow.Cells[4].Text == "Diario")
                    {
                        Verificador = string.Empty;
                        ObjPolizaCFDI.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
                        CNPolizaCFDI.PolizaCFDIConsultaTotCheque(ref ObjPolizaCFDI, ref Verificador);

                        lblNumCheque.Visible = true;
                        lblTotCheque.Visible = true;
                        lblNumCheque.Text = "# DE CHEQUE: " + grvPolizas.SelectedRow.Cells[18].Text;
                        if (Verificador == "0")
                        {
                            lblTotCheque.Text = "TOTAL DEL CHEQUE: " + string.Format("{0:C}", ObjPolizaCFDI.CFDI_Total);
                            hddnTotCheque.Value = Convert.ToString(ObjPolizaCFDI.CFDI_Total);
                        }
                        else
                        {
                            lblTotCheque.Text = "TOTAL DEL CHEQUE: " + string.Format("{0:C}", grvPolizas.SelectedRow.Cells[19].Text);
                            hddnTotCheque.Value = grvPolizas.SelectedRow.Cells[19].Text;
                        }
                    }
                    //lblTotAbonoPol.Text = "TOTAL ABONO:"+ grvPolizas.SelectedRow.Cells[9].Text;
                    Verificador = string.Empty;
                    if (grvPolizas.SelectedRow.Cells[16].Text == "S")
                        bttnAgregaFactura.Visible = true; //bttnAgregaFactura0.Visible = true; 
                    else
                    {
                        lblNumPolizaCFDI.Text = "MES CERRADO, NO SE PUEDEN AGREGAR MÁS CFDI'S, # DE PÓLIZA: " + grvPolizas.SelectedRow.Cells[2].Text;
                        bttnAgregaFactura.Visible = false; //bttnAgregaFactura0.Visible = false;
                    }


                    ddlTipo_Gasto.SelectedIndex = 0;
                    //ddlProveedor2.SelectedIndex = 0;
                    ObjPolizaCFDI.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
                    ObjPolizaCFDI.Tipo_Docto = grvPolizas.SelectedRow.Cells[20].Text;
                    CNPolizaCFDI.PolizaCFDIConsultaDatos(ObjPolizaCFDI, ref lstPolizasCFDI, ref Verificador);
                    if (lstPolizasCFDI.Count > 0)
                    {
                        Session["PolizasCFDI"] = lstPolizasCFDI;
                        CargarGridPolizaCFDI(lstPolizasCFDI);

                    }


                    MultiView1.ActiveViewIndex = 3;
                }
                else
                {
                    rowProveedor.Visible = false;
                    rowEmpleado.Visible = false;
                    ddlProveedor.SelectedIndex = 0;
                    txtRFC.Text = string.Empty;
                    lblNombreEmp.Text = string.Empty;
                    lblTipoPersonal.Text = string.Empty;
                    lblNumPlaza.Text = string.Empty;
                    grdCatEmpleados.DataSource = null;
                    grdCatEmpleados.DataBind();
                    CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Docto_Oficio", ref DDLTipoDoctoOficio, "p_ejercicio", "p_centro_contable", "P_TIPO_USUARIO", SesionUsu.Usu_Ejercicio, DDLCentro_Contable.SelectedValue, SesionUsu.Usu_TipoUsu);

                    ObjPolizaOficio.IdPoliza_Oficio = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
                    CNPolizaOficio.PolizaOficiosConsulta(ObjPolizaOficio, ref lstPolizaOficios, ref Verificador);
                    if (lstPolizaOficios.Count > 0)
                    {
                        Session["PolizaOficios"] = lstPolizaOficios;
                        CargarGridPolizaOficios(lstPolizaOficios);
                    }

                    if (grvPolizas.SelectedRow.Cells[16].Text == "S")
                        bttnAgregarOficio.Visible = true; //bttnAgregaFactura.Visible = true;
                    else
                    {
                        lblMsjOficios.Text = "MES CERRADO, NO SE PUEDEN AGREGAR MÁS DOCUMENTOS";
                        bttnAgregarOficio.Visible = false; //bttnAgregaFactura.Visible = false;
                    }


                    txtOficio.Text = string.Empty;
                    txtFechaOficio.Text = string.Empty;
                    txtImporte.Text = "0";
                    DDLTipoDoctoOficio.SelectedIndex = 0;
                    ddlProveedor.SelectedIndex = 0;
                    txtRFC.Text = string.Empty;
                    pnlPrincipal.Visible = true;
                    //MultiView1.ActiveViewIndex = 4;
                    pnlPrincipal.Visible = false;

                    MultiView1.ActiveViewIndex = 4;
                    //modalOficios.Show();

                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    //upModal.Update();
                }
                //MultiView1.ActiveViewIndex = 4;
                SesionUsu.EditarCFDI = -1;


            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void btnCancelarCFDI_Click(object sender, EventArgs e)
        {
            LimpiaCamposFiscales();
            MultiView1.ActiveViewIndex = 0;
            filaCentroContable.Visible = true;
            filaFechasBusqueda.Visible = true;
            filaBusqueda.Visible = true;
            pnlPrincipal.Visible = true;
            SesionUsu.Editar = -1;
            if (SesionUsu.EditarCFDI == 1)
            {
                SesionUsu.EditarCFDI = -1;
                CargarGrid(0);
            }
        }
        protected void grvPolizaCFDI_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            lstPolizasCFDI = (List<Poliza_CFDI>)Session["PolizasCFDI"];
            try
            {
                int fila = e.RowIndex;
                //int pagina = grvPolizaCFDI.PageSize * grvPolizaCFDI.PageIndex;
                //fila = pagina + fila;
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

        protected void linkBttnNuevo_Click(object sender, EventArgs e)
        {

            Select_Programa();
            CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Cheque_Cuenta", ref ddlCheque_Cuenta, "p_ejercicio", "p_centro_contable", SesionUsu.Usu_Ejercicio, DDLCentro_Contable.SelectedValue);
            CNComun.LlenaCombo("pkg_contabilidad.Obt_List_Cuentas_Contables_Id", ref ddlCuentas_Contables, "p_ejercicio", "p_centro_contable", SesionUsu.Usu_Ejercicio, Convert.ToString(DDLCentro_Contable.SelectedValue), ref ListCuentas);


            Session["Cuentas"] = ListCuentas;
            DateTime fechaIni = new DateTime();
            DateTime fechaFin = new DateTime();
            string MesCC = VerificaMes();
            SesionUsu.MesActivo = MesCC;
            if (Convert.ToInt32(MesCC) > 12)
            {
                fechaIni = Convert.ToDateTime("01/12/" + SesionUsu.Usu_Ejercicio);
                fechaFin = Convert.ToDateTime("31/12/" + SesionUsu.Usu_Ejercicio);
                SesionUsu.MesActivo = "12";
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'Ejercicio Cerrado.');", true);
            }
            else
            {

                DateTime fecha = Convert.ToDateTime("01/" + MesCC + "/" + SesionUsu.Usu_Ejercicio);
                fechaIni = new DateTime(fecha.Year, fecha.Month, 1);
                fechaFin = fechaIni.AddMonths(1).AddDays(-1);


                CalendarExtenderFecha.StartDate = fechaIni;
                CalendarExtenderFecha.EndDate = fechaFin;

                CalendarExtenderFechaCopia.StartDate = fechaIni;
                CalendarExtenderFechaCopia.EndDate = fechaFin;



                txtFecha.Text = "01/" + MesCC + "/" + SesionUsu.Usu_Ejercicio;
                lblIniPoliza.Text = MesCC;




                //string MesCC = VerificaMes();

                LimpiaCampos();
                DDLCentro_Contable.Enabled = false;
                DDLCentro_Contable_SelectedIndexChanged(null, null);
                ddlTipo0.SelectedIndex = 0;
                ddlTipo0_SelectedIndexChanged(null, null);
                ddlNumCedula.Visible = true;
                txtCedula_Numero.Visible = false;
                CNComun.LlenaCombo("PKG_CONTABILIDAD.Obt_Combo_Num_Cedula", ref ddlNumCedula, "p_ejercicio", "p_centro_contable", "p_mes_anio", "p_editar", "p_num_cedula", "p_tipo", SesionUsu.Usu_Ejercicio, DDLCentro_Contable.SelectedValue, txtFecha.Text.Substring(3, 2) + SesionUsu.Usu_Ejercicio.Substring(2), Convert.ToString(SesionUsu.Editar), "", ddlTipo0.SelectedValue, ref ListCedulas);
                Session["Cedulas"] = ListCedulas;
            }
        }


        protected void bttnAgregarOficio_Click(object sender, EventArgs e)
        {
            //string fullPath;
            int fileSize;

            Poliza_Oficio objPolizaOficio = new Poliza_Oficio();
            List<Poliza_Oficio> lstOficios = new List<Poliza_Oficio>();
            try
            {
                if (FileOficio.HasFile)
                {
                    fileSize = FileOficio.PostedFile.ContentLength;

                    string NombreArchivo = FileOficio.FileName.ToUpper();
                    NombreArchivo = NombreArchivo.Replace(" ", "_");
                    NombreArchivo = NombreArchivo.Replace("%", string.Empty);
                    //NombreArchivo = grvPolizas.SelectedRow.Cells[0].Text + "-" + grvPolizas.SelectedRow.Cells[17].Text + "-" + grvPolizas.SelectedRow.Cells[4].Text + "-"+ DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[2].Text + "-" + NombreArchivo;
                    NombreArchivo = grvPolizas.SelectedRow.Cells[17].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[2].Text + "-" + grvPolizas.SelectedRow.Cells[4].Text.Substring(0, 1) + "-" + grvPolizas.SelectedRow.Cells[0].Text + "-" + NombreArchivo;

                    //grvPolizas.SelectedRow.Cells[0].Text +mes_anio+centro_contable+numero_poliza
                    objPolizaOficio.Tipo_Docto_Oficio = DDLTipoDoctoOficio.SelectedValue;
                    objPolizaOficio.Numero_Oficio = txtOficio.Text;
                    objPolizaOficio.Fecha_Oficio = txtFechaOficio.Text;
                    objPolizaOficio.Importe_Oficio = Convert.ToDouble(txtImporte.Text);
                    objPolizaOficio.RFC = txtRFC.Text.ToUpper();
                    objPolizaOficio.Nombre = lblNombreEmp.Text.ToUpper();
                    objPolizaOficio.Tipo_Personal = lblTipoPersonal.Text.ToUpper();
                    objPolizaOficio.Numero_Plaza = lblNumPlaza.Text.ToUpper();
                    if (ddlProveedor.SelectedValue == "X")
                        objPolizaOficio.Proveedor = txtProveedor.Text.ToUpper();
                    else if (ddlProveedor.SelectedValue == "Z")
                    {
                        objPolizaOficio.Proveedor = string.Empty;
                        objPolizaOficio.RFC = string.Empty;
                    }
                    else
                        objPolizaOficio.Proveedor = ddlProveedor.SelectedItem.Text.ToUpper();


                    //objPolizaOficio.NombreArchivoPDF = NombreArchivo;
                    string Ruta = Path.Combine(Server.MapPath("~/OficiosTemp"), NombreArchivo);
                    objPolizaOficio.NombreArchivoOficio = NombreArchivo;
                    objPolizaOficio.Ruta_Oficio = "~/OficiosTemp/" + objPolizaOficio.NombreArchivoOficio;

                    FileOficio.SaveAs(Ruta);



                    if (Session["PolizaOficios"] != null)
                        lstOficios = (List<Poliza_Oficio>)Session["PolizaOficios"];

                    lstOficios.Add(objPolizaOficio);
                    Session["PolizaOficios"] = lstOficios;
                    CargarGridPolizaOficios(lstOficios);




                    txtOficio.Text = string.Empty;
                    txtFechaOficio.Text = string.Empty;
                    txtImporte.Text = string.Empty;
                    ddlProveedor.SelectedIndex = 0;
                    txtProveedor.Text = string.Empty;
                    txtRFC.Text = string.Empty;

                    //MultiView1.ActiveViewIndex = 4;
                    //}

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'Adjuntar un archivo.');", true);

                }
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
            ////CargarGridPolizaCFDI(lstPolizasCFDI);
            //LimpiaCamposFiscales();
        }

        protected void bttnGuardarOficio_Click(object sender, EventArgs e)
        {
            List<Poliza_Oficio> lstOficios = new List<Poliza_Oficio>();
            Poliza_Oficio objPolizaOficio = new Poliza_Oficio();
            Verificador = string.Empty;
            lblMjErrorOficio.Text = string.Empty;
            try
            {
                if (Session["PolizaOficios"] != null)
                {
                    lstOficios = (List<Poliza_Oficio>)Session["PolizaOficios"];
                    objPolizaOficio.IdPoliza_Oficio = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
                    CNPolizaOficio.PolizaOficioInsertar(objPolizaOficio, SesionUsu.Usu_Nombre, lstOficios, ref Verificador);
                    if (Verificador == "0")
                    {
                        //modalOficios.Hide();
                        pnlPrincipal.Visible = true;
                        MultiView1.ActiveViewIndex = 0;
                        CargarGrid(0);
                    }
                    else
                    {
                        //modalOficios.Show();
                        CNComun.VerificaTextoMensajeError(ref Verificador);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

                        //lblMjErrorOficio.Text = Verificador;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'Se debe adjuntar un oficio, favor de verificar.');", true);

                }
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                //lblMjErrorOficio.Text = Verificador;
            }
        }

        protected void grdOficios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Poliza_Oficio> lstPolizaOficios = new List<Poliza_Oficio>();
            lstPolizaOficios = (List<Poliza_Oficio>)Session["PolizaOficios"];
            Verificador = string.Empty;
            try
            {
                int fila = e.RowIndex;
                int pagina = grdOficios.PageSize * grdOficios.PageIndex;
                fila = pagina + fila;
                lstPolizaOficios.RemoveAt(fila);
                Session["PolizaOficios"] = lstPolizaOficios;
                CargarGridPolizaOficios(lstPolizaOficios);
                //modalOficios.Show();
            }

            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

                //lblMjErrorOficio.Text = Verificador;
                //modalOficios.Show();
            }
        }


        protected void ddlCuentas_Contables_SelectedIndexChanged2(object sender, EventArgs e)
        {
            txtCargo.Focus();
        }

        protected void bttnSalirModal_Click(object sender, EventArgs e)
        {
            pnlPrincipal.Visible = true;
            MultiView1.ActiveViewIndex = 0;
        }

        protected void txtAbono_TextChanged(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        protected void linkBttnBuscar_Click(object sender, EventArgs e)
        {
            ddlFiltDocto.SelectedIndex = 0;
            if (SesionUsu.Editar == -1)
                CargarGrid(0);
        }



        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProveedor.SelectedValue == "X")
                txtProveedor.Visible = true;
            else
                txtProveedor.Visible = false;

            txtRFC.Text = ddlProveedor.SelectedValue;
        }

        protected void ddlProveedor2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProveedor2.SelectedValue == "X")
                txtProveedor2.Visible = true;
            else
                txtProveedor2.Visible = false;

            txtCFDI_RFC.Text = ddlProveedor2.SelectedValue;
        }




        protected void DDLTipoDoctoOficio_SelectedIndexChanged(object sender, EventArgs e)
        {
            rowProveedor.Visible = false;
            rowEmpleado.Visible = false;
            //rowCatEmpleados.Visible = false;
            if (DDLTipoDoctoOficio.SelectedValue == "PROVEEDOR")
                rowProveedor.Visible = true;
            else if (DDLTipoDoctoOficio.SelectedValue == "EMPLEADO")
            {
                rowEmpleado.Visible = true;

                //rowCatEmpleados.Visible = true;
            }
        }

        protected void ddlClasifica_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblIniPoliza.Text = SesionUsu.MesActivo + ddlClasifica.SelectedValue;
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }

        }

        protected void linkBttnBuscarEmpleado_Click(object sender, EventArgs e)
        {

            try
            {
                //rowCatEmpleados.Visible = true;

                CargarGridEmpleados();

                //CNComun.LlenaCombo("PKG_CONTABILIDAD.Obt_Combo_Tipo_Empleado", ref ddlTipoPersonal, "p_plaza", txtNumPlaza.Text);               
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }


        protected void ddlClasificaCopia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modalCopia.Show();
                if (hddnMesCopia.Value == "00")
                    lblMsjErrPolizaCopia.Text = "Mes incorrecto, verificar";
                else
                    lblIniPolizaCopia.Text = hddnMesCopia.Value + ddlClasificaCopia.SelectedValue;
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void bttnCancelarCFDI_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            lblErrorEliminarCFDIS.Text = string.Empty;
            modalErrorCFDI.Show();
            try
            {
                Poliza_CFDI ObjPolizaCFDI = new Poliza_CFDI();
                ObjPolizaCFDI.IdPoliza = Convert.ToInt32(grvPolizas.SelectedRow.Cells[0].Text);
                CNPolizaCFDI.EliminarCFDIS(ObjPolizaCFDI, ref Verificador);
                if (Verificador == "0")
                {
                    SesionUsu.EditarCFDI = -1;
                    SesionUsu.Editar = -1;
                    MultiView1.ActiveViewIndex = 0;
                    CargarGrid(0);
                    filaCentroContable.Visible = true;
                    filaFechasBusqueda.Visible = true;
                    filaBusqueda.Visible = true;
                    pnlPrincipal.Visible = true;
                    modalErrorCFDI.Hide();
                }
                else
                    lblErrorEliminarCFDIS.Text = Verificador;
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                lblErrorEliminarCFDIS.Text = Verificador;
            }

        }



        protected void grdCatEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Nombre = grdCatEmpleados.SelectedRow.Cells[1].Text.Replace("&#180;", "'");
            lblNombreEmp.Text = Nombre;   // Convert.ToInt32(DataBinder.Eval(sender, "CommandArgument").ToString());
            lblTipoPersonal.Text = Convert.ToString(grdCatEmpleados.SelectedRow.Cells[2].Text);   // Convert.ToInt32(DataBinder.Eval(sender, "CommandArgument").ToString());
            lblNumPlaza.Text = Convert.ToString(grdCatEmpleados.SelectedRow.Cells[3].Text);   // Convert.ToInt32(DataBinder.Eval(sender, "CommandArgument").ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupEmp", "$('#modalEmp').modal('hide')", true);
        }

        protected void linkBttnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            //modalCatEmpleados.Show();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupEmp", "$('#modalEmp').modal('show')", true);

        }



        protected void linkBttnVolante_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grvPolizas.SelectedIndex = row.RowIndex;
            //https://sysweb.unach.mx/SAF/Patrimonio/Reportes/VisualizadorCrystal_patrimonio.aspx?Tipo=RP-VOLANTE&Id=3321
            //grvPolizas.SelectedRow.Cells[21].Text;

        }



        protected void ddlDocto_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblErrorCFDI.Text = string.Empty;
            if (ddlDocto.SelectedValue == "OFICIO")
            {
                //divOficio.Visible = true;
                divDatosOficio.Visible = true;
                divFacturas.Visible = false;
                ddlProveedor2.SelectedIndex = 0;
                ddlProveedor2_SelectedIndexChanged(null, null);
            }
            else
            {
                //divOficio.Visible = false;
                divDatosOficio.Visible = false;
                divFacturas.Visible = true;
            }
        }



        protected void linkBttnOficioFact_Click(object sender, EventArgs e)
        {
            string NombreArchivo;
            string Ruta;
            Verificador = string.Empty;
            Poliza_CFDI objCFDI = new Poliza_CFDI();
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            objCFDI.CFDI_Observaciones = string.Empty;
            objCFDI.CFDI_UUID = string.Empty;
            try
            {
                if (fileOficioFactura.HasFile)
                {
                    NombreArchivo = fileOficioFactura.FileName.ToUpper();
                    Ruta = Path.Combine(Server.MapPath("~/AdjuntosTemp"), grvPolizas.SelectedRow.Cells[17].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[2].Text + "-" + grvPolizas.SelectedRow.Cells[4].Text.Substring(0, 1) + "-" + grvPolizas.SelectedRow.Cells[0].Text + "-" + NombreArchivo);
                    fileOficioFactura.SaveAs(Ruta);
                    ObjPolizaCFDI.NombreArchivoPDF = grvPolizas.SelectedRow.Cells[17].Text + "-" + DDLCentro_Contable.SelectedValue + "-" + grvPolizas.SelectedRow.Cells[2].Text + "-" + grvPolizas.SelectedRow.Cells[4].Text.Substring(0, 1) + "-" + grvPolizas.SelectedRow.Cells[0].Text + "-" + NombreArchivo;
                    ObjPolizaCFDI.Ruta_PDF = "~/AdjuntosTemp/" + ObjPolizaCFDI.NombreArchivoPDF;


                    ObjPolizaCFDI.CFDI_Folio = string.Empty;
                    ObjPolizaCFDI.CFDI_Fecha = txtCFDI_Fecha.Text;
                    ObjPolizaCFDI.CFDI_Total = Convert.ToDouble(txtCFDI_Total.Text);
                    if (chkDocto.Checked == true)
                    {
                        if (ddlProveedor2.SelectedValue == "X")
                            ObjPolizaCFDI.CFDI_Nombre = txtProveedor2.Text.ToUpper();
                        else
                            ObjPolizaCFDI.CFDI_Nombre = ddlProveedor2.SelectedItem.Text;

                        ObjPolizaCFDI.CFDI_RFC = txtCFDI_RFC.Text;
                    }
                    else
                    {
                        ObjPolizaCFDI.CFDI_Nombre = string.Empty;
                        ObjPolizaCFDI.CFDI_RFC = string.Empty;
                    }

                    ObjPolizaCFDI.CFDI_UUID = txtNumFactura.Text; //string.Empty;
                    ObjPolizaCFDI.Beneficiario_Tipo = ddlTipo_Beneficiario.SelectedValue;
                    ObjPolizaCFDI.Tipo_Gasto = ddlTipo_Gasto.SelectedValue;
                    DateTime FechaActual = DateTime.Today;
                    ObjPolizaCFDI.Fecha_Captura = FechaActual.ToString("dd/MM/yyyy");
                    ObjPolizaCFDI.Usuario_Captura = SesionUsu.Usu_Nombre;
                    ObjPolizaCFDI.Tipo_Docto = "OFICIO";
                    if (Session["PolizasCFDI"] != null)
                        lstPolizasCFDI = (List<Poliza_CFDI>)Session["PolizasCFDI"];

                    lstPolizasCFDI.Add(ObjPolizaCFDI);

                    ddlProveedor2.SelectedIndex = 0;
                    txtCFDI_RFC.Text = string.Empty;
                    txtCFDI_Fecha.Text = string.Empty;
                    txtCFDI_Total.Text = string.Empty;
                    txtNumFactura.Text = string.Empty;

                    Session["PolizasCFDI"] = lstPolizasCFDI;
                    CargarGridPolizaCFDI(lstPolizasCFDI);
                }
                else
                {
                    lblErrorCFDI.Visible = true;
                    lblErrorCFDI.Text = "Debe adjuntar el oficio.";
                }
            }

            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                lblErrorCFDI.Visible = true;
                lblErrorCFDI.Text = Verificador;
            }
        }
        protected void linkBttnVer_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grvPolizaCFDI.SelectedIndex = row.RowIndex;
            List<Poliza_CFDI> lstPolizasCFDI = new List<Poliza_CFDI>();
            lblConceptos.Text = string.Empty;
            try
            {
                if (Session["PolizasCFDI"] != null)
                    lstPolizasCFDI = (List<Poliza_CFDI>)Session["PolizasCFDI"];


                lblConceptos.Text = lstPolizasCFDI[row.RowIndex].CFDI_Concepto_Descripcion;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupError", "$('#modalConceptos').modal('show')", true);
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                lblErrorCFDI.Visible = true;
                lblErrorCFDI.Text = Verificador;
            }
        }



        protected void reqNumFactura3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlTipo_Gasto.SelectedValue == "COMISIONES")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void chkDocto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDocto.Checked == true)
            {
                rowProveedores.Visible = true;
                rowDatosProveedor.Visible = true;
            }
            else
            {
                rowProveedores.Visible = false;
                rowDatosProveedor.Visible = false;
            }
        }
        protected void bttnAgregarPartidas_Click(object sender, EventArgs e)
        {
            Poliza_CFDI objPoliza = new Poliza_CFDI();
            List<Poliza_CFDI> lstPartidas = new List<Poliza_CFDI>();
            ImageButton cbi = (ImageButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            Panel pnlPartidas = (Panel)(row.Cells[11].FindControl("pnlPartidas"));
            GridView grvPartidas = (GridView)(row.Cells[11].FindControl("grdPartidas"));
            DataTable dt2 = new DataTable();
            if (cbi.ImageUrl == "../../images/plus.png")
            {
                pnlPartidas.Visible = true;
                cbi.ImageUrl = "../../images/minus.png";
                grvPolizaCFDI.SelectedIndex = row.RowIndex;
                objPoliza.IdPoliza = 1204918;

                CNPolizaCFDI.PolizaPartidasDatos(objPoliza, ref lstPartidas, ref Verificador);
                grvPartidas.DataSource = lstPartidas;
                grvPartidas.DataBind();
            }
            else
            {
                cbi.ImageUrl = "../../images/plus.png";
                pnlPartidas.Visible = false;
            }

            //if (cbi.Text == "- Ocultar")
            //{
            //    grvPartidas.DataSource = null;
            //    grvPartidas.DataBind();
            //    cbi.Text = "+ Partida";
            //}
            //else
            //{
            //    cbi.Text = "- Ocultar";
            //    grvPolizaCFDI.SelectedIndex = row.RowIndex;
            //    objPoliza.IdPoliza = 1204918;

            //    CNPolizaCFDI.PolizaPartidasDatos(objPoliza, ref lstPartidas, ref Verificador);
            //    grvPartidas.DataSource = lstPartidas;
            //    grvPartidas.DataBind();
            //}
        }
        protected void linkBttnAgregarPartidas_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#album-photo').modal('toggle');</script>");


            Poliza_CFDI objPoliza = new Poliza_CFDI();
            List<Poliza_CFDI> lstPartidas = new List<Poliza_CFDI>();
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            GridView grvPartidas = (GridView)(row.Cells[11].FindControl("grdPartidas"));
            //Panel pnlPartidas = (Panel)(row.Cells[11].FindControl("pnlPartidas"));
            //            GridView grvPartidas = (GridView)(row.Cells[11].FindControl("grdPartidas"));
            DataTable dt2 = new DataTable();

            grvPolizaCFDI.SelectedIndex = row.RowIndex;
            objPoliza.IdPoliza = 1204918;

            CNPolizaCFDI.PolizaPartidasDatos(objPoliza, ref lstPartidas, ref Verificador);
            grvPartidas.DataSource = lstPartidas;
            grvPartidas.DataBind();

        }

        protected void grvPolizaCFDI_DataBound(object sender, EventArgs e)
        {

        }

        protected void grvPolizaCFDI_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void linkBttnAddPart_Click(object sender, EventArgs e)
        {
            Poliza_CFDI objPoliza = new Poliza_CFDI();
            List<Poliza_Partida> lstPartidas = new List<Poliza_Partida>();
            List<Poliza_CFDI> lstPolizasCfdi = new List<Poliza_CFDI>();
            //Poliza_Partida objPartida = new Poliza_Partida();
            Poliza_Partida objPartida;
            Poliza_CFDI objPoliza_Partida = new Poliza_CFDI();
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            try
            {
                GridView grvPartidas = (GridView)(row.Cells[3].FindControl("grdPartidas"));
                DropDownList ddlCatPartidas = (DropDownList)(row.Cells[3].FindControl("ddlCatPartidas"));
                TextBox txtImporte = (TextBox)(row.Cells[3].FindControl("txtImpPartida"));
                DataTable dt2 = new DataTable();

                grvPolizaCFDI.SelectedIndex = row.RowIndex;
                objPoliza.IdPoliza = 1204918;


                //objPartida.Partida = ddlCatPartidas.SelectedValue;
                //objPartida.Importe_Partida = Convert.ToDouble(txtImporte.Text);
                //objPartida.Partida_Descripcion = ddlCatPartidas.SelectedItem.Text;

                if (Session["PolizasCFDI"] != null)
                    lstPolizasCfdi = (List<Poliza_CFDI>)Session["PolizasCFDI"];


                //lstPartidas.Add(objPartida);

                //lstPolizasCfdi[row.RowIndex].listPolizaPartidas.Add(objPartida);// = lstPartidas;
                grvPartidas.DataSource = lstPolizasCfdi[row.RowIndex].listPolizaPartidas;
                grvPartidas.DataBind();

                Session["PolizasCFDI"] = lstPolizasCfdi;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupError", "$('#multiCollapse_" + grvPolizaCFDI.SelectedRow.Cells[7].Text+"').collapse()", true);
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
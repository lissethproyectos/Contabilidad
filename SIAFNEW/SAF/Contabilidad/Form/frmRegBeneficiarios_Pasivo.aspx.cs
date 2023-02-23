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
    public partial class frmRegBeneficiarios_Pasivo : System.Web.UI.Page
    {
        #region <Variables>
        string Verificador = string.Empty;
        string VerificadorDet = string.Empty;
        Sesion SesionUsu = new Sesion();
        Pasivo objPasivo = new Pasivo();
        CN_Comun CNComun = new CN_Comun();
        CN_Empleado CNEmpleado = new CN_Empleado();
        CN_Poliza CNPasivo = new CN_Poliza();
        private static List<Comun> ListTipo = new List<Comun>();
        List<Comun> ListPolizas = new List<Comun>();
        List<Comun> ListCedulas = new List<Comun>();
        List<Comun> ListCuentas = new List<Comun>();
        Comun objBeneficiario = new Comun();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            SesionUsu = (Sesion)Session["Usuario"];
            if (!IsPostBack)
                Inicializar();

            ScriptManager.RegisterStartupScript(this, GetType(), "Empleados", "Autocomplete();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "GridPasivos", "Pasivos();", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "GridEmpleados", "CatEmpleados();", true);

        }
        private void Inicializar()
        {
            MultiView1.ActiveViewIndex = 0;
            Cargarcombos();
            CargarGrid();
        }

        private void CargarGrid()
        {
            Verificador = string.Empty;
            grdPasivos0.DataSource = null;
            grdPasivos0.DataBind();
            try
            {
                DataTable dt = new DataTable();
                grdPasivos0.DataSource = dt;
                grdPasivos0.DataSource = GetList();
                grdPasivos0.DataBind();
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }


        private List<Pasivo> GetList()
        {
            try
            {
                List<Pasivo> List = new List<Pasivo>();
                objPasivo.centro_contable = DDLCentro_Contable.SelectedValue;
                objPasivo.ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                objPasivo.formato = DDLFormato.SelectedValue;
                CNPasivo.PasivoConsultaGrid(objPasivo, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void CargarGridEmpleados()
        {
            Verificador = string.Empty;
            grdEmpleados.DataSource = null;
            grdEmpleados.DataBind();
            try
            {
                DataTable dt = new DataTable();
                grdEmpleados.DataSource = dt;
                grdEmpleados.DataSource = GetListEmpleados();
                grdEmpleados.DataBind();
            }
            catch (Exception ex)
            {
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
                List<Empleado> ListEmpleados = new List<Empleado>();
                objEmpleado.Nombre = txtNombre.Text.ToUpper();
                CNEmpleado.ConsultarCatEmpleados(objEmpleado, ref ListEmpleados);
                return ListEmpleados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Cargarcombos()
        {
            Verificador = string.Empty;
            try
            {
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref DDLCentro_Contable, "p_usuario", "p_ejercicio", SesionUsu.Usu_Nombre, SesionUsu.Usu_Ejercicio);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref DDLCentro_Contable2, "p_usuario", "p_ejercicio", SesionUsu.Usu_Nombre, SesionUsu.Usu_Ejercicio);
                //CNComun.LlenaCombo("PKG_PRESUPUESTO.Obt_Combo_Proyecto", ref DDLProyecto, "p_ejercicio", SesionUsu.Usu_Ejercicio);
                //CNComun.LlenaCombo("PKG_PRESUPUESTO.Obt_Combo_Proyecto", ref DDLProyecto2, "p_ejercicio", SesionUsu.Usu_Ejercicio);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Empleados", ref DDLBeneficiario);
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Proveedores", ref DDLProveedor);
                //CNComun.LlenaCombo("PKG_PRESUPUESTO.Obt_Grid_Cat_TipoProy", ref DDLProyecto, "p_todos", "S");
                //CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref DDLCentro_Contable, "p_usuario", "p_ejercicio", "p_sistema", SesionUsu.Usu_Nombre, SesionUsu.Usu_Ejercicio, "CONCILIACION", ref ListCentroContable);
                //Session["CentrosContab"] = ListCentroContable;
                //CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Conciliacion", ref ddlTipo, "p_ejercicio", SesionUsu.Usu_Ejercicio, ref ListTipo);
                //CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Tipo_Conciliacion", ref ddlTipo2, "p_ejercicio", SesionUsu.Usu_Ejercicio, ref ListTipo);
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
            MultiView1.ActiveViewIndex = 1;
            grdPasivos.DataSource = null;
            grdPasivos.DataBind();
            Session["Pasivos"] = null;
            Session["Cedulas"] = null;
            Session["Polizas"] = null;
            SesionUsu.Editar = 0;
            DDLCentro_Contable2.SelectedValue = DDLCentro_Contable.SelectedValue;
            DDLCentro_Contable2_SelectedIndexChanged(null, null);
            DDLCentro_Contable2.Enabled = true;
            DDLFormato2.Enabled = true;
        }

        protected void DDLCentro_Contable2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            try
            {
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Cedulas", ref DDLCedula, "p_ejercicio", "p_centro_contable", "p_mes_anio", "p_clave_evento", SesionUsu.Usu_Ejercicio, DDLCentro_Contable2.SelectedValue, "1222", "97", ref ListCedulas);
                Session["Cedulas"] = ListCedulas;
                DDLCedula_SelectedIndexChanged(null, null);
                //DDLFormato2.SelectedIndex = 0;
                //DDLFormato2_SelectedIndexChanged(null, null);
                //CNComun.LlenaCombo("PKG_PRESUPUESTO.Obt_Combo_Fuente_F", ref DDLFuente2, "p_ejercicio", "p_dependencia", "p_evento", SesionUsu.Usu_Ejercicio, DDLCentro_Contable2.SelectedValue, "00");
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void linkBttnSalir_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void DDLFormato2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            rowBenef.Visible = false;
            rowProveedor.Visible = false;
            Session["Cuentas"] = null;



            try
            {
                if (DDLFormato2.SelectedValue == "2112" || DDLFormato2.SelectedValue == "2113")
                    rowProveedor.Visible = true;
                else
                    rowBenef.Visible = true;

                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Cuentas_Poliza", ref DDLCuenta, "p_id_poliza", "p_mayor", "p_centro_contable", "p_ejercicio", DDLPoliza.SelectedValue, DDLFormato2.SelectedValue, DDLCentro_Contable2.SelectedValue, SesionUsu.Usu_Ejercicio, ref ListCuentas);
                if (ListCuentas.Count > 0)
                    Session["Cuentas"] = ListCuentas;
                else
                    Session["Cuentas"] = null;

                DDLCuenta_SelectedIndexChanged(null, null);

                //if (Session["Cedulas"] == null)
                //{
                //    CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Polizas_Cedula", ref DDLPoliza, "p_id_cedula", "p_mayor", "0", DDLFormato2.SelectedValue, ref ListPolizas);
                //    if (ListPolizas.Count > 0)
                //    {
                //        Session["Polizas"] = ListPolizas;
                //        DDLPoliza_SelectedIndexChanged(null, null);
                //    }
                //}
                //else
                //{
                //    ListCedulas = (List<Comun>)Session["Cedulas"];
                //    if (ListCedulas.Count > 0)
                //    {
                //        CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Polizas_Cedula", ref DDLPoliza, "p_id_cedula", "p_mayor", ListCedulas[DDLCedula.SelectedIndex].EtiquetaDos, DDLFormato2.SelectedValue, ref ListPolizas);
                //        if (ListPolizas.Count > 0)
                //        {
                //            Session["Polizas"] = ListPolizas;
                //            DDLPoliza_SelectedIndexChanged(null, null);
                //        }
                //        else
                //        {
                //            Session["Polizas"] = null;
                //            CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Cuentas_Poliza", ref DDLCuenta, "p_id_poliza", "p_mayor", "p_centro_contable", "p_ejercicio", "0", DDLFormato2.SelectedValue, DDLCentro_Contable2.SelectedValue, SesionUsu.Usu_Ejercicio, ref ListCuentas);
                //            Session["Cuentas"] = null;
                //            DDLCuenta_SelectedIndexChanged(null, null);
                //        }
                //    }
                //    else
                //    {
                //        Session["Polizas"] = null;
                //        CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Cuentas_Poliza", ref DDLCuenta, "p_id_poliza", "p_mayor", "p_centro_contable", "p_ejercicio", "0", DDLFormato2.SelectedValue, DDLCentro_Contable2.SelectedValue, SesionUsu.Usu_Ejercicio, ref ListCuentas);
                //        Session["Cuentas"] = null;
                //        DDLCuenta_SelectedIndexChanged(null, null);
                //    }

                //}
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void linkBttnAgregarP_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            List<Pasivo> lstPasivos = new List<Pasivo>();

            try
            {
                objPasivo.centro_contable = DDLCentro_Contable2.SelectedValue;
                ListCedulas = (List<Comun>)Session["Cedulas"];
                ListPolizas = (List<Comun>)Session["Polizas"];
                if (Session["Pasivos"] != null)
                    lstPasivos = (List<Pasivo>)Session["Pasivos"];


                bool valido = ValidaFormato(lstPasivos);
                if (valido == true)
                {
                    if (ListCedulas.Count > 0 && ListPolizas.Count > 0)
                    {



                        objPasivo.ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                        objPasivo.id_cedula = Convert.ToInt32(ListCedulas[DDLCedula.SelectedIndex].EtiquetaDos);
                        if (DDLPoliza.SelectedValue == "0")
                            objPasivo.id_poliza = 0;
                        else
                            objPasivo.id_poliza = Convert.ToInt32(DDLPoliza.SelectedValue);
                        objPasivo.cedula = DDLCedula.SelectedValue;
                        if (DDLPoliza.SelectedValue == "0")
                            objPasivo.poliza = txtNumPoliza.Text.ToUpper();
                        else
                            objPasivo.poliza = ListPolizas[DDLPoliza.SelectedIndex].EtiquetaTres;

                        objPasivo.formato = DDLFormato2.SelectedValue;
                        objPasivo.id_cuenta = Convert.ToInt32(DDLCuenta.SelectedValue);
                        objPasivo.cuenta = DDLCuenta.SelectedItem.Text;
                        objPasivo.importe = Convert.ToDouble(txtImporte.Text);
                        objPasivo.id_fuente = Convert.ToInt32(DDLFuente2.SelectedValue);
                        objPasivo.fuente = DDLFuente2.SelectedItem.Text.Substring(0, 5);
                        objPasivo.id_proyecto = Convert.ToInt32(DDLProyecto2.SelectedValue);
                        objPasivo.proyecto = DDLProyecto2.SelectedItem.Text.Substring(0, 4);


                        //objBeneficiario = (Comun)Session["Beneficiario"];
                        //if (objBeneficiario != null)
                        //    objPasivo.beneficiario = objBeneficiario.Descripcion;
                        if (DDLFormato2.SelectedValue == "2111")
                        {
                            objPasivo.id_beneficiario = Convert.ToString(DDLBeneficiario.SelectedValue);
                            objPasivo.beneficiario = DDLBeneficiario.SelectedItem.Text; //.Substring(1, 20);
                        }
                        else
                        {
                            objPasivo.id_beneficiario = Convert.ToString(DDLProveedor.SelectedValue);
                            if (DDLProveedor.SelectedItem.Text.Length > 20)
                                objPasivo.beneficiario = DDLProveedor.SelectedItem.Text; //.Substring(1, 20);
                            else
                                objPasivo.beneficiario = DDLProveedor.SelectedItem.Text;

                        }

                        objPasivo.importe = Convert.ToDouble(txtImporte.Text);



                        lstPasivos.Add(objPasivo);
                        Session["Pasivos"] = lstPasivos;
                        CargarGridPasivos(lstPasivos);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'Debe seleccionar una cédula valida.');", true);
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'El FORMATO debe ser del mismo tipo, al agregado en la lista.');", true);

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected bool ValidaFormato(List<Pasivo> lstPasivos)
        {
            int count = 0;
            try
            {
                if (lstPasivos.Count > 0)
                {
                    count = (from item in lstPasivos where item.formato == DDLFormato2.SelectedValue select item).Count();
                    if (count > 0)
                        return true;
                    else
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void grdPasivos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Pasivo> lstPasivos = new List<Pasivo>();
            lstPasivos = (List<Pasivo>)Session["Pasivos"];
            try
            {
                int fila = e.RowIndex;
                lstPasivos.RemoveAt(fila);

                Session["Pasivos"] = lstPasivos;
                CargarGridPasivos(lstPasivos);
            }

            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        private void CargarGridPasivos(List<Pasivo> lstPasivos)
        {
            grdPasivos.DataSource = null;
            grdPasivos.DataBind();
            try
            {
                double TotalPagos;
                DataTable dt = new DataTable();
                grdPasivos.DataSource = lstPasivos;
                grdPasivos.DataBind();
                if (lstPasivos.Count() > 0)
                    TotalPagos = lstPasivos.Sum(item => Convert.ToDouble(item.importe));
                else
                    TotalPagos = 0;



            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        private void CargarGridInicialPasivos(List<Pasivo> lstPasivos)
        {
            grdPasivos.DataSource = null;
            grdPasivos.DataBind();
            try
            {
                DataTable dt = new DataTable();
                grdPasivos.DataSource = lstPasivos;
                grdPasivos.DataBind();
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        protected void DDLPoliza_SelectedIndexChanged(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            try
            {
                if (DDLPoliza.SelectedValue == "0")
                    rowNumPol.Visible = true;
                else
                    rowNumPol.Visible = false;

                DDLFormato2_SelectedIndexChanged(null, null);
                //if (DDLPoliza.Items.Count > 0)
                //{
                //    CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Cuentas_Poliza", ref DDLCuenta, "p_id_poliza", "p_mayor", "p_centro_contable", "p_ejercicio", DDLPoliza.SelectedValue, DDLFormato2.SelectedValue, DDLCentro_Contable2.SelectedValue, SesionUsu.Usu_Ejercicio, ref ListCuentas);
                //    Session["Cuentas"] = ListCuentas;
                //    DDLCuenta_SelectedIndexChanged(null, null);
                //}
                //else
                //{
                //    CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Cuentas_Poliza", ref DDLCuenta, "p_id_poliza", "p_mayor", "p_centro_contable", "p_ejercicio", DDLPoliza.SelectedValue, DDLFormato2.SelectedValue, DDLCentro_Contable2.SelectedValue, SesionUsu.Usu_Ejercicio, ref ListCuentas);
                //    txtImporte.Text = string.Empty;
                //    Session["Cuentas"] = null;
                //    DDLCuenta_SelectedIndexChanged(null, null);
                //}

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void DDLCedula_SelectedIndexChanged(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            try
            {
                ListCedulas = (List<Comun>)Session["Cedulas"];



                if (ListCedulas.Count > 0)
                {
                    CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Polizas_Cedula", ref DDLPoliza, "p_id_cedula", "p_mayor", ListCedulas[DDLCedula.SelectedIndex].EtiquetaDos, "0", ref ListPolizas);
                    CNComun.LlenaCombo("PKG_CONTABILIDAD.Obt_Combo_Fuente_Cedula", ref DDLFuente2, "p_id_cedula", "p_dependencia", "p_ejercicio", ListCedulas[DDLCedula.SelectedIndex].EtiquetaDos, DDLCentro_Contable2.SelectedValue, SesionUsu.Usu_Ejercicio);
                    CNComun.LlenaCombo("PKG_CONTABILIDAD.Obt_Combo_Proyecto_Cedula", ref DDLProyecto2, "p_id_cedula", "p_dependencia", "p_ejercicio", ListCedulas[DDLCedula.SelectedIndex].EtiquetaDos, DDLCentro_Contable2.SelectedValue, SesionUsu.Usu_Ejercicio);
                }
                else
                {
                    CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Polizas_Cedula", ref DDLPoliza, "p_id_cedula", "p_mayor", "-1", "0", ref ListPolizas);
                    CNComun.LlenaCombo("PKG_CONTABILIDAD.Obt_Combo_Fuente_Cedula", ref DDLFuente2, "p_id_cedula", "p_dependencia", "p_ejercicio", "0", DDLCentro_Contable2.SelectedValue, SesionUsu.Usu_Ejercicio);
                    CNComun.LlenaCombo("PKG_CONTABILIDAD.Obt_Combo_Proyecto_Cedula", ref DDLFuente2, "p_id_cedula", "p_dependencia", "p_ejercicio", "0", DDLCentro_Contable2.SelectedValue, SesionUsu.Usu_Ejercicio);
                }

                Session["Polizas"] = ListPolizas;
                DDLPoliza_SelectedIndexChanged(null, null);
                //DDLFormato2_SelectedIndexChanged(null, null);

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void DDLCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            try
            {

                if (Session["Cuentas"] == null)
                    txtImporte.Text = "0";
                else
                {
                    ListCuentas = (List<Comun>)Session["Cuentas"];
                    if (ListCuentas.Count > 0)
                        txtImporte.Text = ListCuentas[DDLCuenta.SelectedIndex].EtiquetaDos;
                }



            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void linkBttnBuscaNombreEmp_Click(object sender, EventArgs e)
        {
            CargarGridEmpleados();
        }


        protected void linkBttnBuscaEmp_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupEmpleados", "$('#modalEmpleados').modal('show')", true);
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void linkBttnGuardar_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            List<Pasivo> lstPasivos = new List<Pasivo>();
            Pasivo objPasivo = new Pasivo();
            objPasivo.centro_contable = DDLCentro_Contable2.SelectedValue;
            objPasivo.formato = DDLFormato2.SelectedValue;
            objPasivo.ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
            try
            {
                if (Session["Pasivos"] != null)
                {
                    lstPasivos = (List<Pasivo>)Session["Pasivos"];
                    if (SesionUsu.Editar == 0)
                        CNPasivo.PasivoInsertar(lstPasivos, ref Verificador);
                    else
                        CNPasivo.PasivoEditar(objPasivo, lstPasivos, ref Verificador);


                    if (Verificador == "0")
                    {
                        MultiView1.ActiveViewIndex = 0;
                        CargarGrid();
                        //CargarGridInicialPasivos();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(1, 'Los datos han sido guardados correctamente.');", true);
                    }
                    else
                    {
                        CNComun.VerificaTextoMensajeError(ref Verificador);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, 'Debes agregar al menos un elemento.');", true);

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

        protected void grdPasivos0_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Pasivo> lstPasivos = new List<Pasivo>();
            objPasivo.centro_contable = Convert.ToString(grdPasivos0.SelectedRow.Cells[7].Text);
            objPasivo.formato = Convert.ToString(grdPasivos0.SelectedRow.Cells[1].Text);
            objPasivo.ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
            SesionUsu.Editar = 1;
            try
            {
                //linkBttnAgregar_Click(null, null);
                MultiView1.ActiveViewIndex = 1;
                grdPasivos.DataSource = null;
                grdPasivos.DataBind();
                Session["Pasivos"] = null;
                Session["Cedulas"] = null;
                Session["Polizas"] = null;

                DDLCentro_Contable2.SelectedValue = Convert.ToString(grdPasivos0.SelectedRow.Cells[7].Text);
                DDLCentro_Contable2_SelectedIndexChanged(null, null);
                DDLCentro_Contable2.Enabled = false;
                DDLFormato2.SelectedValue = Convert.ToString(grdPasivos0.SelectedRow.Cells[1].Text);
                DDLFormato2.Enabled = false;
                CNPasivo.ListPasivos(objPasivo, ref lstPasivos);
                Session["Pasivos"] = lstPasivos;
                CargarGridPasivos(lstPasivos);
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void linkBttnReporte_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grdPasivos0.SelectedIndex = row.RowIndex;
            string cc = grdPasivos0.SelectedRow.Cells[7].Text;
            string mayor = grdPasivos0.SelectedRow.Cells[1].Text;
            string ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=RP_Pasivos&centro_contable=" + cc + "&ejercicio=" + SesionUsu.Usu_Ejercicio + "&mayor=" + mayor;
            string _open = "window.open('" + ruta + "', '_newtab');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

        }
        protected void linkBttnExcel_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grdPasivos0.SelectedIndex = row.RowIndex;
            string cc = grdPasivos0.SelectedRow.Cells[7].Text;
            string mayor = grdPasivos0.SelectedRow.Cells[1].Text;
            string ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=RP_Pasivosxls&centro_contable=" + cc + "&ejercicio=" + SesionUsu.Usu_Ejercicio + "&mayor=" + mayor;
            string _open = "window.open('" + ruta + "', '_newtab');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

        }

        protected void linkBttnEliminar_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grdPasivos0.SelectedIndex = row.RowIndex;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupBorrar", "$('#modalBorrar').modal('show')", true);
        }
        protected void linkBttnEliminarPasivo_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            try
            {
                objPasivo.ejercicio = Convert.ToInt32(SesionUsu.Usu_Ejercicio);
                objPasivo.centro_contable = grdPasivos0.SelectedRow.Cells[7].Text;
                objPasivo.formato = grdPasivos0.SelectedRow.Cells[1].Text;
                CNPasivo.PasivoEliminar(objPasivo, ref Verificador);
                if (Verificador == "0")
                {
                    CargarGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupBorrar", "$('#modalBorrar').modal('hide')", true);
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

        protected void linkBttnGralPDF_Click(object sender, EventArgs e)
        {
            string cc = DDLCentro_Contable.SelectedValue;
            string mayor =DDLFormato.SelectedValue;
            string ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=RP_PasivosGral&centro_contable=" + cc + "&ejercicio=" + SesionUsu.Usu_Ejercicio;
            string _open = "window.open('" + ruta + "', '_newtab');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
        }

        protected void linkBttnGralExcel_Click(object sender, EventArgs e)
        {
            string cc = DDLCentro_Contable.SelectedValue;
            string mayor = DDLFormato.SelectedValue;
            string ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=RP_PasivosGralxls&centro_contable=" + cc + "&ejercicio=" + SesionUsu.Usu_Ejercicio;
            string _open = "window.open('" + ruta + "', '_newtab');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

        }
    }
}
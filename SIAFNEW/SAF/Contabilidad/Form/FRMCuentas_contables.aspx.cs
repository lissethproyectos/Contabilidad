using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocio;

namespace SAF.Rep
{
    public partial class FRMCuentas_contables : System.Web.UI.Page
    {

        #region <Variables>
        //Int32[] Celdas = new Int32[] { 0 };
        string Verificador = string.Empty;
        string cta_mayor = string.Empty;
        CN_Usuario CNUsuario = new CN_Usuario();
        Usuario Usuario = new Usuario();
        Sesion SesionUsu = new Sesion();
        CN_Comun CNComun = new CN_Comun();
        CN_Cuentas_contables CNcuentas_contables = new CN_Cuentas_contables();
        cuentas_contables Objcuentas_contables = new cuentas_contables();
        private static List<Comun> ListConceptos = new List<Comun>();
        private static List<Comun> ListBienes = new List<Comun>();
        private static List<Comun> Listcodigo = new List<Comun>(); //En tu declaración de variables
        int guar_continue;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            SesionUsu = (Sesion)Session["Usuario"];
            if (!IsPostBack)
            {
                //SesionUsu.Editar = -1;
                inicializar();
                cargarcombos();
                DDLCentro_Contable_SelectedIndexChanged1(null, null);
                ScriptManager.RegisterStartupScript(this, GetType(), "Grid", "CuentasContablesInicio();", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "Grid", "CuentasContables();", true);


            ScriptManager.RegisterStartupScript(this, GetType(), "GridPolizas", "Polizas();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "GridCOG", "CatCOG();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "GridCatalogos", "Catalogos();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "GridCatalogos2", "Catalogos2();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "Bienes", "FiltBienes();", true);


        }
        private void CargarGrid()
        {
            Int32[] Celdas;
            try
            {
                DataTable dt = new DataTable();

                grdcuentas_contables.DataSource = dt;
                grdcuentas_contables.DataSource = GetList();
                grdcuentas_contables.DataBind();
                if (SesionUsu.Usu_TipoUsu == "2" || SesionUsu.Usu_TipoUsu == "3")
                {
                    Celdas = new Int32[] { 0 };
                }
                else
                {
                    Celdas = new Int32[] { 6, 7, 8 };
                }


                for (int i = 0; i < Celdas.Length; i++)
                {

                    grdcuentas_contables.HeaderRow.Cells[Convert.ToInt32(Celdas.GetValue(i))].Visible = false;
                    Button bttnAgregarCtaContab = grdcuentas_contables.HeaderRow.Cells[9].FindControl("bttnAgregarCtaContab") as Button;
                    if (SesionUsu.Usu_TipoUsu == "3" || SesionUsu.Usu_TipoUsu == "2")
                        bttnAgregarCtaContab.Visible = true;
                    else
                        bttnAgregarCtaContab.Visible = false;

                    foreach (GridViewRow row in grdcuentas_contables.Rows)
                    {
                        row.Cells[Convert.ToInt32(Celdas.GetValue(i))].Visible = false;
                    }
                    //grdcuentas_contables.FooterRow.Cells[Convert.ToInt32(Celdas.GetValue(i))].Visible = false;
                }



            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        private void CargarGridCOG()
        {
            try
            {
                DataTable dt = new DataTable();
                grdCatCOG.DataSource = dt;
                grdCatCOG.DataSource = GetListCOG();
                grdCatCOG.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        private void CargarGridCatalogos()
        {
            try
            {
                DataTable dt = new DataTable();
                grdCatalogos.DataSource = dt;
                grdCatalogos.DataSource = GetListCatalogos();
                grdCatalogos.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void CargarGridCatalogos2()
        {
            try
            {
                DataTable dt = new DataTable();
                grdCatalogos2.DataSource = dt;
                grdCatalogos2.DataSource = GetListCatalogos();
                grdCatalogos2.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private List<cuentas_contables> GetList()
        {
            try
            {
                List<cuentas_contables> List = new List<cuentas_contables>();

                Objcuentas_contables.ejercicio = SesionUsu.Usu_Ejercicio;
                Objcuentas_contables.centro_contable = DDLCentro_Contable.SelectedValue;
                Objcuentas_contables.cuenta_mayor = ddlCuenta_Mayor.SelectedValue;
                //CNcuentas_contables.ConsultarCuentas_contables(ref Objcuentas_contables,TXTbuscar.Text, ref List);
                CNcuentas_contables.ConsultarCuentas_contables(ref Objcuentas_contables, "", ref List);

                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private List<cuentas_contables> GetListCOG()
        {
            try
            {
                List<cuentas_contables> List = new List<cuentas_contables>();
                CNcuentas_contables.ConsultarCatCOG(ref List);

                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private List<Comun> GetListCatalogos()
        {
            try
            {
                List<Comun> List = new List<Comun>();
                Comun objCatalogo = new Comun();
                objCatalogo.Etiqueta = ddlTipoCat.SelectedValue;
                CNcuentas_contables.ConsultarCatalogos(objCatalogo, ref List);

                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void inicializar()
        {
            BTN_continuar.Visible = false;
            DDLCentro_Contable.Enabled = true;
            ddlCuenta_Mayor.Enabled = true;
            lblError.Text = string.Empty;
            SesionUsu.Editar = -1;
            MultiViewcuentas_contables.ActiveViewIndex = 1;
            Label15.Visible = false;
            DDLSubdependencia.Visible = false;
            CargarGrid();
            CargarGridCOG();
            ddlTipoCat_SelectedIndexChanged(null, null);
            if (SesionUsu.Usu_TipoUsu != "3")
                ScriptManager.RegisterStartupScript(this, GetType(), "PestaniaActualizar", "OcultarPestania();", true);



        }

        protected void cargarcombos()
        {
            CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref DDLCentro_Contable, "P_USUARIO", "p_ejercicio", SesionUsu.Usu_Nombre, SesionUsu.Usu_Ejercicio);
            //CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Centros_Contables", ref DDLCentro_Contable);
            CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Cuentas_Mayor", ref ddlCuenta_Mayor, "P_Ejercicio", SesionUsu.Usu_Ejercicio, ref Listcodigo);
            CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Cuentas_Mayor_COG", ref ddlMayor);



        }

        protected void ddlCuentas_Contables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMayor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "cboCatCog", "cboCatCog();", true);

        }



        protected void DDLCentro_Contable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SesionUsu.Editar == -1)
            {
                CargarGrid();
            }


            txtcuenta_contable.Text = Listcodigo[ddlCuenta_Mayor.SelectedIndex].EtiquetaDos + ".00000.00000.00000";
            txtdescripcion.Text = Listcodigo[ddlCuenta_Mayor.SelectedIndex].EtiquetaTres;
        }

        //protected void grdcuentas_contables_SelectedIndexChanged(object sender, EventArgs e)
        //{            
        //    try
        //    {
        //        lblError.Text = string.Empty;
        //        MultiViewcuentas_contables.ActiveViewIndex = 0;
        //        SesionUsu.Editar = 1;
        //        int v = grdcuentas_contables.SelectedIndex;

        //        Objcuentas_contables.id = grdcuentas_contables.Rows[v].Cells[0].Text;
        //        CNcuentas_contables.Consultarcuenta_contable(ref Objcuentas_contables, ref Verificador);

        //        if (Verificador == "0")
        //        {

        //            DDLCentro_Contable.SelectedValue = Objcuentas_contables.centro_contable;
        //            ddlCuenta_Mayor.SelectedValue = Objcuentas_contables.cuenta_mayor;
        //            txtcuenta_contable.Text  = Objcuentas_contables.cuenta_contable;
        //            txtdescripcion.Text = Objcuentas_contables.descripcion;
        //            txttipo.Text = Objcuentas_contables.tipo;
        //            ddlclasificacion.SelectedValue = Objcuentas_contables.clasificacion;
        //            ddlstatus.SelectedValue = Objcuentas_contables.status;
        //            ddlclasificacion.SelectedValue = Objcuentas_contables.clasificacion;
        //            ddlnivel.SelectedValue = Objcuentas_contables.nivel;                                     
        //           //lbtnagregar_Click(null, null);
        //            habil_cuenta();
        //            DDLCentro_Contable.Enabled = false;
        //            ddlCuenta_Mayor.Enabled = false;
        //            txttipo.Enabled = false;
        //            ddlclasificacion.Enabled = false;



        //        }
        //        else
        //        {
        //            lblError.Text = Verificador;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = ex.Message;
        //    }
        //}

        protected void BTN_Cancelar_Click(object sender, EventArgs e)
        {
            inicializar();

        }

        protected void BTN_Guardar_Click(object sender, EventArgs e)
        {
            guar_continue = 0;
            GuardarDatos();

            Label15.Visible = false;
            DDLSubdependencia.Visible = false;
            DDLCentro_Contable.Enabled = true;
            ddlCuenta_Mayor.Enabled = true;
        }
        private void GuardarDatos()
        {
            txtcuenta_contable.Text = txt1.Text + "." + txt2.Text + "." + txt3.Text + "." + txt4.Text;
            lblError.Text = string.Empty;
            // Objcuentas_contables.id  = grdcuentas_contables.Rows[grdcuentas_contables.SelectedIndex].Cells[0].Text;
            Objcuentas_contables = new cuentas_contables();
            Objcuentas_contables.ejercicio = SesionUsu.Usu_Ejercicio;
            Objcuentas_contables.centro_contable = DDLCentro_Contable.SelectedValue;
            Objcuentas_contables.cuenta_contable = txtcuenta_contable.Text;
            cta_mayor = txt1.Text;
            //if (ddlnivel.SelectedValue == "4" && (cta_mayor == "5518" || cta_mayor == "5515" || cta_mayor == "1263" || cta_mayor == "1241" || cta_mayor == "1242" || cta_mayor == "1243" || cta_mayor == "1244" || cta_mayor == "1245" || cta_mayor == "1246" || cta_mayor == "1247" || cta_mayor == "1248" || cta_mayor == "1251" || cta_mayor == "1252" || cta_mayor == "1253" || cta_mayor == "1254" || cta_mayor == "1293"))
            //    Objcuentas_contables.descripcion = ddlDescripcion.Items.ToString();
            //else

            Objcuentas_contables.descripcion = txtdescripcion.Text;

            Objcuentas_contables.tipo = txttipo.SelectedValue;
            Objcuentas_contables.clasificacion = ddlclasificacion.SelectedValue;
            Objcuentas_contables.nivel = ddlnivel.SelectedValue;
            Objcuentas_contables.status = ddlstatus.SelectedValue;
            Objcuentas_contables.usuario = SesionUsu.Usu_Nombre;
            Objcuentas_contables.cuenta_mayor = ddlCuenta_Mayor.SelectedValue.ToString();


            try
            {

                Verificador = string.Empty;
                if (SesionUsu.Editar == 0)
                {
                    CNcuentas_contables.insertar_cuenta_contable(ref Objcuentas_contables, ref Verificador);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "confirmacion();", true);


                }
                else
                {
                    Objcuentas_contables.id = grdcuentas_contables.Rows[grdcuentas_contables.SelectedIndex].Cells[0].Text;
                    CNcuentas_contables.Editar_cuentas_contables(ref Objcuentas_contables, SesionUsu.Usu_Nombre, SesionUsu.Correo_UNACH, ref Verificador);
                }
                if (Verificador != "0")
                {
                    lblError.Text = Verificador;

                }
                else
                {
                    if (guar_continue == 0)
                    {
                        inicializar();
                        CargarGrid();
                    }
                    else
                    {
                        lblError.Text = "Se agrego correctamente el registro";

                    }

                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void BTNbuscar_Click(object sender, ImageClickEventArgs e)
        {
            lblError.Text = string.Empty;
            CargarGrid();
        }

        protected void BTNver_reporte_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "VerCatalogo_Cuentas('RP-003'," + SesionUsu.Usu_Ejercicio + ",'" + Convert.ToString(DDLCentro_Contable.SelectedValue) + "','" + ddlCuenta_Mayor.SelectedValue + "');", true);


        }

        protected void DDLCentro_Contable_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (SesionUsu.Editar == -1)
            {
                CargarGrid();
            }
        }


        protected void index_linbtn(object sender)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grdcuentas_contables.SelectedIndex = row.RowIndex;
            Objcuentas_contables.id = grdcuentas_contables.SelectedRow.Cells[0].Text;

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = string.Empty;
                index_linbtn(sender);
                Objcuentas_contables.id = grdcuentas_contables.SelectedRow.Cells[0].Text;
                Verificador = string.Empty;
                CNcuentas_contables.Eliminar_cuenta_contable(ref Objcuentas_contables, ref Verificador);

                if (Verificador != "0")
                {
                    lblError.Text = Verificador;

                }
                else
                {
                    inicializar();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "confirmacion('" + Verificador + "');", true);
                    CargarGrid();

                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }




        }

        protected void lbtnagregar_Click(object sender, EventArgs e)
        {

            DDLCentro_Contable.Enabled = false;
            ddlCuenta_Mayor.Enabled = false;
            Label15.Visible = false;
            DDLSubdependencia.Visible = false;
            //txtdescripcion.Visible = true;
            linkBttnBuscarBien.Visible = false;
            ddlDescripcion.Visible = false;
            ddlDescripcion.Items.Clear();
            this.ddlDescripcion.Items.Insert(0, new ListItem("---SELECCIONAR---", "0"));


            index_linbtn(sender);
            SesionUsu.Editar = 0;

            MultiViewcuentas_contables.ActiveViewIndex = 0;
            int nivel_cuenta = Convert.ToInt32(grdcuentas_contables.SelectedRow.Cells[5].Text);
            if (nivel_cuenta < 4)
            {
                nivel_cuenta = nivel_cuenta + 1;
                txt4.Enabled = true;
                txtdescripcion.Text = string.Empty;
            }

            txtcuenta_contable.Text = grdcuentas_contables.SelectedRow.Cells[2].Text;
            ddlnivel.SelectedValue = Convert.ToString(nivel_cuenta);
            if (nivel_cuenta == 2)
            {
                txttipo.SelectedValue = "AC";
                ddlclasificacion.Enabled = true;
                habil_cuenta();
                txt2.Enabled = true;
            }
            if (nivel_cuenta == 3)
            {
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Sub_Contables", ref DDLSubdependencia, "p_centro_contable", "p_ejercicio", DDLCentro_Contable.SelectedValue, SesionUsu.Usu_Ejercicio);
                string cadena = txtcuenta_contable.Text;
                txtcuenta_contable.Text = cadena.Substring(0, 11) + DDLSubdependencia.SelectedValue + ".00000";
                txtdescripcion.Text = DDLSubdependencia.SelectedItem.Text.Substring(8);
                Label15.Visible = true;
                DDLSubdependencia.Visible = true;
                ddlclasificacion.Enabled = false;
                txttipo.SelectedValue = "AC";
                ddlclasificacion.SelectedValue = "ESP";
                habil_cuenta();
                txt3.Enabled = true;

            }
            if (nivel_cuenta == 4)
            {
                ddlclasificacion.Enabled = false;
                txttipo.SelectedValue = "AF";
                ddlclasificacion.SelectedValue = "ESP";
                habil_cuenta();
                txt4.Enabled = true;
                txtdescripcion.Text = string.Empty;
                BTN_continuar.Visible = true;
                //string mayor = grdcuentas_contables.SelectedRow.Cells[2].Text.Substring(0, 4);
                cta_mayor = grdcuentas_contables.SelectedRow.Cells[2].Text.Substring(0, 4);
                if (cta_mayor == "5518" || cta_mayor == "5515" || cta_mayor == "1263" || cta_mayor == "1241" || cta_mayor == "1242" || cta_mayor == "1243" ||
                    cta_mayor == "1244" || cta_mayor == "1245" || cta_mayor == "1246" || cta_mayor == "1247" || cta_mayor == "1248" || cta_mayor == "1251" ||
                    cta_mayor == "1252" || cta_mayor == "1253" || cta_mayor == "1254" || cta_mayor == "1293")
                {
                    //txtdescripcion.Visible = false;
                    linkBttnBuscarBien.Visible = true;
                    ddlDescripcion.Visible = true;
                    ListBienes.Clear();
                    CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Bienes", ref ddlDescripcion, "p_cta_mayor", "p_nivel3", "p_edicion", cta_mayor, null, "0", ref ListBienes);
                    Session["Bienes"] = ListBienes;

                }
            }




        }
        protected void habil_cuenta()
        {

            txt1.Text = txtcuenta_contable.Text.Substring(0, 4);
            txt2.Text = txtcuenta_contable.Text.Substring(5, 5);
            txt3.Text = txtcuenta_contable.Text.Substring(11, 5);
            txt4.Text = txtcuenta_contable.Text.Substring(17, 5);
            txt1.Enabled = false;
            txt2.Enabled = false;
            txt3.Enabled = false;
            txt4.Enabled = false;
        }

        protected void grdcuentas_contables_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdcuentas_contables.PageIndex = 0;
            grdcuentas_contables.PageIndex = e.NewPageIndex;
            CargarGrid();
        }

        protected void DDLSubdependencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadena = txtcuenta_contable.Text;
            txtcuenta_contable.Text = cadena.Substring(0, 11) + DDLSubdependencia.SelectedValue + ".00000";

            txtdescripcion.Text = DDLSubdependencia.SelectedItem.Text.Substring(8);
            habil_cuenta();
            txt3.Enabled = true;
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txt1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txt2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txt4_TextChanged(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
        }

        protected void BTN_continuar_Click(object sender, EventArgs e)
        {

            guar_continue = 1;
            GuardarDatos();
            txt4.Text = "00000";
            txtdescripcion.Text = string.Empty;
            ddlDescripcion.Items.Clear();
            this.ddlDescripcion.Items.Insert(0, new ListItem("---SELECCIONAR---", "0"));
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                MultiViewcuentas_contables.ActiveViewIndex = 2;
                ddlCuenta_Mayor.Enabled = false;
                index_linbtn(sender);
                CargarGrid_polizas();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        private void CargarGrid_polizas()
        {
            try
            {
                DataTable dt = new DataTable();

                grvPolizas.DataSource = dt;
                grvPolizas.DataSource = GetList_poliza();
                grvPolizas.DataBind();

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        private List<cuentas_contables> GetList_poliza()
        {
            try
            {
                List<cuentas_contables> List = new List<cuentas_contables>();

                Objcuentas_contables.ejercicio = SesionUsu.Usu_Ejercicio;
                Objcuentas_contables.centro_contable = DDLCentro_Contable.SelectedValue;
                Objcuentas_contables.tipo = "T";
                Objcuentas_contables.cuenta_contable = grdcuentas_contables.SelectedRow.Cells[2].Text;
                Objcuentas_contables.nivel = grdcuentas_contables.SelectedRow.Cells[5].Text;
                Objcuentas_contables.buscar = "";/*txtBuscar0.Text;*/
                CNcuentas_contables.PolizaConsultaGrid(ref Objcuentas_contables, ref List);

                return List;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void txtBuscar0_TextChanged(object sender, EventArgs e)
        {

        }

        protected void imgbtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CargarGrid_polizas();
        }

        protected void linkBttnImprimir_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grvPolizas.SelectedIndex = row.RowIndex;
            string ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=RP-005&Ejercicio=" + SesionUsu.Usu_Ejercicio + "&id=" + grvPolizas.SelectedRow.Cells[0].Text;
            string _open = "window.open('" + ruta + "', '_newtab');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

        }

        protected void DDLCentro_Contable0_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrid_polizas();
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            MultiViewcuentas_contables.ActiveViewIndex = 1;
            ddlCuenta_Mayor.Enabled = true;
        }

        protected void grvPolizas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvPolizas.PageIndex = 0;
            grvPolizas.PageIndex = e.NewPageIndex;
            CargarGrid_polizas();
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grdcuentas_contables.SelectedIndex = row.RowIndex;
            ddlDescripcion.Items.Clear();
            linkBttnBuscarBien.Visible = false;
            this.ddlDescripcion.Items.Insert(0, new ListItem("---SELECCIONAR---", "0"));

            try
            {
                lblError.Text = string.Empty;
                MultiViewcuentas_contables.ActiveViewIndex = 0;
                SesionUsu.Editar = 1;
                int v = grdcuentas_contables.SelectedIndex;

                Objcuentas_contables.id = grdcuentas_contables.Rows[v].Cells[0].Text;
                CNcuentas_contables.Consultarcuenta_contable(ref Objcuentas_contables, ref Verificador);

                if (Verificador == "0")
                {

                    DDLCentro_Contable.SelectedValue = Objcuentas_contables.centro_contable;
                    ddlCuenta_Mayor.SelectedValue = Objcuentas_contables.cuenta_mayor;
                    txtcuenta_contable.Text = Objcuentas_contables.cuenta_contable;
                    cta_mayor = Objcuentas_contables.cuenta_contable.Substring(0, 4); //Objcuentas_contables.cuenta_mayor;
                    //if(Objcuentas_contables.nivel=="4")


                    txttipo.Text = Objcuentas_contables.tipo;
                    ddlclasificacion.SelectedValue = Objcuentas_contables.clasificacion;
                    ddlstatus.SelectedValue = Objcuentas_contables.status;
                    ddlclasificacion.SelectedValue = Objcuentas_contables.clasificacion;
                    ddlnivel.SelectedValue = Objcuentas_contables.nivel;
                    //lbtnagregar_Click(null, null);
                    habil_cuenta();
                    if (Objcuentas_contables.nivel == "4" && (cta_mayor == "5518" || cta_mayor == "5515" || cta_mayor == "1263" || cta_mayor == "1241" || cta_mayor == "1242" || cta_mayor == "1243" || cta_mayor == "1244" || cta_mayor == "1245" || cta_mayor == "1246" || cta_mayor == "1247" || cta_mayor == "1248" || cta_mayor == "1251" || cta_mayor == "1252" || cta_mayor == "1253" || cta_mayor == "1254" || cta_mayor == "1293"))
                    {
                        linkBttnBuscarBien.Visible = true;
                        ddlDescripcion.Visible = true;
                        ListBienes.Clear();
                        CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Bienes", ref ddlDescripcion, "p_cta_mayor", "p_nivel3", "p_edicion", cta_mayor, txtcuenta_contable.Text.Substring(17, 5), Convert.ToString(SesionUsu.Editar), ref ListBienes);
                        Session["Bienes"] = ListBienes;
                        //ddlDescripcion.SelectedValue = txtcuenta_contable.Text.Substring(17, 5);
                    }


                    txtdescripcion.Text = Objcuentas_contables.descripcion;


                    DDLCentro_Contable.Enabled = false;
                    ddlCuenta_Mayor.Enabled = false;
                    txttipo.Enabled = false;
                    ddlclasificacion.Enabled = false;
                    if (grdcuentas_contables.SelectedRow.Cells[5].Text == "3")
                        DDLSubdependencia.Visible = true;

                }
                else
                {
                    lblError.Text = Verificador;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
        {
            lblError.Text = string.Empty;
            MultiViewcuentas_contables.ActiveViewIndex = 0;
            txtcuenta_contable.Text = string.Empty;
            txtdescripcion.Text = string.Empty;
            //cargarcombos();

            SesionUsu.Editar = 0;
            //txtcuenta_contable.Visible = true;
            ddlclasificacion.Enabled = false;
            txttipo.SelectedValue = "AC";
            ddlclasificacion.SelectedValue = "ESP";
            ddlnivel.SelectedValue = "1";
            txtcuenta_contable.Text = Listcodigo[ddlCuenta_Mayor.SelectedIndex].EtiquetaDos + ".00000.00000.00000";
            txtdescripcion.Text = Listcodigo[ddlCuenta_Mayor.SelectedIndex].EtiquetaTres;
            habil_cuenta();
            txt1.Enabled = true;
        }

        protected void bttnAgregarCtaContab_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            MultiViewcuentas_contables.ActiveViewIndex = 0;
            txtcuenta_contable.Text = string.Empty;
            txtdescripcion.Text = string.Empty;
            //cargarcombos();

            SesionUsu.Editar = 0;
            //txtcuenta_contable.Visible = true;
            ddlclasificacion.Enabled = false;
            txttipo.SelectedValue = "AC";
            ddlclasificacion.SelectedValue = "ESP";
            ddlnivel.SelectedValue = "1";
            txtcuenta_contable.Text = Listcodigo[ddlCuenta_Mayor.SelectedIndex].EtiquetaDos + ".00000.00000.00000";
            txtdescripcion.Text = Listcodigo[ddlCuenta_Mayor.SelectedIndex].EtiquetaTres;
            habil_cuenta();
            txt1.Enabled = true;
            ddlDescripcion.Visible = false;
            ddlDescripcion.Items.Clear();
            this.ddlDescripcion.Items.Insert(0, new ListItem("---SELECCIONAR---", "0"));
            //CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Bienes", ref ddlDescripcion, "p_cta_mayor", "p_nivel3", ddlCuenta_Mayor.SelectedValue, null);


        }

        protected void linkBttnCat_Click(object sender, EventArgs e)
        {
            try
            {
                CargarGridCOG();
                ScriptManager.RegisterStartupScript(this, GetType(), "GridCOG", "CatCOG();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCOG", "$('#modalCOG').modal('show')", true);
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

            }
        }

        protected void bttnAgregar_Click(object sender, EventArgs e)
        {
            SesionUsu.Editar = 0;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCOG", "$('#modalCOG').modal('show')", true);


        }
        protected void linkBttnActualizar_Click(object sender, EventArgs e)
        {
            cuentas_contables objCta = new cuentas_contables();
            int Total = 0;
            Verificador = string.Empty;
            lblMsj2.Text = string.Empty;
            try
            {
                objCta.ejercicio = SesionUsu.Usu_Ejercicio;
                CNcuentas_contables.CuentasContables_ActDesc(objCta, ref Total, ref Verificador);
                if (Verificador == "0")
                {
                    lblMsj2.Text = "Se actualizaron " + Total + " registros.";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(1, 'Las cuentas fueron actualizadas correctamente.');", true);
                }
                else
                {
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    lblMsj2.Text = Verificador;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                }
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                lblMsj2.Text = Verificador;
            }
        }

        protected void grdCatCOG_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdCatCOG.EditIndex = e.NewEditIndex;
            CargarGridCOG();
        }

        protected void linkBttnEditarCOG_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grdCatCOG.SelectedIndex = row.RowIndex;
            Verificador = string.Empty;

            cuentas_contables objCta = new cuentas_contables();
            objCta.cuenta_mayor = Convert.ToString(grdCatCOG.SelectedRow.Cells[0].Text);
            objCta.natura = Convert.ToString(grdCatCOG.SelectedRow.Cells[1].Text);
            CNcuentas_contables.ObtCatCOG(ref objCta, ref Verificador);
            if (Verificador == "0")
            {
                txtMayor.Text = Convert.ToString(grdCatCOG.SelectedRow.Cells[0].Text);
                txtCOG.Text = Convert.ToString(grdCatCOG.SelectedRow.Cells[1].Text);

                txtMayor.Enabled = false;
                txtCOG.Enabled = false;
                txtDescripcionCOG.Text = objCta.descripcion;
                ddlStatusCOG.SelectedValue = objCta.status;
            }
        }

        //protected void grdCatCOG_SelectedIndexChanged(object sender, GridViewEditEventArgs e)
        //{
        //    grdCatCOG.EditIndex = e.NewEditIndex;
        //    CargarGridCOG();
        //}

        protected void grdCatCOG_SelectedIndexChanged(object sender, EventArgs e)
        {

            Verificador = string.Empty;
            txtMayor.Enabled = true;
            txtCOG.Enabled = true;
            cuentas_contables objCta = new cuentas_contables();
            objCta.cuenta_mayor = Convert.ToString(grdCatCOG.SelectedRow.Cells[0].Text);
            objCta.natura = Convert.ToString(grdCatCOG.SelectedRow.Cells[1].Text);
            CNcuentas_contables.ObtCatCOG(ref objCta, ref Verificador);
            if (Verificador == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCOG", "$('#modalCOG').modal('show')", true);
                txtMayor.Text = Convert.ToString(grdCatCOG.SelectedRow.Cells[0].Text);
                txtCOG.Text = Convert.ToString(grdCatCOG.SelectedRow.Cells[1].Text);

                txtMayor.Enabled = false;
                txtCOG.Enabled = false;
                txtDescripcionCOG.Text = objCta.descripcion;
                ddlStatusCOG.SelectedValue = objCta.status;
            }
            else
            {
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "modal", "mostrar_modal(0, '" + Verificador + "');", true);
            }

        }

        protected void grdCatCOG_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Verificador = string.Empty;
            int fila = e.RowIndex;
            GridViewRow row = grdCatCOG.Rows[e.RowIndex];
            try
            {
                cuentas_contables objCtas = new cuentas_contables();
                objCtas.cuenta_mayor = Convert.ToString(row.Cells[0].Text);
                objCtas.natura = Convert.ToString(row.Cells[1].Text);
                objCtas.descripcion = Convert.ToString(row.Cells[2].Text);
                TextBox txtStatus = (TextBox)(row.Cells[2].FindControl("txtStatus"));
                objCtas.status = txtStatus.Text;
                CNcuentas_contables.Editar_Catalogo_COG(objCtas, ref Verificador);
                if (Verificador == "0")
                {
                    grdCatCOG.EditIndex = -1;
                    CargarGridCOG();
                }
                else
                {
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "modal", "mostrar_modal(0, '" + Verificador + "');", true);
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "GridCOG", "CatCOG();", true);
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "modal", "mostrar_modal(0, '" + Verificador + "');", true);

            }
        }

        protected void grdCatCOG_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdCatCOG.EditIndex = -1;
            CargarGridCOG();
            ScriptManager.RegisterStartupScript(this, GetType(), "GridCOG", "CatCOG();", true);

        }

        protected void linkBttnAgregarCOG_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grdCatCOG.SelectedIndex = row.RowIndex;
            //LinkButton linkBttnAgregarReg = (LinkButton)(grdCatCOG.HeaderRow.Cells[5].FindControl("linkBttnAgregarCOG"));

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCOG", "$('#modalCOG').modal('show')", true);
            bttnGuardarCOG.Visible = true;
            bttnEditarCOG.Visible = false;
        }

        protected void linkBttnAgregarCat2_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grdCatalogos2.SelectedIndex = row.RowIndex;

            if (ddlTipoCat.SelectedValue == "N")
                rowCta2.Visible = true;
            else
                rowCta2.Visible = false;

            bttnAgregarCat.Visible = true;
            bttnModificarCat.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCOG", "$('#modalCat').modal('show')", true);
        }

        protected void linkCatNiv_Click(object sender, EventArgs e)
        {
            //CargarGridCatNiv2_3();
            //ScriptManager.RegisterStartupScript(this, GetType(), "GridCOG", "CatCOG();", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCOG", "$('#modalCOG').modal('show')", true);
        }

        protected void grdCatalogos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            if (ddlTipoCat.SelectedValue == "N")
                rowCta2.Visible = true;
            else
                rowCta2.Visible = false;

            rowCta2.Visible = false;
            txtCve.Enabled = true;
            bttnAgregarCat.Visible = false;
            bttnModificarCat.Visible = true;
            cuentas_contables objCta = new cuentas_contables();
            objCta.tipo = ddlTipoCat.SelectedValue;
            objCta.natura = Convert.ToString(grdCatalogos.SelectedRow.Cells[0].Text);
            objCta.concepto = "";
            CNcuentas_contables.ObtCatalogo(ref objCta, ref Verificador);
            if (Verificador == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCat", "$('#modalCat').modal('show')", true);
                txtCve.Enabled = false;
                txtCve.Text = Convert.ToString(grdCatalogos.SelectedRow.Cells[0].Text);
                txtDescCat.Text = objCta.descripcion;
                ddlStatusCat.SelectedValue = objCta.status;
            }
            else
            {
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }
        protected void linkBttnAgregarCat_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grdCatalogos.SelectedIndex = row.RowIndex;
            if (ddlTipoCat.SelectedValue == "N")
                rowCta2.Visible = true;
            else
                rowCta2.Visible = false;

            bttnAgregarCat.Visible = true;
            bttnModificarCat.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCOG", "$('#modalCat').modal('show')", true);
        }

        protected void grdCatalogos2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            rowCta2.Visible = true;
            txtCve.Enabled = true;
            bttnAgregarCat.Visible = false;
            bttnModificarCat.Visible = true;
            if (ddlTipoCat.SelectedValue == "N")
                rowCta2.Visible = true;
            else
                rowCta2.Visible = false;

            cuentas_contables objCta = new cuentas_contables();
            objCta.tipo = ddlTipoCat.SelectedValue;
            objCta.natura = Convert.ToString(grdCatalogos2.SelectedRow.Cells[0].Text);
            objCta.concepto = Convert.ToString(grdCatalogos2.SelectedRow.Cells[1].Text);
            CNcuentas_contables.ObtCatalogo(ref objCta, ref Verificador);
            if (Verificador == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCat", "$('#modalCat').modal('show')", true);
                txtCve.Enabled = false;
                txtCta2.Enabled = false;
                txtCve.Text = Convert.ToString(grdCatalogos2.SelectedRow.Cells[0].Text);
                txtCta2.Text = Convert.ToString(grdCatalogos2.SelectedRow.Cells[1].Text);
                txtDescCat.Text = objCta.descripcion;
                ddlStatusCat.SelectedValue = objCta.status;
            }
            else
            {
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void linkActNiv_Click(object sender, EventArgs e)
        {
            cuentas_contables objCta = new cuentas_contables();
            int Total = 0;
            Verificador = string.Empty;
            lblMsj.Text = string.Empty;
            try
            {
                objCta.ejercicio = SesionUsu.Usu_Ejercicio;
                CNcuentas_contables.CuentasContables_ActDescNiv2_3(objCta, ref Total, ref Verificador);
                if (Verificador == "0")
                {
                    lblMsj.Text = "Se actualizaron " + Total + " registros de niveles 2 y 3.";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(1, 'Las cuentas fueron actualizadas correctamente.');", true);
                }
                else
                {
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    lblMsj.Text = Verificador;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
                }
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);
            }
        }

        protected void ddlTipoCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoCat.SelectedValue == "N")
            {
                grdCatalogos2.Visible = true;
                grdCatalogos.Visible = false;
                CargarGridCatalogos2();
            }
            else
            {
                grdCatalogos2.Visible = false;
                grdCatalogos.Visible = true;
                CargarGridCatalogos();
            }
        }

        protected void bttnGuardarCOG_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            lblErrorCOG.Text = string.Empty;
            Comun objCatCta = new Comun();
            try
            {
                objCatCta.Etiqueta = txtMayor.Text.ToUpper();
                objCatCta.EtiquetaDos = txtCOG.Text.ToUpper();
                objCatCta.EtiquetaTres = txtDescripcionCOG.Text.ToUpper();
                objCatCta.EtiquetaCuatro = ddlStatusCOG.SelectedValue;
                CNcuentas_contables.InsertarCatCOG(objCatCta, ref Verificador);
                if (Verificador == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCOG", "$('#modalCOG').modal('hide')", true);
                    CargarGridCOG();
                }
                else
                    lblErrorCOG.Text = Verificador;

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                lblErrorCat.Text = Verificador;
            }
        }

        protected void bttnEditarCOG_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            lblErrorCOG.Text = string.Empty;
            Comun objCatCta = new Comun();
            bttnGuardarCOG.Visible = false;
            bttnEditarCOG.Visible = true;
            try
            {
                objCatCta.Etiqueta = txtMayor.Text.ToUpper();
                objCatCta.EtiquetaDos = txtCOG.Text.ToUpper();
                objCatCta.EtiquetaTres = txtDescripcionCOG.Text.ToUpper();
                objCatCta.EtiquetaCuatro = ddlStatusCOG.SelectedValue;
                CNcuentas_contables.EditarCatCOG(objCatCta, ref Verificador);
                if (Verificador == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCOG", "$('#modalCOG').modal('hide')", true);
                    CargarGridCOG();
                }
                else
                    lblErrorCOG.Text = Verificador;

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                lblErrorCat.Text = Verificador;
            }
        }

        protected void bttnAgregarCatCta_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCat", "$('#modalCat').modal('show')", true);
                //CNComun.insertar_datos_sesion
            }
            catch (Exception)
            {

            }
        }

        protected void bttnAgregarCat_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            lblErrorCat.Text = string.Empty;
            cuentas_contables objCatCta = new cuentas_contables();
            try
            {
                objCatCta.tipo = ddlTipoCat.SelectedValue;
                objCatCta.natura = txtCve.Text.ToUpper();
                if (ddlTipoCat.SelectedValue == "N")
                    objCatCta.concepto = txtCta2.Text.ToUpper();

                objCatCta.descripcion = txtDescCat.Text.ToUpper();
                objCatCta.status = ddlStatusCat.SelectedValue;
                CNcuentas_contables.InsertarCatCtas(objCatCta, ref Verificador);
                if (Verificador == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCat", "$('#modalCat').modal('hide')", true);
                    CargarGridCatalogos();
                }
                else
                    lblErrorCat.Text = Verificador;

            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                lblErrorCat.Text = Verificador;
            }
        }
        protected void bttnModificarCat_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            cuentas_contables objCat = new cuentas_contables();
            //objCat.na = Convert.ToString(grdCatalogos.SelectedRow.Cells[0].Text);

            try
            {

                objCat.tipo = ddlTipoCat.SelectedValue;//ddlTipoCat.SelectedValue;
                                                       //txtCve.Text;
                if (ddlTipoCat.SelectedValue == "N")
                {
                    objCat.natura = Convert.ToString(grdCatalogos2.SelectedRow.Cells[0].Text);
                    objCat.concepto = Convert.ToString(grdCatalogos2.SelectedRow.Cells[1].Text);
                }
                else
                {
                    objCat.natura = Convert.ToString(grdCatalogos.SelectedRow.Cells[0].Text);
                    objCat.concepto = string.Empty;
                }


                objCat.descripcion = txtDescCat.Text.ToUpper();
                objCat.status = ddlStatusCat.SelectedValue;
                CNcuentas_contables.EditarCatCta(objCat, ref Verificador);
                if (Verificador == "0")
                {
                    CargarGridCatalogos();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupCOG", "$('#modalCat').modal('hide')", true);
                }
                else
                {
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "modal", "mostrar_modal(0, '" + Verificador + "');", true);
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "GridCOG", "CatCOG();", true);
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "modal", "mostrar_modal(0, '" + Verificador + "');", true);

            }
        }
        protected void grdCatalogos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblErrorCat.Text = string.Empty;
            Verificador = string.Empty;
            Comun objCat = new Comun();
            try
            {
                int fila = e.RowIndex;
                objCat.Etiqueta = ddlTipoCat.SelectedValue;
                objCat.EtiquetaDos = grdCatalogos.Rows[fila].Cells[0].Text;
                CNcuentas_contables.EliminarCatCtas(objCat, ref Verificador);

                if (Verificador == "0")
                    CargarGridCatalogos();
                else
                {
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    lblErrorCat.Text = Verificador;

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
        protected void grdCatCOG_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblMsj2.Text = string.Empty;
            Verificador = string.Empty;
            cuentas_contables objCatCOG = new cuentas_contables();
            try
            {
                int fila = e.RowIndex;
                objCatCOG.cuenta_mayor = grdCatCOG.Rows[fila].Cells[0].Text;
                objCatCOG.natura = grdCatCOG.Rows[fila].Cells[1].Text;
                CNcuentas_contables.EliminarCatCOG(objCatCOG, ref Verificador);

                if (Verificador == "0")
                    CargarGridCOG();
                else
                {
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    lblMsj2.Text = Verificador;

                }
                //modalOficios.Show();
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                lblMsj2.Text = Verificador;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "mostrar_modal(0, '" + Verificador + "');", true);

                //lblMjErrorOficio.Text = Verificador;
                //modalOficios.Show();
            }
        }
        protected void grdCatalogos_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            Verificador = string.Empty;
            int fila = e.RowIndex;
            GridViewRow row = grdCatalogos.Rows[e.RowIndex];
            try
            {
                Comun objCat = new Comun();
                objCat.Etiqueta = Convert.ToString(row.Cells[0].Text);
                objCat.EtiquetaDos = Convert.ToString(row.Cells[1].Text);
                TextBox txtStatus = (TextBox)(row.Cells[2].FindControl("txtStatus"));
                objCat.EtiquetaTres = txtStatus.Text;
                //CNcuentas_contables.EditarCatCta(objCat, ref Verificador);
                if (Verificador == "0")
                {
                    grdCatalogos.EditIndex = -1;
                    CargarGridCatalogos();
                }
                else
                {
                    CNComun.VerificaTextoMensajeError(ref Verificador);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "modal", "mostrar_modal(0, '" + Verificador + "');", true);
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "GridCOG", "CatCOG();", true);
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                CNComun.VerificaTextoMensajeError(ref Verificador);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "modal", "mostrar_modal(0, '" + Verificador + "');", true);

            }
        }

        protected void grdCatalogos_RowEditing1(object sender, GridViewEditEventArgs e)
        {
            grdCatalogos.EditIndex = e.NewEditIndex;
            CargarGridCatalogos();
        }

        protected void grdCatalogos_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {
            grdCatalogos.EditIndex = -1;
            CargarGridCatalogos();
            ScriptManager.RegisterStartupScript(this, GetType(), "GridCatalogos", "Catalogos();", true);
        }

        protected void linkBttnBuscarBien_Click(object sender, EventArgs e)
        {
            if (txt4.Text == string.Empty || txt4.Text == "00000")
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Bienes", ref ddlDescripcion, "p_cta_mayor", "p_nivel3", "p_edicion", ddlCuenta_Mayor.SelectedValue, null, "0");
            else
                CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Bienes", ref ddlDescripcion, "p_cta_mayor", "p_nivel3", "p_edicion", ddlCuenta_Mayor.SelectedValue, txt4.Text, "0");

            ddlDescripcion_SelectedIndexChanged(null, null);
        }

        protected void ddlDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtdescripcion.Text = ddlDescripcion.SelectedItem.Text;
            int position = txtdescripcion.Text.IndexOf("]");
            txtdescripcion.Text = txtdescripcion.Text.Substring(position + 1);
            //txt4.Text = "00000";
            //if(txt4.Text==string.Empty || txt4.Text== "00000")
            //if (Session["Bienes"] != null)
            //    txt4.Text = ListBienes[ddlDescripcion.SelectedIndex].EtiquetaDos;


        }
    }
}
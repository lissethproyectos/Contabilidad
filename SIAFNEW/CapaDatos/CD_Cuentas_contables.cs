using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using CapaEntidad;
namespace CapaDatos
{
    public class CD_Cuentas_contables
    {
        public void InsertarCatCtas(cuentas_contables objCat, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_TIPO", "P_CVE", "P_CVE2", "P_DESCRIPCION", "P_STATUS" };
                object[] Valores = { objCat.tipo, objCat.natura, objCat.concepto, objCat.descripcion, objCat.status };
                String[] ParametrosOut = { "P_BANDERA" };

                Cmd = CDDatos.GenerarOracleCommand("INS_TEMP_CAT_CTAS", ref Verificador, Parametros, Valores, ParametrosOut);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }

        }
        public void InsertarCatCOG(Comun objComun, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_MAYOR", "P_COG", "P_DESCRIPCION", "P_STATUS" };
                object[] Valores = { objComun.Etiqueta, objComun.EtiquetaDos, objComun.EtiquetaTres, objComun.EtiquetaCuatro };
                String[] ParametrosOut = { "P_BANDERA" };

                Cmd = CDDatos.GenerarOracleCommand("INS_CAT_COG", ref Verificador, Parametros, Valores, ParametrosOut);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }

        }

        public void EditarCatCOG(Comun objComun, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_MAYOR", "P_COG", "P_DESCRIPCION", "P_STATUS" };
                object[] Valores = { objComun.Etiqueta, objComun.EtiquetaDos, objComun.EtiquetaTres, objComun.EtiquetaCuatro };
                String[] ParametrosOut = { "P_BANDERA" };

                Cmd = CDDatos.GenerarOracleCommand("UPD_CAT_COG", ref Verificador, Parametros, Valores, ParametrosOut);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }

        }
        public void EliminarCatCtas(Comun objComun, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_TIPO", "P_CVE" };
                object[] Valores = { objComun.Etiqueta, objComun.EtiquetaDos };
                String[] ParametrosOut = { "P_BANDERA" };

                Cmd = CDDatos.GenerarOracleCommand("DEL_TEMP_CAT_CTAS", ref Verificador, Parametros, Valores, ParametrosOut);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }
        }
        public void EliminarCatCOG(cuentas_contables objCatCOG, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_MAYOR", "P_COG" };
                object[] Valores = { objCatCOG.cuenta_mayor, objCatCOG.natura };
                String[] ParametrosOut = { "P_BANDERA" };

                Cmd = CDDatos.GenerarOracleCommand("DEL_CAT_COG", ref Verificador, Parametros, Valores, ParametrosOut);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }
        }
        public void PolizaConsultaGrid(ref cuentas_contables Objcuentas_contables, ref List<cuentas_contables> List)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand cmm = null;
            try
            {
                OracleDataReader dr = null;
                string Centro_Contable = Objcuentas_contables.centro_contable;
                String[] Parametros = { "p_ejercicio", "p_centro_contable", "p_tipo", "p_cuenta_contable", "p_nivel", "p_buscar" };
                String[] Valores = { Objcuentas_contables.ejercicio, Objcuentas_contables.centro_contable, Objcuentas_contables.tipo, Objcuentas_contables.cuenta_contable, Objcuentas_contables.nivel, Objcuentas_contables.buscar };

                cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Grid_Polizas_CC", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Objcuentas_contables = new cuentas_contables();
                    Objcuentas_contables.IdPoliza = Convert.ToInt32(dr.GetValue(0));
                    Objcuentas_contables.centro_contable = Convert.ToString(dr.GetValue(3));
                    Objcuentas_contables.numero_poliza = Convert.ToString(dr.GetValue(2));
                    Objcuentas_contables.tipo = Convert.ToString(dr.GetValue(4));
                    Objcuentas_contables.fecha = Convert.ToString(dr.GetValue(7));
                    Objcuentas_contables.status = Convert.ToString(dr.GetValue(9));
                    Objcuentas_contables.concepto = Convert.ToString(dr.GetValue(6));
                    Objcuentas_contables.Tot_Cargo = Convert.ToDouble(dr.GetValue(19));
                    Objcuentas_contables.Tot_Abono = Convert.ToDouble(dr.GetValue(20));

                    List.Add(Objcuentas_contables);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref cmm);
            }
        }
        public void ConsultarCuentas_Contables(ref cuentas_contables Objcuentas_contables, string buscar, ref List<cuentas_contables> List)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand cmm = null;
            try
            {

                OracleDataReader dr = null;
                String[] Parametros = { "p_ejercicio", "p_centro_contable", "p_cuenta_mayor", "p_buscar" };
                Object[] Valores = { Objcuentas_contables.ejercicio, Objcuentas_contables.centro_contable, Objcuentas_contables.cuenta_mayor, buscar };
                //String[] ParametrosOut = { "p_dependencia", "p_evento", "p_descripcion", "p_fecha_inicial", "p_fecha_final", "p_nivel" };

                cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.obt_grid_cuentas_contables", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Objcuentas_contables = new cuentas_contables();

                    Objcuentas_contables.id = Convert.ToString(dr[0]);
                    Objcuentas_contables.descripcion = Convert.ToString(dr[1]);
                    Objcuentas_contables.natura = Convert.ToString(dr[4]);
                    Objcuentas_contables.nivel = Convert.ToString(dr[2]);
                    if (Convert.ToString(dr[2]) == "4") { Objcuentas_contables.bandera = true; } else { Objcuentas_contables.bandera = false; }
                    Objcuentas_contables.cuenta_contable = Convert.ToString(dr[5]);
                    Objcuentas_contables.centro_contable = Convert.ToString(dr[7]);
                    List.Add(Objcuentas_contables);

                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref cmm);
            }
        }
        public void ConsultarCatCOG(ref List<cuentas_contables> List)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand cmm = null;
            cuentas_contables Objcuentas_contables;
            try
            {

                OracleDataReader dr = null;
                cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Grid_COG", ref dr);
                while (dr.Read())
                {
                    Objcuentas_contables = new cuentas_contables();
                    Objcuentas_contables.cuenta_mayor = Convert.ToString(dr[0]);
                    Objcuentas_contables.descripcion = Convert.ToString(dr[1]);
                    Objcuentas_contables.natura = Convert.ToString(dr[2]);
                    Objcuentas_contables.status = Convert.ToString(dr[3]);
                    List.Add(Objcuentas_contables);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref cmm);
            }
        }
        public void ConsultarCatalogos(Comun objCat, ref List<Comun> List)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand cmm = null;
            Comun objComun;
            try
            {

                OracleDataReader dr = null;
                String[] Parametros = { "p_tipo" };
                Object[] Valores = { objCat.Etiqueta };
                string Tipo = objCat.Etiqueta;
                cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Grid_Catalogos_Ctas", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    objComun = new Comun();                    
                    if (Tipo == "N")
                    {
                        objComun.Etiqueta = Convert.ToString(dr[0]);
                        objComun.EtiquetaDos = Convert.ToString(dr[1]);
                        objComun.EtiquetaTres = Convert.ToString(dr[2]);
                        objComun.EtiquetaCuatro = Convert.ToString(dr[3]);
                    }
                    else
                    {
                        objComun.Etiqueta = Convert.ToString(dr[0]);
                        objComun.EtiquetaDos = Convert.ToString(dr[1]);
                        objComun.EtiquetaTres = Convert.ToString(dr[2]);
                    }
                    List.Add(objComun);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref cmm);
            }
        }
        public void Consultarcuenta(ref cuentas_contables Objcuentas_contables, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                string[] ParametrosIn = { "p_id" };
                object[] Valores = { Objcuentas_contables.id };
                string[] ParametrosOut = { "P_CENTRO_CONTABLE", "P_CUENTA", "P_DESCRIPCION", "P_TIPO", "P_CLASIFICACION", "P_NIVEL", "P_STATUS", "P_ID_CUENTA_MAYOR", "p_bandera" };

                Cmd = CDDatos.GenerarOracleCommand("SEL_SAF_CUENTAS_CONTABLES", ref Verificador, ParametrosIn, Valores, ParametrosOut);
                if (Verificador == "0")
                {
                    Objcuentas_contables = new cuentas_contables();
                    Objcuentas_contables.centro_contable = Convert.ToString(Cmd.Parameters["P_CENTRO_CONTABLE"].Value);
                    Objcuentas_contables.cuenta_contable = Convert.ToString(Cmd.Parameters["p_cuenta"].Value);
                    Objcuentas_contables.descripcion = Convert.ToString(Cmd.Parameters["p_descripcion"].Value);
                    Objcuentas_contables.tipo = Convert.ToString(Cmd.Parameters["p_tipo"].Value);
                    Objcuentas_contables.clasificacion = Convert.ToString(Cmd.Parameters["p_clasificacion"].Value);
                    Objcuentas_contables.nivel = Convert.ToString(Cmd.Parameters["p_nivel"].Value);
                    Objcuentas_contables.status = Convert.ToString(Cmd.Parameters["p_status"].Value);
                    Objcuentas_contables.cuenta_mayor = Convert.ToString(Cmd.Parameters["p_id_cuenta_mayor"].Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }
        }
        public void insertar_cuenta_contable(ref cuentas_contables objcuentas_contables, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_EJERCICIO", "P_CENTRO_CONTABLE", "P_CUENTA", "P_DESCRIPCION", "P_TIPO", "P_CLASIFICACION", "P_NIVEL", "P_STATUS", "P_ID_CUENTA_MAYOR", "P_ALTA_USUARIO" };
                object[] Valores = { Convert.ToInt32(objcuentas_contables.ejercicio), objcuentas_contables.centro_contable, objcuentas_contables.cuenta_contable, objcuentas_contables.descripcion, objcuentas_contables.tipo, objcuentas_contables.clasificacion, Convert.ToInt32(objcuentas_contables.nivel), objcuentas_contables.status, Convert.ToInt32(objcuentas_contables.cuenta_mayor), objcuentas_contables.usuario };
                String[] ParametrosOut = { "p_Bandera" };

                Cmd = CDDatos.GenerarOracleCommand("INS_saf_cuentas_contables", ref Verificador, Parametros, Valores, ParametrosOut);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }
        }
        public void CuentasContables_ActDesc(cuentas_contables objcuentas_contables, ref int Total, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_EJERCICIO" };
                object[] Valores = { Convert.ToInt32(objcuentas_contables.ejercicio) };
                String[] ParametrosOut = { "P_TOT_ACTUALIZADO", "P_BANDERA" };
                Cmd = CDDatos.GenerarOracleCommand("ACT_DESC_CTAS", ref Verificador, Parametros, Valores, ParametrosOut);
                Total = Convert.ToInt32(Cmd.Parameters["P_TOT_ACTUALIZADO"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }
        }
        public void CuentasContables_ActDescNiv2_3(cuentas_contables objcuentas_contables, ref int Total, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_EJERCICIO" };
                object[] Valores = { Convert.ToInt32(objcuentas_contables.ejercicio) };
                String[] ParametrosOut = { "P_TOT_ACTUALIZADO", "P_BANDERA" };
                Cmd = CDDatos.GenerarOracleCommand("UPD_DESC_CTAS", ref Verificador, Parametros, Valores, ParametrosOut);
                Total = Convert.ToInt32(Cmd.Parameters["P_TOT_ACTUALIZADO"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }
        }
        public void Eliminar_cuenta_contable(ref cuentas_contables objcuentas_contables, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_ID" };
                object[] Valores = { objcuentas_contables.id };
                String[] ParametrosOut = { "p_Bandera" };

                Cmd = CDDatos.GenerarOracleCommand("DEL_SAF_CUENTAS_CONTABLES", ref Verificador, Parametros, Valores, ParametrosOut);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }
        }
        public void Editar_cuentas_contables(ref cuentas_contables objcuentas_contables, string Usuario, string Correo_Unach, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_CENTRO_CONTABLE", "P_CUENTA", "P_DESCRIPCION", "P_TIPO", "P_CLASIFICACION", "P_NIVEL", "P_STATUS", "P_ID_CUENTA_MAYOR", "P_ID", "P_USUARIO", "P_CORREO" };

                object[] Valores = { objcuentas_contables.centro_contable, objcuentas_contables.cuenta_contable, objcuentas_contables.descripcion, objcuentas_contables.tipo, objcuentas_contables.clasificacion, 
                    Convert.ToInt32(objcuentas_contables.nivel), objcuentas_contables.status, Convert.ToInt32(objcuentas_contables.cuenta_mayor), Convert.ToInt32(objcuentas_contables.id),
                Usuario, Correo_Unach
                };

                String[] ParametrosOut = { "p_Bandera" };
                Cmd = CDDatos.GenerarOracleCommand("UPD_SAF_CUENTAS_CONTABLES", ref Verificador, Parametros, Valores, ParametrosOut);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }
        }
        public void Editar_Catalogo_COG(cuentas_contables objcuentas_contables, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_MAYOR", "P_COG", "P_NOMBRE", "P_STATUS" };
                object[] Valores = { objcuentas_contables.cuenta_mayor, objcuentas_contables.natura, objcuentas_contables.descripcion, objcuentas_contables.status };
                String[] ParametrosOut = { "P_BANDERA" };
                Cmd = CDDatos.GenerarOracleCommand("UPD_SAF_CATALOGO_COG", ref Verificador, Parametros, Valores, ParametrosOut);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }
        }
        public void EditarCatCta(cuentas_contables objCatCta, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_TIPO", "P_CVE", "P_CVE2", "P_DESCRIPCION", "P_STATUS" };
                object[] Valores = { objCatCta.tipo, objCatCta.natura, objCatCta.concepto, objCatCta.descripcion, objCatCta.status };
                String[] ParametrosOut = { "P_BANDERA" };
                Cmd = CDDatos.GenerarOracleCommand("UPD_TEMP_CAT_CTAS", ref Verificador, Parametros, Valores, ParametrosOut);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }
        }
        public void ObtCatCOG(ref cuentas_contables objCuenta, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {


                string[] ParametrosIn = {
                                          "P_MAYOR",
                                          "P_COG"
                };
                object[] Valores = { objCuenta.cuenta_mayor, objCuenta.natura
                };
                string[] ParametrosOut ={
                                          "P_NOMBRE_DEL_COG",
                                          "P_STATUS",
                                          "P_BANDERA"
                };
                Cmd = CDDatos.GenerarOracleCommand("OBT_CATALOGO_COG", ref Verificador, ParametrosIn, Valores, ParametrosOut);
                if (Verificador == "0")
                {
                    objCuenta = new cuentas_contables();
                    objCuenta.descripcion = Convert.ToString(Cmd.Parameters["P_NOMBRE_DEL_COG"].Value);
                    objCuenta.status = Convert.ToString(Cmd.Parameters["P_STATUS"].Value);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }
        }
        public void ObtCatalogo(ref cuentas_contables objCuenta, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {


                string[] ParametrosIn = {
                                          "P_TIPO",
                                          "P_PROYECTO",
                                          "P_CVE2"
                };
                object[] Valores = { objCuenta.tipo, objCuenta.natura, objCuenta.concepto
                };
                string[] ParametrosOut ={
                                          "P_DESCRIPCION",
                                          "P_STATUS",
                                          "P_BANDERA"
                };
                Cmd = CDDatos.GenerarOracleCommand("OBT_DESC_CATALOGO_CUENTAS", ref Verificador, ParametrosIn, Valores, ParametrosOut);
                if (Verificador == "0")
                {
                    objCuenta = new cuentas_contables();
                    objCuenta.descripcion = Convert.ToString(Cmd.Parameters["P_DESCRIPCION"].Value);
                    objCuenta.status = Convert.ToString(Cmd.Parameters["P_STATUS"].Value);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CDDatos.LimpiarOracleCommand(ref Cmd);
            }
        }

    }
}
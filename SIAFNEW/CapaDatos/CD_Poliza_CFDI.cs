using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace CapaDatos
{
    public class CD_Poliza_CFDI
    {
        public void PolizaCFDIGuardar(Poliza_CFDI objPolizaCFDI, ref string Verificador)
        {


            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_ID_POLIZA", "P_NOMBRE_ARCHIVO_XML", "P_NOMBRE_ARCHIVO_PDF", "P_BENEF_TIPO",
                                        "P_CFDI_FOLIO", "P_CFDI_FECHA", "P_CFDI_TOTAL", "P_CFDI_RFC","P_TIPO_GASTO","P_CFDI_UUID","P_CFDI_NOMBRE", "P_FECHA_CAPTURA", "P_USUARIO_CAPTURA" };
                object[] Valores = {    objPolizaCFDI.IdPoliza, objPolizaCFDI.NombreArchivoXML,  objPolizaCFDI.NombreArchivoPDF, objPolizaCFDI.Beneficiario_Tipo,
                    objPolizaCFDI.CFDI_Folio, objPolizaCFDI.CFDI_Fecha,   objPolizaCFDI.CFDI_Total, objPolizaCFDI.CFDI_RFC, objPolizaCFDI.Tipo_Gasto,objPolizaCFDI.CFDI_UUID,objPolizaCFDI.CFDI_Nombre,
                        objPolizaCFDI.Fecha_Captura,objPolizaCFDI.Usuario_Captura};
                String[] ParametrosOut = { "p_Bandera" };
                Cmd = CDDatos.GenerarOracleCommand("INS_SAF_POLIZAS_CFDI", ref Verificador, Parametros, Valores, ParametrosOut);

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

        public void PolizaCFDIInsertar(Poliza_CFDI objPolizaCFDI, List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {

            for (int i = 0; i < lstPolizasCFDI.Count; i++)
            {
                CD_Datos CDDatos = new CD_Datos();
                OracleCommand Cmd = null;
                try
                {
                    if (lstPolizasCFDI[i].Tipo_Docto == "OFICIO")
                    {
                        String[] Parametros = { "P_ID_POLIZA", "P_OFICIO_NUMERO", "P_OFICIO_FECHA", 
                            "P_NOMBRE_ARCHIVO", "P_USUARIO", "P_PROVEEDOR", "P_RFC", "P_IMPORTE", "P_TIPO_DOCTO", 
                            "P_NOMBRE_EMPLEADO", "P_TIPO_EMPLEADO", "P_PLAZA_EMPLEADO", "P_TIPO_GASTO", "P_TIPO_BENEF", "P_CFDI_UUID" };
                        object[] Valores = { objPolizaCFDI.IdPoliza, lstPolizasCFDI[i].CFDI_Folio,  lstPolizasCFDI[i].CFDI_Fecha, 
                            lstPolizasCFDI[i].NombreArchivoPDF, lstPolizasCFDI[i].Usuario_Captura, lstPolizasCFDI[i].CFDI_Nombre,
                            lstPolizasCFDI[i].CFDI_RFC, lstPolizasCFDI[i].CFDI_Total,"COMPLEMENTO_CFDI",
                        "","","",lstPolizasCFDI[i].Beneficiario_Tipo,lstPolizasCFDI[i].Tipo_Gasto,lstPolizasCFDI[i].CFDI_UUID};
                        String[] ParametrosOut = { "p_Bandera" };
                        Cmd = CDDatos.GenerarOracleCommand("INS_SAF_POLIZAS_OFICIOS2", ref Verificador, Parametros, Valores, ParametrosOut);
                    }
                    else
                    {
                        String[] Parametros = { "P_ID_POLIZA", "P_NOMBRE_ARCHIVO_XML", "P_NOMBRE_ARCHIVO_PDF", "P_BENEF_TIPO",
                        "P_CFDI_FOLIO", "P_CFDI_FECHA", "P_CFDI_TOTAL", "P_CFDI_RFC","P_TIPO_GASTO","P_CFDI_UUID","P_CFDI_NOMBRE", "P_FECHA_CAPTURA", "P_USUARIO_CAPTURA",
                        "P_CFDI_CONC_DESC", "P_REGIMEN_FISCAL"};
                        object[] Valores = {    objPolizaCFDI.IdPoliza, lstPolizasCFDI[i].NombreArchivoXML,  lstPolizasCFDI[i].NombreArchivoPDF, lstPolizasCFDI[i].Beneficiario_Tipo,
                        lstPolizasCFDI[i].CFDI_Folio, lstPolizasCFDI[i].CFDI_Fecha,   lstPolizasCFDI[i].CFDI_Total, lstPolizasCFDI[i].CFDI_RFC, lstPolizasCFDI[i].Tipo_Gasto,lstPolizasCFDI[i].CFDI_UUID,lstPolizasCFDI[i].CFDI_Nombre,
                        lstPolizasCFDI[i].Fecha_Captura,lstPolizasCFDI[i].Usuario_Captura,
                        lstPolizasCFDI[i].CFDI_Concepto_Descripcion, lstPolizasCFDI[i].RegimenFiscal};
                        String[] ParametrosOut = { "p_Bandera" };
                        Cmd = CDDatos.GenerarOracleCommand("INS_SAF_POLIZAS_CFDI", ref Verificador, Parametros, Valores, ParametrosOut);
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
        public void PolizaCFDIPartidaInsertar(Poliza_CFDI objPolizaCFDI, List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {

            for (int i = 0; i < lstPolizasCFDI.Count; i++)
            {
                CD_Datos CDDatos = new CD_Datos();
                OracleCommand Cmd = null;
                try
                {
                    if (lstPolizasCFDI[i].Tipo_Docto == "OFICIO")
                    {
                        String[] Parametros = { "P_ID_POLIZA", "P_OFICIO_NUMERO", "P_OFICIO_FECHA",
                            "P_NOMBRE_ARCHIVO", "P_USUARIO", "P_PROVEEDOR", "P_RFC", "P_IMPORTE", "P_TIPO_DOCTO",
                            "P_NOMBRE_EMPLEADO", "P_TIPO_EMPLEADO", "P_PLAZA_EMPLEADO", "P_TIPO_GASTO", "P_TIPO_BENEF", "P_CFDI_UUID" };
                        object[] Valores = { objPolizaCFDI.IdPoliza, lstPolizasCFDI[i].CFDI_Folio,  lstPolizasCFDI[i].CFDI_Fecha,
                            lstPolizasCFDI[i].NombreArchivoPDF, lstPolizasCFDI[i].Usuario_Captura, lstPolizasCFDI[i].CFDI_Nombre,
                            lstPolizasCFDI[i].CFDI_RFC, lstPolizasCFDI[i].CFDI_Total,"COMPLEMENTO_CFDI",
                        "","","",lstPolizasCFDI[i].Beneficiario_Tipo,lstPolizasCFDI[i].Tipo_Gasto,lstPolizasCFDI[i].CFDI_UUID};
                        String[] ParametrosOut = { "p_Bandera" };
                        Cmd = CDDatos.GenerarOracleCommand("INS_SAF_POLIZAS_OFICIOS2", ref Verificador, Parametros, Valores, ParametrosOut);
                    }
                    else
                    {
                        String[] Parametros = { "P_ID_POLIZA", "P_NOMBRE_ARCHIVO_XML", "P_NOMBRE_ARCHIVO_PDF", "P_BENEF_TIPO",
                        "P_CFDI_FOLIO", "P_CFDI_FECHA", "P_CFDI_TOTAL", "P_CFDI_RFC","P_TIPO_GASTO","P_CFDI_UUID","P_CFDI_NOMBRE", "P_FECHA_CAPTURA", "P_USUARIO_CAPTURA",
                        "P_CFDI_CONC_DESC", "P_REGIMEN_FISCAL"};
                        object[] Valores = {    objPolizaCFDI.IdPoliza, lstPolizasCFDI[i].NombreArchivoXML,  lstPolizasCFDI[i].NombreArchivoPDF, lstPolizasCFDI[i].Beneficiario_Tipo,
                        lstPolizasCFDI[i].CFDI_Folio, lstPolizasCFDI[i].CFDI_Fecha,   lstPolizasCFDI[i].CFDI_Total, lstPolizasCFDI[i].CFDI_RFC, lstPolizasCFDI[i].Tipo_Gasto,lstPolizasCFDI[i].CFDI_UUID,lstPolizasCFDI[i].CFDI_Nombre,
                        lstPolizasCFDI[i].Fecha_Captura,lstPolizasCFDI[i].Usuario_Captura,
                        lstPolizasCFDI[i].CFDI_Concepto_Descripcion, lstPolizasCFDI[i].RegimenFiscal};
                        String[] ParametrosOut = { "P_ID_CFDI", "p_Bandera" };
                        Cmd = CDDatos.GenerarOracleCommand("INS_SAF_POLIZAS_CFDI_PARTIDAS", ref Verificador, Parametros, Valores, ParametrosOut);
                        if (Verificador == "0")
                        {                            
                            objPolizaCFDI.Id_CFDI = Convert.ToInt32(Cmd.Parameters["P_ID_CFDI"].Value);
                            if (lstPolizasCFDI[i].GrupoPartidas.Count > 0)
                            {
                                Verificador = string.Empty;
                                PolizaPartidaInsertar(objPolizaCFDI, lstPolizasCFDI[i].GrupoPartidas, ref Verificador);
                            }
                        }

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
        public void PolizaPartidaInsertar(Poliza_CFDI objPolizaCFDI, ICollection<Poliza_Partida> GrupoPartidas, ref string Verificador)
        {
            //List<Poliza_Partida> lstPartidas= List<Poliza_Partida>GrupoPartidas;
            foreach (var listPartidas in GrupoPartidas)
            {

                //lstPolizasCfdi[row.RowIndex].GrupoPartidas.
                CD_Datos CDDatos = new CD_Datos();
                OracleCommand Cmd = null;
                try
                {
                    String[] Parametros = { "P_ID_POLIZA", "P_ID_CFDI", "P_PARTIDA", "P_TOTAL" };
                    object[] Valores = { objPolizaCFDI.IdPoliza, objPolizaCFDI.Id_CFDI, listPartidas.Partida, listPartidas.Importe_Partida };
                    String[] ParametrosOut = { "p_Bandera" };
                    Cmd = CDDatos.GenerarOracleCommand("INS_SAF_POLIZA_PARTIDA", ref Verificador, Parametros, Valores, ParametrosOut);

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

        public void PolizaCFDIExtraInsertar(Poliza objPoliza, List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {

            for (int i = 0; i < lstPolizasCFDI.Count; i++)
            {
                CD_Datos CDDatos = new CD_Datos();
                OracleCommand Cmd = null;
                try
                {
                    String[] Parametros = { "P_ID_POLIZA", "P_NOMBRE_ARCHIVO_XML", "P_NOMBRE_ARCHIVO_PDF", "P_BENEF_TIPO",
                                        "P_CFDI_FECHA", "P_CFDI_TOTAL", "P_CFDI_RFC","P_TIPO_GASTO","P_CFDI_UUID","P_CFDI_NOMBRE", "P_FECHA_CAPTURA", "P_USUARIO_CAPTURA", "P_TIPO_DOCTO", "P_PARTIDA" };
                    object[] Valores = { objPoliza.IdPoliza, lstPolizasCFDI[i].NombreArchivoXML,  lstPolizasCFDI[i].NombreArchivoPDF, lstPolizasCFDI[i].Beneficiario_Tipo,
                    lstPolizasCFDI[i].CFDI_Fecha,   lstPolizasCFDI[i].CFDI_Total, lstPolizasCFDI[i].CFDI_RFC, lstPolizasCFDI[i].Tipo_Gasto,lstPolizasCFDI[i].CFDI_UUID,lstPolizasCFDI[i].CFDI_Nombre,
                    lstPolizasCFDI[i].Fecha_Captura,lstPolizasCFDI[i].Usuario_Captura, lstPolizasCFDI[i].Tipo_Docto, objPoliza.Partida };
                    String[] ParametrosOut = { "p_Bandera" };
                    Cmd = CDDatos.GenerarOracleCommand("INS_SAF_POLIZAS_CFDI_EXTRAS2", ref Verificador, Parametros, Valores, ParametrosOut);

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

        public void PolizaCFDIEditar(Poliza_CFDI objPolizaCFDI, List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {
            EliminarCFDIEditar(objPolizaCFDI.IdPoliza, ref Verificador);
            PolizaCFDIInsertar(objPolizaCFDI, lstPolizasCFDI, ref Verificador);

        }
        public void PolizaCFDIPartidaEditar(Poliza_CFDI objPolizaCFDI, List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {
            EliminarCFDIEditar(objPolizaCFDI.IdPoliza, ref Verificador);
            PolizaCFDIPartidaInsertar(objPolizaCFDI, lstPolizasCFDI, ref Verificador);

        }
        public void PolizaCFDIEditarConceptos(Poliza_CFDI objPolizaCFDI, ref string Verificador)
        {

            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_ID_CFDI", "P_CONCEPTOS" };
                object[] Valores = { objPolizaCFDI.Id_CFDI, objPolizaCFDI.CFDI_Concepto_Descripcion };
                String[] ParametrosOut = { "p_Bandera" };
                Cmd = CDDatos.GenerarOracleCommand("UPD_SAF_CONCEPTOS_CFDI", ref Verificador, Parametros, Valores, ParametrosOut);
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
        public void PolizaCFDIEditar(Poliza_CFDI objPolizaCFDI, ref string Verificador)
        {

            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_ID_CFDI", "P_REGIMEN_FISCAL" };
                object[] Valores = { objPolizaCFDI.Id_CFDI, objPolizaCFDI.RegimenFiscal };
                String[] ParametrosOut = { "p_Bandera" };
                Cmd = CDDatos.GenerarOracleCommand("UPD_SAF_DATOS_CFDI", ref Verificador, Parametros, Valores, ParametrosOut);
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

        public void PolizaCFDIExtraEditar(Poliza objPoliza, List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {
            EliminarCFDIExtra(objPoliza, ref Verificador);
            PolizaCFDIExtraInsertar(objPoliza, lstPolizasCFDI, ref Verificador);

        }

        public void EliminarCFDIExtra(Poliza objPoliza, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_ID_POLIZA", "P_PARTIDA" };
                object[] Valores = { objPoliza.IdPoliza, objPoliza.Partida };
                String[] ParametrosOut = { "p_Bandera" };
                Cmd = CDDatos.GenerarOracleCommand("DEL_SAF_POLIZAS_CFDI_EXTRAS2", ref Verificador, Parametros, Valores, ParametrosOut);
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

        public void EliminarCFDIEditar(int IdPoliza, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_ID_POLIZA" };
                object[] Valores = { IdPoliza };
                String[] ParametrosOut = { "p_Bandera" };
                Cmd = CDDatos.GenerarOracleCommand("DEL_SAF_POLIZAS_CFDI", ref Verificador, Parametros, Valores, ParametrosOut);
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
        public void EliminarCFDI(int IdCFDI, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_ID_CFDI" };
                object[] Valores = { IdCFDI };
                String[] ParametrosOut = { "P_BANDERA" };
                Cmd = CDDatos.GenerarOracleCommand("DEL_SAF_CFDI", ref Verificador, Parametros, Valores, ParametrosOut);
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

        public void EliminarCFDIS(Poliza_CFDI ObjPolizaCFDI, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_ID_POLIZA" };
                object[] Valores = { ObjPolizaCFDI.IdPoliza };
                String[] ParametrosOut = { "P_BANDERA" };
                Cmd = CDDatos.GenerarOracleCommand("DEL_SAF_CFDIS", ref Verificador, Parametros, Valores, ParametrosOut);
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

        public void PolizaCFDIConsultaTotCheque(ref Poliza_CFDI objPolizaCFDI, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_ID_POLIZA" };
                object[] Valores = { objPolizaCFDI.IdPoliza };
                String[] ParametrosOut = { "P_IMPORTE", "p_Bandera" };
                Cmd = CDDatos.GenerarOracleCommand("SEL_SAF_IMPORTE_CHEQUE", ref Verificador, Parametros, Valores, ParametrosOut);
                if (Verificador == "0")
                {
                    objPolizaCFDI = new Poliza_CFDI();
                    objPolizaCFDI.CFDI_Total = Convert.ToDouble(Cmd.Parameters["P_IMPORTE"].Value);
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
        public void PolizaCFDIConsulta(ref Poliza_CFDI objPolizaCFDI, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_ID" };
                object[] Valores = { objPolizaCFDI.Id_CFDI };
                String[] ParametrosOut = { "P_RUTA_XML", "P_CONCEPTOS", "P_BANDERA" };
                Cmd = CDDatos.GenerarOracleCommand("SEL_SAF_CFDI", ref Verificador, Parametros, Valores, ParametrosOut);
                if (Verificador == "0")
                {
                    objPolizaCFDI = new Poliza_CFDI();
                    objPolizaCFDI.Ruta_XML = Convert.ToString(Cmd.Parameters["P_RUTA_XML"].Value);
                    objPolizaCFDI.CFDI_Concepto_Descripcion = Convert.ToString(Cmd.Parameters["P_CONCEPTOS"].Value);
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

        public void PolizaCFDIValidar(ref Poliza_CFDI objPolizaCFDI, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_ID_POLIZA", "P_UUID" };
                object[] Valores = { objPolizaCFDI.IdPoliza, objPolizaCFDI.CFDI_UUID };
                String[] ParametrosOut = { "P_NUM_CFDI", "P_DESCRIPCION", "P_BANDERA" };
                Cmd = CDDatos.GenerarOracleCommand("OBT_CFDI", ref Verificador, Parametros, Valores, ParametrosOut);
                if (Verificador == "0")
                {
                    objPolizaCFDI = new Poliza_CFDI();
                    objPolizaCFDI.CFDI_Existe = Convert.ToInt32(Cmd.Parameters["P_NUM_CFDI"].Value);
                    objPolizaCFDI.CFDI_Observaciones = Convert.ToString(Cmd.Parameters["P_DESCRIPCION"].Value);
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
        public void PolizaCFDIConsultaDatos(Poliza_CFDI objPolizaCFDI, ref List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            CD_Comun CDComun = new CD_Comun();
            OracleCommand cmm = null;
            
            
            int IdPoliza = objPolizaCFDI.IdPoliza;
            try
            {

                OracleDataReader dr = null;
                String[] Parametros = { "P_ID_POLIZA" };
                String[] Valores = { Convert.ToString(objPolizaCFDI.IdPoliza) };

                if(objPolizaCFDI.Tipo_Docto=="CFDI")
                    cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Grid_Polizas_CFDI", ref dr, Parametros, Valores);
                else
                    cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Grid_Cfdi_Oficios", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    
                    objPolizaCFDI = new Poliza_CFDI();
                    List<Comun> list = new List<Comun>();
                    List<Poliza_Partida> lstPartidasPoliza = new List<Poliza_Partida>();
                    objPolizaCFDI.Beneficiario_Tipo = Convert.ToString(dr.GetValue(0));
                    objPolizaCFDI.CFDI_Folio = Convert.ToString(dr.GetValue(1));
                    objPolizaCFDI.CFDI_Fecha = Convert.ToString(dr.GetValue(2));
                    objPolizaCFDI.CFDI_Total = Convert.ToDouble(dr.GetValue(3));
                    objPolizaCFDI.CFDI_RFC = Convert.ToString(dr.GetValue(4));
                    objPolizaCFDI.Tipo_Gasto = Convert.ToString(dr.GetValue(5));
                    objPolizaCFDI.NombreArchivoXML = Convert.ToString(dr.GetValue(6));
                    objPolizaCFDI.NombreArchivoPDF = Convert.ToString(dr.GetValue(7));
                    objPolizaCFDI.Ruta_XML = "~/AdjuntosTemp/" + Convert.ToString(dr.GetValue(6));
                    objPolizaCFDI.Ruta_PDF = "~/AdjuntosTemp/" + Convert.ToString(dr.GetValue(7));
                    objPolizaCFDI.CFDI_UUID = Convert.ToString(dr.GetValue(8));
                    objPolizaCFDI.CFDI_Nombre = Convert.ToString(dr.GetValue(9));
                    objPolizaCFDI.Fecha_Captura = Convert.ToString(dr.GetValue(10));
                    objPolizaCFDI.Usuario_Captura = Convert.ToString(dr.GetValue(11));
                    objPolizaCFDI.Habilita = (Convert.ToString(dr.GetValue(12)) == "S") ? true : false;
                    objPolizaCFDI.Id_CFDI = Convert.ToInt32(dr.GetValue(13));
                    //CDComun.LlenaCombo("PKG_Contabilidad.Obt_Combo_Poliza_Partidas", ref list, "P_ID_POLIZA", Convert.ToString(IdPoliza));
                    //this.PartidasDatos(objPolizaCFDI.IdPoliza, ref lstPartidasPoliza, ref Verificador);
                    
                    this.PolizaPartidasDatos(objPolizaCFDI.Id_CFDI, ref lstPartidasPoliza, ref Verificador);
                    objPolizaCFDI.GrupoPartidas=lstPartidasPoliza;

                    objPolizaCFDI.lstPartidas = list;
                    //objPolizaCFDI.listPolizaPartidas = lstPartidasPoliza;
                    //objPolizaCFDI.lstPolizaPartidas = lstPartidasPoliza;
                    objPolizaCFDI.CFDI_Concepto_Descripcion = Convert.ToString(dr.GetValue(22));
                    objPolizaCFDI.Tipo_Docto = Convert.ToString(dr.GetValue(23));
                    //objPolizaCFDI.objPartida.l = lstPartidasPoliza;


                    lstPolizasCFDI.Add(objPolizaCFDI);
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

        public void PolizaPartidasDatos(int IdCfdi, ref List<Poliza_Partida>lstPartidas, ref string Verificador)
        {
            //List<Poliza_Partida> lstPartidas= new List <Poliza_Partida>();
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand cmm = null;
            
            try
            {

                OracleDataReader dr = null;
                String[] Parametros = { "P_ID_CFDI" };
                String[] Valores = { Convert.ToString(IdCfdi) };

                cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Grid_Poliza_Partidas", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    string Partida = Convert.ToString(dr.GetValue(0));
                    double Importe_Partida = Convert.ToDouble(dr.GetValue(1));
                    string Partida_Descripcion = Convert.ToString(dr.GetValue(2));
                    Poliza_Partida objPartida = new Poliza_Partida(Partida, Partida_Descripcion, Importe_Partida);
                    
                    //lstPartidas.Add(objPartida);
                    lstPartidas.Add(objPartida);
                        //lstPolizasCFDI.Add(objPolizaCFDI);
                    ///objPolizaCFDI. .Add(objPoliza);
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

        //public void PartidasDatos(int IdPoliza, ref List<Poliza_Partida> lstPartidas, ref string Verificador)
        //{
        //    //List<Poliza_Partida> lstPartidas= new List <Poliza_Partida>();
        //    CD_Datos CDDatos = new CD_Datos();
        //    OracleCommand cmm = null;

        //    try
        //    {

        //        OracleDataReader dr = null;
        //        String[] Parametros = { "P_ID_POLIZA" };
        //        String[] Valores = { Convert.ToString(IdPoliza) };

        //        cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Combo_Poliza_Partidas", ref dr, Parametros, Valores);
        //        while (dr.Read())
        //        {


        //            Poliza_Partida objPartida = new Poliza_Partida();
        //            objPartida.Partida = Convert.ToString(dr.GetValue(0));
        //            objPartida.Importe_Partida = Convert.ToDouble(dr.GetValue(1));
        //            objPartida.Partida_Descripcion = Convert.ToString(dr.GetValue(2));
        //            //lstPartidas.Add(objPartida);
        //            lstPartidas.Add(objPartida);
        //            //lstPolizasCFDI.Add(objPolizaCFDI);
        //            ///objPolizaCFDI. .Add(objPoliza);
        //        }
        //        dr.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        CDDatos.LimpiarOracleCommand(ref cmm);
        //    }
        //}

        public void PolizaCFDIConsultaConceptos(Poliza_CFDI objPolizaCFDI, ref List<Poliza_CFDI> lstPolizasCFDI)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand cmm = null;
            try
            {

                OracleDataReader dr = null;
                String[] Parametros = { "P_ID_CFDI" };
                String[] Valores = { Convert.ToString(objPolizaCFDI.Id_CFDI) };

                cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Conceptos_CFDI", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    objPolizaCFDI = new Poliza_CFDI();
                    objPolizaCFDI.CFDI_Concepto_Descripcion = Convert.ToString(dr.GetValue(0));
                    lstPolizasCFDI.Add(objPolizaCFDI);
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

        public void PolizaCFDIExtrasConsultaDatos(Poliza_CFDI objPolizaCFDI, int idPoliza, string Partida, ref List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand cmm = null;
            try
            {

                OracleDataReader dr = null;
                String[] Parametros = { "P_ID_POLIZA", "P_PARTIDA" };
                String[] Valores = { Convert.ToString(idPoliza), Partida };

                cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Grid_Polizas_CFDI_Extras2", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    objPolizaCFDI = new Poliza_CFDI();
                    objPolizaCFDI.Beneficiario_Tipo = Convert.ToString(dr.GetValue(0));
                    objPolizaCFDI.CFDI_Folio = Convert.ToString(dr.GetValue(1));
                    objPolizaCFDI.CFDI_Fecha = Convert.ToString(dr.GetValue(2));
                    objPolizaCFDI.CFDI_Total = Convert.ToDouble(dr.GetValue(3));
                    objPolizaCFDI.CFDI_RFC = Convert.ToString(dr.GetValue(4));
                    objPolizaCFDI.Tipo_Gasto = Convert.ToString(dr.GetValue(5));
                    objPolizaCFDI.NombreArchivoXML = Convert.ToString(dr.GetValue(6));
                    objPolizaCFDI.NombreArchivoPDF = Convert.ToString(dr.GetValue(7));
                    objPolizaCFDI.Ruta_XML = "~/AdjuntosExtras/" + Convert.ToString(dr.GetValue(6));
                    objPolizaCFDI.Ruta_PDF = "~/AdjuntosExtras/" + Convert.ToString(dr.GetValue(7));
                    objPolizaCFDI.CFDI_UUID = Convert.ToString(dr.GetValue(8));
                    objPolizaCFDI.CFDI_Nombre = Convert.ToString(dr.GetValue(9));
                    objPolizaCFDI.Fecha_Captura = Convert.ToString(dr.GetValue(10));
                    objPolizaCFDI.Usuario_Captura = Convert.ToString(dr.GetValue(11));
                    objPolizaCFDI.Tipo_Docto = Convert.ToString(dr.GetValue(12));
                    lstPolizasCFDI.Add(objPolizaCFDI);
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

        public void PolizaCFDIConsultaDatosAdmin(Poliza_CFDI objPolizaCFDI, ref List<Poliza_CFDI> lstPolizasCFDI, string Buscar)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand cmm = null;
            try
            {

                OracleDataReader dr = null;
                String[] Parametros = { "P_TIPO_GASTO", "P_TIPO_BENEFICIARIO", "P_CENTRO_CONTABLE", "P_EJERCICIO", "P_MES", "P_BUSCAR" };
                String[] Valores = { objPolizaCFDI.Tipo_Gasto, objPolizaCFDI.Beneficiario_Tipo, objPolizaCFDI.Centro_Contable, objPolizaCFDI.Ejercicio, objPolizaCFDI.Mes_anio, Buscar };

                cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Grid_Polizas_CFDI_Admin", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    objPolizaCFDI = new Poliza_CFDI();
                    objPolizaCFDI.Beneficiario_Tipo = Convert.ToString(dr.GetValue(0));
                    objPolizaCFDI.CFDI_Folio = Convert.ToString(dr.GetValue(1));
                    objPolizaCFDI.CFDI_Fecha = Convert.ToString(dr.GetValue(2));
                    objPolizaCFDI.CFDI_Total = Convert.ToDouble(dr.GetValue(3));
                    objPolizaCFDI.CFDI_RFC = Convert.ToString(dr.GetValue(4));
                    objPolizaCFDI.Tipo_Gasto = Convert.ToString(dr.GetValue(5));
                    objPolizaCFDI.NombreArchivoXML = Convert.ToString(dr.GetValue(6));
                    objPolizaCFDI.NombreArchivoPDF = Convert.ToString(dr.GetValue(7));
                    objPolizaCFDI.Ruta_XML = "~/AdjuntosTemp/" + Convert.ToString(dr.GetValue(6));
                    objPolizaCFDI.Ruta_PDF = "~/AdjuntosTemp/" + Convert.ToString(dr.GetValue(7));
                    objPolizaCFDI.CFDI_UUID = Convert.ToString(dr.GetValue(8));
                    objPolizaCFDI.CFDI_Nombre = Convert.ToString(dr.GetValue(9));
                    objPolizaCFDI.Fecha_Captura = Convert.ToString(dr.GetValue(10));
                    objPolizaCFDI.Usuario_Captura = Convert.ToString(dr.GetValue(11));
                    objPolizaCFDI.Centro_Contable = Convert.ToString(dr.GetValue(12));
                    objPolizaCFDI.Numero_poliza = Convert.ToString(dr.GetValue(13));
                    objPolizaCFDI.Mes_anio = Convert.ToString(dr.GetValue(14));
                    objPolizaCFDI.CFDI_Total = Convert.ToDouble(dr.GetValue(15));
                    objPolizaCFDI.Id_CFDI = Convert.ToInt32(dr.GetValue(16));
                    lstPolizasCFDI.Add(objPolizaCFDI);
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
        public void PolizasSinComprobar(Poliza objPoliza, ref List<Poliza> lstPolizas/*, string Buscar*/)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand cmm = null;
            try
            {

                OracleDataReader dr = null;
                String[] Parametros = { "p_centro_contable", "p_mes" };
                String[] Valores = { objPoliza.Centro_contable, objPoliza.Mes_anio };

                cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Grid_Polizas_sin_comprobar", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    objPoliza = new Poliza();
                    objPoliza.IdPoliza = Convert.ToInt32(dr.GetValue(0));
                    objPoliza.Centro_contable = Convert.ToString(dr.GetValue(1));
                    objPoliza.Desc_Tipo_Documento = Convert.ToString(dr.GetValue(2));
                    objPoliza.Numero_poliza = Convert.ToString(dr.GetValue(3));
                    objPoliza.Fecha = Convert.ToString(dr.GetValue(4));
                    objPoliza.Concepto = Convert.ToString(dr.GetValue(5));
                    objPoliza.Tot_Cargo = Convert.ToDouble(dr.GetValue(6));
                    objPoliza.Clasificacion = "(" + Convert.ToString(dr.GetValue(7)) + ") Registro(s)";
                    objPoliza.Tot_Comprobado = Convert.ToDouble(dr.GetValue(8));
                    objPoliza.Partida = Convert.ToString(dr.GetValue(9));
                    lstPolizas.Add(objPoliza);
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
        public void PolizasPorComprobar(Poliza objPoliza, ref List<Poliza> lstPolizas/*, string Buscar*/)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand cmm = null;
            try
            {

                OracleDataReader dr = null;
                String[] Parametros = { "p_centro_contable", "p_mes" };
                String[] Valores = { objPoliza.Centro_contable, objPoliza.Mes_anio };

                cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Grid_Polizas_por_comprobar", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    objPoliza = new Poliza();
                    objPoliza.IdPoliza = Convert.ToInt32(dr.GetValue(0));
                    objPoliza.Centro_contable = Convert.ToString(dr.GetValue(1));
                    objPoliza.Desc_Tipo_Documento = Convert.ToString(dr.GetValue(2));
                    objPoliza.Numero_poliza = Convert.ToString(dr.GetValue(3));
                    objPoliza.Fecha = Convert.ToString(dr.GetValue(4));
                    objPoliza.Tot_Cargo = Convert.ToDouble(dr.GetValue(5));
                    objPoliza.Clasificacion = "(" + Convert.ToString(dr.GetValue(6)) + ") Registro(s)";
                    objPoliza.Tot_Comprobado = Convert.ToDouble(dr.GetValue(7));
                    objPoliza.Mes_anio = Convert.ToString(dr.GetValue(8));
                    //objPoliza.Partida = Convert.ToString(dr.GetValue(9));
                    lstPolizas.Add(objPoliza);
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

    }
    public class CD_Poliza_Oficio
    {
        public void PolizaOficioInsertar(Poliza_Oficio objPolizaOficio, string Usuario, List<Poliza_Oficio> lstPolizaOficios, ref string Verificador)
        {

            for (int i = 0; i < lstPolizaOficios.Count; i++)
            {
                CD_Datos CDDatos = new CD_Datos();
                OracleCommand Cmd = null;
                try
                {
                    String[] Parametros = { "P_ID_POLIZA", "P_OFICIO_NUMERO", "P_OFICIO_FECHA", "P_NOMBRE_ARCHIVO", "P_USUARIO", "P_PROVEEDOR", "P_RFC", "P_IMPORTE", "P_TIPO_DOCTO", "P_NOMBRE_EMPLEADO", "P_TIPO_EMPLEADO", "P_PLAZA_EMPLEADO" };
                    object[] Valores = { objPolizaOficio.IdPoliza_Oficio, lstPolizaOficios[i].Numero_Oficio,  lstPolizaOficios[i].Fecha_Oficio, lstPolizaOficios[i].NombreArchivoOficio, Usuario, lstPolizaOficios[i].Proveedor, lstPolizaOficios[i].RFC, lstPolizaOficios[i].Importe_Oficio,lstPolizaOficios[i].Tipo_Docto_Oficio,
                    lstPolizaOficios[i].Nombre,lstPolizaOficios[i].Tipo_Personal,lstPolizaOficios[i].Numero_Plaza
                    };
                    String[] ParametrosOut = { "p_Bandera" };
                    Cmd = CDDatos.GenerarOracleCommand("INS_SAF_POLIZAS_OFICIOS", ref Verificador, Parametros, Valores, ParametrosOut);

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

        public void PolizaOficioBorrar(Poliza_Oficio objPolizaOficio, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand Cmd = null;
            try
            {
                String[] Parametros = { "P_ID_POLIZA" };
                object[] Valores = { objPolizaOficio.IdPoliza_Oficio };
                String[] ParametrosOut = { "p_Bandera" };
                Cmd = CDDatos.GenerarOracleCommand("DEL_SAF_POLIZAS_OFICIOS", ref Verificador, Parametros, Valores, ParametrosOut);

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




        public void PolizaOficiosDatos(Poliza_Oficio objPolizaOficio, ref List<Poliza_Oficio> lstPolizaOficios, ref string Verificador)
        {
            CD_Datos CDDatos = new CD_Datos();
            OracleCommand cmm = null;
            try
            {

                OracleDataReader dr = null;
                String[] Parametros = { "P_ID_POLIZA" };
                String[] Valores = { Convert.ToString(objPolizaOficio.IdPoliza_Oficio) };

                cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Grid_Poliza_Oficios", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    objPolizaOficio = new Poliza_Oficio();
                    objPolizaOficio.Numero_Oficio = Convert.ToString(dr.GetValue(2));
                    objPolizaOficio.Fecha_Oficio = Convert.ToString(dr.GetValue(3));
                    objPolizaOficio.Importe_Oficio = Convert.ToDouble(dr.GetValue(7));
                    objPolizaOficio.Proveedor = Convert.ToString(dr.GetValue(8));
                    objPolizaOficio.RFC = Convert.ToString(dr.GetValue(9));
                    objPolizaOficio.NombreArchivoOficio = Convert.ToString(dr.GetValue(4));
                    objPolizaOficio.Ruta_Oficio = "~/OficiosTemp/" + Convert.ToString(dr.GetValue(4));
                    objPolizaOficio.Tipo_Docto_Oficio = Convert.ToString(dr.GetValue(10));
                    objPolizaOficio.Nombre = Convert.ToString(dr.GetValue(11));
                    objPolizaOficio.Tipo_Personal = Convert.ToString(dr.GetValue(12));
                    objPolizaOficio.Numero_Plaza = Convert.ToString(dr.GetValue(13));
                    lstPolizaOficios.Add(objPolizaOficio);
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
        //public void ConsultaEmpleado(ref Poliza_Oficio objEmpleado, ref string Verificador)
        //{
        //    CD_Datos CDDatos = new CD_Datos();
        //    OracleCommand Cmd = null;
        //    try
        //    {
        //        string[] ParametrosIn = { "P_PLAZA",
        //                                  "P_TIPO_PERSONAL"};
        //        object[] Valores = { objEmpleado.PlazaEmpleado,
        //                             objEmpleado.TipoEmpleado};
        //        string[] ParametrosOut ={

        //                                  "p_PATERNO",
        //                                  "p_MATERNO" ,
        //                                  "p_NOMBRES",
        //                                  "P_BANDERA"
        //        };

        //        Cmd = CDDatos.GenerarOracleCommand("OBT_EMPLEADO", ref Verificador, ParametrosIn, Valores, ParametrosOut);
        //        if (Verificador == "0")
        //        {
        //            objEmpleado = new Poliza_Oficio();
        //            objEmpleado.Empleado = Convert.ToString(Cmd.Parameters["p_NOMBRES"].Value)+ Convert.ToString(Cmd.Parameters["p_PATERNO"].Value)+ Convert.ToString(Cmd.Parameters["p_MATERNO"].Value);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        CDDatos.LimpiarOracleCommand(ref Cmd);
        //    }
        //}



    }
}

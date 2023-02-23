using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDatos
{
    class pruebas
    {
        //public void PolizaConsultaGrid(ref Poliza ObjPoliza, String FechaInicial, String FechaFinal, String Buscar, String TipoUsu, ref List<Poliza> List)
        //{
        //    CD_Datos CDDatos = new CD_Datos();
        //    OracleCommand cmm = null;
        //    try
        //    {
        //        OracleDataReader dr = null;
        //        string Centro_Contable = ObjPoliza.Centro_contable;
        //        String[] Parametros = { "p_centro_contable", "p_fecha_inicial", "p_fecha_final", "p_tipo", "p_status", "p_buscar", "p_editor", "p_tipo_captura", "p_clasifica", "p_ejercicio" };
        //        String[] Valores = { ObjPoliza.Centro_contable, FechaInicial, FechaFinal, ObjPoliza.Tipo, ObjPoliza.Status, Buscar, TipoUsu, ObjPoliza.Tipo_captura, ObjPoliza.Clasificacion, Convert.ToString(ObjPoliza.Ejercicio) };

        //        cmm = CDDatos.GenerarOracleCommandCursor("pkg_contabilidad.Obt_Grid_Polizas", ref dr, Parametros, Valores);

        //        while (dr.Read())
        //        {
        //            ObjPoliza = new Poliza();
        //            ObjPoliza.IdPoliza = Convert.ToInt32(dr.GetValue(0));
        //            ObjPoliza.Numero_poliza = Convert.ToString(dr.GetValue(1));
        //            ObjPoliza.Centro_contable = Convert.ToString(dr.GetValue(2));                    
        //            ObjPoliza.Tipo = Convert.ToString(dr.GetValue(3));
        //            ObjPoliza.Concepto = Convert.ToString(dr.GetValue(4));
        //            ObjPoliza.Fecha = Convert.ToString(dr.GetValue(5));
        //            ObjPoliza.Mes_anio = Convert.ToString(dr.GetValue(6));
        //            ObjPoliza.Status = Convert.ToString(dr.GetValue(7));
        //            ObjPoliza.Opcion_Copiar = Convert.ToString(dr.GetValue(8)) == "MC" ? (Centro_Contable == "00000" || Convert.ToString(dr.GetValue(12)) == "S") ? false : true : false;
        //            ObjPoliza.Opcion_Copiar2 = Convert.ToString(dr.GetValue(8)) == "MC" ? (Centro_Contable == "00000" || Convert.ToString(dr.GetValue(12)) == "S") ? true : false : true;

        //            ObjPoliza.Tot_Cargo = Convert.ToDouble(dr.GetValue(9));
        //            ObjPoliza.Tot_Abono = Convert.ToDouble(dr.GetValue(10));
        //            ObjPoliza.Cedula_numero = Convert.ToString(dr.GetValue(11));
        //            //string Pasa = Convert.ToString(dr.GetValue(12)); //Verifica si el mes esta cerrado

        //            ObjPoliza.Opcion_Eliminar = Convert.ToString(dr.GetValue(12)) == "S" ? false : true;
        //            ObjPoliza.Opcion_Eliminar2 = Convert.ToString(dr.GetValue(12)) == "S" ? true : false;
        //            ObjPoliza.Opcion_Modificar = Convert.ToString(dr.GetValue(12)) == "S" ? false : true;
        //            ObjPoliza.Opcion_Modificar2 = Convert.ToString(dr.GetValue(12)) == "S" ? true : false;

        //            if (Convert.ToString(dr.GetValue(13)) == "E" && (Convert.ToString(dr.GetValue(18)) == "CFDI" || Convert.ToString(dr.GetValue(18)) == "OFICIO"))
        //            {
        //                ObjPoliza.Opcion_CFDI = true;
        //                ObjPoliza.Opcion_CFDI2 = false;
        //                ObjPoliza.Desc_Tipo_Documento = Convert.ToString(dr.GetValue(14)) == "0" ? "+ " + Convert.ToString(dr.GetValue(18)) : "(" + Convert.ToString(dr.GetValue(14)) + ")" + Convert.ToString(dr.GetValue(18));
        //            }
        //            else if (Convert.ToString(dr.GetValue(13)) == "I" && Convert.ToString(dr.GetValue(18)) == "OFICIO")
        //            {
        //                ObjPoliza.Opcion_CFDI = true;
        //                ObjPoliza.Opcion_CFDI2 = false;
        //                ObjPoliza.Desc_Tipo_Documento = Convert.ToString(dr.GetValue(14)) == "0" ? "+ " + Convert.ToString(dr.GetValue(18)) : "(" + Convert.ToString(dr.GetValue(14)) + ")" + Convert.ToString(dr.GetValue(18));
        //            }
        //            else if (Convert.ToString(dr.GetValue(13)) == "D" && (Convert.ToString(dr.GetValue(18)) == "CFDI" || Convert.ToString(dr.GetValue(18)) == "OFICIO"))
        //            {
        //                ObjPoliza.Opcion_CFDI = true;
        //                ObjPoliza.Opcion_CFDI2 = false;
        //                ObjPoliza.Desc_Tipo_Documento = Convert.ToString(dr.GetValue(14)) == "0" ? "+ " + Convert.ToString(dr.GetValue(18)) : "(" + Convert.ToString(dr.GetValue(14)) + ")" + Convert.ToString(dr.GetValue(18));
        //            }
        //            else
        //            {
        //                ObjPoliza.Opcion_CFDI = false;
        //                ObjPoliza.Opcion_CFDI2 = true;
        //                ObjPoliza.Desc_Tipo_Documento = "S/N";
        //            }

        //            ObjPoliza.Tiene_CFDI = Convert.ToInt32(dr.GetValue(14)) > 0 ? true : false;
        //            ObjPoliza.Total_CFDI = Convert.ToInt32(dr.GetValue(14));
        //            ObjPoliza.Mes_Cerrado = Convert.ToString(dr.GetValue((15)));
        //            ObjPoliza.Cheque_numero = Convert.ToString(dr.GetValue(16));
        //            ObjPoliza.Cheque_importe = Convert.ToDouble(dr.GetValue(17));
        //            ObjPoliza.Tipo_Documento = Convert.ToString(dr.GetValue(18));
        //            List.Add(ObjPoliza);
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

    }
}

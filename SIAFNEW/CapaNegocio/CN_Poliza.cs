using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Data;
using System.Web.UI;
using CapaEntidad;
using CapaDatos;


namespace CapaNegocio
{
    public class CN_Poliza
    {
        public string ValidarTotalCta(string Centro_Contable, string Cuenta, ref string Verificador)
        {
            string Existe = "N";
            try
            {
                CD_Poliza CDPoliza = new CD_Poliza();
                Existe=CDPoliza.ValidarTotalCta(Centro_Contable, Cuenta, ref Verificador);
                return Existe;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizaConsultaGrid(ref Poliza ObjPoliza, String FechaInicial, String FechaFinal, String Buscar, String TipoUsu, ref List<Poliza> List)
        {
            try
            {
                CD_Poliza CDPoliza = new CD_Poliza();
                CDPoliza.PolizaConsultaGrid(ref ObjPoliza, FechaInicial, FechaFinal, Buscar, TipoUsu, ref List);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void GenPolizasAuto(int Ejercicio,  string Mes, string Tipo, ref int TotalPolizasGen, ref string Verificador)
        {
            try
            {
                CD_Poliza CDPoliza = new CD_Poliza();
                CDPoliza.GenPolizasAuto(Ejercicio, Mes, Tipo, ref TotalPolizasGen, ref Verificador);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarPolizasAuto(int Ejercicio, string Mes, string Tipo, ref string Verificador)
        {
            try
            {
                CD_Poliza CDPoliza = new CD_Poliza();
                CDPoliza.EliminarPolizasAuto(Ejercicio, Mes, Tipo, ref Verificador);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizaConsultaGrid_Min(ref Poliza ObjPoliza, String FechaInicial, String FechaFinal, String Buscar, String TipoUsu, ref List<Poliza> List, ref List<Poliza> List2)
        {
            try
            {
                CD_Poliza CDPoliza = new CD_Poliza();
                CDPoliza.PolizaConsultaGrid_Min(ref ObjPoliza, FechaInicial, FechaFinal, Buscar, TipoUsu, ref List, ref List2);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ValidarTotal(ref Poliza objPoliza, ref string Verificador)
        {
            try
            {
                CD_Poliza CDPolizaCFDI = new CD_Poliza();
                CDPolizaCFDI.ValidarTotal(ref objPoliza, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void ValidarMes(int Ejercicio, string Sistema, string Centro_Contable, ref string MesAct, ref string Verificador)
        {
            try
            {
                CD_Poliza CDPolizaCFDI = new CD_Poliza();
                CDPolizaCFDI.ValidarMes(Ejercicio, Sistema, Centro_Contable, ref MesAct, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void ConsultarPolizaSel(ref Poliza ObjPoliza, ref string Verificador)
        {
            try
            {
                CD_Poliza CDPoliza = new CD_Poliza();
                CDPoliza.ConsultarPolizaSel(ref ObjPoliza, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //public void ConsultarPasivoSel(ref Pasivo objPasivo, ref string Verificador)
        //{
        //    try
        //    {
        //        CD_Poliza CDPoliza = new CD_Poliza();
        //        CDPoliza.ConsultarPasivoSel(ref ObjPoliza, ref Verificador);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        public void PolizaInsertar(ref Poliza ObjPoliza, ref string Verificador)
        {
            try
            {
                CD_Poliza CDPoliza = new CD_Poliza();
                CDPoliza.PolizaInsertar(ref ObjPoliza,ref Verificador);                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PasivoConsultaGrid(Pasivo objPasivo, ref List<Pasivo> List)
        {
            try
            {
                CD_Poliza CDPasivo = new CD_Poliza();
                CDPasivo.PasivoConsultaGrid(objPasivo, ref List);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizasCjaConsultaGrid(Poliza objPoliza, string Tipo, ref List<Poliza> List)
        {
            try
            {
                CD_Poliza CDPasivo = new CD_Poliza();
                CDPasivo.PolizasCjaConsultaGrid(objPoliza, Tipo, ref List);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PasivoInsertar(List<Pasivo> lstPasivos, ref string Verificador)
        {
            try
            {
                CD_Poliza CDPoliza = new CD_Poliza();
                CDPoliza.PasivoInsertar(lstPasivos, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PasivoEditar(Pasivo objPasivo, List<Pasivo> lstPasivos, ref string Verificador)
        {
            try
            {
                CD_Poliza CDPasivo = new CD_Poliza();
                CDPasivo.PasivoEliminar(objPasivo, ref Verificador);
                if(Verificador=="0")
                    CDPasivo.PasivoInsertar(lstPasivos, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PasivoEliminar(Pasivo objPasivo, ref string Verificador)
        {
            try
            {
                CD_Poliza CDPasivo = new CD_Poliza();
                CDPasivo.PasivoEliminar(objPasivo, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ListPasivos(Pasivo objPasivo, ref List<Pasivo> lstPasivos)
        {
            try
            {
                CD_Poliza CDPasivo = new CD_Poliza();
                CDPasivo.ListPasivos(objPasivo, ref lstPasivos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizaEditar(ref Poliza ObjPoliza, ref string Verificador)
        {
            try
            {
                CD_Poliza CDPoliza = new CD_Poliza();
                CDPoliza.PolizaEditar(ref ObjPoliza, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizaEliminar(Poliza ObjPoliza, ref string Verificador)
        {
            try
            {
                CD_Poliza CDPoliza = new CD_Poliza();
                CDPoliza.PolizaEliminar(ObjPoliza, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizaEliminarRegistro(Poliza ObjPoliza, ref string Verificador)
        {
            try
            {
                CD_Poliza CDPoliza = new CD_Poliza();
                CDPoliza.PolizaEliminarRegistro(ObjPoliza, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizaCopiar(ref Poliza ObjPoliza, ref string Verificador)
        {
            try
            {
                CD_Poliza CDPoliza = new CD_Poliza();
                CDPoliza.PolizaCopiar(ref ObjPoliza, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaNegocio
{
    public class CN_Poliza_CFDI
    {
        public void PolizaCFDIInsertar(Poliza_CFDI objPolizaCFDI, List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIInsertar(objPolizaCFDI, lstPolizasCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizaCFDIPartidaInsertar(Poliza_CFDI objPolizaCFDI, List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIPartidaInsertar(objPolizaCFDI, lstPolizasCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void PolizaCFDIExtraInsertar(Poliza objPoliza, List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIExtraInsertar(objPoliza, lstPolizasCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void PolizaCFDIGuardar(Poliza_CFDI objPolizaCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIGuardar(objPolizaCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizaCFDIEditar(Poliza_CFDI objPolizaCFDI, List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIEditar(objPolizaCFDI, lstPolizasCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizaCFDIPartidaEditar(Poliza_CFDI objPolizaCFDI, List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIPartidaEditar(objPolizaCFDI, lstPolizasCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizaCFDIEditarConceptos(Poliza_CFDI objPolizaCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIEditarConceptos(objPolizaCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizaCFDIEditar(Poliza_CFDI objPolizaCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIEditar(objPolizaCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void PolizaCFDIValidar(ref Poliza_CFDI objPolizaCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIValidar(ref objPolizaCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void PolizaCFDIExtraEditar(Poliza objPoliza, List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIExtraEditar(objPoliza, lstPolizasCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarCFDIExtra(Poliza objPoliza, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.EliminarCFDIExtra(objPoliza, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarCFDIEditar(int IdPoliza, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.EliminarCFDIEditar(IdPoliza, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarCFDI(int IdCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.EliminarCFDI(IdCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void EliminarCFDIS(Poliza_CFDI ObjPolizaCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.EliminarCFDIS(ObjPolizaCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void PolizaCFDIConsultaTotCheque(ref Poliza_CFDI objPolizaCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIConsultaTotCheque(ref objPolizaCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizaCFDIConsulta(ref Poliza_CFDI objPolizaCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIConsulta(ref objPolizaCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



        public void PolizaCFDIConsultaDatos(Poliza_CFDI objPolizaCFDI, ref List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIConsultaDatos(objPolizaCFDI, ref lstPolizasCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void PolizaCFDIConsultaConceptos(Poliza_CFDI objPolizaCFDI, ref List<Poliza_CFDI> lstPolizasCFDI)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIConsultaConceptos(objPolizaCFDI, ref lstPolizasCFDI);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void PolizaCFDIExtrasConsultaDatos(Poliza_CFDI objPolizaCFDI, int idPoliza, string Partida, ref List<Poliza_CFDI> lstPolizasCFDI, ref string Verificador)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIExtrasConsultaDatos(objPolizaCFDI, idPoliza, Partida, ref lstPolizasCFDI, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void PolizaOficiosDatos(Poliza_Oficio objPolizaOficio, ref List<Poliza_Oficio> lstPolizaOficios, ref string Verificador)
        {
            try
            {
                CD_Poliza_Oficio CDPolizaOficio = new CD_Poliza_Oficio();
                CDPolizaOficio.PolizaOficiosDatos(objPolizaOficio, ref lstPolizaOficios, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void PolizaPartidasDatos(Poliza_CFDI objPoliza, ref List<Poliza_CFDI> lstPartidas, ref string Verificador)
        {
            try
            {
                CD_Poliza_Oficio CDPolizaOficio = new CD_Poliza_Oficio();
                //CDPolizaOficio.PolizaPartidasDatos(objPoliza, ref lstPartidas, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void PolizaCFDIConsultaDatosAdmin(Poliza_CFDI objPolizaCFDI, ref List<Poliza_CFDI> lstPolizasCFDI, string Buscar)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizaCFDIConsultaDatosAdmin(objPolizaCFDI, ref lstPolizasCFDI, Buscar);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void PolizasSinComprobar(Poliza objPoliza, ref List<Poliza> lstPolizas/*, string Buscar*/)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizasSinComprobar(objPoliza, ref lstPolizas/*, Buscar*/);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void PolizasPorComprobar(Poliza objPoliza, ref List<Poliza> lstPolizas/*, string Buscar*/)
        {
            try
            {
                CD_Poliza_CFDI CDPolizaCFDI = new CD_Poliza_CFDI();
                CDPolizaCFDI.PolizasPorComprobar(objPoliza, ref lstPolizas/*, Buscar*/);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
    public class CN_Poliza_Oficio
    {
        public void PolizaOficioInsertar(Poliza_Oficio objPolizaOficio, string Usuario, List<Poliza_Oficio> lstPolizaOficios, ref string Verificador)
        {
            try
            {
                CD_Poliza_Oficio CDPolizaOficio = new CD_Poliza_Oficio();
                CDPolizaOficio.PolizaOficioBorrar(objPolizaOficio, ref Verificador);
                if (Verificador == "0")
                {
                    //Verificador = string.Empty;
                    CDPolizaOficio.PolizaOficioInsertar(objPolizaOficio, Usuario, lstPolizaOficios, ref Verificador);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void PolizaOficiosConsulta(Poliza_Oficio objPolizaOficio, ref List<Poliza_Oficio> lstPolizaOficios, ref string Verificador)
        {
            try
            {
                CD_Poliza_Oficio CDPolizaOficio = new CD_Poliza_Oficio();
                CDPolizaOficio.PolizaOficiosDatos(objPolizaOficio, ref lstPolizaOficios, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void ConsultaEmpleado(ref Poliza_Oficio objEmpleado, ref string Verificador)
        {
            try
            {
                CD_Poliza_Oficio CDPolizaOficio = new CD_Poliza_Oficio();
                //CDPolizaOficio.PolizaOficiosDatos(ref objPolizaOficio, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}

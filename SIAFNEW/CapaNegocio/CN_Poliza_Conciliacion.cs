using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaNegocio
{
    public class CN_Poliza_Conciliacion
    {

        public void ConciliacionInsertarEnc(ref Poliza_Conciliacion ObjConciliacion, List<Poliza_Conciliacion> lstConciliacionDet, ref string Verificador)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                CDConciliacion.ConciliacionInsertarEnc(ref ObjConciliacion, ref Verificador);
                if(Verificador=="0")
                {
                    Verificador = string.Empty;
                    CDConciliacion.ConciliacionDetInsertar(ObjConciliacion.IdEnc, lstConciliacionDet, ref Verificador);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConciliacionInsertarEnc2(ref Poliza_Conciliacion ObjConciliacion, List<Poliza_Conciliacion> lstConciliacionDet, ref string Verificador)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                CDConciliacion.ConciliacionInsertarEnc2(ref ObjConciliacion, ref Verificador);
                if (Verificador == "0")
                {
                    if (lstConciliacionDet != null)
                    {
                        Verificador = string.Empty;
                        CDConciliacion.ConciliacionDetInsertar(ObjConciliacion.IdEnc, lstConciliacionDet, ref Verificador);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConciliacionEditarEnc(ref Poliza_Conciliacion ObjConciliacion, List<Poliza_Conciliacion> lstConciliacionDet, ref string Verificador)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                int IdEnc = ObjConciliacion.IdEnc;
                CDConciliacion.ConciliacionEditarEnc(ref ObjConciliacion, ref Verificador);
                if (Verificador == "0")
                {
                    Verificador = string.Empty;
                    CDConciliacion.ConciliacionDetEliminar(IdEnc, ref Verificador);
                    if (Verificador == "0")
                    {
                        Verificador = string.Empty;
                        CDConciliacion.ConciliacionDetInsertar(IdEnc, lstConciliacionDet, ref Verificador);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConciliacionEditarEnc2(ref Poliza_Conciliacion ObjConciliacion, List<Poliza_Conciliacion> lstConciliacionDet, ref string Verificador)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                int IdEnc = ObjConciliacion.IdEnc;
                CDConciliacion.ConciliacionEditarEnc2(ref ObjConciliacion, ref Verificador);
                if (Verificador == "0")
                {
                    Verificador = string.Empty;
                    CDConciliacion.ConciliacionDetEliminar(IdEnc, ref Verificador);
                    if (Verificador == "0")
                    {
                        if (lstConciliacionDet != null)
                        {
                            Verificador = string.Empty;
                            CDConciliacion.ConciliacionDetInsertar(IdEnc, lstConciliacionDet, ref Verificador);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ConciliacionInsertar(Poliza_Conciliacion ObjConciliacion, List<Poliza_Conciliacion> lstPolizaDet, ref string Verificador)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                CDConciliacion.ConciliacionInsertar(ObjConciliacion, lstPolizaDet, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConciliacionDetInsertar(int idConciliacion, List<Poliza_Conciliacion> lstPolizaDet, ref string Verificador)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                //CDConciliacion.ConciliacionDetInsertar(idConciliacion, lstPolizaDet, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void ConciliacionEditar(Poliza_Conciliacion ObjConciliacion, List<Poliza_Conciliacion> lstPolizaDet, ref string Verificador)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                CDConciliacion.ConciliacionEliminar(ObjConciliacion, ref Verificador);
                if (Verificador == "0")
                {
                    Verificador = string.Empty;
                    CDConciliacion.ConciliacionInsertar(ObjConciliacion, lstPolizaDet, ref Verificador);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConciliacionConsultaGrid(Poliza_Conciliacion ObjConciliacion, ref List<Poliza_Conciliacion> List)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                CDConciliacion.ConciliacionConsultaGrid(ObjConciliacion, ref List);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConciliacionConsultaGrid2(Poliza_Conciliacion ObjConciliacion, ref List<Poliza_Conciliacion> List)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                CDConciliacion.ConciliacionConsultaGrid2(ObjConciliacion, ref List);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConciliacionAdjConsultaGrid(Poliza_Conciliacion ObjConciliacion, ref List<Poliza_Conciliacion> List)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                CDConciliacion.ConciliacionAdjConsultaGrid(ObjConciliacion, ref List);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Polizas_ConciliacionConsultaGrid(Poliza_Detalle ObjPolizaDet, ref List<Poliza_Detalle> List)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                CDConciliacion.Polizas_ConciliacionConsultaGrid(ObjPolizaDet, ref List);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConsultarConciliacionEncSel(ref Poliza_Conciliacion ObjConciliacion, ref string Verificador)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                CDConciliacion.ConsultarConciliacionEncSel(ref ObjConciliacion, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConsultarConciliacionEncSel2(ref Poliza_Conciliacion ObjConciliacion, ref string Verificador)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                CDConciliacion.ConsultarConciliacionEncSel2(ref ObjConciliacion, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConsultarConciliacionSel(Poliza_Conciliacion ObjConciliacion, ref Poliza_Conciliacion ObjRespConciliacion, ref string Verificador)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                CDConciliacion.ConsultarConciliacionSel(ObjConciliacion, ref ObjRespConciliacion, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConciliacionDetConsultaGrid(Poliza_Conciliacion objConciliacion, ref List<Poliza_Conciliacion> lstConciliacionDet)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                CDConciliacion.ConciliacionDetConsultaGrid(objConciliacion, ref lstConciliacionDet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConciliacionEliminar(Poliza_Conciliacion ObjConciliacion, ref string Verificador)
        {
            try
            {
                CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
                CDConciliacion.ConciliacionEliminar(ObjConciliacion, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void PolizaAdjInsertar(Poliza_Conciliacion objConciliacion, List<Poliza_Conciliacion> lstAdjuntos, ref string Verificador)
        {
            try
            {
                CD_Poliza_Conciliacion CDPolizaAdj = new CD_Poliza_Conciliacion();
                CDPolizaAdj.PolizaAdjEliminar(objConciliacion, ref Verificador);
                if (Verificador == "0")
                {
                    Verificador = string.Empty;
                    if (lstAdjuntos.Count >= 1)
                        CDPolizaAdj.PolizaAdjInsertar(objConciliacion, lstAdjuntos, ref Verificador);
                    else
                        Verificador = "0";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public void ConciliacionDetInsertar(Poliza_Conciliacion ObjConciliacion, ref string Verificador)
        //{
        //    try
        //    {
        //        CD_Poliza_Conciliacion CDConciliacion = new CD_Poliza_Conciliacion();
        //        CDConciliacion.ConciliacionInsertar(ObjConciliacion, ref Verificador);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

    }
}

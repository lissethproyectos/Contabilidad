using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;
namespace CapaNegocio
{
    public class CN_Cuentas_contables
    {
        public void InsertarCatCtas(cuentas_contables objCta, ref string Verificador)
        {
            try
            {

                CD_Cuentas_contables CDCatCtas = new CD_Cuentas_contables();
                CDCatCtas.InsertarCatCtas(objCta, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarCatCtas(Comun obComun, ref string Verificador)
        {
            try
            {

                CD_Cuentas_contables CDCatCtas = new CD_Cuentas_contables();
                CDCatCtas.EliminarCatCtas(obComun, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarCatCOG(cuentas_contables obComun, ref string Verificador)
        {
            try
            {

                CD_Cuentas_contables CDCatCtas = new CD_Cuentas_contables();
                CDCatCtas.EliminarCatCOG(obComun, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void PolizaConsultaGrid(ref cuentas_contables Objcuentas_contables, ref List<cuentas_contables> List)
        {
            try
            {
                CD_Cuentas_contables CDcuenta_contable = new CD_Cuentas_contables();
                CDcuenta_contable.PolizaConsultaGrid(ref Objcuentas_contables, ref List);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ConsultarCuentas_contables(ref cuentas_contables Objcuentas_contables, string buscar, ref List<cuentas_contables> List)
        {
            try
            {

                CD_Cuentas_contables CDcuentas_contables = new CD_Cuentas_contables();
                CDcuentas_contables.ConsultarCuentas_Contables(ref Objcuentas_contables, buscar, ref List);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ConsultarCatCOG(ref List<cuentas_contables> List)
        {
            try
            {

                CD_Cuentas_contables CDcuentas_contables = new CD_Cuentas_contables();
                CDcuentas_contables.ConsultarCatCOG(ref List);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ObtCatCOG(ref cuentas_contables objCatCog, ref string Verificador)
        {
            try
            {

                CD_Cuentas_contables CDCtas = new CD_Cuentas_contables();
                CDCtas.ObtCatCOG(ref objCatCog, ref Verificador);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ObtCatalogo(ref cuentas_contables objCatalogo, ref string Verificador)
        {
            try
            {

                CD_Cuentas_contables CDCtas = new CD_Cuentas_contables();
                CDCtas.ObtCatalogo(ref objCatalogo, ref Verificador);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertarCatCOG(Comun objCatCog, ref string Verificador)
        {
            try
            {

                CD_Cuentas_contables CDcuentas_contables = new CD_Cuentas_contables();
                CDcuentas_contables.InsertarCatCOG(objCatCog, ref Verificador);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EditarCatCOG(Comun objCatCog, ref string Verificador)
        {
            try
            {

                CD_Cuentas_contables CDcuentas_contables = new CD_Cuentas_contables();
                CDcuentas_contables.EditarCatCOG(objCatCog, ref Verificador);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EditarCatCta(cuentas_contables objCatCog, ref string Verificador)
        {
            try
            {
                CD_Cuentas_contables cdcuenta = new CD_Cuentas_contables();
                cdcuenta.EditarCatCta(objCatCog, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConsultarCatalogos(Comun objCat, ref List<Comun> List)
        {
            try
            {
                CD_Cuentas_contables CDcuentas_contables = new CD_Cuentas_contables();
                CDcuentas_contables.ConsultarCatalogos(objCat, ref List);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Consultarcuenta_contable(ref cuentas_contables objcuentas_contables, ref string Verificador)
        {
            try
            {
                CD_Cuentas_contables CDcuenta = new CD_Cuentas_contables();
                CDcuenta.Consultarcuenta(ref objcuentas_contables, ref Verificador);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void insertar_cuenta_contable(ref cuentas_contables objcuentas_contables, ref string Verificador)
        {
            try
            {

                CD_Cuentas_contables CDcuenta = new CD_Cuentas_contables();
                CDcuenta.insertar_cuenta_contable(ref objcuentas_contables, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CuentasContables_ActDesc(cuentas_contables objcuentas_contables, ref int Total, ref string Verificador)
        {
            try
            {

                CD_Cuentas_contables CDcuenta = new CD_Cuentas_contables();
                CDcuenta.CuentasContables_ActDesc(objcuentas_contables, ref Total, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CuentasContables_ActDescNiv2_3(cuentas_contables objcuentas_contables, ref int Total, ref string Verificador)
        {
            try
            {

                CD_Cuentas_contables CDcuenta = new CD_Cuentas_contables();
                CDcuenta.CuentasContables_ActDescNiv2_3(objcuentas_contables, ref Total, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Editar_cuentas_contables(ref cuentas_contables objcuentas_contables, string Usuario, string Correo_Unach, ref string Verificador)
        {
            try
            {
                CD_Cuentas_contables cdcuenta = new CD_Cuentas_contables();
                cdcuenta.Editar_cuentas_contables(ref objcuentas_contables, Usuario, Correo_Unach, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Editar_Catalogo_COG(cuentas_contables objcuentas_contables, ref string Verificador)
        {
            try
            {
                CD_Cuentas_contables cdcuenta = new CD_Cuentas_contables();
                cdcuenta.Editar_Catalogo_COG(objcuentas_contables, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Eliminar_cuenta_contable(ref cuentas_contables objcuentas_contables, ref string Verificador)
        {
            try
            {

                CD_Cuentas_contables CDcuenta = new CD_Cuentas_contables();
                CDcuenta.Eliminar_cuenta_contable(ref objcuentas_contables, ref Verificador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}


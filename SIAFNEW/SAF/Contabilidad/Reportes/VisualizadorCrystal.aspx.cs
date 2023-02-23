using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using CrystalDecisions.ReportSource;
using System.IO;
using CapaEntidad;

namespace SAF.Reportes
{
    public partial class VisualizadorCrystal : System.Web.UI.Page
    {

        private ReportDocument report = new ReportDocument();
        private System.Web.UI.Page p;
        ConnectionInfo connectionInfo = new ConnectionInfo();
        string Tipo;
        String Reporte = "";
        Sesion SesionUsu = new Sesion();

        protected void Page_Load(object sender, EventArgs e)
        {
            rptVisor();
        }
        private void rptVisor()
        {
            try
            {

                int Ejercicio = Convert.ToInt32(Request.QueryString["Ejercicio"]);
                string centro_contable = Convert.ToString(Request.QueryString["centro_contable"]);
                string cuenta_contable = Convert.ToString(Request.QueryString["cuenta_contable"]);
                string cuenta_contable_fin = Convert.ToString(Request.QueryString["cuenta_contable_fin"]);
                string mes_inicial = Convert.ToString(Request.QueryString["mes_inicial"]);
                string mes_final = Convert.ToString(Request.QueryString["mes_final"]);
                string cuenta_mayor = Convert.ToString(Request.QueryString["cuenta_mayor"]);
                string tipo_p = Convert.ToString(Request.QueryString["tipo_p"]);
                string poliza = Convert.ToString(Request.QueryString["poliza"]);
                string sistema = Convert.ToString(Request.QueryString["sistema"]);
                string descripcion = Convert.ToString(Request.QueryString["descripcion"]);
                string nivel = Convert.ToString(Request.QueryString["nivel"]);
                string mayor = Convert.ToString(Request.QueryString["mayor"]);
                string cierre = Convert.ToString(Request.QueryString["cierre"]);
                string status = Convert.ToString(Request.QueryString["status"]);
                string id = Convert.ToString(Request.QueryString["id"]);
                string filtro_busca = Convert.ToString(Request.QueryString["filtro_busca"]);
                string desc_mes = Convert.ToString(Request.QueryString["desc_mes"]);
                string Numero_Poliza = Convert.ToString(Request.QueryString["Numero_Poliza"]);
                string Tipo_V = Convert.ToString(Request.QueryString["Tipo_V"]);
                string notas = Convert.ToString(Request.QueryString["notas"]);
                string tipo_adeudo_ini = Convert.ToString(Request.QueryString["tipo_adeudo_ini"]);
                string tipo_adeudo_fin = Convert.ToString(Request.QueryString["tipo_adeudo_fin"]);
                string tipo_rep = Convert.ToString(Request.QueryString["tipo_rep"]);
                string parametro1 = Convert.ToString(Request.QueryString["parametro1"]);
                string parametro2 = Convert.ToString(Request.QueryString["parametro2"]);
                string parametro3 = Convert.ToString(Request.QueryString["parametro3"]);
                string parametro4 = Convert.ToString(Request.QueryString["parametro4"]);
                string enExcel = Convert.ToString(Request.QueryString["enExcel"]);
                Tipo = Convert.ToString(Request.QueryString["Tipo"]);

                SesionUsu = (Sesion)Session["Usuario"];


                string caseSwitch = Tipo;
                switch (caseSwitch)
                {
                    case "RP-001":
                        Reporte = "Contabilidad\\Reportes\\RP-001.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, cuenta_contable); report.SetParameterValue(3, cuenta_contable_fin); reporte_PDF();
                        break;
                    case "RP-002":
                        Reporte = "Contabilidad\\Reportes\\RP-002.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, mes_inicial); report.SetParameterValue(3, mes_final); reporte_PDF();
                        break;
                    case "RP-003":
                        Reporte = "Contabilidad\\Reportes\\RP-003.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, cuenta_mayor); reporte_PDF();
                        break;
                    case "RP-003exc":
                        Reporte = "Contabilidad\\Reportes\\RP-003exc.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, cuenta_mayor); reporte_XLS();
                        break;
                    case "RP-004":
                        Reporte = "Contabilidad\\Reportes\\RP-004.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, centro_contable); report.SetParameterValue(1, mes_inicial); report.SetParameterValue(2, mes_final); report.SetParameterValue(3, tipo_p); report.SetParameterValue(4, status); report.SetParameterValue(5, filtro_busca); reporte_PDF();
                        break;
                    case "RP-004exc":
                        Reporte = "Contabilidad\\Reportes\\RP-004.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, centro_contable); report.SetParameterValue(1, mes_inicial); report.SetParameterValue(2, mes_final); report.SetParameterValue(3, tipo_p); report.SetParameterValue(4, status); report.SetParameterValue(5, filtro_busca); reporte_XLS();
                        break;
                    case "RP-005":
                        Reporte = "Contabilidad\\Reportes\\RP-005.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, id); reporte_PDF();
                        break;
                    case "RP-005_P":
                        Reporte = "Contabilidad\\Reportes\\RP-005_P.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, id); reporte_PDF();
                        break;
                    case "RP-006":
                        if(Ejercicio>=2022)
                            Reporte = "Contabilidad\\Reportes\\RP-006-2022.rpt";
                        else
                            Reporte = "Contabilidad\\Reportes\\RP-006.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(5, mes_inicial); report.SetParameterValue(2, sistema); report.SetParameterValue(3, mes_final); report.SetParameterValue(4, descripcion); report.SetParameterValue(6, desc_mes); reporte_PDF();
                        break;
                    case "RP-006exc":
                        Reporte = "Contabilidad\\Reportes\\RP-006exc.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(5, mes_inicial); report.SetParameterValue(2, sistema); report.SetParameterValue(3, mes_final); report.SetParameterValue(4, descripcion); report.SetParameterValue(6, desc_mes); reporte_XLS();
                        break;
                    case "RP-007":
                        Reporte = "Contabilidad\\Reportes\\RP-007.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, nivel); report.SetParameterValue(2, mes_inicial); reporte_PDF();
                        break;
                    case "RP-008":
                        Reporte = "Contabilidad\\Reportes\\RP-008.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, nivel); report.SetParameterValue(2, mes_inicial); reporte_PDF();
                        break;
                    case "RP-009":
                        Reporte = "Contabilidad\\Reportes\\RP-009.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, cierre); report.SetParameterValue(2, nivel); report.SetParameterValue(3, mayor); reporte_PDF();
                        break;
                    case "RP-010":
                        Reporte = "Contabilidad\\Reportes\\RP-010.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, cierre); report.SetParameterValue(2, nivel); reporte_PDF();
                        break;
                    case "RP-011":
                        Reporte = "Contabilidad\\Reportes\\RP-011.rpt";
                        reportes_dir();
                        break;
                    case "RP-012":
                        Reporte = "Contabilidad\\Reportes\\RP-012.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, cuenta_mayor); report.SetParameterValue(3, mes_inicial); report.SetParameterValue(4, mes_final); report.SetParameterValue(5, nivel); reporte_PDF();
                        break;
                    case "RP-012exc":
                        Reporte = "Contabilidad\\Reportes\\RP-012exc.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, cuenta_mayor); report.SetParameterValue(3, mes_inicial); report.SetParameterValue(4, mes_final); report.SetParameterValue(5, nivel); reporte_GranXLS();
                        break;
                    case "RP-012csv":
                        Reporte = "Contabilidad\\Reportes\\RP-012csv.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, cuenta_mayor); report.SetParameterValue(3, mes_inicial); report.SetParameterValue(4, mes_final); report.SetParameterValue(5, nivel); reporte_CSV();
                        break;
                    case "RP-012-2":
                        Reporte = "Contabilidad\\Reportes\\RP-012-2.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, mes_inicial); report.SetParameterValue(3, mes_final); report.SetParameterValue(4, tipo_rep); reporte_PDF();
                        break;
                    case "RP-012-2_Polizas":
                        Reporte = "Contabilidad\\Reportes\\RP-012-2_Polizas.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, tipo_rep); report.SetParameterValue(3, mes_inicial); report.SetParameterValue(4, mes_final); report.SetParameterValue(5, cuenta_contable); reporte_PDF();
                        break;
                    case "RP-012-2-Grupo":
                        Reporte = "Contabilidad\\Reportes\\RP-012-2-Grupo.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, mes_inicial); report.SetParameterValue(3, mes_final); report.SetParameterValue(4, tipo_rep); reporte_PDF();
                        break;
                    case "RP-012-2exc":
                        Reporte = "Contabilidad\\Reportes\\RP-012-2exc.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, mes_inicial); report.SetParameterValue(3, mes_final); report.SetParameterValue(4, tipo_rep); reporte_XLS();
                        break;
                    case "RP-012-2-Grupoexc":
                        Reporte = "Contabilidad\\Reportes\\RP-012-2-Grupoexc.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, mes_inicial); report.SetParameterValue(3, mes_final); report.SetParameterValue(4, tipo_rep); reporte_XLS();
                        break;
                    case "RP-013":
                        Reporte = "Contabilidad\\Reportes\\RP-013.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, cuenta_contable); report.SetParameterValue(3, mes_inicial); report.SetParameterValue(4, mes_final); reporte_PDF();
                        break;
                    case "RP-013xls":
                        Reporte = "Contabilidad\\Reportes\\RP-013xls.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, cuenta_contable); report.SetParameterValue(3, mes_inicial); report.SetParameterValue(4, mes_final); reporte_XLS();
                        break;
                    case "RP-014":
                        if (Convert.ToInt32(SesionUsu.Usu_Ejercicio) >= 2022)
                            Reporte = "Contabilidad\\Reportes\\RP-014.rpt";
                        else
                            Reporte = "Contabilidad\\Reportes\\RP-014-old.rpt";
                        reportes_dir();
                        report.SetParameterValue(2, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); reporte_PDF();
                        break;
                    case "RP-014exc":
                        if (Convert.ToInt32(SesionUsu.Usu_Ejercicio) >= 2022)
                            Reporte = "Contabilidad\\Reportes\\RP-014exc.rpt";
                        else
                            Reporte = "Contabilidad\\Reportes\\RP-014exc-old.rpt";

                        reportes_dir();
                        report.SetParameterValue(2, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); reporte_XLS();
                        break;
                    case "RP-015":
                        Reporte = "Contabilidad\\Reportes\\RP-015.rpt";
                        reportes_dir();
                        report.SetParameterValue(2, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); reporte_PDF();
                        break;
                    case "RP-015exc":
                        Reporte = "Contabilidad\\Reportes\\RP-015exc.rpt";
                        reportes_dir();
                        report.SetParameterValue(2, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); reporte_XLS();
                        break;
                    case "RP-016":
                        Reporte = "Contabilidad\\Reportes\\RP-016.rpt";
                        reportes_dir();
                        report.SetParameterValue(2, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); reporte_PDF();
                        break;
                    case "RP-017":
                        Reporte = "Contabilidad\\Reportes\\RP-017.rpt";
                        reportes_dir();
                        report.SetParameterValue(3, centro_contable); report.SetParameterValue(2, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); reporte_PDF();
                        break;
                    case "RP-016xls":
                        Reporte = "Contabilidad\\Reportes\\RP-016.rpt";
                        reportes_dir();
                        report.SetParameterValue(2, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); reporte_XLS();
                        break;
                    case "RP-017xls":
                        Reporte = "Contabilidad\\Reportes\\RP-017.rpt";
                        reportes_dir();
                        report.SetParameterValue(3, centro_contable); report.SetParameterValue(2, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); reporte_XLS();
                        break;
                    case "RP-018":
                        Reporte = "Contabilidad\\Reportes\\RP-018.rpt";
                        reportes_dir();
                        break;
                    case "RP-14-det":
                        if (Convert.ToInt32(SesionUsu.Usu_Ejercicio) >= 2022)
                            Reporte = "Contabilidad\\Reportes\\RP-014-det.rpt";
                        else
                            Reporte = "Contabilidad\\Reportes\\RP-014-det-old.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); report.SetParameterValue(2, Ejercicio); reporte_PDF_Legal();
                        break;
                    case "RP-14-detxls":
                        if (Convert.ToInt32(SesionUsu.Usu_Ejercicio) >= 2022)
                            Reporte = "Contabilidad\\Reportes\\RP-014-detxls.rpt";
                        else
                            Reporte = "Contabilidad\\Reportes\\RP-014-detxls-old.rpt";

                        reportes_dir();
                        report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); report.SetParameterValue(2, Ejercicio); reporte_XLS();
                        break;
                    case "RP-compara-01":
                        Reporte = "Contabilidad\\Reportes\\RP-compara-01.rpt";
                        reportes_dir();                       
                            report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, cuenta_contable); report.SetParameterValue(2, mes_inicial); reporte_PDF();                        
                        break;                   
                    case "RP-compara-02":
                        Reporte = "Contabilidad\\Reportes\\RP-compara-02.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, cuenta_contable); report.SetParameterValue(2, mes_inicial); reporte_PDF();
                        break;
                    case "RP-compara-02xls":
                        Reporte = "Contabilidad\\Reportes\\RP-compara-02.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, cuenta_contable); report.SetParameterValue(2, mes_inicial); reporte_XLS();
                        break;
                    case "RP-compara-04":
                        Reporte = "Contabilidad\\Reportes\\RP-compara-04.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, cuenta_contable); report.SetParameterValue(2, mes_inicial); reporte_PDF();
                        break;
                    case "RP-compara-04xls":
                        Reporte = "Contabilidad\\Reportes\\RP-compara-04exc.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, cuenta_contable); report.SetParameterValue(2, mes_inicial); reporte_XLS();
                        break;
                    case "RP-compara-05":
                        Reporte = "Contabilidad\\Reportes\\RP-compara-05.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, cuenta_contable); report.SetParameterValue(2, mes_inicial); reporte_PDF();
                        break;
                    case "RP-compara-05-Caja":
                        Reporte = "Contabilidad\\Reportes\\RP-compara-05-Caja.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, cuenta_contable); report.SetParameterValue(2, mes_inicial); reporte_PDF();
                        break;
                    case "RP-revisar-exc":
                        Reporte = "Contabilidad\\Reportes\\RP-revisar-exc.rpt";
                        reportes_dir();
                        reporte_XLS();
                        break;
                    case "RP-Volante_T":
                        Reporte = "Contabilidad\\Reportes\\RP-Volante_T.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, Numero_Poliza); report.SetParameterValue(3, Tipo_V); reporte_PDF();
                        break;
                    case "RP-005-lote":
                        Reporte = "Contabilidad\\Reportes\\RP-005-lote.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, mes_inicial); report.SetParameterValue(3, mes_final); report.SetParameterValue(4, tipo_p); report.SetParameterValue(5, status); reporte_PDF();
                        break;
                    case "RP-005-lote-exc":
                        Reporte = "Contabilidad\\Reportes\\RP-005-lote-exc.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, mes_inicial); report.SetParameterValue(3, mes_final); report.SetParameterValue(4, tipo_p); report.SetParameterValue(5, status); reporte_XLS();
                        break;
                    case "RP-Diario-General":
                        Reporte = "Contabilidad\\Reportes\\RP-Diario-General.rpt";
                        reportes_dir();
                        report.SetParameterValue(2, mes_inicial); report.SetParameterValue(3, mes_final); report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); reporte_PDF();
                        break;
                    case "RP-Diario-Generalxls":
                        Reporte = "Contabilidad\\Reportes\\RP-Diario-Generalxls.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); report.SetParameterValue(2, Ejercicio); report.SetParameterValue(3, centro_contable); reporte_GranXLS();
                        break;
                    case "RP-Diario-Generalcsv":
                        Reporte = "Contabilidad\\Reportes\\RP-Diario-Generalcsv.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); report.SetParameterValue(2, Ejercicio); report.SetParameterValue(3, centro_contable); reporte_CSV();
                        break;
                    case "RP-Mayor-General":
                        Reporte = "Contabilidad\\Reportes\\RP-Mayor-General.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); reporte_GranXLS();
                        break;
                    case "RP-Mayor-Generalcsv":
                        Reporte = "Contabilidad\\Reportes\\RP-Mayor-Generalcsv.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); reporte_CSV();
                        break;
                    case "RP-Resumen-de-cuentas":
                        Reporte = "Contabilidad\\Reportes\\RP-Resumen-de-cuentas.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, cuenta_contable); report.SetParameterValue(3, mes_inicial); report.SetParameterValue(4, mes_final); reporte_PDF();
                        break;
                    case "RP-Resumen-de-cuentasxls":
                        Reporte = "Contabilidad\\Reportes\\RP-Resumen-de-cuentasxls.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, cuenta_contable); report.SetParameterValue(3, mes_inicial); report.SetParameterValue(4, mes_final); reporte_XLS();
                        break;
                    case "RP-Adecuaciones":
                        Reporte = "Contabilidad\\Reportes\\RP-Adecuaciones.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, centro_contable); reporte_PDF();
                        break;
                    case "RP-019":
                        Reporte = "Contabilidad\\Reportes\\RP-019.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); report.SetParameterValue(2, Ejercicio); reporte_PDF();
                        break;
                    case "RP-019xls":
                        Reporte = "Contabilidad\\Reportes\\RP-019.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); report.SetParameterValue(2, Ejercicio); reporte_XLS();

                        break;
                    case "RP-020":
                        if (Convert.ToInt32(SesionUsu.Usu_Ejercicio) >= 2022)
                            Reporte = "Contabilidad\\Reportes\\RP-020-2022.rpt";
                        else
                            Reporte = "Contabilidad\\Reportes\\RP-020.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); report.SetParameterValue(2, Ejercicio); reporte_PDF();
                        break;
                    case "RP-020xls":
                        if (Convert.ToInt32(SesionUsu.Usu_Ejercicio) >= 2022)
                            Reporte = "Contabilidad\\Reportes\\RP-020-2022.rpt";
                        else
                            Reporte = "Contabilidad\\Reportes\\RP-020.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); report.SetParameterValue(2, Ejercicio); reporte_XLS();
                        break;
                    case "RP-020-det":
                        if(Convert.ToInt32(SesionUsu.Usu_Ejercicio)>=2022)
                            Reporte = "Contabilidad\\Reportes\\RP-020-det.rpt"; 
                        else
                            Reporte = "Contabilidad\\Reportes\\RP-020-det-old.rpt";

                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); report.SetParameterValue(2, Ejercicio); reporte_PDF();
                        break;
                    case "RP-020-detxls":
                        Reporte = "Contabilidad\\Reportes\\RP-020-det.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(0, mes_inicial); report.SetParameterValue(1, mes_final); report.SetParameterValue(2, Ejercicio); reporte_XLS();
                        break;
                    case "RP-021":
                        Reporte = "Contabilidad\\Reportes\\RP-021.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, cuenta_contable); report.SetParameterValue(2, mes_inicial); report.SetParameterValue(3, mes_inicial); reporte_PDF();
                        break;
                    case "RP-022":
                        Reporte = "Contabilidad\\Reportes\\RP-022.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Convert.ToString(Ejercicio)); report.SetParameterValue(1, mes_inicial); report.SetParameterValue(2, mes_final); reporte_PDF();
                        break;
                    case "RP-022xls":
                        Reporte = "Contabilidad\\Reportes\\RP-022.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Convert.ToString(Ejercicio)); report.SetParameterValue(1, mes_inicial); report.SetParameterValue(2, mes_final); reporte_XLS();
                        break;
                    case "RP-023exc":
                        Reporte = "Contabilidad\\Reportes\\RP-023exc.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, centro_contable); report.SetParameterValue(2, cuenta_mayor); report.SetParameterValue(3, mes_inicial); report.SetParameterValue(4, mes_final); report.SetParameterValue(5, nivel); reporte_XLS();
                        break;
                    case "RP-compara-06":
                        Reporte = "Contabilidad\\Reportes\\RP-compara-06.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, cuenta_contable); report.SetParameterValue(2, mes_inicial); reporte_PDF();
                        break;
                    case "RP-SAT":
                        Reporte = "Contabilidad\\Reportes\\RP-SAT.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, mes_inicial); report.SetParameterValue(2, mes_final); report.SetParameterValue(3, notas); report.SetParameterValue(4, tipo_adeudo_ini); report.SetParameterValue(5, tipo_adeudo_fin); reporte_PDF();
                        break;
                    case "RP-SATxls":
                        Reporte = "Contabilidad\\Reportes\\RP-SATxls.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, mes_inicial); report.SetParameterValue(2, mes_final); report.SetParameterValue(3, notas); report.SetParameterValue(4, tipo_adeudo_ini); report.SetParameterValue(5, tipo_adeudo_fin); reporte_XLS();
                        break;
                    case "RP-FOVISSSTE":
                        Reporte = "Contabilidad\\Reportes\\RP-FOVISSSTE.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, mes_inicial); report.SetParameterValue(2, mes_final); report.SetParameterValue(3, notas); report.SetParameterValue(4, tipo_adeudo_ini); report.SetParameterValue(5, tipo_adeudo_fin); reporte_PDF();
                        break;
                    case "RP-FOVISSSTExls":
                        Reporte = "Contabilidad\\Reportes\\RP-FOVISSSTExls.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, mes_inicial); report.SetParameterValue(2, mes_final); report.SetParameterValue(3, notas); report.SetParameterValue(4, tipo_adeudo_ini); report.SetParameterValue(5, tipo_adeudo_fin); reporte_XLS();
                        break;

                    case "RP-ISSSTE":
                        Reporte = "Contabilidad\\Reportes\\RP-ISSSTE.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, mes_inicial); report.SetParameterValue(2, mes_final); report.SetParameterValue(3, notas); report.SetParameterValue(4, tipo_adeudo_ini); report.SetParameterValue(5, tipo_adeudo_fin); reporte_PDF();
                        break;
                    case "RP-ISSSTExls":
                        Reporte = "Contabilidad\\Reportes\\RP-ISSSTExls.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, mes_inicial); report.SetParameterValue(2, mes_final); report.SetParameterValue(3, notas); report.SetParameterValue(4, tipo_adeudo_ini); report.SetParameterValue(5, tipo_adeudo_fin); reporte_XLS();
                        break;
                    case "RP-Impuestos":
                        Reporte = "Contabilidad\\Reportes\\RP-Impuestos.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, mes_inicial); report.SetParameterValue(2, mes_final); report.SetParameterValue(3, notas); report.SetParameterValue(4, tipo_adeudo_ini); report.SetParameterValue(5, tipo_adeudo_fin); reporte_PDF();
                        break;
                    case "RP-Impuestosxls":
                        Reporte = "Contabilidad\\Reportes\\RP-Impuestosxls.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio); report.SetParameterValue(1, mes_inicial); report.SetParameterValue(2, mes_final); report.SetParameterValue(3, notas); report.SetParameterValue(4, tipo_adeudo_ini); report.SetParameterValue(5, tipo_adeudo_fin); reporte_XLS();
                        break;
                    case "RP_Conciliacion":
                        Reporte = "Contabilidad\\Reportes\\RP_Concilliacion.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, id); reporte_PDF();
                        break;
                    case "RP_Conciliacion2021":
                        Reporte = "Contabilidad\\Reportes\\RP_Concilliacion_2021.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, id); reporte_PDF();
                        break;
                    case "RP_Pasivos":
                        Reporte = "Contabilidad\\Reportes\\RP_Pasivos.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, centro_contable); report.SetParameterValue(1, Ejercicio); report.SetParameterValue(2, mayor); reporte_PDF("FORMATO_"+mayor);
                        break;
                    case "RP_Pasivosxls":
                        Reporte = "Contabilidad\\Reportes\\RP_Pasivosxls.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, centro_contable); report.SetParameterValue(1, Ejercicio); report.SetParameterValue(2, mayor); reporte_XLS("FORMATO_" + mayor);
                        break;
                    case "RP_PasivosGral":
                        Reporte = "Contabilidad\\Reportes\\RP_PasivosGral.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, centro_contable); report.SetParameterValue(1, Ejercicio); reporte_PDF("FORMATO_" + mayor);
                        break;
                    case "RP_PasivosGralxls":
                        Reporte = "Contabilidad\\Reportes\\RP_PasivosGralxls.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, centro_contable); report.SetParameterValue(1, Ejercicio); reporte_XLS("FORMATO_" + mayor);
                        break;
                    case "RP_CFDIS":
                        Reporte = "Contabilidad\\Reportes\\RP_CFDIS.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, parametro1);
                        report.SetParameterValue(1, parametro2);
                        report.SetParameterValue(2, parametro3);
                        report.SetParameterValue(3, parametro4);
                        reporte_PDF();
                        break;
                    case "RP_CFDISxls":
                        
                        Reporte = "Contabilidad\\Reportes\\RP_CFDISxls.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, parametro1);
                        report.SetParameterValue(1, parametro2);
                        report.SetParameterValue(2, parametro3);
                        report.SetParameterValue(3, parametro4);
                        //reporte_CSV();
                        reporte_XLS();
                        break;
                    case "RP_Cedulas":
                        Reporte = "Contabilidad\\Reportes\\RP-Cedulas.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio);
                        report.SetParameterValue(1, mes_inicial);
                        report.SetParameterValue(2, mes_final);
                        reporte_PDF();
                        break;
                    case "RP_Cedulasxls":
                        Reporte = "Contabilidad\\Reportes\\RP-Cedulasxls.rpt";
                        reportes_dir();
                        report.SetParameterValue(0, Ejercicio);
                        report.SetParameterValue(1, mes_inicial);
                        report.SetParameterValue(2, mes_final);
                        reporte_XLS();
                        break;



                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                report.Close();
                report.Dispose();
                CR_Reportes.Dispose();
                GC.Collect();
            }
        }
        private void reportes_dir()
        {
            p = new System.Web.UI.Page();
            string Ruta = p.Server.MapPath("~") + "\\" + Reporte;
            report.Load(p.Server.MapPath("~") + "\\" + Reporte);

        }
        private void reporte_PDF()
        {
            report.PrintOptions.PaperSize = PaperSize.PaperLetter;
            connectionInfo.ServerName = "dsia";
            connectionInfo.UserID = "SAF";
            connectionInfo.Password = "DSIA2014";
            SetDBLogonForReport(connectionInfo, report);

            report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, Tipo);
            CR_Reportes.ReportSource = report;

        }

        private void reporte_PDF(string NombreReporte)
        {
            report.PrintOptions.PaperSize = PaperSize.PaperLetter;
            connectionInfo.ServerName = "dsia";
            connectionInfo.UserID = "SAF";
            connectionInfo.Password = "DSIA2014";
            SetDBLogonForReport(connectionInfo, report);

            report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, NombreReporte);
            CR_Reportes.ReportSource = report;

        }

        private void reporte_PDF_Legal()
        {
            report.PrintOptions.PaperSize = PaperSize.PaperLegal;
            connectionInfo.ServerName = "dsia";
            connectionInfo.UserID = "SAF";
            connectionInfo.Password = "DSIA2014";
            SetDBLogonForReport(connectionInfo, report);

            report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, Tipo);
            CR_Reportes.ReportSource = report;

        }
        private void reporte_XLS()
        {
            report.PrintOptions.PaperSize = PaperSize.PaperLetter;
            connectionInfo.ServerName = "dsia";
            connectionInfo.UserID = "SAF";
            connectionInfo.Password = "DSIA2014";
            SetDBLogonForReport(connectionInfo, report);
            report.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, Tipo);
            CR_Reportes.ReportSource = report;

        }

        private void reporte_XLS(string nombre_reporte)
        {
            report.PrintOptions.PaperSize = PaperSize.PaperLetter;
            connectionInfo.ServerName = "dsia";
            connectionInfo.UserID = "SAF";
            connectionInfo.Password = "DSIA2014";
            SetDBLogonForReport(connectionInfo, report);
            report.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, nombre_reporte);
            CR_Reportes.ReportSource = report;

        }

        private void reporte_GranXLS()
        {

            report.PrintOptions.PaperSize = PaperSize.PaperLetter;
            connectionInfo.ServerName = "dsia";
            connectionInfo.UserID = "SAF";
            connectionInfo.Password = "DSIA2014";
            SetDBLogonForReport(connectionInfo, report);
            report.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, Response, false, Tipo);
            CR_Reportes.ReportSource = report;
        }

        private void reporte_CSV()
        {

            report.PrintOptions.PaperSize = PaperSize.PaperLetter;
            connectionInfo.ServerName = "dsia";
            connectionInfo.UserID = "SAF";
            connectionInfo.Password = "DSIA2014";
            SetDBLogonForReport(connectionInfo, report);
            report.ExportToHttpResponse(ExportFormatType.CharacterSeparatedValues, Response, true, Tipo);
            Response.End();
        }

        private void reporte_GranXLS2()
        {
            string FileName = "ExcelExport.xlsx";
            //string directoryPath ="Contabilidad\\Reportes\\";
            //string Path = directoryPath + FileName;

            p = new System.Web.UI.Page();
            report.Load(p.Server.MapPath("~") + "\\" + Reporte);

            string Ruta = Path.Combine(Server.MapPath("~/Adjuntos"), FileName);

            report.PrintOptions.PaperSize = PaperSize.PaperLetter;
            connectionInfo.ServerName = "dsia";
            connectionInfo.UserID = "SAF";
            connectionInfo.Password = "DSIA2014";
            SetDBLogonForReport(connectionInfo, report);
            report.ExportToDisk(ExportFormatType.Excel, Ruta);
            //report.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, Response, false, Tipo);
            //CR_Reportes.ReportSource = report;

        }

        private void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            try
            {
                Tables tables = reportDocument.Database.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                {
                    TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    table.ApplyLogOnInfo(tableLogonInfo);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

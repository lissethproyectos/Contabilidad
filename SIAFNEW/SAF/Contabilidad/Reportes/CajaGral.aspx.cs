using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAF.Contabilidad.Reportes
{
    public partial class CajaGral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddlPFinal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void imgBttnPDF_Click(object sender, ImageClickEventArgs e)
        {
            string ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=RP-022&Ejercicio=" + ddlEjercicio.SelectedValue + "&mes_inicial=" + ddlPInicial.SelectedValue + "&mes_final=" + ddlPFinal.SelectedValue;
            string _open = "window.open('" + ruta + "', '_newtab');";
            //string _open = "window.open('" + ruta + "', 'miniContenedor', 'toolbar=yes', 'location=no', 'menubar=yes', 'resizable=yes');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

        }

        protected void imgBttnExcel_Click(object sender, ImageClickEventArgs e)
        {
            string ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=RP-022xls&Ejercicio=" + ddlEjercicio.SelectedValue + "&mes_inicial=" + ddlPInicial.SelectedValue + "&mes_final=" + ddlPFinal.SelectedValue;
            string _open = "window.open('" + ruta + "', '_newtab');";
            //string _open = "window.open('" + ruta + "', 'miniContenedor', 'toolbar=yes', 'location=no', 'menubar=yes', 'resizable=yes');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

        }
    }
}
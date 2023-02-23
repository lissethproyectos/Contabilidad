using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAF.Contabilidad.Form
{
    public partial class frmUsuarios : System.Web.UI.Page
    {
        #region <Variables>
        string Verificador = string.Empty;
        string VerificadorDet = string.Empty;

        Sesion SesionUsu = new Sesion();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SesionUsu = (Sesion)Session["Usuario"];
            if (!IsPostBack)
                Inicializar();
            else
            {
            }
        }
        private void Inicializar()
        {

        }
    }
}
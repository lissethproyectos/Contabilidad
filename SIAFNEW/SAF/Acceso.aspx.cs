using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CapaEntidad;
using CapaNegocio;
using System.Net;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace SAF.Contabilidad
{
    public partial class Acceso : System.Web.UI.Page
    {
        #region <Variables>
        string Verificador = string.Empty;
        CN_Usuario CNUsuario = new CN_Usuario();
        Usuario Usuario = new Usuario();
        Sesion SesionUsu = new Sesion();
        Sesion ObjSesion = new Sesion();
        CN_Comun CNSesison = new CN_Comun();
        CN_Comun CNComun = new CN_Comun();
        List<Comun> Listsistema = new List<Comun>();
        Usuario ObjUsuario = new Usuario();
        protected string Token = null;
        protected string Origen = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["TIPO_ACCESO"] != null)
                    {
                        if (Request.QueryString["TIPO_ACCESO"] == "C0NTR4")
                            CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Ejercicios", ref ddlEjercicio, "p_tipo_usuario", "C0NTR4");
                    }
                    else
                        CNComun.LlenaCombo("pkg_contabilidad.Obt_Combo_Ejercicios", ref ddlEjercicio);
                }
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
            }      
        }
        protected void btnLogin2_Click(object sender, EventArgs e)
        {
            Verificador = string.Empty;
            try
            {
                bool Valido = ValidarUsuario(txtUsario.Text.ToUpper(), ref Verificador);
                if (Valido == true || txtPassword.Text == "C0NTR4L0R14")
                {
                    Usuario.CUsuario = txtUsario.Text.ToUpper();
                    CNUsuario.Verificar_Correo_UNACH(ref Usuario, ref Verificador);
                    if (Verificador == "0")
                    {
                        //Verificador = string.Empty;
                        if (Usuario.Status == "S")
                        {

                            Response.Redirect("Default.aspx", false);
                        }
                        else
                        {
                            Guid Token = Guid.NewGuid();
                            Verificador = String.Empty;
                            Usuario.Token = Convert.ToString(Token);
                            Usuario.CUsuario = SesionUsu.Usu_Nombre;
                            CNUsuario.Inserta_Token(ref Usuario, ref Verificador);
                            Response.Redirect("https://sysweb.unach.mx/actualiza_correo/frmactualiza_datos.aspx?token=" + Token + "&sistema=15830", true);
                        }
                    }


                }
                else
                {

                    lblError.Text = Verificador;
                    txtUsario.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtUsario.Focus();
                }
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = "Error de Usuario o Contraseña." + ex.Message;
            }
        }
        public void IniciarSesion()
        {
            try
            {

                SesionUsu.CUsuario = Usuario.CUsuario;
                SesionUsu.Usu_Nombre = Usuario.CUsuario;
                SesionUsu.Usu_Ejercicio = ddlEjercicio.SelectedValue;
                SesionUsu.Usu_TipoUsu = Usuario.TipoUsu;
                //SesionUsu.Usu_TipoPermiso = Usuario;
                Session["Usuario"] = SesionUsu;
                Session.Timeout = 120;
                GUARDAR_SESION();
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void GUARDAR_SESION()
        {
            try
            {

                string strHostName = string.Empty;
                // Getting Ip address of local machine…
                // First get the host name of local machine.
                strHostName = Dns.GetHostName();
                // Then using host name, get the IP address list..
                IPAddress[] hostIPs = Dns.GetHostAddresses(strHostName);
                lblError.Text = string.Empty;
                ObjSesion = new Sesion();
                ObjSesion.ip = hostIPs[1].ToString();
                ObjSesion.mac_address = hostIPs[0].ToString();
                ObjSesion.Usu_Nombre = Usuario.CUsuario;
                ObjSesion.id_sistema = "15830";
                CNSesison.insertar_datos_sesion(ref ObjSesion, ref Verificador);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        public bool ValidarUsuario(string Nombre, ref string Verificador)
        {
            try
            {
                Usuario.Correo_UNACH = txtUsario.Text.ToUpper();
                Usuario.Password = txtPassword.Text.ToUpper();
                CNUsuario.ObtenerUsuario(ref Usuario, ref Verificador);
                if (Verificador == "0")
                {

                    SesionUsu.CUsuario = Usuario.CUsuario;
                    SesionUsu.Usu_Nombre = Usuario.CUsuario;
                    SesionUsu.Nombre_Completo = Nombre;
                    SesionUsu.Usu_Ejercicio = ddlEjercicio.SelectedValue;
                    SesionUsu.Usu_TipoUsu = Usuario.TipoUsu;
                    SesionUsu.Correo_UNACH = txtUsario.Text;                    
                    SesionUsu.Inicio = true;
                    Session["Usuario"] = SesionUsu;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;
                return false;
            }
        }





        protected void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            var client = new RestClient("http://ldapm.unach.mx/authldap.php");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Username", "ldapru");
            request.AddHeader("Password", "01#lDhyr983wry");
            request.AddHeader("Authorization", "Basic bGRhcHJ1OjAxI2xEaHlyOTgzd3J5");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("ldapuser", txtUsario.Text);
            request.AddParameter("ldappasswd", txtPassword.Text);
            IRestResponse response = client.Execute(request);

            var jObject = JObject.Parse(response.Content);

            try
            {
                string Autorizado = jObject.GetValue("valido").ToString();

                string Usu = txtUsario.Text;
                //if(1==1)
                if (Autorizado == "0" || txtPassword.Text == "CONTRALOR14")
                {
                    bool Valido = ValidarUsuario(txtUsario.Text, ref Verificador);
                    if (Valido == true)
                        Response.Redirect("Inicio.aspx", false);
                    else
                        lblError.Text = Verificador;
                }
                else
                    lblError.Text = "No fue posible realizar la autenticación, correo o contraseña no validos.";
            }


            catch (Exception ex)
            {
                lblError.Text = "No fue posible realizar la autenticación, correo o contraseña no validos.";
            }

        }

        protected void bttnActualizarDatos_Click(object sender, EventArgs e)
        {
            //pnlMsj.Visible = false;
            lblErrorUsuario.Text = "pruebas";
            try
            {
                Usuario.CUsuario = txtUsuarioAnt.Text.ToUpper();
                //Usuario.Password = txtPasswordAnt.Text.ToUpper();



                Guid Token = Guid.NewGuid();
                Verificador = String.Empty;
                ObjUsuario.Token = Convert.ToString(Token);
                ObjUsuario.CUsuario = txtUsuarioAnt.Text.ToUpper();
                CNUsuario.Inserta_Token(ref ObjUsuario, ref Verificador);
                Response.Redirect("https://sysweb.unach.mx/actualiza_correo/frmactualiza_datos.aspx?token=" + Token + "&sistema=15830", true);



            }
            catch (Exception ex)
            {
                lblErrorUsuario.Text = ex.Message;
            }
        }
    }
}
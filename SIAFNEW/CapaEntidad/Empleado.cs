using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Empleado
    {
        private string _Nombre;
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private string _Correo_UNACH;
        public string Correo_UNACH
        {
            get { return _Correo_UNACH; }
            set { _Correo_UNACH = value; }
        }

        private string _Paterno;
        public string Paterno
        {
            get { return _Paterno; }
            set { _Paterno = value; }
        }

        private string _Materno=string.Empty;
        public string Materno
        {
            get { return _Materno; }
            set { _Materno = value; }
        }

        private string _Dependencia;
        public string Dependencia
        {
            get { return _Dependencia; }
            set { _Dependencia = value; }
        }

        private string _Nomina;
        public string Nomina
        {
            get { return _Nomina; }
            set { _Nomina = value; }
        }

        private string _Tipo_Personal;
        public string Tipo_Personal
        {
            get { return _Tipo_Personal; }
            set { _Tipo_Personal = value; }
        }

        private string _Numero_Plaza;
        public string Numero_Plaza
        {
            get { return _Numero_Plaza; }
            set { _Numero_Plaza = value; }
        }
    }
}

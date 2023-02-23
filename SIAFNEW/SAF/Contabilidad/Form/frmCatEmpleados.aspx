<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCatEmpleados.aspx.cs" Inherits="SAF.Contabilidad.Form.frmCatEmpleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/DataTables/jquery.dataTables.min.js"></script>
    <link href="../../Content/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-2">
                Tipo de Personal
            </div>
            <div class="col-md-3">
                Nombre
            </div>
            <div class="col-md-3">
                Primer Apellido
            </div>
            <div class="col-md-3">
                Segundo Apellido
            </div>
            <div class="col-md-1">
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:DropDownList ID="ddlTipoPersonal" runat="server">
                    <asp:ListItem Value="T">--TODOS--</asp:ListItem>
                    <asp:ListItem Value="D">DOCENTES</asp:ListItem>
                    <asp:ListItem Value="A">ADMINISTRATIVOS</asp:ListItem>
                    <asp:ListItem Value="C">CONFIANZA</asp:ListItem>
                    <asp:ListItem Value="H">HONORARIOS</asp:ListItem>
                    <asp:ListItem Value="U">TURNEROS</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtNombre" runat="server" Width="100%" placeholder="Nombre(s)" ControlToValidate="txtNombre"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqNombre" runat="server" ErrorMessage="*Nombre requerido" Text="* Requerido" ValidationGroup="buscaEmpleado" ControlToValidate="txtNombre"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="*Minimo 3 letras" ClientValidationFunction="VerificarCantidad" ControlToValidate="txtNombre" ValidationGroup="buscaEmpleado"></asp:CustomValidator>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtPaterno" runat="server" Width="100%" placeholder="Primer Apellido"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqPaterno" runat="server" ErrorMessage="*Primer apellido requerido" Text="* Requerido" ValidationGroup="buscaEmpleado" ControlToValidate="txtPaterno"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="*Minimo 3 letras" ClientValidationFunction="VerificarCantidad" ControlToValidate="txtPaterno" ValidationGroup="buscaEmpleado"></asp:CustomValidator>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtMaterno" runat="server" Width="100%" placeholder="Segundo Apellido"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <asp:UpdatePanel ID="updPnlBuscarEmp" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="linkBttnBuscarEmpleado" runat="server" CssClass="btn btn-primary" OnClick="linkBttnBuscarEmpleado_Click" ValidationGroup="buscaEmpleado" Width="100%"><i class="fa fa-search"></i> Buscar</asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrBuscarEmp" runat="server"
                    AssociatedUpdatePanelID="updPnlBuscarEmp">
                    <ProgressTemplate>
                        <asp:Image ID="imgBuscarEmp" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                            ToolTip="Espere un momento, por favor.." />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrCatEmp" runat="server"
                    AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <asp:Image ID="imgCatEmp" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                            ToolTip="Espere un momento, por favor.." />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdCatEmpleados" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="mGrid">
                            <Columns>
                                <asp:BoundField DataField="TIPO_PERSONAL" HeaderText="TIPO DE PERSONAL">
                                    <ItemStyle Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DEPENDENCIA" HeaderText="DEPENDENCIA" />
                                <asp:BoundField DataField="NUMERO_PLAZA" HeaderText="# PLAZA" />
                                <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRE" />
                                <asp:BoundField DataField="Correo_UNACH" HeaderText="CORREO" />
                                <asp:BoundField DataField="NOMINA" HeaderText="ÚLTIMO PAGO (AÑO/MES)" />
                            </Columns>
                            <FooterStyle CssClass="enc" />
                            <HeaderStyle CssClass="enc" />
                            <PagerStyle CssClass="enc" HorizontalAlign="Center" />
                            <SelectedRowStyle CssClass="sel" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript">        
        function Empleados() {
            $('#<%= grdCatEmpleados.ClientID %>').prepend($("<thead></thead>").append($('#<%= grdCatEmpleados.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "stateSave": true
            });


        }
        function VerificarCantidad(sender, args) {
            args.IsValid = (args.Value.length >= 3);
        }
    </script>
</asp:Content>

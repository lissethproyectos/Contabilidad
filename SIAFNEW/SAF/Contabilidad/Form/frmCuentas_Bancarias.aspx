<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCuentas_Bancarias.aspx.cs" Inherits="SAF.Contabilidad.Form.frmCuentas_Bancarias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/DataTables/jquery.dataTables.min.js"></script>
    <link href="../../Content/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-md-2">
                                            Centro Contable
                                        </div>
                                        <div class="col-md-10">
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlCentros_Contables0" runat="server" AutoPostBack="True" Width="100%" OnSelectedIndexChanged="ddlCentros_Contables0_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                        ControlToValidate="ddlCentros_Contables0" ErrorMessage="*Requerido"
                                                        InitialValue="00000" ValidationGroup="CuentaBancaria"></asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            Dependencia
                                        </div>
                                        <div class="col-md-10">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlDependencia0" runat="server" Width="100%" OnSelectedIndexChanged="ddlDependencia0_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <%-- <div class="col-md-1">
                                            <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="https://sysweb.unach.mx/resources/imagenes/nuevo.png" OnClick="btnNuevo_Click" />
                                        </div>--%>
                                    </div>
                                    <%--<div class="row">
                                        <div class="col-md-11">
                                            <asp:TextBox ID="txtBuscar" runat="server" Width="100%"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:ImageButton ID="imgbtnBuscar1" runat="server" CausesValidation="False" ImageUrl="http://sysweb.unach.mx/resources/imagenes/buscar.png" OnClick="imgbtnBuscar1_Click" Style="text-align: right" title="Buscar" ValidationGroup="Buscar" />
                                        </div>
                                    </div>--%>
                                    <div class="row">
                                        <div class="col text-center">
                                            <asp:UpdateProgress ID="UpdateProgress6" runat="server"
                                                AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <asp:Image ID="Image8" runat="server"
                                                        AlternateText="Espere un momento, por favor.." Height="50px"
                                                        ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                        ToolTip="Espere un momento, por favor.." Width="50px" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grvCuentas_Bancarias" runat="server"
                                                        AutoGenerateColumns="False" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                                        GridLines="Vertical"
                                                        OnPageIndexChanging="grvCuentas_Bancarias_PageIndexChanging"
                                                        OnSelectedIndexChanged="grvCuentas_Bancarias_SelectedIndexChanged" CssClass="mGrid" OnRowDeleting="grvCuentas_Bancarias_RowDeleting" Width="100%" ShowHeaderWhenEmpty="True">
                                                        <Columns>
                                                            <asp:BoundField DataField="IdCuenta_Bancaria" HeaderText="Id" />
                                                            <asp:BoundField DataField="EJERCICIO" HeaderText="Ejercicio" />
                                                            <asp:BoundField DataField="CENTRO_CONTABLE" HeaderText="Contab">
                                                                <ItemStyle BackColor="#3366FF" Font-Bold="True" ForeColor="White" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DEPENDENCIA" HeaderText="Depcia">
                                                                <ItemStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CLAVE" HeaderText="Clave" />
                                                            <asp:BoundField DataField="BANCO" HeaderText="Banco" />
                                                            <asp:BoundField DataField="CUENTA_BANCARIA" HeaderText="Cuenta Bancaria" />
                                                            <asp:BoundField DataField="CUENTA_CONTABLE" HeaderText="Cuenta Contable" />
                                                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripcion" />
                                                            <asp:BoundField DataField="FECHA_APERTURA" HeaderText="Fecha Apertura" />
                                                            <asp:CommandField SelectText="Editar" ShowSelectButton="True" />
                                                            <asp:TemplateField ShowHeader="False">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="linkBttnNuevo" runat="server" CssClass="btn btn-primary" OnClick="linkBttnNuevo_Click">Nuevo</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('¿Desea eliminar la cuenta?');" Text="Eliminar"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="linBttnClonar" runat="server" OnClick="linBttnClonar_Click">Clonar</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle CssClass="enc" />
                                                        <PagerStyle CssClass="enc" HorizontalAlign="Center" />
                                                        <SelectedRowStyle CssClass="sel" />
                                                        <HeaderStyle CssClass="enc" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col text-center">
                                            <asp:UpdateProgress ID="updPgrEjercicio" runat="server" AssociatedUpdatePanelID="updPnlEjercicio">
                                                <ProgressTemplate>
                                                    <asp:Image ID="img1" runat="server" AlternateText="Espere un momento, por favor.." Height="50px" ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif" ToolTip="Espere un momento, por favor.." Width="50px" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            Ejercicio
                                        </div>
                                        <div class="col-md-10">
                                            <asp:UpdatePanel ID="updPnlEjercicio" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlEjercicio" runat="server" OnSelectedIndexChanged="ddlEjercicio_SelectedIndexChanged" AutoPostBack="True">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            Centro Contable
                                        </div>
                                        <div class="col-md-9">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlCentros_Contables" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCentros_Contables_SelectedIndexChanged" Width="100%">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqCentrosContab" runat="server" ControlToValidate="ddlCentros_Contables" ErrorMessage="* Centros Contable" InitialValue="00000" ValidationGroup="GuardaCuentaBancaria">*Requerido</asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel5">
                                                <ProgressTemplate>
                                                    <asp:Image ID="Image7" runat="server" AlternateText="Espere un momento, por favor.." Height="50px" ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif" ToolTip="Espere un momento, por favor.." Width="50px" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            Dependencia
                                        </div>
                                        <div class="col-md-9">
                                            <asp:UpdatePanel ID="updPnlDepcia" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlDependencia" runat="server" Width="100%"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqDepcia" runat="server" ControlToValidate="ddlDependencia" ErrorMessage="* Dependencia" InitialValue="00000" ValidationGroup="GuardaCuentaBancaria">*Requerido</asp:RequiredFieldValidator>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:UpdateProgress ID="updPgrDepcia" runat="server" AssociatedUpdatePanelID="updPnlDepcia">
                                                <ProgressTemplate>
                                                    <asp:Image ID="imgDepcia" runat="server" AlternateText="Espere un momento, por favor.." Height="50px" ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif" ToolTip="Espere un momento, por favor.." Width="50px" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">Tipo Subsidio</div>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddlSubsidio" runat="server">
                                                <asp:ListItem Value="X">--SELECCIONAR--</asp:ListItem>
                                                <asp:ListItem Value="F">FEDERAL</asp:ListItem>
                                                <asp:ListItem Value="E">ESTATAL</asp:ListItem>
                                                <asp:ListItem Value="I">INGRESOS PROPIOS</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:RequiredFieldValidator ID="reqSubsidio" runat="server" ControlToValidate="ddlSubsidio" ErrorMessage="* Subsidio" InitialValue="X" ValidationGroup="GuardaCuentaBancaria">*Requerido</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">Cuenta Contable</div>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddlCuentas_Contables" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlCuentas_Contables" ErrorMessage="RequiredFieldValidator" InitialValue="0" ValidationGroup="GuardaCuentaBancaria">*Requerido</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">Clave</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtClave" runat="server" MaxLength="5" Enabled="False"></asp:TextBox>
                                        </div>                                        
                                        <div class="col-md-2">Banco</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlBancos" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">Cuenta Bancaria</div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtCuenta_Bancaria" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                ControlToValidate="txtCuenta_Bancaria" ErrorMessage="RequiredFieldValidator"
                                                ValidationGroup="GuardaCuentaBancaria">*Requerido</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-1">Descripción</div>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="txtDescripcion" runat="server" Width="100%"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                ControlToValidate="txtDescripcion" ErrorMessage="RequiredFieldValidator"
                                                ValidationGroup="GuardaCuentaBancaria">*Requerido</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">Localidad</div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtLocalidad" runat="server" Width="100%"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                ControlToValidate="txtLocalidad" ErrorMessage="RequiredFieldValidator"
                                                ValidationGroup="GuardaCuentaBancaria">*Requerido</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">Fecha Apertura</div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtFecha_Apertura" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderFApertura" runat="server" TargetControlID="txtFecha_Apertura" PopupButtonID="imgCalendarioApertura" BehaviorID="_content_CalendarExtenderApertura" />
                                            <asp:ImageButton ID="imgCalendarioApertura" runat="server" ImageUrl="https://sysweb.unach.mx/resources/imagenes/calendario.gif" />
                                        </div>
                                        <div class="col-md-2">Status</div>
                                        <div class="col-md-4">
                                            <asp:RadioButtonList ID="rdoBttnStatus" runat="server"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="A">Activo</asp:ListItem>
                                                <asp:ListItem Value="B">Baja</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col text-right">
                                            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-blue-grey" Height="30px" OnClick="btnCancelar_Click" Text="Cancelar" Width="80px" />
                                            &nbsp;<asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Height="30px" OnClick="btnGuardar_Click" Text="Guardar" ValidationGroup="GuardaCuentaBancaria" Width="80px" />
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function Ctas_Bancarias() {

            $('#<%= grvCuentas_Bancarias.ClientID %>').prepend($("<thead></thead>").append($('#<%= grvCuentas_Bancarias.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "stateSave": true,
                "ordering": false
            });

        }
    </script>

</asp:Content>

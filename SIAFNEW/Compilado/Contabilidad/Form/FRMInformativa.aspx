<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FRMInformativa.aspx.cs" Inherits="SAF.Contabilidad.Form.FRMInformativa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1 {
        }

        .style2 {
            width: 166px;
            height: 26px;
        }

        .style3 {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel100" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                Centro Contable
            </div>
            <div class="col-md-10">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="DDLCentro_Contable" runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="UpdateProgress3" runat="server"
                    AssociatedUpdatePanelID="UpdatePanel7">
                    <ProgressTemplate>
                        <asp:Image ID="Image1q0" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtbusca" runat="server" Width="100%"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            
                                                    <asp:ImageButton ID="BTNbuscar" runat="server" class="" Height="41px"
                                                        ImageUrl="https://sysweb.unach.mx/resources/imagenes/buscar.png" OnClick="BTNbuscar_Click" Width="40px" />
                                               
                                        </div>
                                        <%--<div class="col-md-1">
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server"
                                                AssociatedUpdatePanelID="UpdatePanel6">
                                                <ProgressTemplate>
                                                    <asp:Image ID="Image1q" runat="server"
                                                        AlternateText="Espere un momento, por favor.." Height="50px"
                                                        ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                        ToolTip="Espere un momento, por favor.." Width="50px" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>--%>
                                        <div class="col-md-2">
                                            <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="https://sysweb.unach.mx/resources/imagenes/nuevo.png" OnClick="btnNuevo_Click" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdinformativa" runat="server" AutoGenerateColumns="False"
                                                        BorderStyle="None" BorderWidth="1px"
                                                        CellPadding="4" GridLines="Vertical" Width="100%"
                                                        OnSelectedIndexChanged="grdinformativa_SelectedIndexChanged" CssClass="mGrid" EmptyDataText="No hay registros para mostrar">
                                                        <Columns>
                                                            <asp:BoundField DataField="id" HeaderText="ID" />
                                                            <asp:BoundField DataField="centro_contable" HeaderText="CENTRO CONTABLE" />
                                                            <asp:BoundField DataField="observaciones" HeaderText="DESCRIPCIÓN" />
                                                            <asp:BoundField DataField="status" HeaderText="STATUS" />
                                                            <asp:BoundField DataField="fecha_inicial" HeaderText="FECHA INICIO" />
                                                            <asp:BoundField DataField="fecha_final" HeaderText="FECHA TERMINO" />
                                                            <asp:CommandField SelectText="Editar" ShowSelectButton="True" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"
                                                                        OnClientClick="return confirm('¿En realidad desea Eliminar este registro?');">Eliminar</asp:LinkButton>
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
                                <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>--%>
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-2">
                                            Descripción
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtobservacion" runat="server" Height="118px" Width="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            Fecha Inicial
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtFecha_inicial" runat="server" AutoPostBack="True"
                                                CssClass="box" Width="95px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFecha_inicial" PopupButtonID="imgCalendarioIni" BehaviorID="_content_CalendarExtenderFecha" />
                                            <asp:ImageButton ID="imgCalendarioIni" runat="server" ImageUrl="https://sysweb.unach.mx/resources/imagenes/calendario.gif" />

                                        </div>
                                        <div class="col-md-2">
                                            Fecha Termino
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtFecha_final" runat="server" AutoPostBack="True"
                                                CssClass="box" Width="95px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderFecha" runat="server" TargetControlID="txtFecha_final" PopupButtonID="imgCalendario" BehaviorID="_content_CalendarExtenderFechaFin" />
                                            <asp:ImageButton ID="imgCalendario" runat="server" ImageUrl="https://sysweb.unach.mx/resources/imagenes/calendario.gif" />

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            Status
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddl_status" runat="server">
                                                <asp:ListItem Value="A">ACTIVA</asp:ListItem>
                                                <asp:ListItem Value="B">BAJA</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col text-center">
                                            <%--  <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>--%>
                                            <asp:Button ID="BTN_Guardar" runat="server" CssClass="btn btn-primary" OnClick="BTN_Guardar_Click" Text="Guardar" />
                                            &nbsp;
                                            <asp:Button ID="BTN_Cancelar" runat="server" CssClass="btn btn-blue-grey" OnClick="BTN_Cancelar_Click" Text="Cancelar" />
                                            <%-- </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                    </div>
                                </div>
                                <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                            </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

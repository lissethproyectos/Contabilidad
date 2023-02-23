<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="SAF.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 150px;
            height: 140px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-3">
            <div class="card">
                <div class="card-body" style="height: 320px">
                    <h5 class="card-title">
                        <img src="images/presupuesto.png" class="auto-style2" />Movimientos</h5>
                    <p class="card-text">Registro y seguimiento del gasto realizado de acuerdo con el presupuesto de egresos asignado.</p>
                </div>
                <div class="card-footer text-muted">
                    <a href="Default.aspx?mnu=MOV" class="btn btn-warning btn-rounded">Continuar</a>
                </div>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="card">
                <div class="scroll_monitor">
                    <div class="card-body">
                        <%--<h5 class="card-title">--%>
                        <%--<img src="images/pres7.png" class="auto-style2" />Dashboard</h5>--%>
                        <div class="text-center">
                            <asp:UpdateProgress ID="updPnlCC" runat="server" AssociatedUpdatePanelID="UpdatePanel11">
                                <ProgressTemplate>
                                    <asp:Image ID="img5" runat="server" AlternateText="Espere un momento, por favor.." Height="50px" ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif" ToolTip="Espere un momento, por favor.." />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="DDLCentro_Contable" runat="server" Width="100%"
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="DDLCentro_Contable_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grvMonitorCont" runat="server"
                                    BorderStyle="None" CellPadding="4" Width="100%"
                                    GridLines="Vertical" AutoGenerateColumns="False" OnPageIndexChanging="grvMonitorCont_PageIndexChanging"
                                    PageSize="15" CssClass="mGrid" EmptyDataText="Sin ninguna incidencia">
                                    <Columns>
                                        <asp:BoundField DataField="Descripcion" HeaderText="Verificar" />
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
                <div class="card-footer text-muted"><a href="Contabilidad/Form/frmMonitor.aspx" class="btn btn-warning btn-rounded">Continuar</a></div>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="card">
                <div class="card-body" style="height: 320px">
                    <h5 class="card-title">
                        <img src="images/pres6.png" class="auto-style2" />
                        Reportes

                    </h5>
                    <p class="card-text">
                        Balanza de comprobación, anexo de cuentas de balance, mayor auxiliar, entre otros.
                    </p>
                </div>
                <div class="card-footer text-muted">
                    <a href="Default.aspx?mnu=REP" class="btn btn-warning btn-rounded">Continuar</a>
                </div>
            </div>
        </div>
    </div>
    <br />

    <%--<div class="container-fluid">
        <div class="row">
            <div class="col">
                <div class="alert alert-warning text-center">
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>--%>
    <asp:HiddenField ID="HiddenField1" runat="server" />

    <%-- <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="HiddenField1" PopupControlID="Panel3" BackgroundCssClass="modalBackground_Proy">
    </ajaxToolkit:ModalPopupExtender>--%>
    <%--<asp:Panel ID="Panel3" runat="server" class="card text-white bg-dark mb-3" Style="width: 50%; font-size: 16px">
        <div style="overflow-y: scroll; height: 95%;">
            <div class="card-header">
                AVISO
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col">
                        <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblMsg_Observaciones" runat="server" Text="Una mentalidad positiva te ayuda a triunfar. Piensa bien, para vivir mejor" Font-Size="12px"></asp:Label>
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row">
                    <div class="col text-center">
                        <asp:Button ID="btnCancelar" runat="server" Text="Continuar" CssClass="btn btn-primary btn-rounded" />

                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>--%>




    <div class="modal fade" id="modalAviso" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modOficios">Aviso</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col">
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblMsg_Observaciones" runat="server" Text="Una mentalidad positiva te ayuda a triunfar. Piensa bien, para vivir mejor" Font-Size="12px"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

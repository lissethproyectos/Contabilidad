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
        <div class="col-sm-4">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">
                        <img src="images/presupuesto.png" class="auto-style2" />Movimientos</h5>
                    <p class="card-text">Registro y seguimiento del gasto realizado de acuerdo con el presupuesto de egresos asignado.</p>
                    <a href="Default.aspx?mnu=MOV" class="btn btn-warning btn-rounded">Continuar</a>
                </div>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">
                        <img src="images/pres6.png" class="auto-style2" />
                        Reportes

                    </h5>
                    <p class="card-text">
                        Balanza de comprobación, anexo de cuentas de balance, mayor auxiliar, entre otros.
                        <br />


                        <br />
                    </p>
                    <a href="Default.aspx?mnu=REP" class="btn btn-warning btn-rounded">Continuar</a>
                </div>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">
                        <img src="images/pres7.png" class="auto-style2" />Dashboard</h5>

                    <p class="card-text">Proximamente, representación gráfica de las principales métricas de contabilidad.</p>
                    <br />
                    <a href="Contabilidad/Form/frmDashboard.aspx" class="btn btn-warning btn-rounded">Continuar</a>
                </div>
            </div>
        </div>
    </div>
    <br />

    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <div class="alert alert-warning text-center">
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>
        <asp:HiddenField ID="HiddenField1" runat="server" />

        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="HiddenField1" PopupControlID="Panel3" BackgroundCssClass="modalBackground_Proy">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="Panel3" runat="server" class="card text-white bg-dark mb-3" style="width:50%; font-size:15px">
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
    </asp:Panel>

</asp:Content>

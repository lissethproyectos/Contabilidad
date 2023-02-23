<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CajaGral.aspx.cs" Inherits="SAF.Contabilidad.Reportes.CajaGral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-2">
                Ejercicio
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlEjercicio" runat="server">
                    <asp:ListItem Value="20">2020</asp:ListItem>
                    <asp:ListItem Value="19">2019</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                Periodo Inicial
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlPInicial" runat="server">
                    <asp:ListItem Value="01">Enero</asp:ListItem>
                    <asp:ListItem Value="02">Febrero</asp:ListItem>
                    <asp:ListItem Value="03">Marzo</asp:ListItem>
                    <asp:ListItem Value="04">Abril</asp:ListItem>
                    <asp:ListItem Value="05">Mayo</asp:ListItem>
                    <asp:ListItem Value="06">Junio</asp:ListItem>
                    <asp:ListItem Value="07">Julio</asp:ListItem>
                    <asp:ListItem Value="08">Agosto</asp:ListItem>
                    <asp:ListItem Value="09">Septiembre</asp:ListItem>
                    <asp:ListItem Value="10">Octubre</asp:ListItem>
                    <asp:ListItem Value="11">Noviembre</asp:ListItem>
                    <asp:ListItem Value="12">Diciembre</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                Periodo Final
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlPFinal" runat="server" Width="100%">
                    <asp:ListItem Value="01">Enero</asp:ListItem>
                    <asp:ListItem Value="02">Febrero</asp:ListItem>
                    <asp:ListItem Value="03">Marzo</asp:ListItem>
                    <asp:ListItem Value="04">Abril</asp:ListItem>
                    <asp:ListItem Value="05">Mayo</asp:ListItem>
                    <asp:ListItem Value="06">Junio</asp:ListItem>
                    <asp:ListItem Value="07">Julio</asp:ListItem>
                    <asp:ListItem Value="08">Agosto</asp:ListItem>
                    <asp:ListItem Value="09">Septiembre</asp:ListItem>
                    <asp:ListItem Value="10">Octubre</asp:ListItem>
                    <asp:ListItem Value="11">Noviembre</asp:ListItem>
                    <asp:ListItem Value="12">Diciembre</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col text-right">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        &nbsp;<asp:ImageButton ID="imgBttnPDF" runat="server"
                            ImageUrl="http://sysweb.unach.mx/resources/imagenes/pdf.png" title="Reporte PDF" OnClick="imgBttnPDF_Click" />
                        <asp:ImageButton ID="imgBttnExcel" runat="server"
                            ImageUrl="http://sysweb.unach.mx/resources/imagenes/excel.png" title="Reporte Excel" OnClick="imgBttnExcel_Click" />

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPrgReporte" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <asp:Image ID="imgReporte" runat="server" ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif" AlternateText="Espere un momento, por favor.."
                            ToolTip="Espere un momento, por favor.." Width="50px" Height="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdateProgress ID="updPrgBuscar" runat="server" AssociatedUpdatePanelID="updPnlRep">
                    <ProgressTemplate>
                        <asp:Image ID="imgBuscar" runat="server" ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif" AlternateText="Espere un momento, por favor.."
                            ToolTip="Espere un momento, por favor.." Width="50px" Height="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdatePanel ID="updPnlRep" runat="server">
                    <ContentTemplate>
                        <iframe id="miniContenedor" frameborder="0" marginheight="0" marginwidth="0" name="miniContenedor"
                            style="height: 500px" width="100%"></iframe>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

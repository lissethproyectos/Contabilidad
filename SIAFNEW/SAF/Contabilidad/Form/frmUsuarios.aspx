<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmUsuarios.aspx.cs" Inherits="SAF.Contabilidad.Form.frmUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col">
                <h4>Usuarios centrales</h4>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdUsuarios" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="Cve" />
                                <asp:BoundField HeaderText="Usuario" />
                                <asp:BoundField HeaderText="Correo UNACH" />
                                <asp:CommandField ShowSelectButton="True" />
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
</asp:Content>

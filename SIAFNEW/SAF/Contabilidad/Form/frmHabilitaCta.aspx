<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmHabilitaCta.aspx.cs" Inherits="SAF.Contabilidad.Form.frmHabilitaCta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/DataTables/jquery.dataTables.min.js"></script>
    <link href="../../Content/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrDisponibles" runat="server"
                    AssociatedUpdatePanelID="updPnlDisponibles">
                    <ProgressTemplate>
                        <asp:Image ID="imgDisponibles" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrAsignados" runat="server"
                    AssociatedUpdatePanelID="updPnlAsignados">
                    <ProgressTemplate>
                        <asp:Image ID="imgAsignados" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="50px"
                            ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                            ToolTip="Espere un momento, por favor.." Width="50px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6"><h5>CC cerrados para cta 1123</h5></div>
            <div class="col-md-6"><h5>CC aperturados para cta 1123</h5></div>
            </div>
        <div class="row">
            <div class="col-md-6">
                <asp:UpdatePanel ID="updPnlDisponibles" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdCCDisponibles" runat="server" CssClass="mGrid" AutoGenerateColumns="False" EmptyDataText="No hay centros contables disponibles." ShowHeaderWhenEmpty="True" Width="100%" OnSelectedIndexChanged="grdCCDisponibles_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="Centro_Contable" HeaderText="Cve" />
                                <asp:BoundField DataField="Desc_Centro_Contable" HeaderText="Centro Contable" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar" CssClass="btn btn-primary">Agregar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="enc" />
                            <HeaderStyle CssClass="enc" />
                            <PagerStyle CssClass="enc" HorizontalAlign="Center" />
                            <SelectedRowStyle CssClass="sel" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
            <div class="col-md-6">
                <asp:UpdatePanel ID="updPnlAsignados" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdCCAsignados" runat="server" AutoGenerateColumns="False" CssClass="mGrid" EmptyDataText="No hay centros contables asignados." Width="100%" OnSelectedIndexChanged="grdCCAsignados_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttnEliminar" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar" CssClass="btn btn-danger">Eliminar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Centro_Contable" HeaderText="Cve" />
                                <asp:BoundField DataField="Desc_Centro_Contable" HeaderText="Centro Contable" />
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
        function MayorDisponibles() {
            $('#<%= grdCCDisponibles.ClientID %>').prepend($("<thead></thead>").append($('#<%= grdCCDisponibles.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "stateSave": true,
                "columns": [
                    null,
                    null,
                    null
                ]
            })
        }
        function MayorAsignados() {
            $('#<%= grdCCAsignados.ClientID %>').prepend($("<thead></thead>").append($('#<%= grdCCAsignados.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "stateSave": true,
                "columns": [
                    null,
                    null,
                    null
                ]
            })
        }

    </script>
</asp:Content>

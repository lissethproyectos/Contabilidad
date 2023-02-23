<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmGenPolizas.aspx.cs" Inherits="SAF.Contabilidad.Form.frmGenPolizas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/DataTables/jquery.dataTables.min.js"></script>
    <link href="../../Content/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <h4>
                    <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></h4>
            </div>
        </div>
        <div class="row">
            <div class="col-md-1">
                Tipo
            </div>
            <div class="col-md-3">
                <asp:UpdatePanel ID="updPnlTipo" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                            <asp:ListItem Value="01">Pólizas de Ingresos</asp:ListItem>
                            <asp:ListItem Value="02">Pólizas de Rendimientos</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-1">
                Mes
            </div>
            <div class="col-md-2">
                <asp:UpdatePanel ID="updPnlMes" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlMes" runat="server" CssClass="form-control" ValidationGroup="valMes" AutoPostBack="true" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged">
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>        
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-8">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="valMes" InitialValue="00" Text="* Seleccionar el mes" ControlToValidate="ddlMes"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrMes" runat="server"
                    AssociatedUpdatePanelID="updPnlMes">
                    <ProgressTemplate>
                        <span>
                            <img height="26" src="https://www.sysweb.unach.mx/Ingresos/Imagenes/load.gif" width="222" />
                        </span>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblMsjError" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrPolizas" runat="server"
                    AssociatedUpdatePanelID="updPnlPolizas">
                    <ProgressTemplate>
                        <asp:Image ID="img5" runat="server"
                            AlternateText="Espere un momento, por favor.." Height="30px"
                            ImageUrl="~/images/ajax_loader_gray_512.gif"
                            ToolTip="Espere un momento, por favor.." Width="30px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="updPnlPolizas" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvPolizas" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Width="100%" OnRowDeleting="grvPolizas_RowDeleting" EmptyDataText="No se encontraron pólizas." ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="IdPoliza" />
                                <asp:BoundField HeaderText="CC" DataField="CENTRO_CONTABLE" />
                                <asp:BoundField HeaderText="# PÓL	" DataField="NUMERO_POLIZA" />
                                <asp:BoundField HeaderText="TIPO" DataField="TIPO" />
                                <asp:BoundField HeaderText="FECHA" DataField="FECHA" />
                                <asp:BoundField HeaderText="STATUS" DataField="STATUS" />
                                <asp:BoundField HeaderText="CONCEPTO" DataField="CONCEPTO" />
                                <asp:BoundField HeaderText="CARGO" DataField="TOT_CARGO" DataFormatString="{0:c}" />
                                <asp:BoundField HeaderText="ABONO" DataField="TOT_ABONO" DataFormatString="{0:c}" />
                                <asp:TemplateField ShowHeader="False">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="linkBttnGenPolizas" runat="server" CssClass="btn btn-primary" OnClick="linkBttnGenPolizas_Click">Generar Pólizas</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn_grid btn-mdb-color" Width="100%" CausesValidation="False" CommandName="Select"><i class="fa fa-file" aria-hidden="true"></i> Ver Póliza</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="8%" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="linkBttnEliminarTodo" runat="server" CssClass="btn btn-danger" OnClientClick="return confirm('¿Desea eliminar TODAS LAS PÓLIZAS?');" OnClick="linkBttnEliminarTodo_Click">Borrar Todo</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttnEliminar" runat="server" CssClass="btn btn_grid btn-mdb-color" CommandName="Delete" Width="100%" OnClientClick="return confirm('¿Desea eliminar la Póliza?');"><i class="fa fa-trash" aria-hidden="true"></i> Borrar</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="8%" />
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


    <script type="text/javascript">        
        function Polizas() {
            ////$('#<%= grvPolizas.ClientID %>').empty();
            $('#<%= grvPolizas.ClientID %>').prepend($("<thead></thead>").append($('#<%= grvPolizas.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "stateSave": true,
                "columns": [
                    null,
                    null,
                    null,
                    null,

                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null
                ]
            })
        };
    </script>
</asp:Content>

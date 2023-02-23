<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAdmin_CFDI.aspx.cs" Inherits="SAF.Contabilidad.Form.frmAdmin_CFDI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/DataTables/jquery.dataTables.min.js"></script>
    <link href="../../Content/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-2">
                Centro Contable
            </div>
            <div class="col-md-10">
                <asp:DropDownList ID="DDLCentro_Contable" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                Tipo Beneficiario
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlTipo_Beneficiario" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-md-1">
                Tipo Gasto
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddlTipo_Gasto" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-md-1">
                Mes
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddlMes" runat="server" CssClass="form-control">
                    <asp:ListItem Value="00">--TODOS--</asp:ListItem>
                    <asp:ListItem Value="01">ENERO</asp:ListItem>
                    <asp:ListItem Value="02">FEBRERO</asp:ListItem>
                    <asp:ListItem Value="03">MARZO</asp:ListItem>
                    <asp:ListItem Value="04">ABRIL</asp:ListItem>
                    <asp:ListItem Value="05">MAYO</asp:ListItem>
                    <asp:ListItem Value="06">JUNIO</asp:ListItem>
                    <asp:ListItem Value="07">JULIO</asp:ListItem>
                    <asp:ListItem Value="08">AGOSTO</asp:ListItem>
                    <asp:ListItem Value="09">SEPTIEMBRE</asp:ListItem>
                    <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                    <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                    <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                Buscar
            </div>
            <div class="col-md-10">
                <div class="input-group">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" PlaceHolder="Folio/Nombre/UUID"></asp:TextBox>
                    <div class="input-group-prepend">
                        <asp:UpdatePanel ID="updPnlBuscar" runat="server">
                            <ContentTemplate>
                                <%--<asp:ImageButton ID="imgbtnBuscar" runat="server" CausesValidation="False" ImageUrl="http://sysweb.unach.mx/resources/imagenes/buscar.png" OnClick="imgbtnBuscar_Click" Style="text-align: right" title="Buscar" />--%>
                                <asp:LinkButton ID="linkBttnBuscar" runat="server" OnClick="linkBttnBuscar_Click" CssClass="btn btn-primary"><i class="fa fa-search" aria-hidden="true"></i> Ver CFDI´S</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPrgBuscar" runat="server" AssociatedUpdatePanelID="updPnlBuscar">
                    <ProgressTemplate>
                        <asp:Image ID="imgBuscar" runat="server" Height="50px" ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                            Width="50px" AlternateText="Espere un momento, por favor.."
                            ToolTip="Espere un momento, por favor.." />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel11">
                    <ProgressTemplate>
                        <asp:Image ID="Image7" runat="server" Height="50px" ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                            Width="50px" AlternateText="Espere un momento, por favor.."
                            ToolTip="Espere un momento, por favor.." />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPrgAct" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <asp:Image ID="imgAct" runat="server" Height="50px" ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                            Width="50px" AlternateText="Espere un momento, por favor.."
                            ToolTip="Espere un momento, por favor.." />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <%--<div class="row">
            <div class="col text-right">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-grey" Text="Actualizar" OnClick="Button1_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>--%>
        <div class="row">
            <div class="col text-center">
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvPolizaCFDI" runat="server" AutoGenerateColumns="False" CssClass="mGrid" ShowFooter="True" Width="100%" OnPageIndexChanging="grvPolizaCFDI_PageIndexChanging" OnSelectedIndexChanged="grvPolizaCFDI_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="ID_CFDI" />
                                <asp:BoundField DataField="Centro_Contable" HeaderText="Centro Contable" />
                                <asp:BoundField DataField="Numero_poliza" HeaderText="# Póliza" />
                                <asp:BoundField DataField="Mes_anio" HeaderText="Mes Anio" />
                                <asp:BoundField DataField="Beneficiario_Tipo" HeaderText="Tipo Beneficiario" />
                                <asp:BoundField DataField="CFDI_Folio" HeaderText="Folio" />
                                <asp:BoundField DataField="CFDI_Fecha" HeaderText="Fecha" />
                                <asp:BoundField DataField="CFDI_RFC" HeaderText="RFC" />
                                <asp:BoundField DataField="CFDI_Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="CFDI_UUID" HeaderText="UUID" />
                                <asp:BoundField DataField="RegimenFiscal" HeaderText="REGIMEN FISCAL" />
                                <asp:BoundField DataField="CFDI_Total" HeaderText="Total" DataFormatString="{0:c}" />
                                <asp:BoundField HeaderText="Fecha Registro" DataField="Fecha_Captura" />
                                <asp:BoundField HeaderText="Usuario Registra" DataField="Usuario_Captura" />
                                <asp:TemplateField HeaderText="Archivos">
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="linkArchivoXML" runat="server" NavigateUrl='<%# Bind("Ruta_XML") %>' Target="_blank">XML</asp:HyperLink>
                                        &nbsp;<asp:HyperLink ID="linkArchivoPDF" runat="server" NavigateUrl='<%# Bind("Ruta_PDF") %>' Target="_blank">PDF</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <%--<asp:CommandField HeaderText="Movto" SelectText="Actualizar" ShowSelectButton="True" />--%>
                                <asp:TemplateField HeaderText="Ver">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBttnVer" runat="server" OnClick="linkBttnVer_Click">Conceptos</asp:LinkButton>
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
        <hr />
        <div class="row">
            <div class="col-md-10">
            </div>
            <div class="col-md-2 text-right">
                 <asp:UpdatePanel ID="updPnlReps" runat="server">
                    <ContentTemplate>
                <asp:ImageButton ID="imgBttnPDF" runat="server" ImageUrl="http://sysweb.unach.mx/resources/imagenes/pdf.png" title="Reporte PDF" OnClick="imgBttnPDF_Click" />
                &nbsp;<asp:ImageButton ID="imgBttnExcel" runat="server" ImageUrl="http://sysweb.unach.mx/resources/imagenes/excel.png" title="Reporte Excel" OnClick="imgBttnExcel_Click" />
                 </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <%--<div class="row">
            <div class="col text-center">
                <asp:UpdateProgress ID="updPgrReps" runat="server" AssociatedUpdatePanelID="updPnlReps">
                    <ProgressTemplate>
                        <asp:Image ID="imgReps" runat="server" Height="50px" ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                            Width="50px" AlternateText="Espere un momento, por favor.."
                            ToolTip="Espere un momento, por favor.." />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>--%>
    </div>
    <div class="modal fade" id="modalConceptos" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modConceptos">Conceptos</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col text-center">
                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="lblConceptos" runat="server" TextMode="MultiLine" Width="100%" Height="350px" Font-Size="12px"></asp:TextBox>
                                        <%--<asp:Label ID="lblConceptos" runat="server" Text="Sin conceptos..." Font-Names="Calibri" Font-Size="10pt"></asp:Label>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function PolizasCFDI() {
            $('#<%= grvPolizaCFDI.ClientID %>').prepend($("<thead></thead>").append($('#<%= grvPolizaCFDI.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "stateSave": true,
                "ordering": false
            });
        };
    </script>
</asp:Content>

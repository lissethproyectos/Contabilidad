<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmRegistroFacturas.aspx.cs" Inherits="SAF.Contabilidad.Form.frmRegistroFacturas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/jquery/jquery-3.1.1.min.js"></script>
    <script src="../../Scripts/select2/js/select2.min.js"></script>
    <link href="../../Scripts/select2/css/select2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <asp:UpdatePanel ID="updPnlMV" runat="server">
                    <ContentTemplate>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <div class="row">
                                    <div class="col-md-2">
                                        Centro Contable
                                    </div>
                                    <div class="col-md-10">
                                        <asp:DropDownList ID="DDLCentro_Contable" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">



                                    <div class="col-md-2">
                                        Mes
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlMes" runat="server" Width="100%">
                                            <asp:ListItem Value="00">TODOS</asp:ListItem>
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
                                    <div class="col-md-1">
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="linkBttnBuscar" runat="server" OnClick="linkBttnBuscar_Click" CssClass="btn btn-primary" ValidationGroup="Nuevo"><i class="fa fa-search" aria-hidden="true"></i> Buscar</asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <%--<asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="http://sysweb.unach.mx/resources/imagenes/nuevo.png" title="Nuevo" ValidationGroup="Nuevo" OnClick="btnNuevo_Click" />--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col text-center">
                                        <asp:UpdateProgress ID="updPrgTipo" runat="server"
                                            AssociatedUpdatePanelID="UpdatePanel9">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgPreTipo" runat="server"
                                                    AlternateText="Espere un momento, por favor.." Height="50px"
                                                    ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                    ToolTip="Espere un momento, por favor.." Width="50px" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col text-center">
                                        <asp:UpdateProgress ID="updPgrPolizas" runat="server"
                                            AssociatedUpdatePanelID="updPnlPolizas">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgPgrPolizasTipo" runat="server"
                                                    AlternateText="Espere un momento, por favor.." Height="50px"
                                                    ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                    ToolTip="Espere un momento, por favor.." Width="50px" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <asp:UpdatePanel ID="updPnlPolizas" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grvPolizas" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mGrid" EmptyDataText="No existen documentos." ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" OnPageIndexChanging="grvPolizas_PageIndexChanging">
                                                    <Columns>
                                                        <asp:BoundField DataField="IdPoliza" HeaderText="Id" />
                                                        <asp:BoundField DataField="Partida" HeaderText="Partida" >
                                                            <ItemStyle BackColor="#999999" Font-Bold="True" Font-Size="12px" ForeColor="Black" HorizontalAlign="Center" /></asp:BoundField>
                                                        <asp:BoundField DataField="Centro_Contable" HeaderText="Centro Contable" />
                                                        <asp:BoundField DataField="Desc_Tipo_Documento" HeaderText="Tipo" />
                                                        <asp:BoundField DataField="Numero_poliza" HeaderText="# Póliza" />
                                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                                        <asp:TemplateField HeaderText="Desc Partida">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Concepto") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="13px" Text="T O T A L"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Concepto") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="$ por Comprobar">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Tot_Cargo") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotPorComprobar" runat="server" Text="0" Font-Bold="True" Font-Size="13px"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPorComprobar" runat="server" Text='<%# Bind("Tot_Cargo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="$ Comprobado">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Tot_Comprobado") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotComprobado" runat="server" Text="0" Font-Bold="True" Font-Size="13px"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblComprobado" runat="server" Text='<%# Bind("Tot_Comprobado") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkBttnAgregar" runat="server" style="width:100px;font-size: 11px;" CausesValidation="False" CommandName="Select" CssClass="btn btn-blue-grey" OnClick="linkBttnAgregar_Click"><%# Eval("Clasificacion") %></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle Font-Bold="True" Font-Size="Large" />
                                                    <HeaderStyle CssClass="enc" />
                                                    <PagerStyle CssClass="enc" HorizontalAlign="Center" />
                                                    <SelectedRowStyle CssClass="sel" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                        <div class="col text-right">
                                            <asp:ImageButton ID="ImageButton1" runat="server"
                                                ImageUrl="http://sysweb.unach.mx/resources/imagenes/pdf.png" OnClick="imgBttnPdf" title="Reporte PDF" />
                                            &nbsp;<asp:ImageButton ID="ImageButton3" runat="server"
                                                ImageUrl="http://sysweb.unach.mx/resources/imagenes/excel.png" OnClick="imgBttnExcel" title="Reporte Excel" />
                                        </div>
                                    </div>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <div class="row">
                                    <div class="col-md-2">
                                        Tipo Beneficiario
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlTipoBeneficiario2" runat="server" Width="100%">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*Tipo Beneficiario" ControlToValidate="ddlTipoBeneficiario2" ValidationGroup="CFDI" InitialValue="0">*Requerido</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-2">
                                        Tipo Gasto
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlTipoGasto2" runat="server" Width="100%">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Tipo Gasto" ControlToValidate="ddlTipoGasto2" ValidationGroup="CFDI" InitialValue="0">*Requerido</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">Tipo Docto</div>
                                    <div class="col-md-4">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
<asp:DropDownList ID="ddlTipoDocto" runat="server" Width="100%" OnSelectedIndexChanged="ddlTipoDocto_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="F">Factura</asp:ListItem>
                                            <asp:ListItem Value="R">Recibo</asp:ListItem>
                                        </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </div>
                                    <div class="col-md-1">Fecha</div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtFecha" runat="server" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtenderFecha" runat="server" TargetControlID="txtFecha" PopupButtonID="imgCalendario" BehaviorID="_content_CalendarExtenderFecha" />
                                        <asp:ImageButton ID="imgCalendario" runat="server" ImageUrl="https://sysweb.unach.mx/resources/imagenes/calendario.gif" />
                                        <asp:Label ID="lblRFecha" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Fecha" ControlToValidate="txtFecha" ValidationGroup="CFDI">*Requerido</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        Proveedor
                                    </div>
                                    <div class="col-md-10">
                                        <asp:UpdatePanel ID="updPnlProveedor" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlProveedor" runat="server" Width="100%" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged" AutoPostBack="True" CssClass="select2"></asp:DropDownList>
                                                <asp:TextBox ID="txtProveedor" runat="server" Width="100%" Visible="False" AutoPostBack="True" PlaceHolder="Nombre del Proveedor"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col text-center">
                                        <asp:UpdateProgress ID="updPgrProveedor" runat="server"
                                            AssociatedUpdatePanelID="updPnlProveedor">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgPgrProveedor" runat="server"
                                                    AlternateText="Espere un momento, por favor.." Height="50px"
                                                    ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                    ToolTip="Espere un momento, por favor.." Width="50px" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                                <div class="row" id="colUUID1" runat="server">
                                    <div class="col-md-2">
                                        RFC
                                    </div>
                                    <div class="col-md-3">
                                        <asp:UpdatePanel ID="updPnlRFC" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtRFC" runat="server" AutoPostBack="True" Width="100%"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-md-2">
                                        Folio Fiscal
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtFolio" runat="server" Width="100%"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFolio" Mask="CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC" />
                                    </div>
                                    <div class="col-md-1">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*UUID" ControlToValidate="txtFolio" ValidationGroup="CFDI">*Requerido</asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        Importe
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtImporte" runat="server" Width="100%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtImporte" ErrorMessage="*Importe" ValidationGroup="CFDI">*Requerido</asp:RequiredFieldValidator>
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col alert alert-warning">
                                        IMPORTANTE: Si no cuenta con el PDF ó XML, puede emitir este paso.
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6" id="fileXML" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <div class="input-group mb-3">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">XML</span>
                                                    </div>
                                                    <div class="custom-file" style="width: 70%">
                                                        <asp:FileUpload ID="FileFactura" runat="server" class="form-control" Height="40px" Width="100%" />

                                                    </div>
                                                    <div class="input-group-append">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileFactura" ErrorMessage="Archivo incorrecto, debe ser un XML" ValidationExpression="(.*?)\.(xml|XML)$" ValidationGroup="CFDI"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="bttnAgregaFactura" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:UpdatePanel ID="updPnlArchivos" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="input-group mb-3">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">PDF</span>
                                                    </div>
                                                    <div class="custom-file" style="width: 70%">
                                                        <asp:FileUpload ID="FileFacturaPDF" runat="server" class="form-control" Height="40px" Width="100%" />
                                                    </div>
                                                    <div class="input-group-append">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator107" runat="server" ControlToValidate="FileFacturaPDF" ErrorMessage="Archivo incorrecto, debe ser un PDF" ValidationExpression="(.*?)\.(pdf|PDF)$" ValidationGroup="CFDI"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="bttnAgregaFactura" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Button ID="bttnAgregaFactura" runat="server" CssClass="btn btn-blue-grey" OnClick="bttnAgregaFactura_Click" Text="Agregar" ValidationGroup="CFDI" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col text-center">
                                        <asp:UpdateProgress ID="updPgrPolizasCFDI" runat="server"
                                            AssociatedUpdatePanelID="updPnlPolizasCFDI">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgPolizasCFDI" runat="server"
                                                    AlternateText="Espere un momento, por favor.." Height="50px"
                                                    ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                    ToolTip="Espere un momento, por favor.." Width="50px" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <asp:UpdatePanel ID="updPnlPolizasCFDI" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grvPolizaCFDI" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mGrid" EmptyDataText="No existen documentos." OnPageIndexChanging="grvPolizaCFDI_PageIndexChanging" OnRowDeleting="grvPolizaCFDI_RowDeleting" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" OnRowCancelingEdit="grvPolizaCFDI_RowCancelingEdit" OnRowEditing="grvPolizaCFDI_RowEditing" OnRowUpdating="grvPolizaCFDI_RowUpdating">
                                                    <Columns>
                                                        <asp:BoundField DataField="Tipo_Docto" HeaderText="Tipo Docto" ReadOnly="True" />
                                                        <asp:BoundField DataField="Beneficiario_Tipo" HeaderText="Tipo" ReadOnly="True" />
                                                        <asp:BoundField DataField="Tipo_Gasto" HeaderText="Tipo Gasto" ReadOnly="True" />
                                                        <%--                                                <asp:BoundField DataField="CFDI_Folio" HeaderText="CFDI Folio" />--%>
                                                        <asp:BoundField DataField="CFDI_UUID" HeaderText="CFDI UUID" />
                                                        <asp:BoundField DataField="CFDI_Fecha" HeaderText="CFDI Fecha" />
                                                        <asp:TemplateField HeaderText="Proveedor">
                                                            <EditItemTemplate>
                                                                <%--<asp:Label ID="Label1" runat="server" Text='<%# Eval("CFDI_Nombre") %>'></asp:Label>--%>
                                                                <asp:UpdatePanel ID="updPnlProveedor2" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlProveedor2" runat="server" Width="100%" AutoPostBack="True" CssClass="select2"></asp:DropDownList>
                                                <asp:TextBox ID="txtProveedor2" runat="server" Width="100%" Visible="False" AutoPostBack="True" PlaceHolder="Nombre del Proveedor"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("CFDI_Nombre") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CFDI_RFC" HeaderText="CFDI RFC" ReadOnly="True" />
                                                        <asp:TemplateField HeaderText="CFDI Total">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtTotal" runat="server" Text='<%# Bind("CFDI_Total") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblGranTotal" runat="server" Font-Bold="True" Font-Size="Medium" Text="0"></asp:Label>
                                                                <asp:Label ID="lblGranTotalInt" runat="server" Text="0" Visible="False"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("CFDI_Total") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="XML">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="linkArchivoXML" runat="server" NavigateUrl='<%# Bind("Ruta_XML") %>' Target="_blank">Ver</asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PDF">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="linkArchivoPDF" runat="server" NavigateUrl='<%# Bind("Ruta_PDF") %>' Target="_blank">Ver</asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowEditButton="True" />
                                                        <asp:CommandField ShowDeleteButton="True" />
                                                        <asp:BoundField DataField="Fecha_Captura" />
                                                        <asp:BoundField DataField="Usuario_Captura" />
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
                                <div class="row">
                                    <div class="col text-right">
                                        <asp:UpdatePanel ID="updPnlGuardarCFDI" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnCancelarCFDI" runat="server" CausesValidation="False" CssClass="btn btn-blue-grey" Text="Salir" OnClick="btnCancelarCFDI_Click" />
                                                &nbsp;<asp:Button ID="btnGuardarCFDI" runat="server" CssClass="btn btn-primary" Text="Guardar" ValidationGroup="Poliza" OnClick="btnGuardarCFDI_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col text-center">
                                        <asp:UpdateProgress ID="updPgrGuardarCFDI" runat="server"
                                            AssociatedUpdatePanelID="updPnlGuardarCFDI">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgGuardar" runat="server"
                                                    AlternateText="Espere un momento, por favor.." Height="50px"
                                                    ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                    ToolTip="Espere un momento, por favor.." Width="50px" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
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
        function Autocomplete() {
            $(".select2").select2();
        }
    </script>
</asp:Content>

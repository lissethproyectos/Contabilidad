<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmComprobaciones.aspx.cs" Inherits="SAF.Contabilidad.Form.frmComprobaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                    <asp:GridView ID="grvPolizas" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mGrid" EmptyDataText="No existen documentos." ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="IdPoliza" HeaderText="Id" />
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
                                                    <asp:LinkButton ID="linkBttnAgregar" runat="server" Style="width: 100px; font-size: 11px;" CausesValidation="False" CommandName="Select" CssClass="btn btn-blue-grey" OnClick="linkBttnAgregar_Click"><%# Eval("Clasificacion") %></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MES_ANIO" />
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
                                ImageUrl="http://sysweb.unach.mx/resources/imagenes/pdf.png" title="Reporte PDF" />
                            &nbsp;<asp:ImageButton ID="ImageButton3" runat="server"
                                ImageUrl="http://sysweb.unach.mx/resources/imagenes/excel.png" title="Reporte Excel" />
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-3">
                                Tipo de Beneficiario
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlTipo_Beneficiario" runat="server" Width="100%">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                Tipo de Gasto
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlTipo_Gasto" runat="server" Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlTipo_Beneficiario" ErrorMessage="* Tipo de Beneficiario" InitialValue="0" ValidationGroup="CFDI">* Requerido</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-5">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlTipo_Gasto" ErrorMessage="* Tipo de Gasto" InitialValue="0" ValidationGroup="CFDI">* Requerido</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:UpdatePanel ID="updPnlDoctos" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend alert-primary">
                                                <span class="input-group-text">XML</span>
                                            </div>
                                            <div class="custom-file" style="width: 70%">
                                                <asp:FileUpload ID="FileFactura" runat="server" class="form-control" Height="38px" Width="100%" />

                                            </div>
                                            <div class="input-group-append">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileFactura" ErrorMessage="Archivo incorrecto, debe ser un XML" ValidationExpression="(.*?)\.(xml|XML)$" ValidationGroup="CFDI"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Archivo XML" ControlToValidate="FileFactura" Text="* Requerido" ValidationGroup="CFDI"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="bttnAgregaFactura" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-5">
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">PDF</span>
                                            </div>
                                            <div class="custom-file" style="width: 70%">
                                                <asp:FileUpload ID="FileFacturaPDF" runat="server" class="form-control" Height="38px" Width="100%" />
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
                                <asp:Button ID="bttnAgregaFactura" runat="server" CssClass="btn btn-blue-grey" Text="Agregar" ValidationGroup="CFDI" OnClick="bttnAgregaFactura_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:GridView ID="grvPolizaCFDI" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mGrid" EmptyDataText="No existen documentos." ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>
                                        <asp:TemplateField>
                                            <AlternatingItemTemplate>
                                                lklkl
                                            </AlternatingItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBtnnAgregar" runat="server" CssClass="btn btn-blue-grey" OnClick="linkBtnnAgregar_Click">+ Partida</asp:LinkButton>
                                                <asp:GridView ID="grdPartidas" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="Partida" HeaderText="PARTIDA" />
                                                        <asp:BoundField DataField="Importe_Partida" HeaderText="IMPORTE" />
                                                        <asp:CommandField ShowDeleteButton="True" />
                                                    </Columns>
                                                    <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Tipo_Docto" HeaderText="Tipo Docto" ReadOnly="True" />
                                        <asp:BoundField DataField="Beneficiario_Tipo" HeaderText="Tipo" ReadOnly="True">


                                            <ItemStyle VerticalAlign="Top" />

                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tipo_Gasto" HeaderText="Tipo Gasto" ReadOnly="True">


                                            <ItemStyle VerticalAlign="Top" />

                                        </asp:BoundField>
                                        <asp:BoundField DataField="CFDI_Folio" HeaderText="Folio">


                                            <ItemStyle VerticalAlign="Top" />

                                        </asp:BoundField>
                                        <asp:BoundField DataField="CFDI_UUID" HeaderText="UUID">


                                            <ItemStyle VerticalAlign="Top" />

                                        </asp:BoundField>
                                        <asp:BoundField DataField="CFDI_Fecha" HeaderText="Fecha">


                                            <ItemStyle VerticalAlign="Top" />

                                        </asp:BoundField>
                                        <asp:BoundField DataField="CFDI_RFC" HeaderText="RFC" ReadOnly="True">


                                            <ItemStyle VerticalAlign="Top" />

                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Total">
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
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="XML">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="linkArchivoXML" runat="server" NavigateUrl='<%# Bind("Ruta_XML") %>' Target="_blank">Ver</asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PDF">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="linkArchivoPDF" runat="server" NavigateUrl='<%# Bind("Ruta_PDF") %>' Target="_blank">Ver</asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBttnEliminar" runat="server" CausesValidation="False" CommandName="Delete" Text="Eliminar"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Fecha_Captura" />
                                        <asp:BoundField DataField="Usuario_Captura" />
                                    </Columns>
                                    <FooterStyle CssClass="enc" />
                                    <HeaderStyle CssClass="enc" />
                                    <PagerStyle CssClass="enc" HorizontalAlign="Center" />
                                    <SelectedRowStyle CssClass="sel" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>


                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

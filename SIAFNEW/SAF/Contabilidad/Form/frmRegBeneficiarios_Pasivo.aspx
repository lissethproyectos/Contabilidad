<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmRegBeneficiarios_Pasivo.aspx.cs" Inherits="SAF.Contabilidad.Form.frmRegBeneficiarios_Pasivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/select2/js/select2.min.js"></script>
    <link href="../../Scripts/select2/css/select2.min.css" type="text/css" rel="stylesheet" />
    <script src="../../Scripts/DataTables/jquery.dataTables.min.js"></script>
    <link href="../../Content/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" />
     <style>
        :after, :before {
            box-sizing: border-box;
        }

        a {
            color: #337ab7;
            text-decoration: none;
        }

        i {
            margin-bottom: 4px;
        }

        .btn {
            display: inline-block;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }


        .btn-app {
            color: white;
            box-shadow: none;
            border-radius: 3px;
            position: relative;
            padding: 10px 15px;
            margin: 0;
            min-width: 40px;
            max-width: 60px;
            text-align: center;
            border: 1px solid #ddd;
            background-color: #f4f4f4;
            font-size: 10px;
            transition: all .2s;
            background-color: steelblue !important;
        }

            .btn-app > .fa, .btn-app > .glyphicon, .btn-app > .ion {
                font-size: 20px;
                display: block;
            }

            .btn-app:hover {
                border-color: #aaa;
                transform: scale(1.1);
            }

        .pdf {
            background-color: #5e5e5e !important;
            /*background-color: #dc2f2f !important;*/
        }

        .excel {
            background-color: #3ca23c !important;
        }

        .csv {
            background-color: #e86c3a !important;
        }

        .imprimir {
            background-color: #8766b1 !important;
        }

        /*
Esto es opcional pero sirve para que todos los botones de exportacion se distribuyan de manera equitativa usando flexbox

.flexcontent {
    display: flex;
    justify-content: space-around;
}
*/

        .selectTable {
            height: 40px;
            float: right;
        }

        div.dataTables_wrapper div.dataTables_filter {
            text-align: left;
            margin-top: 15px;
        }

        .btn-secondary {
            color: #fff;
            background-color: #4682b4;
            border-color: #4682b4;
        }

            .btn-secondary:hover {
                color: #fff;
                background-color: #315f86;
                border-color: #545b62;
            }



        .titulo-tabla {
            color: #606263;
            text-align: center;
            margin-top: 15px;
            margin-bottom: 15px;
            font-weight: bold;
        }






        .inline {
            display: inline-block;
            padding: 0;
        }

        .auto-style1 {
            left: -930px;
            top: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <div class="container">
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
                                Formato
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="DDLFormato" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0000">--TODOS--</asp:ListItem>
                                    <asp:ListItem>2111</asp:ListItem>
                                    <asp:ListItem>2112</asp:ListItem>
                                    <asp:ListItem>2113</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <asp:UpdatePanel ID="updPnlBttnsIni" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="linkBttnBuscar" CssClass="btn btn-grey" runat="server" OnClick="linkBttnBuscar_Click"><i class="fa fa-search" aria-hidden="true"></i> Ver Pasivos</asp:LinkButton>
                                        <asp:LinkButton ID="linkBttnAgregar" CssClass="btn btn-primary" runat="server" OnClick="linkBttnAgregar_Click"><i class="fa fa-plus-circle" aria-hidden="true"></i> Agregar</asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col text-center">
                                <asp:UpdateProgress ID="updPgrBttnsIni" runat="server" AssociatedUpdatePanelID="updPnlBttnsIni">
                                    <ProgressTemplate>
                                        <asp:Image ID="imgBttnsIni" runat="server" Height="50px" ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            Width="50px" AlternateText="Espere un momento, por favor.."
                                            ToolTip="Espere un momento, por favor.." />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col text-center">
                                <asp:UpdateProgress ID="updPgrPasivos" runat="server" AssociatedUpdatePanelID="updPnlPasivos">
                                    <ProgressTemplate>
                                        <asp:Image ID="pasivo" runat="server" Height="50px" ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            Width="50px" AlternateText="Espere un momento, por favor.."
                                            ToolTip="Espere un momento, por favor.." />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col text-center">
                                <asp:UpdatePanel ID="updPnlPasivos" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdPasivos0" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowDeleting="grdPasivos_RowDeleting" Width="100%" OnSelectedIndexChanged="grdPasivos0_SelectedIndexChanged" EmptyDataText="No hay registros para mostrar">
                                            <Columns>
                                                <asp:BoundField DataField="desc_centro_contable" HeaderText="Centro Contable">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="formato" HeaderText="Formato">
                                                    <ItemStyle BackColor="#FFFF99" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="importe" HeaderText="Total" DataFormatString="{0:C2}">
                                                    <ItemStyle Font-Bold="True" />
                                                </asp:BoundField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkBttnEdit" runat="server" CausesValidation="False" CommandName="Select" CssClass="btn btn-mdb-color"><i class="fa fa-pencil-square-o" aria-hidden="true"></i> Editar</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="85px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkBttnEliminar" runat="server" CausesValidation="False" CssClass="btn btn-mdb-color" OnClick="linkBttnEliminar_Click"><i class="fa fa-trash" aria-hidden="true"></i> Eliminar</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="85px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkBttnReporte" runat="server" CausesValidation="False" CssClass="btn btn-mdb-color" OnClick="linkBttnReporte_Click"><i class="fa fa-file-pdf-o" aria-hidden="true"></i> PDF</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="70px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkBttnExcel" runat="server" CssClass="btn btn-mdb-color" OnClick="linkBttnExcel_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i> XLS</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="70px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="centro_contable">
                                                    <ControlStyle CssClass="classHide" />
                                                    <FooterStyle CssClass="classHide" />
                                                    <HeaderStyle CssClass="classHide" />
                                                    <ItemStyle CssClass="classHide" />
                                                </asp:BoundField>
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
                        <div class="row">
                            <div class="col text-right">
                                <asp:LinkButton ID="linkBttnGralPDF" runat="server" class="btn btn-secondary buttons-pdf buttons-html5 btn-app export pdf" OnClick="linkBttnGralPDF_Click" title="PDF"><i class="fa fa-file-pdf-o"></i>PDF</asp:LinkButton>
                                <asp:LinkButton ID="linkBttnGralExcel" runat="server" class="btn btn-secondary buttons-excel buttons-html5 btn-app export excel" OnClick="linkBttnGralExcel_Click" title="Excel"><i class="fa fa-file-excel-o"></i>Excel</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-1">
                                CC
                            </div>
                            <div class="col-md-11">
                                <asp:UpdatePanel ID="updPnlCC2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DDLCentro_Contable2" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDLCentro_Contable2_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1">
                                Cédula
                            </div>
                            <div class="col-md-11">
                                <asp:UpdatePanel ID="updPnlCedula" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DDLCedula" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DDLCedula_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1">
                                Póliza
                            </div>
                            <div class="col-md-11">
                                <asp:UpdatePanel ID="updPnlPoliza" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DDLPoliza" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DDLPoliza_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row" runat="server" visible="false" id="rowNumPol">
                            <div class="col-md-1">
                                # Póliza
                            </div>
                            <div class="col-md-2">
                                <asp:UpdatePanel ID="updPnlAddPoliza" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtNumPoliza" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:LinkButton ID="linkBttnAgregarPoliza" runat="server" CssClass="btn btn-primary" OnClick="linkBttnAgregarPoliza_Click">Agregar</asp:LinkButton>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-9 font-weight-bold">
                                <asp:RequiredFieldValidator ID="reqNumPol" runat="server" ErrorMessage="* Número de Póliza" Text="*" ControlToValidate="txtNumPoliza" ValidationGroup="NewPasivo"></asp:RequiredFieldValidator>
                                La póliza debe ser de 7 digitos
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1">
                                Formato
                            </div>
                            <div class="col-md-1">
                                <asp:UpdatePanel ID="updPnlFormato2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DDLFormato2" runat="server" CssClass="form-control btn btn-primary dropdown-toggle browser-default custom-select custom-select-lg mb-3" OnSelectedIndexChanged="DDLFormato2_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem>2111</asp:ListItem>
                                            <asp:ListItem>2112</asp:ListItem>
                                            <asp:ListItem>2113</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-1">
                                Cuenta
                            </div>
                            <div class="col-md-5">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DDLCuenta" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DDLCuenta_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-1">
                                <asp:RequiredFieldValidator ID="reqCuenta" runat="server" ErrorMessage="* Cuenta" Text="*" ControlToValidate="DDLCuenta" ValidationGroup="NewPasivo" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-1">
                                Importe
                            </div>
                            <div class="col-md-1">
                                <asp:UpdatePanel ID="updPnlImporte" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtImporte" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-1">
                                <asp:RequiredFieldValidator ID="reqImporte" runat="server" ErrorMessage="* Importe" Text="*" ControlToValidate="txtImporte" ValidationGroup="NewPasivo" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1">
                                Fuente
                            </div>
                            <div class="col-md-11">
                                <asp:UpdatePanel ID="updPnlFuente2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DDLFuente2" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1">
                                Proyecto
                            </div>
                            <div class="col-md-11">
                                <asp:UpdatePanel ID="updPnlProyecto" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DDLProyecto2" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row" id="rowBenef" runat="server">
                            <div class="col-md-1">
                                Beneficiario
                            </div>
                            <div class="col-md-8">
                                <asp:UpdatePanel ID="updPnlBeneficiario" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DDLBeneficiario" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-1">
                                <asp:RequiredFieldValidator ID="reqBeneficiario" runat="server" ErrorMessage="* Proveedor" Text="*" ControlToValidate="DDLBeneficiario" ValidationGroup="NewPasivo"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-2">
                                <asp:UpdatePanel ID="updPnlAgregarP" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="linBttnAgregar" runat="server" CssClass="btn btn-grey" OnClick="linkBttnAgregarP_Click" ValidationGroup="NewPasivo">Agregar</asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row" id="rowProveedor" runat="server">
                            <div class="col-md-1">
                                Proveedor
                            </div>
                            <div class="col-md-8">
                                <asp:UpdatePanel ID="updPnlProveedor" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DDLProveedor" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-1">
                                <asp:RequiredFieldValidator ID="reqProveedor" runat="server" ErrorMessage="* Proveedor" Text="*" ControlToValidate="DDLProveedor" ValidationGroup="NewPasivo" InitialValue="Z"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-2">
                                <asp:UpdatePanel ID="updPnlAgregarP2" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="linkBttnAgregar2" runat="server" CssClass="btn btn-grey" OnClick="linkBttnAgregarP_Click" ValidationGroup="NewPasivo">Agregar</asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-2"></div>
                            <div class="col-md-10">
                                <asp:ValidationSummary ID="valDatosPasivo" runat="server" ValidationGroup="NewPasivo" HeaderText="Estos campos son requeridos:" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col text-center">
                                <asp:UpdateProgress ID="updPgrAgregarP2" runat="server" AssociatedUpdatePanelID="updPnlAgregarP2">
                                    <ProgressTemplate>
                                        <asp:Image ID="imgAgregarP2" runat="server" Height="50px" ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            Width="50px" AlternateText="Espere un momento, por favor.."
                                            ToolTip="Espere un momento, por favor.." />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:UpdateProgress ID="updPgrAgregarP" runat="server" AssociatedUpdatePanelID="updPnlAgregarP">
                                    <ProgressTemplate>
                                        <asp:Image ID="imgAgregarP" runat="server" Height="50px" ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            Width="50px" AlternateText="Espere un momento, por favor.."
                                            ToolTip="Espere un momento, por favor.." />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:UpdateProgress ID="updPgrCC2" runat="server" AssociatedUpdatePanelID="updPnlCC2">
                                    <ProgressTemplate>
                                        <asp:Image ID="img1" runat="server" Height="50px" ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            Width="50px" AlternateText="Espere un momento, por favor.."
                                            ToolTip="Espere un momento, por favor.." />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:UpdateProgress ID="updPgrFormato2" runat="server" AssociatedUpdatePanelID="updPnlFormato2">
                                    <ProgressTemplate>
                                        <asp:Image ID="img2" runat="server" Height="50px" ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            Width="50px" AlternateText="Espere un momento, por favor.."
                                            ToolTip="Espere un momento, por favor.." />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:UpdateProgress ID="updPgrCedula" runat="server" AssociatedUpdatePanelID="updPnlCedula">
                                    <ProgressTemplate>
                                        <asp:Image ID="img3" runat="server" Height="50px" ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            Width="50px" AlternateText="Espere un momento, por favor.."
                                            ToolTip="Espere un momento, por favor.." />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:UpdateProgress ID="updPgrPoliza" runat="server" AssociatedUpdatePanelID="updPnlPoliza">
                                    <ProgressTemplate>
                                        <asp:Image ID="img4" runat="server" Height="50px" ImageUrl="http://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            Width="50px" AlternateText="Espere un momento, por favor.."
                                            ToolTip="Espere un momento, por favor.." />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:UpdatePanel ID="updPnlGridPasivos2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdPasivos" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDeleting="grdPasivos_RowDeleting" CssClass="mGrid">
                                            <Columns>
                                                <asp:BoundField HeaderText="CC" DataField="centro_contable" />
                                                <asp:BoundField HeaderText="Cédula" DataField="cedula" />
                                                <asp:BoundField HeaderText="Póliza" DataField="poliza" />
                                                <asp:BoundField HeaderText="Formato" DataField="formato">
                                                    <ItemStyle BackColor="#FFFF99" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="cuenta" DataField="cuenta" />
                                                <asp:BoundField HeaderText="Importe" DataField="importe" />
                                                <asp:BoundField HeaderText="Beneficiario" DataField="beneficiario" />
                                                <asp:BoundField DataField="fuente" HeaderText="Fuente" />
                                                <asp:BoundField DataField="proyecto" HeaderText="Proyecto" />
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Eliminar"></asp:LinkButton>
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
                        <div class="row">
                            <div class="col text-right">
                                <asp:LinkButton ID="linkBttnSalir" runat="server" CssClass="btn btn-grey" OnClick="linkBttnSalir_Click">Salir</asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="linkBttnGuardar" runat="server" CssClass="btn btn-primary" OnClick="linkBttnGuardar_Click">Guardar</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal" tabindex="-1" role="dialog" id="modalEmpleados">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Empleados</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2">
                            Buscar
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="linkBttnBuscaNombreEmp" runat="server" CssClass="btn btn-blue-grey" OnClick="linkBttnBuscaNombreEmp_Click">Buscar</asp:LinkButton></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdEmpleados" runat="server">
                                            <Columns>
                                                <asp:BoundField HeaderText="Empleado" DataField="Nombre" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary">Agregar</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" tabindex="-1" role="dialog" id="modalBorrar">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Eliminar</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>¿Esta seguro de eliminar el registro?</p>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:LinkButton ID="linkBttnEliminarPasivo" runat="server" CssClass="btn btn-danger" OnClick="linkBttnEliminarPasivo_Click"><i class="fa fa-trash" aria-hidden="true"></i> Borrar</asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function Autocomplete() {
            $(".select2").select2();
        }
        function CatEmpleados() {
            $('#<%= grdEmpleados.ClientID %>').prepend($("<thead></thead>").append($('#<%= grdEmpleados.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "stateSave": true
            })
        }
        function Pasivos() {
            $('#<%= grdPasivos0.ClientID %>').prepend($("<thead></thead>").append($('#<%= grdPasivos0.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "stateSave": true
            })
        };
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FRMCuentas_contables.aspx.cs" Inherits="SAF.Rep.FRMCuentas_contables" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/DataTables/jquery.dataTables.min.js"></script>
    <link href="../../Content/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="https://sysweb.unach.mx/INGRESOS/Scripts/select2/css/select2.min.css" type="text/css" rel="stylesheet" />
    <script src="https://sysweb.unach.mx/INGRESOS/Scripts/select2/js/select2.min.js"></script>

    <style type="text/css">
        section {
            float: left;
            width: 100%;
            background: #43cea2; /* fallback for old browsers */
            background: -webkit-linear-gradient(to left, #185a9d, #43cea2); /* Chrome 10-25, Safari 5.1-6 */
            background: linear-gradient(to left, #185a9d, #43cea2); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
            padding: 30px 0;
        }

        .card {
            -moz-box-direction: normal;
            -moz-box-orient: vertical;
            background-color: #fff;
            border-radius: 0.25rem;
            display: flex;
            flex-direction: column;
            position: relative;
            margin-bottom: 1px;
            border: none;
        }

        .card-header:first-child {
            border-radius: 0;
        }

        .card-header {
            background-color: #f7f7f9;
            margin-bottom: 0;
            padding: 20px 1.25rem;
            border: none;
        }

            .card-header a i {
                float: left;
                font-size: 25px;
                padding: 5px 0;
                margin: 0 25px 0 0px;
                color: #195C9D;
            }

            .card-header i {
                float: right;
                font-size: 30px;
                width: 1%;
                margin-top: 8px;
                margin-right: 10px;
            }

            .card-header a {
                width: 97%;
                float: left;
                color: #565656;
            }

            .card-header p {
                margin: 0;
            }

            .card-header h3 {
                margin: 0 0 0px;
                font-size: 20px;
                font-family: 'Slabo 27px', serif;
                font-weight: bold;
                color: #3fc199;
            }

        .card-block {
            -moz-box-flex: 1;
            flex: 1 1 auto;
            padding: 20px;
            color: #232323;
            box-shadow: inset 0px 4px 5px rgba(0,0,0,0.1);
            border-top: 1px soild #000;
            border-radius: 0;
        }

        .panel-group {
            margin-bottom: 20px;
        }

            .panel-group .panel {
                margin-bottom: 0;
                border-radius: 4px;
            }

        .panel-default {
            border-color: #ddd;
        }

            .panel-default > .panel-heading {
                color: #333;
                background-color: #f5f5f5;
                border-color: #ddd;
            }


        .nav-tabs .nav-link {
            border: 1px solid #f8f9fa;
            border-top-left-radius: .25rem;
            border-top-right-radius: .25rem;
            font-size: 13px;
        }

        .nav-tabs li a {
            padding: 8px 40px;
            border: 1px solid #ededed;
            border-top: 2px solid #dfdfdf;
            border-right: 0px none;
            background: #65635d;
            color: #fff;
            border-radius: 0px;
            margin-right: 0px;
            /* border-color: #cdcdcd; */
            font-weight: bold;
            transition: all 0.3s ease-in 0s;
        }

        .lds-ring {
            display: inline-block;
            position: relative;
            width: 80px;
            height: 80px;
        }

            .lds-ring div {
                box-sizing: border-box;
                display: block;
                position: absolute;
                width: 64px;
                height: 64px;
                margin: 8px;
                border: 8px solid #fff;
                border-radius: 50%;
                animation: lds-ring 1.2s cubic-bezier(0.5, 0, 0.5, 1) infinite;
                border-color: #fff transparent transparent transparent;
            }

                .lds-ring div:nth-child(1) {
                    animation-delay: -0.45s;
                }

                .lds-ring div:nth-child(2) {
                    animation-delay: -0.3s;
                }

                .lds-ring div:nth-child(3) {
                    animation-delay: -0.15s;
                }

        @keyframes lds-ring {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <ul class="nav nav-tabs" id="myTab" role="tablist">
                    <li class="nav-item">
                        <a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Catálogo</a>
                    </li>
                    <li class="nav-item" id="Pestania2">
                        <a class="nav-link" id="pago-tdc" data-toggle="tab" href="#profile" role="tab" aria-controls="profile" aria-selected="false">Actualizar Cuentas</a>
                    </li>
                </ul>
                <div class="tab-content" id="myTabContent">
                    <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                        <div class="container-fluid">
                            <div class="row alert alert-danger">
                                <div class="col">
                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-2">
                                    Centro Contable
                                </div>
                                <div class="col-md-10">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DDLCentro_Contable" runat="server"
                                                OnSelectedIndexChanged="DDLCentro_Contable_SelectedIndexChanged1" CssClass="form-control">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    Cuenta Mayor
                                </div>
                                <div class="col-md-8">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlCuenta_Mayor" runat="server"
                                                OnSelectedIndexChanged="DDLCentro_Contable_SelectedIndexChanged" CssClass="form-control">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-md-2">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="bttnBuscar" runat="server" Text="Ver Cuentas" CssClass="btn btn-primary" OnClick="DDLCentro_Contable_SelectedIndexChanged1" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col text-center">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                        AssociatedUpdatePanelID="UpdatePanel13">
                                        <ProgressTemplate>
                                            <asp:Image ID="imgBuscar" runat="server"
                                                AlternateText="Espere un momento, por favor.." Height="50px"
                                                ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                ToolTip="Espere un momento, por favor.." Width="50px" Style="text-align: center" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-center">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server"
                                        AssociatedUpdatePanelID="UpdatePanel3">
                                        <ProgressTemplate>
                                            <asp:Image ID="Image1q" runat="server"
                                                AlternateText="Espere un momento, por favor.." Height="50px"
                                                ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                ToolTip="Espere un momento, por favor.." Width="50px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-center">
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server"
                                        AssociatedUpdatePanelID="UpdatePanel9">
                                        <ProgressTemplate>
                                            <asp:Image ID="Image1q0" runat="server"
                                                AlternateText="Espere un momento, por favor.." Height="50px"
                                                ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                ToolTip="Espere un momento, por favor.." Width="50px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:MultiView ID="MultiViewcuentas_contables" runat="server">
                                        <asp:View ID="View1" runat="server">

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="Label15" runat="server" Text="Subdependencia"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="DDLSubdependencia" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLSubdependencia_SelectedIndexChanged"
                                                                Width="100%">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row note note-warning">
                                                <div class="col-md-2">
                                                    <p>Cuenta Contable</p>
                                                </div>
                                                <div class="col-md-10">

                                                    <asp:TextBox ID="txtcuenta_contable" runat="server" MaxLength="22"
                                                        Width="420px" Visible="False"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    Nivel1
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txt1" runat="server" Enabled="False" MaxLength="4"
                                                                OnTextChanged="txt1_TextChanged" CssClass="form-control">0000</asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:CustomValidator ID="valNiv1" runat="server" ClientValidationFunction="ValidaLongitudIni" ControlToValidate="txt1" ErrorMessage="*Se requiere 4 caracteres en nivel 1" ValidationGroup="guardar">*Se requiere 4 caracteres</asp:CustomValidator>
                                                </div>
                                                <div class="col-md-2">
                                                    Nivel2
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txt2" runat="server" Enabled="False" MaxLength="5"
                                                                OnTextChanged="txt2_TextChanged" CssClass="form-control">00000</asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:CustomValidator ID="valNiv2" runat="server" ClientValidationFunction="ValidaLongitud" ControlToValidate="txt2" ErrorMessage="*Se requiere 5 caracteres en niveles 2,3,4" ValidationGroup="guardar">*Se requiere 5 caracteres</asp:CustomValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    Nivel 3
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txt3" runat="server" OnTextChanged="TextBox3_TextChanged"
                                                                Enabled="False" MaxLength="5" CssClass="form-control">00000</asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:CustomValidator ID="valNiv3" runat="server" ClientValidationFunction="ValidaLongitud" ControlToValidate="txt3" ErrorMessage="*Se requiere 5 caracteres" ValidationGroup="guardar"></asp:CustomValidator>
                                                </div>
                                                <div class="col-md-2">
                                                    Nivel 4
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txt4" runat="server" Enabled="False" MaxLength="5"
                                                                OnTextChanged="txt4_TextChanged" AutoPostBack="True" CssClass="form-control" Width="100%">00000</asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:CustomValidator ID="valNiv4" runat="server" ClientValidationFunction="ValidaLongitud" ControlToValidate="txt4" ErrorMessage="*Se requiere 5 caracteres" ValidationGroup="guardar"></asp:CustomValidator>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:UpdatePanel ID="updPnlBuscarBien" runat="server">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="linkBttnBuscarBien" runat="server" CssClass="btn btn-warning" OnClick="linkBttnBuscarBien_Click"><i class="fa fa-search" aria-hidden="true"></i> Buscar Bien</asp:LinkButton>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col text-center">
                                                    <asp:UpdateProgress ID="updPgrBuscarBien" runat="server"
                                                        AssociatedUpdatePanelID="updPnlBuscarBien">
                                                        <ProgressTemplate>
                                                            <asp:Image ID="imgBuscarBien" runat="server"
                                                                AlternateText="Espere un momento, por favor.." Height="50px"
                                                                ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                                ToolTip="Espere un momento, por favor.." Width="50px" />
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </div>
                                            </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    Descripción
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlDescripcion" runat="server" Visible="false" Width="100%" OnSelectedIndexChanged="ddlDescripcion_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                            <asp:TextBox ID="txtdescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:RequiredFieldValidator ID="reqDescripcion" runat="server" ErrorMessage="Descripción" ControlToValidate="txtdescripcion" Text="* Requerido" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label5" runat="server" Text="Tipo"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="txttipo" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DDLCentro_Contable_SelectedIndexChanged"
                                                        Enabled="False" CssClass="form-control">
                                                        <asp:ListItem Value="AF">AFECTABLE</asp:ListItem>
                                                        <asp:ListItem Value="AC">ACUMULABLE</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="col-md-2">
                                                    <asp:Label ID="Label9" runat="server" Text="Status:"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlstatus" runat="server"
                                                        Enabled="False" CssClass="form-control">
                                                        <asp:ListItem Value="A">ALTA</asp:ListItem>
                                                        <asp:ListItem Value="B">BAJA</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label12" runat="server" Text="Clasificación"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlclasificacion" runat="server" AutoPostBack="True" CssClass="form-control">
                                                                <asp:ListItem Value="DET">DETALLE</asp:ListItem>
                                                                <asp:ListItem Value="ESP">ESPECIFICA</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </div>

                                                <div class="col-md-2">

                                                    <asp:Label ID="Label13" runat="server" Text="Nivel"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:UpdatePanel ID="UpdatePanel150" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlnivel" runat="server" AutoPostBack="True"
                                                                Enabled="False"
                                                                OnSelectedIndexChanged="DDLCentro_Contable_SelectedIndexChanged" CssClass="form-control">
                                                                <asp:ListItem Value="1">NIVEL 1</asp:ListItem>
                                                                <asp:ListItem Value="2">NIVEL 2</asp:ListItem>
                                                                <asp:ListItem Value="3">NIVEL 3</asp:ListItem>
                                                                <asp:ListItem Value="4">NIVEL 4</asp:ListItem>
                                                                <asp:ListItem Value="5">NIVEL 5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col text-right">
                                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button ID="BTN_Guardar" runat="server" OnClick="BTN_Guardar_Click"
                                                                Text="Guardar" CssClass="btn btn-primary" ValidationGroup="guardar" />
                                                            &nbsp;
                                    <asp:Button ID="BTN_continuar" runat="server" OnClick="BTN_continuar_Click"
                                        Text="Guardar y Continuar" Visible="False" CssClass="btn btn-info" ValidationGroup="guardar" />
                                                            &nbsp;
                                    <asp:Button ID="BTN_Cancelar" runat="server" OnClick="BTN_Cancelar_Click"
                                        Text="Cancelar" CssClass="btn btn-blue-grey" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col text-center">
                                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server"
                                                        AssociatedUpdatePanelID="UpdatePanel8">
                                                        <ProgressTemplate>
                                                            <asp:Image ID="Image2q" runat="server"
                                                                AlternateText="Espere un momento, por favor.." Height="50px"
                                                                ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                                ToolTip="Espere un momento, por favor.." Width="50px" />
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </div>
                                            </div>

                                        </asp:View>
                                        <asp:View ID="View2" runat="server">
                                            <div class="row">
                                                <div class="col text-center">
                                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server"
                                                        AssociatedUpdatePanelID="UpdatePanel2">
                                                        <ProgressTemplate>
                                                            <asp:Image ID="Image1q1" runat="server"
                                                                AlternateText="Espere un momento, por favor.." Height="50px"
                                                                ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                                ToolTip="Espere un momento, por favor.." Width="50px" Style="text-align: center" />
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="grdcuentas_contables" runat="server" Width="100%"
                                                                AutoGenerateColumns="False"
                                                                BorderStyle="None" CellPadding="4"
                                                                GridLines="Vertical"
                                                                OnPageIndexChanging="grdcuentas_contables_PageIndexChanging" CssClass="mGrid" EmptyDataText="No hay registros para mostrar" ShowHeaderWhenEmpty="True">
                                                                <Columns>
                                                                    <asp:BoundField DataField="id" HeaderText="ID" />
                                                                    <asp:BoundField DataField="CENTRO_CONTABLE" HeaderText="CENTRO CONTABLE">
                                                                        <ItemStyle Width="7%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="cuenta_contable" HeaderText="CUENTA" />
                                                                    <asp:BoundField DataField="descripcion" HeaderText="DESCRIPCIÓN" />
                                                                    <asp:BoundField DataField="natura" HeaderText="NATURALEZA" />
                                                                    <asp:BoundField DataField="nivel" HeaderText="NIVEL">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Editar</asp:LinkButton>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnagregar" runat="server" OnClick="lbtnagregar_Click">Agregar</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="return confirm('¿En realidad desea Eliminar este registro?');">Eliminar</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <asp:Button ID="bttnAgregarCtaContab" runat="server" CssClass="btn btn-blue-grey" OnClick="bttnAgregarCtaContab_Click" Text="Agregar" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" Visible='<%# Bind("bandera") %>'>Ver Polizas</asp:LinkButton>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
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
                                                    <asp:ImageButton ID="BTNver_reporte" runat="server"
                                                        ImageUrl="http://sysweb.unach.mx/resources/imagenes/pdf.png" OnClick="BTNver_reporte_Click" />
                                                </div>
                                            </div>
                                        </asp:View>
                                        <asp:View ID="View3" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col">
                                                            <asp:GridView ID="grvPolizas" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" EmptyDataText="No hay registros para mostrar" Font-Size="11px" ForeColor="Black" GridLines="Vertical" Width="100%" OnPageIndexChanging="grvPolizas_PageIndexChanging" CssClass="mGrid">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="IdPoliza" />
                                                                    <asp:BoundField DataField="CENTRO_CONTABLE" HeaderText="CENTRO CONTABLE" />
                                                                    <asp:BoundField DataField="NUMERO_POLIZA" HeaderText="# PÓLIZA" />
                                                                    <asp:BoundField DataField="TIPO" HeaderText="TIPO" />
                                                                    <asp:BoundField DataField="FECHA" DataFormatString="{0:d}" HeaderText="FECHA" />
                                                                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                                                                    <asp:BoundField DataField="CONCEPTO" HeaderText="CONCEPTO" />
                                                                    <asp:BoundField DataField="TOT_CARGO" DataFormatString="{0:c}" HeaderText="CARGO">
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="TOT_ABONO" DataFormatString="{0:c}" HeaderText="ABONO">
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="linkBttnImprimir" runat="server" OnClick="linkBttnImprimir_Click">Imprimir</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle CssClass="enc" />
                                                                <PagerStyle CssClass="enc" HorizontalAlign="Center" />
                                                                <SelectedRowStyle CssClass="sel" />
                                                                <HeaderStyle CssClass="enc" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="row text-right">
                                                        <div class="col">
                                                            <asp:Button ID="btnRegresar" runat="server" OnClick="btnRegresar_Click" Text="Regresar" CssClass="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                    <%--                        </div>--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:View>
                                    </asp:MultiView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="tab-pane" id="profile" role="tabpanel" aria-labelledby="home-profile">
                        <div id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="card">
                                <div class="card-header" role="tab" id="headingOne">
                                    <div class="mb-0">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" aria-controls="collapseOne" class="collapsed">
                                            <i class="fa fa-file-text-o" aria-hidden="true"></i>
                                            <h3>Matriz COG</h3>
                                            <p>Se actualizaran las cuentas con la leyenda REVISAR, de acuerdo al siguiente catálogo.</p>
                                        </a>
                                        <i class="fa fa-angle-right" aria-hidden="true"></i>
                                    </div>
                                </div>

                                <div id="collapseOne" class="collapse" role="tabpanel" aria-labelledby="headingOne" aria-expanded="false" style="">
                                    <div class="card-block">
                                        <div class="row">
                                            <div class="col text-right">
                                                <asp:UpdatePanel ID="updPnlActualizar" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="linkBttnActualizar" runat="server" CssClass="btn btn-warning" OnClick="linkBttnActualizar_Click">Actualizar</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row alert alert-warning">
                                            <div class="col">
                                                <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblMsj2" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                Cta de Mayor
                                            </div>
                                            <div class="col-md-10">
                                                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlMayor" runat="server" Width="100%" onChange="cboCatCog();"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col text-center">
                                                <asp:UpdateProgress ID="updPgrActualizar" runat="server" AssociatedUpdatePanelID="updPnlActualizar">
                                                    <ProgressTemplate>
                                                        <asp:Image ID="imgPgrActualizar" runat="server" AlternateText="Espere un momento, por favor.." Height="50px" ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif" ToolTip="Espere un momento, por favor.." Width="50px" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grdCatCOG" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Width="100%" OnRowDeleting="grdCatCOG_RowDeleting" OnSelectedIndexChanged="grdCatCOG_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="MAYOR" DataField="cuenta_mayor" />
                                                                <asp:BoundField HeaderText="COG" DataField="natura" />
                                                                <asp:BoundField HeaderText="NOMBRE" DataField="descripcion">
                                                                    <ItemStyle Width="75%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="STATUS" DataField="status" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="linkBttnEliminarCOG" runat="server" CommandName="Delete" Width="100%" OnClientClick="return confirm('¿Desea eliminar el registro?');">Eliminar</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="linkBttnAgregarCOG" runat="server" CssClass="btn btn-primary" OnClick="linkBttnAgregarCOG_Click">Agregar</asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="linkBttnEditarCOG" runat="server" Width="100%" CommandName="Select">Editar</asp:LinkButton>
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
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header" role="tab" id="headingTwo">
                                    <div class="mb-0">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseOne" class="collapsed">
                                            <i class="fa fa-file-text-o" aria-hidden="true"></i>
                                            <h3>Catálogo proyectos/fuentes/dependencias</h3>
                                            <p>Se actualizaran las cuentas con la leyenda REVISAR, de acuerdo al siguiente catálogo.</p>
                                        </a>
                                        <i class="fa fa-angle-right" aria-hidden="true"></i>
                                    </div>
                                </div>
                                <div id="collapseTwo" class="collapse" role="tabpanel" aria-labelledby="headingTwo" aria-expanded="false">
                                    <div class="card-block">
                                        <div class="row">
                                            <div class="col-md-1">
                                                Tipo
                                            </div>
                                            <div class="col-md-10">
                                                <asp:UpdatePanel ID="updPnlTipoCat" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlTipoCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoCat_SelectedIndexChanged">
                                                            <asp:ListItem Value="P">Proyectos</asp:ListItem>
                                                            <asp:ListItem Value="F">Fuentes</asp:ListItem>
                                                            <asp:ListItem Value="D">Dependencias</asp:ListItem>
                                                            <asp:ListItem Value="N">2do nivel</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:UpdatePanel ID="updPnlActCatNiv" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="linkActNiv" runat="server" CssClass="btn btn-warning" OnClick="linkActNiv_Click">Actualizar</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col text-center">
                                                <asp:UpdateProgress ID="updPgrCatNiv" runat="server" AssociatedUpdatePanelID="updPnlActCatNiv">
                                                    <ProgressTemplate>
                                                        <asp:Image ID="imgActCatNiv" runat="server" AlternateText="Espere un momento, por favor.." Height="50px" ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif" ToolTip="Espere un momento, por favor.." Width="50px" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
                                        </div>
                                        <div class="row alert alert-warning">
                                            <div class="col">
                                                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblMsj" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col text-center">
                                                <asp:UpdateProgress ID="updPgrTipoCat" runat="server"
                                                    AssociatedUpdatePanelID="updPnlTipoCat">
                                                    <ProgressTemplate>
                                                        <asp:Image ID="imgTipoCat" runat="server"
                                                            AlternateText="Espere un momento, por favor.." Height="50px"
                                                            ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                            ToolTip="Espere un momento, por favor.." Width="50px" Style="text-align: center" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col text-center">
                                                <asp:UpdateProgress ID="updPgrCat" runat="server"
                                                    AssociatedUpdatePanelID="UpdatePanel20">
                                                    <ProgressTemplate>
                                                        <asp:Image ID="imgCat" runat="server"
                                                            AlternateText="Espere un momento, por favor.." Height="50px"
                                                            ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                            ToolTip="Espere un momento, por favor.." Width="50px" Style="text-align: center" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grdCatalogos" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Width="100%" OnSelectedIndexChanged="grdCatalogos_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="CVE" DataField="Etiqueta" />
                                                                <asp:BoundField HeaderText="DESCRIPCIÓN" DataField="EtiquetaDos">
                                                                    <ItemStyle Width="75%" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="STATUS">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtStatus" runat="server" Width="50px" Text='<%# Bind("EtiquetaTres") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("EtiquetaTres") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="linkBttnEliminarCat" runat="server" CommandName="Delete" Width="100%" OnClientClick="return confirm('¿Desea eliminar el registro?');">Eliminar</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="linkBttnAgregarCat" runat="server" CausesValidation="False" Text="Agregar" OnClick="linkBttnAgregarCat_Click" CssClass="btn btn-primary"></asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="linkBttnEditCat" runat="server" CausesValidation="False" CommandName="Select" Text="Editar"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle CssClass="enc" />
                                                            <PagerStyle CssClass="enc" HorizontalAlign="Center" />
                                                            <SelectedRowStyle CssClass="sel" />
                                                            <HeaderStyle CssClass="enc" />
                                                            <AlternatingRowStyle CssClass="alt" />
                                                        </asp:GridView>
                                                        <asp:GridView ID="grdCatalogos2" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Width="100%" OnSelectedIndexChanged="grdCatalogos2_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="MAYOR" DataField="Etiqueta" />
                                                                <asp:BoundField HeaderText="CTA2" DataField="EtiquetaDos" />
                                                                <asp:BoundField HeaderText="DESCRIPCIÓN" DataField="EtiquetaTres">
                                                                    <ItemStyle Width="75%" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="STATUS">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtStatus" runat="server" Width="50px" Text='<%# Bind("EtiquetaCuatro") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("EtiquetaCuatro") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="linkBttnEliminarCat" runat="server" CommandName="Delete" Width="100%" OnClientClick="return confirm('¿Desea eliminar el registro?');">Eliminar</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="linkBttnAgregarCat2" runat="server" CausesValidation="False" OnClick="linkBttnAgregarCat2_Click" Text="Agregar" CssClass="btn btn-primary"></asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="linkBttnEditCat" runat="server" CausesValidation="False" CommandName="Select" Text="Editar"></asp:LinkButton>
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
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>

        <div class="modal fade" id="modalCOG" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modEmp">Agregar</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-2">Mayor</div>
                                <div class="col-md-4">
                                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtMayor" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-md-2">COG</div>
                                <div class="col-md-4">
                                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCOG" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">Descrip</div>
                                <div class="col-md-10">
                                    <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDescripcionCOG" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">Status</div>
                                <div class="col-md-10">
                                    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlStatusCOG" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="A">Activo</asp:ListItem>
                                                <asp:ListItem Value="B">Baja</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col text-right">
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="bttnGuardarCOG" runat="server" Text="Guardar" CssClass="btn btn-info" OnClick="bttnGuardarCOG_Click" />
                                            <asp:Button ID="bttnEditarCOG" runat="server" Text="Editar" CssClass="btn btn-info" OnClick="bttnEditarCOG_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col text-center">
                                <asp:UpdateProgress ID="UpdateProgress23" runat="server"
                                    AssociatedUpdatePanelID="UpdatePanel23">
                                    <ProgressTemplate>
                                        <asp:Image ID="imgGuardarCOG" runat="server"
                                            AlternateText="Espere un momento, por favor.." Height="50px"
                                            ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            ToolTip="Espere un momento, por favor.." Width="50px" Style="text-align: center" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblErrorCOG" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalCat" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modCat">Agregar</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>

                    </div>
                    <div class="modal-body">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-2">Cve</div>
                                <div class="col-md-4">
                                    <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCve" runat="server" Width="100%"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row" runat="server" id="rowCta2">
                                <div class="col-md-2">Cta 2</div>
                                <div class="col-md-4">
                                    <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCta2" runat="server" Width="100%"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">Status</div>
                                <div class="col-md-4">
                                    <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlStatusCat" runat="server">
                                                <asp:ListItem Value="A">Activo</asp:ListItem>
                                                <asp:ListItem Value="B">Baja</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">Descripción</div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDescCat" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-right">
                                    <asp:UpdatePanel ID="updPnlAgCat" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="bttnModificarCat" runat="server" Text="Modificar" CssClass="btn btn-info" OnClick="bttnModificarCat_Click" />
                                            <asp:Button ID="bttnAgregarCat" runat="server" Text="Guardar" CssClass="btn btn-info" OnClick="bttnAgregarCat_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col text-center">
                                <asp:UpdateProgress ID="updPgrAgCat" runat="server"
                                    AssociatedUpdatePanelID="updPnlAgCat">
                                    <ProgressTemplate>
                                        <asp:Image ID="imgAgCat" runat="server"
                                            AlternateText="Espere un momento, por favor.." Height="50px"
                                            ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            ToolTip="Espere un momento, por favor.." Width="50px" Style="text-align: center" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <div class="row">
                                <div class="col alert alert-danger">
                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblErrorCat" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script type="text/javascript">       
        function CuentasContables() {
            //$('input[type=search]').val('');
            $('#<%= grdcuentas_contables.ClientID %>').prepend($("<thead></thead>").append($('#<%= grdcuentas_contables.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "stateSave": true,
                "ordering": false,
                "lengthMenu": [[15, 30, 45, -1], [15, 30, 45, "All"]]
            });
        };
        function CuentasContablesInicio() {
            //$('input[type=search]').val('');
            $('#<%= grdcuentas_contables.ClientID %>').prepend($("<thead></thead>").append($('#<%= grdcuentas_contables.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "stateSave": false,
                "ordering": false,
                "lengthMenu": [[15, 30, 45, -1], [15, 30, 45, "All"]]
            });
        };
        function CatCOG() {
            $('#<%= grdCatCOG.ClientID %>').prepend($("<thead></thead>").append($('#<%= grdCatCOG.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "ordering": false,
                "bStateSave": false,
                "bFilter": true,
                "lengthMenu": [[7, 14, 21, -1], [7, 14, 21, "All"]]
            });
        };
        function Polizas() {
            //$('input[type=search]').val('');
            $('#<%= grvPolizas.ClientID %>').prepend($("<thead></thead>").append($('#<%= grvPolizas.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "stateSave": true,
                "ordering": false
            });
        };
        function Catalogos() {
            $('#<%= grdCatalogos.ClientID %>').prepend($("<thead></thead>").append($('#<%= grdCatalogos.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "stateSave": true,
                "ordering": false,
                "lengthMenu": [[7, 14, 21, -1], [7, 14, 21, "All"]]
            });
        };
        function Catalogos2() {
            $('#<%= grdCatalogos2.ClientID %>').prepend($("<thead></thead>").append($('#<%= grdCatalogos2.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "stateSave": true,
                "ordering": false,
                "lengthMenu": [[7, 14, 21, -1], [7, 14, 21, "All"]]
            });
        };
        function OcultarPestania() {
            $('#Pestania2').hide();
        };
        function cboCatCog() {
               <%-- var table = $('#<%= grdCatCOG.ClientID %>').DataTable();
                var selectedValue = $('#<%= ddlMayor.ClientID %>').val();
                table.search(selectedValue).draw();--%>


            var table = $('#<%= grdCatCOG.ClientID %>').DataTable();
            var selectedValue = $('#<%= ddlMayor.ClientID %>').val();
            if (selectedValue != "00") {
                table.columns(0).search(selectedValue).draw();
            }
            else {
                table.columns(0).search("").draw();
            }

        };
        function FiltBienes() {
            $('#<%= ddlDescripcion.ClientID %>').select2();
        };
        function ValidaLongitud(source, arguments) {
            var Valor
            Valor = arguments.Value;
            if (Valor.length === 5) {
                arguments.IsValid = true;
            }
            else {
                arguments.IsValid = false;
            }
        };
        function ValidaLongitudIni(source, arguments) {
            var Valor
            Valor = arguments.Value;
            if (Valor.length === 4) {                
                arguments.IsValid = true;
            }
            else {                
                arguments.IsValid = false;
            }
        };
    </script>
</asp:Content>

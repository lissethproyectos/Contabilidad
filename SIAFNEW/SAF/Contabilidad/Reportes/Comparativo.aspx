<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Comparativo.aspx.cs" Inherits="SAF.Contabilidad.Reportes.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-2">Cuenta Contable</div>
                            <div class="col-md-10">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddl_cuentas" runat="server"
                                            OnSelectedIndexChanged="ddl_cuentas_SelectedIndexChanged" Width="100%">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lbl_f_ini" runat="server" Text="Mes Inicial"></asp:Label>
                            </div>
                            <div class="col-md-10">
                                <asp:DropDownList ID="txtmes_inicial" runat="server"
                                    OnSelectedIndexChanged="txtmes_inicial_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col text-center">
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server"
                                    AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <asp:Image ID="Image1q" runat="server"
                                            AlternateText="Espere un momento, por favor.." Height="30px"
                                            ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            ToolTip="Espere un momento, por favor.." Width="30px" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col text-right">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="linkBttnPDF1" runat="server" CssClass="btn btn-secondary buttons-pdf buttons-html5 btn-app export pdf" OnClick="linkBttnPDF1_Click" title="PDF"><i class="fa fa-file-pdf-o"></i>PDF</asp:LinkButton>
                                        <asp:LinkButton ID="linkBttnExcel1" runat="server" CssClass="btn btn-secondary buttons-excel buttons-html5 btn-app export excel" title="Excel" OnClick="linkBttnExcel1_Click"><i class="fa fa-file-excel-o"></i>Excel</asp:LinkButton>
                                        <asp:LinkButton ID="linkBttnCSV1" runat="server" CssClass="btn btn-secondary buttons-csv buttons-html5 btn-app export csv" OnClick="linkBttnCSV_Click" title="CSV" Visible="false"><i class="fa fa-file-text-o"></i>CSV</asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cuadro_botones">&nbsp; &nbsp; &nbsp;
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:ImageButton ID="ImageButton3" runat="server"
                                                ImageUrl="https://sysweb.unach.mx/resources/imagenes/excel.png" OnClick="imgBttnExcel"
                                                Style="text-align: center" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td style="text-align: center">&nbsp;
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server"
                                        AssociatedUpdatePanelID="UpdatePanel6">
                                        <ProgressTemplate>
                                            <asp:Image ID="Image1q0" runat="server"
                                                AlternateText="Espere un momento, por favor.." Height="30px"
                                                ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                ToolTip="Espere un momento, por favor.." Width="30px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <div class="container">
                                <div class="row">
                                    <div class="col text-center">
                                        <asp:UpdateProgress ID="UpdateProgress4" runat="server"
                                            AssociatedUpdatePanelID="UpdatePanel8">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image1q1" runat="server"
                                                    AlternateText="Espere un momento, por favor.." Height="30px"
                                                    ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                                    ToolTip="Espere un momento, por favor.." Width="30px" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        Fecha Inicial
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlMes_inicial" runat="server" Width="100%"
                                            OnSelectedIndexChanged="txtmes_inicial_SelectedIndexChanged">
                                            <asp:ListItem Value="00">APERTURA</asp:ListItem>
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
                                            <asp:ListItem Value="13">CIERRE</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">Fecha Final</div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlMes_final" runat="server" Width="100%"
                                            OnSelectedIndexChanged="txtmes_inicial_SelectedIndexChanged">
                                            <asp:ListItem Value="00">APERTURA</asp:ListItem>
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
                                            <asp:ListItem Value="13">CIERRE</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblCentro_Contable" runat="server" Text="Centro Contable"
                                                    Visible="False"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DDLCentro_Contable" runat="server" Visible="False"
                                                    Width="100%" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DDLCentro_Contable_SelectedIndexChanged1">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-1">
                                        <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel10">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image1q6" runat="server" AlternateText="Espere un momento, por favor.." Height="30px" ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif" ToolTip="Espere un momento, por favor.." Width="30px" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblcuenta1" runat="server" Text="Cuenta Contable"
                                            Visible="False"></asp:Label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlcuenta1" runat="server" Visible="False" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col text-right">
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="linkBttnPDF" runat="server" CssClass="btn btn-secondary buttons-pdf buttons-html5 btn-app export pdf" title="PDF" OnClick="linkBttnPDF_Click"><i class="fa fa-file-pdf-o"></i>PDF</asp:LinkButton>
                                                <asp:LinkButton ID="linkBttnPDFLote" runat="server" CssClass="btn btn-secondary buttons-pdf buttons-html5 btn-app export pdf" title="PDF por Lote" OnClick="linkBttnPDFLote_Click" Visible="False"><i class="fa fa-file-pdf-o"></i>Lote</asp:LinkButton>
                                                <asp:LinkButton ID="linkBttnExcel" runat="server" CssClass="btn btn-secondary buttons-excel buttons-html5 btn-app export excel" title="Excel" OnClick="linkBttnExcel_Click"><i class="fa fa-file-excel-o"></i>Excel</asp:LinkButton>
                                                <asp:LinkButton ID="linkBttnExcelLote" runat="server" CssClass="btn btn-secondary buttons-excel buttons-html5 btn-app export excel" title="Excel por Lote" OnClick="linkBttnExcelLote_Click" Visible="False"><i class="fa fa-file-excel-o"></i>Lote</asp:LinkButton>
                                                <asp:LinkButton ID="linkBttnCSV" runat="server" CssClass="btn btn-secondary buttons-csv buttons-html5 btn-app export csv" title="CSV" OnClick="linkBttnCSV_Click" Visible="false"><i class="fa fa-file-text-o"></i>CSV</asp:LinkButton>




                                                <%--<asp:ImageButton ID="btnAceptar0" runat="server" ImageUrl="https://sysweb.unach.mx/resources/imagenes/pdf.png" OnClick="btnAceptar0_Click" />
                                                <asp:ImageButton ID="imgBttnLotesPDF" runat="server" ImageUrl="http://sysweb.unach.mx/resources/imagenes/pdf2.png" OnClick="imgBttnLotesPDF_Click" title="Reporte/Lote" ValidationGroup="RepLotes" Visible="False" />
                                                <asp:ImageButton ID="btn_excel" runat="server" ImageUrl="https://sysweb.unach.mx/resources/imagenes/excel.png" OnClick="btn_excel_Click" Style="text-align: center" />
                                                <asp:ImageButton ID="imgBttnLotesExcel" runat="server" ImageUrl="http://sysweb.unach.mx/resources/imagenes/EXCEL2.png" OnClick="imgBttnExcelLotes_Click" title="Reporte Excel" ValidationGroup="RepLotes" Visible="False" />--%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:View>
                <asp:View ID="View4" runat="server">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-2">
                                Centro Contable
                            </div>
                            <div class="col-md-10">
                                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DDLCentro_Contable_v" runat="server"
                                            OnSelectedIndexChanged="DDLCentro_Contable_v_SelectedIndexChanged"
                                            Width="100%" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col text-center">
                                <asp:UpdateProgress ID="UpdateProgress6" runat="server"
                                    AssociatedUpdatePanelID="UpdatePanel12">
                                    <ProgressTemplate>
                                        <asp:Image ID="Image1q3" runat="server"
                                            AlternateText="Espere un momento, por favor.." Height="30px"
                                            ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            ToolTip="Espere un momento, por favor.." Width="30px" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                Mes
                            </div>
                            <div class="col-md-3">
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlmes" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="txtmes_inicial_SelectedIndexChanged" Width="100%">
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-1">
                                <asp:UpdateProgress ID="UpdateProgress7" runat="server"
                                    AssociatedUpdatePanelID="UpdatePanel13">
                                    <ProgressTemplate>
                                        <asp:Image ID="Image1q4" runat="server"
                                            AlternateText="Espere un momento, por favor.." Height="30px"
                                            ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            ToolTip="Espere un momento, por favor.." Width="30px" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <div class="col-md-1">
                                Tipo
                            </div>
                            <div class="col-md-4">
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddltipo" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddltipo_SelectedIndexChanged" Width="100%">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-1">
                                <asp:UpdateProgress ID="UpdateProgress8" runat="server"
                                    AssociatedUpdatePanelID="UpdatePanel14">
                                    <ProgressTemplate>
                                        <asp:Image ID="Image1q5" runat="server"
                                            AlternateText="Espere un momento, por favor.." Height="30px"
                                            ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            ToolTip="Espere un momento, por favor.." Width="30px" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                # Poliza
                            </div>
                            <div class="col-md-10">
                                <asp:DropDownList ID="ddlnumero_poliza" runat="server" Width="100%"
                                    OnSelectedIndexChanged="ddlnumero_poliza_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col text-right">
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="linkBttnPDF0" runat="server" CssClass="btn btn-secondary buttons-pdf buttons-html5 btn-app export pdf" title="PDF" OnClick="linkBttnPDF0_Click"><i class="fa fa-file-pdf-o"></i>PDF</asp:LinkButton>
                                        <%--<asp:ImageButton ID="btnAceptar_v" runat="server"
                                            ImageUrl="https://sysweb.unach.mx/resources/imagenes/pdf.png" OnClick="btnAceptar_v_Click" />--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col text-center">
                                <asp:UpdateProgress ID="UpdateProgress5" runat="server"
                                    AssociatedUpdatePanelID="UpdatePanel11">
                                    <ProgressTemplate>
                                        <asp:Image ID="Image1q2" runat="server"
                                            AlternateText="Espere un momento, por favor.." Height="30px"
                                            ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif"
                                            ToolTip="Espere un momento, por favor.." Width="30px" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View5" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel101" runat="server">
                        <ContentTemplate>
                            <div class="container">
                                <div class="row">
                                    <div class="col-md-2">
                                        Fecha Inicial
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlMes_inicial1" runat="server" OnSelectedIndexChanged="txtmes_inicial_SelectedIndexChanged">
                                            <asp:ListItem Value="00">APERTURA</asp:ListItem>
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
                                            <asp:ListItem Value="13">CIERRE</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        Fecha Final
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlMes_final1" runat="server" OnSelectedIndexChanged="txtmes_inicial_SelectedIndexChanged">
                                            <asp:ListItem Value="00">APERTURA</asp:ListItem>
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
                                            <asp:ListItem Value="13">CIERRE</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        Tipo
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddl_Tipo_D" runat="server" Width="100%">
                                            <asp:ListItem Value="0">Seleccione..</asp:ListItem>
                                            <asp:ListItem Value="1">Adeudos SAT</asp:ListItem>
                                            <asp:ListItem Value="2">Adeudos FOVISSSTE</asp:ListItem>
                                            <asp:ListItem Value="3">Adeudos ISSSTE</asp:ListItem>
                                            <asp:ListItem Value="4">Impuestos sobre Nómina</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_Tipo_D" ErrorMessage="*Requerido" InitialValue="0">*Requerido</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-2">
                                        Adeudo
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddl_subtipo" runat="server" Width="100%">
                                            <asp:ListItem Value="1">Corto Plazo</asp:ListItem>
                                            <asp:ListItem Value="2">Diferido</asp:ListItem>
                                            <asp:ListItem Value="3">Conciliado</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        Notas
                                    </div>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtNotas" runat="server" Height="150px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col text-right">
                                        <asp:UpdatePanel ID="UpdatePanel103" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="linkBttnPDF2"  runat="server" CssClass="btn btn-secondary buttons-pdf buttons-html5 btn-app export pdf" title="PDF" OnClick="linkBttnPDF2_Click"><i class="fa fa-file-pdf-o"></i>PDF</asp:LinkButton>
                                                <asp:LinkButton ID="linkBttnExcel2" runat="server" CssClass="btn btn-secondary buttons-excel buttons-html5 btn-app export excel" title="Excel" OnClick="linkBttnExcel2_Click"><i class="fa fa-file-excel-o"></i>Excel</asp:LinkButton>

<%--                                                <asp:ImageButton ID="btnAceptar_D" runat="server" ImageUrl="https://sysweb.unach.mx/resources/imagenes/pdf.png" OnClick="btnAceptar_D_Click" />
                                                &nbsp;<asp:ImageButton ID="btn_excel_D" runat="server" ImageUrl="https://sysweb.unach.mx/resources/imagenes/excel.png" OnClick="imgBttnExcel_D" Style="text-align: center" />--%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col text-center">
                                        <asp:UpdateProgress ID="UpdateProgress10" runat="server" AssociatedUpdatePanelID="UpdatePanel103">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image1q7" runat="server" AlternateText="Espere un momento, por favor.." Height="70px" ImageUrl="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif" ToolTip="Espere un momento, por favor.." Width="70px" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:View>
            </asp:MultiView>


        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function Reporte(ruta) {
            window.open(ruta, '_blank');
        };
    </script>
</asp:Content>

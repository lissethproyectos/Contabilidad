<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pruebas.aspx.cs" Inherits="SAF.pruebas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
                                                                        <asp:LinkButton ID="linkBttnAgregarCat" runat="server" CausesValidation="False" CommandName="Select" Text="Agregar" CssClass="btn btn-primary" OnClick="linkBttnAgregarCat_Click"></asp:LinkButton>
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
        </div>
    </form>
</body>
</html>

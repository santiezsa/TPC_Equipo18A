<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="TPC_Equipo18A.FormularioProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfId" runat="server" />
    <h3>
        <asp:Label ID="lblTitulo" runat="server" Text="Agregar Producto"></asp:Label></h3>
    <hr />

    <div class="row">
        <div class="col-md-8">
            <div class="card">
                <div class="card-body">

                    <%-- Codigo y nombre --%>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label>Código</label>
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El código es obligatorio" ControlToValidate="txtCodigo" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                        <div class="form-group col-md-6">
                            <label>Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio" ControlToValidate="txtNombre" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                    </div>

                    <%-- Marca y Categoría --%>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label>Marca</label>
                            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ErrorMessage="La marca es obligatoria" ControlToValidate="ddlMarca" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                        <div class="form-group col-md-6">
                            <label>Categoría</label>
                            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ErrorMessage="La categoría es obligatoria" ControlToValidate="ddlCategoria" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                    </div>

                    <%-- Descripcion --%>
                    <div class="form-group">
                        <label>Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="La descripción es obligatoria" ControlToValidate="txtDescripcion" CssClass="text-danger small" Display="Dynamic" runat="server" />
                    </div>

                    <%-- Stock y ganancia --%>
                    <div class="form-row">
                        <div class="form-group col-md-4">
                            <label>Stock Actual</label>
                            <asp:TextBox ID="txtStockActual" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtStockActual" CssClass="text-danger small" Display="Dynamic" runat="server" />
                            <asp:RegularExpressionValidator ErrorMessage="Número entero" ControlToValidate="txtStockActual" ValidationExpression="^\d+$" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>

                        <%-- Stock minimo --%>
                        <div class="form-group col-md-4">
                            <label>Stock Mínimo</label>
                            <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtStockMinimo" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>

                        <%-- Ganancia --%>
                        <div class="form-group col-md-4">
                            <label>Ganancia (%)</label>
                            <asp:TextBox ID="txtPorcentajeGanancia" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Requerido" ControlToValidate="txtPorcentajeGanancia" CssClass="text-danger small" Display="Dynamic" runat="server" />
                        </div>
                    </div>

                    <hr />

                    <div class="form-group">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                        <asp:HyperLink NavigateUrl="~/GestionProductos.aspx" runat="server"
                            Text="Cancelar" CssClass="btn btn-secondary ml-2" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

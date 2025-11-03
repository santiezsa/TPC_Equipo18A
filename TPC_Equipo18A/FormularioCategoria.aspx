<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="FormularioCategoria.aspx.cs" Inherits="TPC_Equipo18A.FormularioCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%-- En este caso cambia el titulo segun si edita o creando --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        <asp:Label ID="lblTitulo" runat="server" Text="Agregar Categoria"></asp:Label></h3>
    <hr />

        <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="card-body">
                    <div class="form-group">
                        <label>Descripción de la Categoría</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                        <%-- Validacion --%>
                        <asp:RequiredFieldValidator
                            ErrorMessage="La descripción es obligatoria."
                            ControlToValidate="txtDescripcion"
                            CssClass="text-danger"
                            Display="Dynamic"
                            runat="server" />
                    </div>

                    <hr />
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                        CssClass="btn btn-success" OnClick="btnGuardar_Click" />

                    <asp:HyperLink NavigateUrl="~/GestionCategorias.aspx" runat="server"
                        Text="Cancelar" CssClass="btn btn-secondary" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

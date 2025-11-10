<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="TPC_Equipo18A.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alert alert-danger text-center mt-5">
        <h3><i class="bi bi-exclamation-triangle-fill"></i>Ocurrió un problema</h3>
        <hr />
        <p class="lead">
            <asp:Label ID="lblMensajeError" runat="server" Text="Error inesperado."></asp:Label>
        </p>
        <asp:HyperLink NavigateUrl="~/Default.aspx" runat="server" CssClass="btn btn-primary mt-3">Volver al Inicio</asp:HyperLink>
    </div>
</asp:Content>

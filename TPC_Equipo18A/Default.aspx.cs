using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo18A
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarStockBajo();
                cargarVentasRecientes();
                cargarMetricasMensuales();
            }
        }

        private void cargarStockBajo()
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Producto> lista = productoNegocio.listarStockBajo();

            rptStockBajo.DataSource = lista;
            rptStockBajo.DataBind();
        }
        private void cargarVentasRecientes()
        {
            VentaNegocio negocio = new VentaNegocio();
            List<Venta> ventas = negocio.ListarUltimasVentas(5);

            repVentasRecientes.DataSource = ventas;
            repVentasRecientes.DataBind();
        }

        private void cargarMetricasMensuales()
        {
            VentaNegocio negocio = new VentaNegocio();
            DateTime hoy = DateTime.Now;

            // Traigo las ventas de este mes
            List<Venta> ventasMes = negocio.listarVentasPorMes(hoy.Month, hoy.Year);

            // Calculo de total acumulado
            decimal totalMes = 0;
            // Arrays para acumular por semana
            // 0=Sem1, 1=Sem2, 2=Sem3, 3=Sem4
            decimal[] totalPorSemana = new decimal[4];

            foreach (Venta venta in ventasMes)
            {
                totalMes += venta.Total;

                // Distribuido en semanas 1-7, 8-14, 15-21, 22-Fin
                int dia = venta.Fecha.Day;
                if (dia <= 7)
                {
                    totalPorSemana[0] += venta.Total;
                }
                else if (dia <= 14)
                {
                    totalPorSemana[1] += venta.Total;
                }
                else if (dia <= 21)
                {
                    totalPorSemana[2] += venta.Total;
                }
                else
                {
                    totalPorSemana[3] += venta.Total;
                }
            }

            // Mostrar total
            lblTotalVentasMes.Text = totalMes.ToString("C0"); // formato moneda sin decimales

            // Comparativa con mes anterior
            if (totalMes > 0)
            {
                lblPorcentajeCambio.Text = "<i class='bi bi-graph-up-arrow'></i> Activo";
                lblPorcentajeCambio.CssClass = "text-success ml-3";
            }
            else
            {
                lblPorcentajeCambio.Text = "Sin movimientos";
                lblPorcentajeCambio.CssClass = "text-muted ml-3";
            }

            // Config del grafico
            // Calculo de semana con mas ventas para que tenga maxima altura
            decimal maxVentaSemanal = totalPorSemana.Max();

            // Validacion para no dividir por cero
            if (maxVentaSemanal == 0) maxVentaSemanal = 1;

            // Asigno alturas dinamicamente
            // (VentaSemana / VentaMaxima) * 100 = Porcentaje de altura

            configurarBarra(barSemana1, totalPorSemana[0], maxVentaSemanal);
            configurarBarra(barSemana2, totalPorSemana[1], maxVentaSemanal);
            configurarBarra(barSemana3, totalPorSemana[2], maxVentaSemanal);
            configurarBarra(barSemana4, totalPorSemana[3], maxVentaSemanal);
        }

        private void configurarBarra(System.Web.UI.HtmlControls.HtmlGenericControl barra, decimal venta, decimal maximo)
        {
            // Calculo porcentaje altura
            int porcentajeAltura = (int)((venta / maximo) * 100);

            // Minimo de 1% para verse en el grafico
            if (porcentajeAltura < 2 && venta > 0) porcentajeAltura = 2;
            {
                barra.Style["height"] = porcentajeAltura + "%";
            }

            // Si es la semana actual o la mayor queda azul (sino gris)
            if (porcentajeAltura == 100 && venta > 0)
            {
                barra.Attributes["class"] = "chart-bar active"; // Azul
            }
            else
            {
                barra.Attributes["class"] = "chart-bar"; // Gris
            }

            // Tooltip del navegador al pasar el mouse
            barra.Attributes["title"] = venta.ToString("C");
        }
    }
}
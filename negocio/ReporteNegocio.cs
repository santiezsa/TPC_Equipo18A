using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ReporteNegocio
    {
        // 1) VENTAS POR PERÍODO (detalle)
        public List<object> ReporteVentasPorPeriodo(DateTime? desde, DateTime? hasta)
        {
            List<object> lista = new List<object>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = @"
                    SELECT v.Fecha, c.Nombre AS Cliente, v.Total
                    FROM Ventas v
                    INNER JOIN Clientes c ON v.IdCliente = c.Id
                    WHERE v.Activo = 1";

                if (desde.HasValue)
                    query += " AND CONVERT(date, v.Fecha) >= @desde";

                if (hasta.HasValue)
                    query += " AND CONVERT(date, v.Fecha) <= @hasta";

                query += " ORDER BY v.Fecha DESC";

                datos.setearConsulta(query);

                if (desde.HasValue)
                    datos.setearParametro("@desde", desde.Value.Date);   // solo fecha

                if (hasta.HasValue)
                    datos.setearParametro("@hasta", hasta.Value.Date);   // solo fecha

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new
                    {
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        Cliente = (string)datos.Lector["Cliente"],
                        Total = (decimal)datos.Lector["Total"]
                    });
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // 2) VENTAS POR PRODUCTO (ranking productos)
        public List<object> ReporteVentasPorProducto(DateTime? desde, DateTime? hasta)
        {
            List<object> lista = new List<object>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = @"
            SELECT p.Codigo,
                   p.Nombre AS Producto,
                   SUM(dv.Cantidad) AS CantidadVendida,
                   SUM(dv.Cantidad * dv.PrecioUnitario) AS TotalFacturado
            FROM DetalleVenta dv
            INNER JOIN Ventas v ON dv.IdVenta = v.Id
            INNER JOIN Productos p ON dv.IdProducto = p.Id
            WHERE v.Activo = 1";

                if (desde.HasValue)
                    query += " AND CONVERT(date, v.Fecha) >= @desde";

                if (hasta.HasValue)
                    query += " AND CONVERT(date, v.Fecha) <= @hasta";

                query += @"
            GROUP BY p.Codigo, p.Nombre
            ORDER BY CantidadVendida DESC";

                datos.setearConsulta(query);

                if (desde.HasValue)
                    datos.setearParametro("@desde", desde.Value.Date);

                if (hasta.HasValue)
                    datos.setearParametro("@hasta", hasta.Value.Date);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new
                    {
                        Codigo = (string)datos.Lector["Codigo"],
                        Producto = (string)datos.Lector["Producto"],
                        CantidadVendida = (int)datos.Lector["CantidadVendida"],
                        TotalFacturado = (decimal)datos.Lector["TotalFacturado"]
                    });
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // 3) VENTAS POR CLIENTE (ranking clientes)
        public List<object> ReporteVentasPorCliente(DateTime? desde, DateTime? hasta)
        {
            List<object> lista = new List<object>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = @"
            SELECT c.Nombre AS Cliente,
                   COUNT(v.Id) AS CantidadVentas,
                   SUM(v.Total) AS MontoTotal
            FROM Ventas v
            INNER JOIN Clientes c ON v.IdCliente = c.Id
            WHERE v.Activo = 1";

                if (desde.HasValue)
                    query += " AND CONVERT(date, v.Fecha) >= @desde";

                if (hasta.HasValue)
                    query += " AND CONVERT(date, v.Fecha) <= @hasta";

                query += @"
            GROUP BY c.Nombre
            ORDER BY MontoTotal DESC";

                datos.setearConsulta(query);

                if (desde.HasValue)
                    datos.setearParametro("@desde", desde.Value.Date);

                if (hasta.HasValue)
                    datos.setearParametro("@hasta", hasta.Value.Date);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new
                    {
                        Cliente = (string)datos.Lector["Cliente"],
                        CantidadVentas = (int)datos.Lector["CantidadVentas"],
                        MontoTotal = (decimal)datos.Lector["MontoTotal"]
                    });
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // 4) COMPRAS POR PROVEEDOR
        public List<object> ReporteComprasPorProveedor(DateTime? desde, DateTime? hasta)
        {
            List<object> lista = new List<object>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = @"
            SELECT p.Nombre AS Proveedor,
                   COUNT(c.Id) AS CantidadCompras,
                   SUM(c.Total) AS MontoTotal
            FROM Compras c
            INNER JOIN Proveedores p ON c.IdProveedor = p.Id
            WHERE c.Activo = 1";

                if (desde.HasValue)
                    query += " AND CONVERT(date, c.Fecha) >= @desde";

                if (hasta.HasValue)
                    query += " AND CONVERT(date, c.Fecha) <= @hasta";

                query += @"
            GROUP BY p.Nombre
            ORDER BY MontoTotal DESC";

                datos.setearConsulta(query);

                if (desde.HasValue)
                    datos.setearParametro("@desde", desde.Value.Date);

                if (hasta.HasValue)
                    datos.setearParametro("@hasta", hasta.Value.Date);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new
                    {
                        Proveedor = (string)datos.Lector["Proveedor"],
                        CantidadCompras = (int)datos.Lector["CantidadCompras"],
                        MontoTotal = (decimal)datos.Lector["MontoTotal"]
                    });
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // 5) STOCK BAJO MÍNIMO
        public List<object> ReporteStockBajoMinimo(int stockMinimo)
        {
            List<object> lista = new List<object>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = @"
            SELECT p.Codigo,
                   p.Nombre AS Producto,
                   p.StockActual
            FROM Productos p
            WHERE p.StockActual <= @minimo
            ORDER BY p.StockActual ASC";

                datos.setearConsulta(query);
                datos.setearParametro("@minimo", stockMinimo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new
                    {
                        Codigo = (string)datos.Lector["Codigo"],
                        Producto = (string)datos.Lector["Producto"],
                        StockActual = (int)datos.Lector["StockActual"]
                    });
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}

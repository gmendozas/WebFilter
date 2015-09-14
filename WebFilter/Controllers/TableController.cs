using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFilter.Models;

namespace WebFilter.Controllers
{
    public class TableController : Controller
    {
        // GET: Table
        public ActionResult Index()
        {
            
            return View(getGatitos());
        }

        public ActionResult Filter(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return PartialView("PartialTable", getGatitos());
            }
            else
            {
                filter = filter.Trim();
                var gatitos = from g in getGatitos()
                              where g.Nombre.ToLower().Contains(filter)
                              || g.Raza.ToLower().Contains(filter)
                              || g.Adoptante.ToLower().Contains(filter)
                              || g.Edad.ToString().ToLower().Contains(filter) 
                              select g;
                return PartialView("PartialTable", gatitos);
            }
        }

        public ActionResult ViewDetail(string parameter)
        {
            return PartialView("_ViewDetail", getDetalles(parameter));
        }

        private List<GatitoModel> getGatitos()
        {
            List<GatitoModel> gatitos = new List<GatitoModel>();
            gatitos.Add(new GatitoModel("Simon", 2, "Eléctrico", "Yosse"));
            gatitos.Add(new GatitoModel("Mateo", 2, "Eléctrico", "Yosse"));
            gatitos.Add(new GatitoModel("Bicha", 4, "Eléctrico", "Gil"));
            gatitos.Add(new GatitoModel("Piedritas", 2, "Eléctrico", "Gil"));
            gatitos.Add(new GatitoModel("Cobijas", 4, "Eléctrico", "Mamá"));
            return gatitos;
        }

        public List<GatitoDetalle> getDetalles(string parametro)
        {
            var detalles = from d in getGatitos()
                           where d.Nombre.Equals(parametro)
                           select new GatitoDetalle { Nombre = d.Nombre, Color = "Pardo", NoPatas = 5, Fisonomia = "Felina" };
            return detalles.ToList<GatitoDetalle>();
        }

        public ActionResult ExportarExcel()
        {
            var bytes = new byte[0];
            var model = getModel();
            bytes = GatitoModel.ExportarDetalleProcesoExcel(document =>
            {
                document.SetCellValue(1, 1, "Titulo");
                document.SetCellValue(3, 1, "Subtitulo");

                var column = 1;
                document.SetCellValue(5, column++, "Nombre del colaborador");
                document.SetCellValue(5, column++, "Fecha de ingreso");
                document.SetCellValue(5, column++, "Cliente");
                document.SetCellValue(5, column++, "Esquema");
                document.SetCellValue(5, column++, "Salario 1q");
                document.SetCellValue(5, column++, "Salario 2q");
                document.SetCellValue(5, column++, "Exento 1q");
                document.SetCellValue(5, column++, "Excento 2q");
                document.SetCellValue(5, column++, "Descuentos 1q");
                document.SetCellValue(5, column++, "Descuento 2q");
                document.SetCellValue(5, column++, "Asimilados");
                document.SetCellValue(5, column++, "ISR");
                document.SetCellValue(5, column++, "Monedero");
                document.SetCellValue(5, column++, "Comisión 8 %");
                document.SetCellValue(5, column++, "Bonos 1q");
                document.SetCellValue(5, column++, "Bonos 2q");
                document.SetCellValue(5, column++, "Impuestos 1q");
                document.SetCellValue(5, column++, "Impuestos 2q");
                document.SetCellValue(5, column++, "Infonavit 1q");
                document.SetCellValue(5, column++, "Infonavit 2q");
                document.SetCellValue(5, column++, "Aprov agunaldos 1q");
                document.SetCellValue(5, column++, "Aprov aguinal 2q");
                document.SetCellValue(5, column++, "Comisión people");
                document.SetCellValue(5, column++, "Prima vacacional q");
                document.SetCellValue(5, column++, "Prima vacacional 2q");

                var style = new SLStyle();
                style.SetFontBold(true);
                document.SetCellStyle(5, 1, 5, column, style);

                if (model.Count > 0)
                {
                    var currentRow = 6;
                    foreach (var k in model.Keys)
                    {
                        column = 1;
                        column = CurrentReporteRow(document, column, currentRow, model[k]);
                        currentRow++;
                    }
                }
                else
                    document.SetCellValue(6, 1, "No se encontraron resultados");

                for (int i = 1; i < column; i++)
                    document.AutoFitColumn(i);

                return true;
            });

            return File(bytes, "application/ms-excel", "MiReporte.xlsx");
        }

        private int CurrentReporteRow(SLDocument document, int column, int currentRow, string[] item)
        {
            foreach (var i in item)
            {
                // O todos los atributos de la entidad :)
                document.SetCellValue(currentRow, column++, i);
            }

            return column;
        }

        private Dictionary<string, string[]> getModel()
        {
            Dictionary<string, string[]> model = new Dictionary<string, string[]>();
            string[] array = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25" };
            model.Add("fila1", array);
            model.Add("fila2", array);
            model.Add("fila3", array);
            model.Add("fila4", array);
            model.Add("fila5", array);
            return model;
        }
    }
}
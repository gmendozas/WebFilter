using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebFilter.Models
{
    public class GatitoModel
    {
        public GatitoModel() { }

        public GatitoModel(string nombre, int edad, string raza, string adoptante) 
        {
            Nombre = nombre;
            Edad = edad;
            Raza = raza;
            Adoptante = adoptante;
        }


        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private int edad;

        public int Edad
        {
            get { return edad; }
            set { edad = value; }
        }
        private string raza;

        public string Raza
        {
            get { return raza; }
            set { raza = value; }
        }
        private string adoptante;

        public string Adoptante
        {
            get { return adoptante; }
            set { adoptante = value; }
        }

        public static byte[] ExportarDetalleProcesoExcel(Func<SLDocument, bool> body)
        {
            using (var document = new SLDocument())
            {
                var name = document.GetCurrentWorksheetName();
                document.AddWorksheet("DetalleProceso");
                document.DeleteWorksheet(name);
                document.SelectWorksheet("DetalleProceso");
                body(document);

                var stream = new MemoryStream();
                document.SaveAs(stream);

                stream.Position = 0;
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
    }
}
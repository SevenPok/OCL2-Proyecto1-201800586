using Irony.Parsing;
using OCL2_Proyecto1_201800586.Analizador;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace OCL2_Proyecto1_201800586.Graphviz
{
    class Reporte
    {
        int i = 0;
        public void graficarArbol(ParseTreeNode raiz)
        {
            String arbolAST = "digraph ArbolAST{\n";
            arbolAST += "}";
            Graficador graficar = new Graficador();
            graficar.graficar(arbolAST);
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\compiladores2\arbolAST.svg")
            {
                UseShellExecute = true
            };
            p.Start();
        }

        public void Html_Errores(LinkedList<Errores> listaError)
        {

            String Contenido_html;
            Contenido_html = "<html><head><meta charset=\u0022utf-8\u0022></head>\n" +
            "<body>" +
            "<h1 align='center'>ERRORES ENCONTRADOS</h1></br>" +
            "<table cellpadding='10' border = '1' align='center'>" +
            "<tr>" +

            "<td><strong>Descripcion" +
            "</strong></td>" +

            "<td><strong>Tipo Error" +
            "</strong></td>" +

            "<td><strong>Fila" +
            "</strong></td>" +

            "<td><strong>Columna" +
            "</strong></td>" +

            "</tr>";

            String Cad_tokens = "";
            String tempo_tokens;


            Contenido_html = Contenido_html + Cad_tokens +
            "</table>" +
            "</body>" +
            "</html>";

            File.WriteAllText("C:\\compiladores2\\Reporte_de_Errores.html", Contenido_html);
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\compiladores2\Reporte_de_Errores.html")
            {
                UseShellExecute = true
            };
            p.Start();


        }

        public void HTML_ts(TablaSimbolo ts)
        {

            String Contenido_html;
            Contenido_html = "<html><head><meta charset=\u0022utf-8\u0022></head>\n" +
            "<body>" +
            "<h1 align='center'>Tabla de Simbolos</h1></br>" +
            "<table cellpadding='10' border = '1' align='center'>" +
            "<tr>" +

            "<td><strong>Id" +
            "</strong></td>" +

            "<td><strong>Tipo Dato" +
            "</strong></td>" +

             "<td><strong>Tipo Simbolo" +
            "</strong></td>" +


            "<td><strong>Valor" +
            "</strong></td>" +

            "<td><strong>Entorno" +
            "</strong></td>" +

            "</tr>";

            String Cad_tokens = "";
            String tempo_tokens;
            foreach (Simbolo sim in ts)
            {

            }

            Contenido_html = Contenido_html + Cad_tokens +
            "</table>" +
            "</body>" +
            "</html>";

            File.WriteAllText("C:\\compiladores2\\Tabla_Simbolos.html", Contenido_html);
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\compiladores2\Tabla_Simbolos.html")
            {
                UseShellExecute = true
            };
            p.Start();


        }

    }
}

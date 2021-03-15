using Irony.Parsing;
using OCL2_Proyecto1_201800586.Analizador;
using OCL2_Proyecto1_201800586.Arbol.Instrucciones;
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

        public void Html_Errores(LinkedList<Errores> listaError)
        {

            String Contenido_html;
            Contenido_html = "<html><head><meta charset=\u0022utf-8\u0022></head>\n" +
            "<body>" +
            "<h1 align='center'>ERRORES ENCONTRADOS</h1></br>" +
            "<table cellpadding='10' border = '1' align='center'>" +
            "<tr>" +

            "<td><strong>Tipo" +
            "</strong></td>" +

            "<td><strong>Descripcion" +
            "</strong></td>" +

            "<td><strong>Linea" +
            "</strong></td>" +

            "<td><strong>Columna" +
            "</strong></td>" +

            "</tr>";

            String Cad_tokens = "";
            String tempo_tokens;

            foreach(Errores e in listaError)
            {

                tempo_tokens = "";
                tempo_tokens = "<tr>" +

                "<td>" + e.tipo.ToString() +
                "</td>" +

                "<td>" + e.descripcion+
                "</td>" +

                "<td>" + e.linea +
                "</td>" +

                "<td>" + e.columna +
                "</td>" +

                "</tr>";
                Cad_tokens = Cad_tokens + tempo_tokens;
            }

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

             "<td><strong>Tipo Simbolo" +
            "</strong></td>" +

            "<td><strong>Ambito" +
            "</strong></td>" +

            "<td><strong>Linea" +
            "</strong></td>" +

            "<td><strong>Columna" +
            "</strong></td>" +

            "</tr>";

            String Cad_tokens = "";
            String tempo_tokens;
            foreach (Simbolo s in ts)
            {
                tempo_tokens = "";
                tempo_tokens = "<tr>" +

                "<td>" + s.Identificador+
                "</td>" +

                "<td>" + s.type.ToString() +
                "</td>" +

                "<td>" + s.Ambito +
                "</td>" +

                "<td>" + s.linea +
                "</td>" +

                 "<td>" + s.columna +
                "</td>" +

                "</tr>";
                Cad_tokens = Cad_tokens + tempo_tokens;
            }

            foreach(Funcion f in Sintactico.funciones)
            {
                foreach(Simbolo s in f.tablaLocal)
                {
                    tempo_tokens = "";
                    tempo_tokens = "<tr>" +

                    "<td>" + s.Identificador +
                    "</td>" +

                    "<td>" + s.type.ToString() +
                    "</td>" +

                    "<td>" + "Local en '" + f.identificador + "'" +
                    "</td>" +

                    "<td>" + s.linea +
                    "</td>" +

                     "<td>" + s.columna +
                    "</td>" +

                    "</tr>";
                    Cad_tokens = Cad_tokens + tempo_tokens;
                }
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

        public void errorLexicoSintactico(ParseTree arbol, ParseTreeNode raiz)
        {
            String Contenido_html;
            Contenido_html = "<html><head><meta charset=\u0022utf-8\u0022></head>\n" +
            "<body>" +
            "<h1 align='center'>ERRORES ENCONTRADOS</h1></br>" +
            "<table cellpadding='10' border = '1' align='center'>" +
            "<tr>" +

            "<td><strong>Descripcion" +
            "</strong></td>" +

            "<td><strong>Linea" +
            "</strong></td>" +

            "<td><strong>Columna" +
            "</strong></td>" +

            "</tr>";

            String Cad_tokens = "";
            
            if (arbol.ParserMessages.Count > 0 || raiz == null)
            {
                String tempo_tokens;
                for (int i = 0; i < arbol.ParserMessages.Count; i++)
                {

                    tempo_tokens = "";
                    tempo_tokens = "<tr>" +

                    "<td>" + arbol.ParserMessages[i].Message +
                    "</td>" +

                    "<td>" + arbol.ParserMessages[i].Location.Line +
                    "</td>" +

                    "<td>" + arbol.ParserMessages[i].Location.Column +
                    "</td>" +

                    "</tr>";
                    Cad_tokens = Cad_tokens + tempo_tokens;
                }
            }

            Contenido_html = Contenido_html + Cad_tokens +
            "</table>" +
            "</body>" +
            "</html>";

            File.WriteAllText("C:\\compiladores2\\Reporte_de_Errores_Lexicos_Sintacticos.html", Contenido_html);
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\compiladores2\Reporte_de_Errores_Lexicos_Sintacticos.html")
            {
                UseShellExecute = true
            };
            p.Start();
        }

    }
}

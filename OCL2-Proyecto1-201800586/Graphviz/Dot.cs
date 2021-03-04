using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OCL2_Proyecto1_201800586.Graphviz
{
    class Dot
    {
        private static int contador;
        private static String grafo;

        public static string getDot(ParseTreeNode root)
        {
            grafo = "digraph G {";
            grafo += "nodo0[label=\""+ escapar(root.ToString())+"\"];\n";
            contador = 1;
            recorrerAST("nodo0", root);
            grafo += "}\n";
            return grafo;
        }

        private static void recorrerAST(String padre, ParseTreeNode hijos)
        {
            foreach(ParseTreeNode hijo in hijos.ChildNodes)
            {
                String nombreHijo = "nodo" + contador.ToString();
                grafo += nombreHijo + "[label=\"" + escapar(Regex.Replace(hijo.ToString(), " \\([a-zA-Z ]*\\)", "")) + "\"];\n";
                grafo += padre + "->" + nombreHijo + ";\n";
                contador++;
                recorrerAST(nombreHijo, hijo);
            }
        }
        private static String escapar(String cadena)
        {
            cadena = cadena.Replace("\\", "\\\\");
            cadena = cadena.Replace("\"", "\\\"");
            return cadena;
        }
    }
}

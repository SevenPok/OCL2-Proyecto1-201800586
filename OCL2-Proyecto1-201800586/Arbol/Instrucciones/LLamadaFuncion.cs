using OCL2_Proyecto1_201800586.Analizador;
using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class LLamadaFuncion : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public LinkedList<Operacion> parametros;
        public String identificador;

        public LLamadaFuncion(String identificador, LinkedList<Operacion> parametros, int linea, int columna)
        {
            this.identificador = identificador;
            this.parametros = parametros;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            //if (funcion == null)
            //{
            //    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " La funcion '" + identificador + "' no existe.\n";
            //    return null;
            //}
            foreach(Funcion f in Sintactico.funciones)
            {
                if (f.identificador == identificador)
                {
                    return f.llamar(ts, parametros);
                }
            }
            Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " La funcion '" + identificador + "' no existe.\n";
            return null;
        }
    }
}

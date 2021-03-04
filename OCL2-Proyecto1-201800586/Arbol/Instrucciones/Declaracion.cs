using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Declaracion : Instruccion
    {
        private String identificador;
        Simbolo.Tipo tipo;

        public Declaracion(String identificador, Simbolo.Tipo tipo, int linea, int columna)
        {
            this.identificador = identificador;
            this.tipo = tipo;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public object ejeuctar(TablaSimbolo ts)
        {
            if (ts.getIdentificador(identificador) == null)
            {
                ts.AddLast(new Simbolo(identificador, tipo));
            }
            else
            {
                Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " La variable '" + identificador + "' ya ha sido declarado\n";
            }
            
            return null;
        }
    }
}

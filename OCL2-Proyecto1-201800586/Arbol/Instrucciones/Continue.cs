using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Continue : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public Continue(int linea, int columna)
        {
            this.linea = linea + 1;
            this.columna = columna + 1;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            return this;
        }
    }
}

using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Break : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public Break(int linea, int columna)
        {
            this.linea = linea;
            this.columna = columna;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            return this;
        }
    }
}

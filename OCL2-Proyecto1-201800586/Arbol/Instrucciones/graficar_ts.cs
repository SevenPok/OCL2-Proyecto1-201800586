using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using OCL2_Proyecto1_201800586.Graphviz;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class graficar_ts : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public graficar_ts(int linea, int columna)
        {
            this.linea = linea + 1;
            this.columna = columna + 1;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            Reporte reporte = new Reporte();
            reporte.HTML_ts(ts);
            return null;
        }
    }
}

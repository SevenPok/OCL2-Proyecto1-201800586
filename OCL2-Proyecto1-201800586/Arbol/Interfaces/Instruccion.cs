using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;
using static OCL2_Proyecto1_201800586.Arbol.Valores.Simbolo;

namespace OCL2_Proyecto1_201800586.Arbol.Interfaces
{
    interface Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public Object ejeuctar(TablaSimbolo ts);
    }
}

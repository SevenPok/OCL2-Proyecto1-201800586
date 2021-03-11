using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Procedimiento : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }
        public object ejeuctar(TablaSimbolo ts)
        {
            throw new NotImplementedException();
        }
    }
}

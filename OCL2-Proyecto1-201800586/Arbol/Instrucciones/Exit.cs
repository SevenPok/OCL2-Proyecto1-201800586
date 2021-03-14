using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Exit : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public Operacion operacion;

        public Exit(Operacion operacion, int linea, int columna)
        {
            this.operacion = operacion;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public object ejeuctar(TablaSimbolo ts)
        {
            return this;
        }

        public object getValorImplicito(TablaSimbolo ts)
        {
            if (operacion != null)
            {
                return operacion.ejeuctar(ts);
            }
            return null;    
        }
    }
}

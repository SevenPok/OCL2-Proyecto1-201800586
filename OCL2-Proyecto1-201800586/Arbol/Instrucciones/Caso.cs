using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Caso : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        private Operacion caso;
        private LinkedList<Instruccion> sentencias;

        public Caso(Operacion caso, LinkedList<Instruccion> sentencias)
        {
            this.caso = caso;
            this.sentencias = sentencias;
        }

        public object ejeuctar(TablaSimbolo ts)
        {
            foreach(Instruccion sentencia in sentencias)
            {
                sentencia.ejeuctar(ts);
            }
            return null;
        }

        public object ejecutarCaso(TablaSimbolo ts)
        {
            return caso.ejeuctar(ts);
        }
    }
}

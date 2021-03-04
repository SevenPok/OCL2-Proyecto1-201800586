using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Main : Instruccion
    {
        private LinkedList<Instruccion> sentencias;
        public int linea { get; set; }
        public int columna { get; set; }
        public Main(LinkedList<Instruccion> sentencias)
        {
            this.sentencias = sentencias;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            foreach (Instruccion sentencia in sentencias)
            {
                sentencia.ejeuctar(ts);
            }

            return null;
        }
    }
}

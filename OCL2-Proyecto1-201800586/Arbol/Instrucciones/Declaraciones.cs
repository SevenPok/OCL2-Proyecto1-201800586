using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Declaraciones : Instruccion
    {
        private LinkedList<Instruccion> declaraciones;
        public int linea { get; set; }
        public int columna { get; set; }
        public Declaraciones(LinkedList<Instruccion> declaraciones)
        {
            this.declaraciones = declaraciones;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            foreach (Instruccion declaracion in declaraciones)
            {
                declaracion.ejeuctar(ts);
            }
            return null;
        }
    }
}

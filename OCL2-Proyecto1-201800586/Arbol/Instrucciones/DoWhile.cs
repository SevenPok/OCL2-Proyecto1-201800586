using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class DoWhile : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        private LinkedList<Instruccion> sentencias;
        private Operacion condicion;

        public DoWhile(LinkedList<Instruccion> sentencias, Operacion condicion, int linea, int columna)
        {
            this.sentencias = sentencias;
            this.condicion = condicion;
            this.linea = linea;
            this.columna = columna;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            Object expresion = condicion.ejeuctar(ts);

            if (expresion != null)
            {
                do
                {
                    TablaSimbolo tablaLocal = new TablaSimbolo();
                    foreach (Simbolo item in ts)
                    {
                        tablaLocal.AddLast(item);
                    }
                    foreach (Instruccion ins in sentencias)
                    {
                        ins.ejeuctar(tablaLocal);
                    }
                } while (!(Boolean)condicion.ejeuctar(ts));
            }
            else
            {
                Form1.consola.Text += "La sentencia repeat solo acepta condiciones logicas y relacionales.";
            }
            return null;
        }
    }
}

using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class While : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        private Operacion condicion;
        private LinkedList<Instruccion> instrucciones;

        public While(Operacion condicion, LinkedList<Instruccion> instrucciones)
        {
            this.condicion = condicion;
            this.instrucciones = instrucciones;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            //TablaSimbolo local = new TablaSimbolo();
            Object expresion = condicion.ejeuctar(ts);
        
            if (expresion != null)
            {
                while ((Boolean)condicion.ejeuctar(ts))
                {
                    TablaSimbolo tablaLocal = new TablaSimbolo();
                    foreach (Simbolo item in ts)
                    {
                        tablaLocal.AddLast(item);
                    }
                    foreach (Instruccion ins in instrucciones)
                    {
                        ins.ejeuctar(tablaLocal);
                    }
                }
            }
            else
            {
                Form1.consola.Text += "La sentencia while solo acepta condiciones logicas y relacionales.";
            }
            return null;

        }
    }
}

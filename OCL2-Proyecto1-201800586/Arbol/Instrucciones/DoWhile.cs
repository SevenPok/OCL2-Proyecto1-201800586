using OCL2_Proyecto1_201800586.Analizador;
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
            this.linea = linea + 1;
            this.columna = columna + 1;
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
                        Object o = ins.ejeuctar(tablaLocal);
                        if (o != null && o.GetType().Equals(typeof(Break)))
                        {
                            return null;
                        }
                        else if (o != null && o.GetType().Equals(typeof(Continue)))
                        {
                            break;
                        }
                        else if (o is Exit)
                        {
                            return o;
                        }

                    }
                } while (!(Boolean)condicion.ejeuctar(ts));
            }
            else
            {
                Form1.consola.Text += "La sentencia repeat solo acepta condiciones logicas y relacionales.";
                Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "La sentencia 'repeat' solo acepta condiciones logicas y relacionales."));
            }
            return null;
        }
    }
}

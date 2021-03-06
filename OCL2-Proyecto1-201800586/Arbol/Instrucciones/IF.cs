using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class IF : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        private Operacion condicion;
        private LinkedList<Instruccion> instruccionesIf;
        private LinkedList<Instruccion> instruccionesElse;
        private LinkedList<Instruccion> elseif;

        public IF(Operacion condicion, LinkedList<Instruccion> instruccionesIf)
        {
            this.condicion = condicion;
            this.instruccionesIf = instruccionesIf;
        }

        public IF(Operacion condicion, LinkedList<Instruccion> instruccionesIf, LinkedList<Instruccion> instruccionesElse)
        {
            this.condicion = condicion;
            this.instruccionesIf = instruccionesIf;
            this.instruccionesElse = instruccionesElse;
        }

        public IF(Operacion condicion, LinkedList<Instruccion> instruccionesIf, LinkedList<Instruccion> instruccionesElse, LinkedList<Instruccion> elseif)
        {
            this.condicion = condicion;
            this.instruccionesIf = instruccionesIf;
            this.instruccionesElse = instruccionesElse;
            this.elseif = elseif;
        }

        public object ejeuctar(TablaSimbolo ts)
        {
            Object expresion = condicion.ejeuctar(ts);

            if (expresion != null && expresion.GetType().Equals(typeof(Boolean)))
            {
                if ((Boolean)expresion)
                {
                    TablaSimbolo tablaLocal = new TablaSimbolo();

                    foreach (Simbolo item in ts)
                    {
                        tablaLocal.AddLast(item);
                    }

                    foreach (Instruccion ins in instruccionesIf)
                    {
                        ins.ejeuctar(tablaLocal);
                    }
                    return true;
                }
                else
                {
                    if (elseif != null)
                    {
                        foreach(Instruccion inst in elseif)
                        {
                           Object aux = inst.ejeuctar(ts);
                            if (aux == null)
                            {
                                return false;
                            }
                            if ((Boolean)aux == true)
                            {
                                return true;
                            }
                        }
                    }

                    if (instruccionesElse != null)
                    {
                        TablaSimbolo tablaLocal = new TablaSimbolo();
                        foreach (Simbolo item in ts)
                        {
                            tablaLocal.AddLast(item);
                        }
                        foreach (Instruccion ins in instruccionesElse)
                        {
                            ins.ejeuctar(tablaLocal);
                        }
                        return true;
                    }
                }
            }
            else
            {
                Form1.consola.Text += "Linea: " + condicion.linea + " Columna: " + condicion.columna + " El if solo acepta condiciones \n";
                return null;
            }
            
            return false;
        }
    }
}

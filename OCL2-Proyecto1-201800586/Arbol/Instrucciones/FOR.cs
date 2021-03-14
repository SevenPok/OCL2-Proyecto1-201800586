
using OCL2_Proyecto1_201800586.Analizador;
using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class FOR : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        private Asignacion asignar;
        private Operacion inicializar;
        private Operacion finalizar;
        //Solo acpeta 1 o -1
        private int incrementar_decrementar;
        private LinkedList<Instruccion> instrucciones;
        public FOR(Asignacion asignar, Operacion inicializar, Operacion finalizar, int incrementar_decrementar, LinkedList<Instruccion> instrucciones, int linea, int columna)
        {
            this.asignar = asignar;
            this.inicializar = inicializar;
            this.finalizar = finalizar;
            this.incrementar_decrementar = incrementar_decrementar;
            this.instrucciones = instrucciones;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        object Instruccion.ejeuctar(TablaSimbolo ts)
        {
            if ((Boolean)asignar.ejeuctar(ts))
            {
                Object ini = inicializar.ejeuctar(ts);
                Object fin = finalizar.ejeuctar(ts);
                if (ini != null && fin != null && Regex.IsMatch(ini.ToString(), "^-?[0-9]+$") && Regex.IsMatch(fin.ToString(), "^-?[0-9]+$"))
                {
                    if (incrementar_decrementar > 0)
                    {
                        if((double)fin - (double)fin >= 0)
                        {
                            for (double i = (double)ini; i <= (double)fin; i++)
                            {
                                ts.setValor(asignar.identificador, i);
                                foreach (Instruccion sentencia in instrucciones)
                                {
                                    Object o = sentencia.ejeuctar(ts);
                                    if (o != null && o.GetType().Equals(typeof(Break)))
                                    {
                                        return null;
                                    }
                                    else if (o != null && o is Continue)
                                    {
                                        break;
                                    }
                                    else if (o is Exit)
                                    {
                                        return o;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + ", Los limites de la sentencia 'For' no son los correctos\n";
                            Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "Los limites de la sentencia 'For' no son los correctos"));
                        }
                    }
                    else
                    {
                        if ((double)fin - (double)fin <= 0)
                        {
                            for (double i = (double)ini; i >= (double)fin; i--)
                            {
                                ts.setValor(asignar.identificador, i);
                                foreach (Instruccion sentencia in instrucciones)
                                {
                                    Object o = sentencia.ejeuctar(ts);
                                    if (o != null && o.GetType().Equals(typeof(Break)))
                                    {
                                        return null;
                                    }
                                    else if (o != null && o is Continue)
                                    {
                                        break;
                                    }
                                    else if (o is Exit)
                                    {
                                        return o;
                                    }

                                }
                            }
                        }
                        else
                        {
                            Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + ", Los limites de la sentencia 'For' no son los correctos\n";
                            Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "Los limites de la sentencia 'For' no son los correctos"));
                        }
                    }
                }
                else
                {
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + ", La sentencia 'For' solo acepta enteros\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "La sentencia 'For' solo acepta enteros"));
                }
            }
            return null;
        }
    }
}

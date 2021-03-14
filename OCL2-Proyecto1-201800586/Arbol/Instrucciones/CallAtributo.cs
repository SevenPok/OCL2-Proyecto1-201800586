using OCL2_Proyecto1_201800586.Analizador;
using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class CallAtributo : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public String identificador;
        public LinkedList<String> atributos;

        public CallAtributo(String identificador, LinkedList<String> atributos, int linea, int columna)
        {
            this.identificador = identificador;
            this.atributos = atributos;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            if (ts.existe(identificador) && ts.getTipo(identificador) == Simbolo.Tipo.OBJETO)
            {
                TablaSimbolo local = (TablaSimbolo)ts.getValor(identificador);
                int i = 1;
                foreach(String atributo in atributos)
                {
                    if (local.existe(atributo))
                    {
                        Object o = local.getValor(atributo);
                        //Form1.consola.Text += o + "\n";
                        if (o is TablaSimbolo)
                        {
                            local = (TablaSimbolo)o;
                            if(i == atributos.Count)
                            {
                                return local;
                            }
                        }
                        else if (i == atributos.Count)
                        {
                            return o;
                        }
                        else
                        {
                            Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El tributo '" + atributo + "' no es de tipo objeto para acceder a sus atributos\n";
                            Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, " El tributo '" + atributo + "' no es de tipo objeto para acceder a sus atributos"));
                            return null;
                        }
                    }
                    else
                    {
                        Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El tributo '" + atributo + "' no esta declarado\n";
                        Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, " El tributo '" + atributo + "' no esta declarado"));
                        return null;
                    }
                    i++;
                }
            }
            else
            {
                Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El objeto '" + identificador + "' no esta declarado\n";
                Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, " El objeto '" + identificador + "' no esta declarado"));
            }
            return null;
        }
    }
}

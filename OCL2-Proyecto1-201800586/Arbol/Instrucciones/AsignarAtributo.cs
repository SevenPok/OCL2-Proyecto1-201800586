using OCL2_Proyecto1_201800586.Analizador;
using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class AsignarAtributo : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public String identificador;
        private LinkedList<String> atributos;
        public Operacion valor;
        public TablaSimbolo objeto;

        public AsignarAtributo(String identificador, LinkedList<String> atributos, Operacion valor, int linea, int columna)
        {
            this.identificador = identificador;
            this.atributos = atributos;
            this.valor = valor;
        }

        public AsignarAtributo(String identificador, LinkedList<String> atributos, TablaSimbolo objeto, int linea, int columna)
        {
            this.identificador = identificador;
            this.atributos = atributos;
            this.objeto = objeto;
        }

        public object ejeuctar(TablaSimbolo ts)
        {
            if (ts.existe(identificador))
            {
                if(ts.getValor(identificador) is TablaSimbolo)
                {
                    Object ob = valor.ejeuctar(ts);
                    if (ob != null)
                    {
                        TablaSimbolo local = (TablaSimbolo)ts.getValor(identificador);
                        int i = 1;
                        foreach (String atributo in atributos)
                        {
                            if (local.existe(atributo))
                            {
                                Object o = local.getValor(atributo);
                                //Form1.consola.Text += o + "\n";
                                if (o is TablaSimbolo)
                                {
                                    if (i == atributos.Count)
                                    {
                                        if (ob is TablaSimbolo)
                                        {
                                            local.setValor(atributo, (TablaSimbolo)((TablaSimbolo)ob).Clone());
                                        }
                                        else
                                        {
                                            Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El tributo '" + atributo + "' no es de tipo objeto\n";
                                            Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El tributo '" + atributo + "' no es de tipo objeto"));
                                        }
                                        return null;
                                    }
                                    local = (TablaSimbolo)o;
                                }
                                else if (i == atributos.Count)
                                {
                                    String tipo = local.getSimbolo(atributo).type.ToString();
                                    if (tipo == "ENTERO" && Regex.IsMatch(ob.ToString(), "^-?[0-9]+$"))
                                    {
                                        local.setValor(atributo, ob);
                                    }
                                    else if (tipo == "DECIMAL" && ob.GetType().Equals(typeof(Double)))
                                    {
                                        local.setValor(atributo, ob);
                                    }
                                    else if (tipo == "STRING" && ob.GetType().Equals(typeof(String)))
                                    {
                                        local.setValor(atributo, ob);
                                    }
                                    else if (tipo == "BOOLEANA" && ob.GetType().Equals(typeof(Boolean)))
                                    {
                                        local.setValor(atributo, ob);
                                    }
                                    else
                                    {
                                        Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " La expresion no coincide con el tipo de dato\n";
                                        Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "La expresion no coincide con el tipo de dato"));
                                        return null;
                                    }
                                    return null;
                                }
                                else
                                {
                                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El tributo '" + atributo + "' no es de tipo objeto para acceder a sus atributos\n";
                                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El tributo '" + atributo + "' no es de tipo objeto para acceder a sus atributos"));
                                    return null;
                                }
                            }
                            else
                            {
                                Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El tributo '" + atributo + "' no esta declarado\n";
                                Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El tributo '" + atributo + "' no esta declarado"));
                                return null;
                            }
                            i++;
                        }
                    }
                    else
                    {
                        Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " La expresion no coincide con el tipo de dato\n";
                        Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "La expresion no coincide con el tipo de dato"));
                    }
                }
                else
                {
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " La variable '" + identificador + "' no es objeto\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "La variable '" + identificador + "' no es objeto"));
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

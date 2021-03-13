using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Asignacion : Instruccion
    {
        public String identificador;
        public String atributo;
        private Operacion valor;
        public int linea { get; set; }
        public int columna { get; set; }
        public Asignacion(String identificador, Operacion valor, int linea, int columna)
        {
            this.identificador = identificador;
            this.valor = valor;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public Asignacion(String identificador, String atributo, Operacion valor, int linea, int columna)
        {
            this.identificador = identificador;
            this.atributo = atributo;
            this.valor = valor;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            string tipo = ts.getTipo(identificador).ToString();
            Simbolo constante = ts.getSimbolo(identificador);
            if(constante != null && constante.constate)
            {
                Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El identificador '" + identificador + "' es una constante.\n";
                return null;
            }
            Object aux = valor.ejeuctar(ts);
            //Form1.consola.Text += aux;
            if (aux != null)
            {
                if (tipo == Simbolo.Tipo.ENTERO.ToString())
                {
                    if (Regex.IsMatch(aux.ToString(), "^-?[0-9]+$"))
                    {
                        ts.setValor(identificador, aux);
                        return true;
                    }
                    else
                    {
                        Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " No se puede asignar un tipo '" + aux.GetType().ToString() + "' a un tipo '" + tipo + "'\n";
                    }
                }
                else if (tipo == Simbolo.Tipo.DECIMAL.ToString())
                {
                    if (aux.GetType().Equals(typeof(Double)))
                    {
                        ts.setValor(identificador, aux);
                        return true;
                    }
                    else
                    {
                        Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " No se puede asignar un tipo '" + aux.GetType().ToString() + "' a un tipo '" + tipo + "'\n";
                    }
                }
                else if (tipo == Simbolo.Tipo.CADENA.ToString())
                {
                    if (aux.GetType().Equals(typeof(String)))
                    {
                        ts.setValor(identificador, aux);
                        return true;
                    }
                    else
                    {
                        Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " No se puede asignar un tipo '" + aux.GetType().ToString() + "' a un tipo '" + tipo + "'\n";
                    }
                }
                else if (tipo == Simbolo.Tipo.BOOLEANA.ToString())
                {
                    if (aux.GetType().Equals(typeof(Boolean)))
                    {
                        ts.setValor(identificador, aux);
                        return true;
                    }
                    else
                    {
                        Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " No se puede asignar un tipo '" + aux.GetType().ToString() + "' a un tipo '" + tipo + "'\n";
                    }
                }
                else if (tipo == Simbolo.Tipo.OBJETO.ToString())
                {
                    if (ts.existe(identificador))
                    {
                        TablaSimbolo atributos = (TablaSimbolo)ts.getValor(identificador);
                        if (atributos.existe(atributo))
                        {
                            if (Regex.IsMatch(aux.ToString(), "^-?[0-9]+$") && atributos.getTipo(atributo) == Simbolo.Tipo.ENTERO)
                            {
                                atributos.setValor(atributo, (Double)aux);
                                return true;
                            }
                            else if (aux.GetType().Equals(typeof(Double)) && atributos.getTipo(atributo) == Simbolo.Tipo.DECIMAL)
                            {
                                atributos.setValor(atributo, (Double)aux);
                                return true;
                            }
                            else if (aux.GetType().Equals(typeof(String)) && atributos.getTipo(atributo) == Simbolo.Tipo.CADENA)
                            {
                                atributos.setValor(atributo, (String)aux);
                                return true;
                            }
                            else if (aux.GetType().Equals(typeof(Boolean)) && atributos.getTipo(atributo) == Simbolo.Tipo.BOOLEANA)
                            {
                                atributos.setValor(atributo, (Boolean)aux);
                                return true;
                            }

                            Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " No se puede asignar un tipo '" + aux.GetType().ToString() + "' a un tipo '" + atributos.getTipo(atributo) + "'\n";
                        }
                        else
                        {
                            Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El atributo '" + atributo + "' no existe\n";
                        }
                    }
                }
                else if (tipo == Simbolo.Tipo.STRUCT.ToString())
                {
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " No se puede asignar un objeto'\n";
                }
                else
                {
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " No se puede asignar un tipo '" + aux.GetType().ToString() + "' a un tipo '" + tipo + "'\n";
                }
            }
            Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El identificador '" + identificador + "' es desconocido.\n";
            return false;
        }
    }
}

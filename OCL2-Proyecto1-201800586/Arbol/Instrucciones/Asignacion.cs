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
        private String identificador;
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
        public object ejeuctar(TablaSimbolo ts)
        {
            string tipo = ts.getTipo(identificador).ToString();
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
                else
                {
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " No se puede asignar un tipo '" + aux.GetType().ToString() + "' a un tipo '" + tipo + "'\n";
                }
            }
            return false;
        }
    }
}

using OCL2_Proyecto1_201800586.Analizador;
using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Funcion : Instruccion, ICloneable
    {
        public int linea { get; set; }
        public int columna { get; set; }

        public String identificador;
        public LinkedList<Declaracion> parametros;
        //public LinkedList<Asignacion> asignaciones;
        private LinkedList<Instruccion> instrucciones;
        private LinkedList<Instruccion> sentencias;
        //public LinkedList<Operacion> operaciones;
        public Simbolo.Tipo tipo;
        public bool disponible = true;
        public TablaSimbolo tablaLocal = new TablaSimbolo();

        public Funcion(String identificador, LinkedList<Declaracion> parametros, LinkedList<Instruccion> instrucciones, LinkedList<Instruccion> sentencias, Simbolo.Tipo tipo, int linea, int columna)
        {
            this.identificador = identificador;
            this.parametros = parametros;
            this.instrucciones = instrucciones;
            this.sentencias = sentencias;
            this.tipo = tipo;
            this.linea = linea + 1;
            this.columna = columna + 1;
            //operaciones = new LinkedList<Operacion>();
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            if (ts.getIdentificador(identificador) == null)
            {
                if (tipo == Simbolo.Tipo.ENTERO)
                {
                    Simbolo nuevo = new Simbolo(identificador, tipo, 0, linea, columna, "global");
                    nuevo.linea = linea;
                    nuevo.columna = columna;
                    ts.AddLast(nuevo);
                }
                else if (tipo == Simbolo.Tipo.DECIMAL)
                {
                    Simbolo nuevo = new Simbolo(identificador, tipo, (Double)0, linea, columna, "global");
                    nuevo.linea = linea;
                    nuevo.columna = columna;
                    ts.AddLast(nuevo);
                }
                else if (tipo == Simbolo.Tipo.BOOLEANA)
                {
                    Simbolo nuevo = new Simbolo(identificador, tipo, true, linea, columna, "global");
                    nuevo.linea = linea;
                    nuevo.columna = columna;
                    ts.AddLast(nuevo);
                }
                else if (tipo == Simbolo.Tipo.CADENA)
                {
                    Simbolo nuevo = new Simbolo(identificador, tipo, "", linea, columna, "global");
                    nuevo.linea = linea;
                    nuevo.columna = columna;
                    ts.AddLast(nuevo);
                }
                else if(tipo == Simbolo.Tipo.VOID)
                {
                    Simbolo nuevo = new Simbolo(identificador, tipo, null, linea, columna, "global");
                    nuevo.linea = linea;
                    nuevo.columna = columna;
                    ts.AddLast(nuevo);
                }
                else
                {
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El tipo de dato es desconocido\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El tipo de dato es desconocido"));
                }
            }
            else
            {
                Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El identificador '" + identificador + "' ya ha sido declarado\n";
                Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El identificador '" + identificador + "' ya ha sido declarado"));
                disponible = false;
            }
            
            return null;
        }

        public object llamar(TablaSimbolo ts, LinkedList<Operacion> operaciones)
        {
            
            if (parametros.Count == operaciones.Count)
            {
                TablaSimbolo local = new TablaSimbolo();
                local.AddLast(ts.getSimbolo(identificador));
                ArrayList ides = new ArrayList();
                LinkedList<String> referncias = new LinkedList<string>();
                LinkedList<String> refernciasParametros = new LinkedList<string>();
                LinkedList<int> posicion = new LinkedList<int>();
                int i = 0;
                foreach (Declaracion declaracion in parametros)
                {
                    ides.Add(declaracion);
                    declaracion.ejeuctar(local);
                    if (declaracion.referencia)
                    {
                        referncias.AddLast(declaracion.identificador);
                        posicion.AddLast(i);
                    }
                    i++;
                }
                int contador = 0;
                foreach (Operacion operacion in operaciones)
                {
                    Object valor = operacion.ejeuctar(ts);
                    if(valor != null)
                    {
                        if (Regex.IsMatch(valor.ToString(), "^-?[0-9]+$") && ((Declaracion)ides[contador]).tipo == Simbolo.Tipo.ENTERO)
                        {
                            local.setValor(((Declaracion)ides[contador]).identificador, (Double)valor);
                        }
                        else if (valor.GetType().Equals(typeof(Double)) && ((Declaracion)ides[contador]).tipo == Simbolo.Tipo.DECIMAL || ((Declaracion)ides[contador]).tipo == Simbolo.Tipo.ENTERO)
                        {
                            local.setValor(((Declaracion)ides[contador]).identificador, (Double)valor);
                        }
                        else if (valor.GetType().Equals(typeof(Boolean)) && ((Declaracion)ides[contador]).tipo == Simbolo.Tipo.BOOLEANA)
                        {
                            local.setValor(((Declaracion)ides[contador]).identificador, (Boolean)valor);
                        }
                        else if (valor.GetType().Equals(typeof(String)) && ((Declaracion)ides[contador]).tipo == Simbolo.Tipo.CADENA)
                        {
                            local.setValor(((Declaracion)ides[contador]).identificador, (string)valor);
                        }



                        foreach (int pos in posicion)
                        {
                            if (pos == contador)
                            {
                                if (operacion.tipo == Operacion.Tipo.IDENTIFICADOR)
                                {
                                    //Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " referencia a: " + operacion.valor.ToString() + "\n";
                                    refernciasParametros.AddLast(operacion.valor.ToString());
                                }
                                else
                                {
                                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " No se hizo referencia a un identificador.\n";
                                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "No se hizo referencia a un identificador"));
                                    return null;
                                }
                                  
                            }
                        }
                        
                    }
                    else
                    {
                        //Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " La variable no existe\n";
                        return null;
                    }
                    contador++;
                }

                foreach (Instruccion instruccion in instrucciones)
                {
                    instruccion.ejeuctar(local);
                }

                tablaLocal = (TablaSimbolo)local.Clone();

                foreach (Simbolo s in ts)
                {
                    if(s.Identificador != identificador)
                    {
                        local.AddLast(s);
                    }
                }

                foreach (Instruccion sentencia in sentencias)
                {
                    Object o = sentencia.ejeuctar(local);
                    if (o is Exit)
                    {
                        local.setValor(identificador, ((Exit)o).getValorImplicito(local));
                    }
                }

                ts.setValor(identificador, local.getValor(identificador));

                foreach (String r in refernciasParametros)
                {
                    Object o = local.getValor(referncias.First.Value);
                   // Form1.consola.Text+= referncias.First.Value.ToString()+"/n";
                    ts.setValor(r, o);
                    referncias.RemoveFirst();
                }

                return local.getValor(identificador);
            }
            else
            {
                Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " el numero de parametros no coincide con los de la funcion\n";
                Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El numero de parametros no coincide con los de la funcion"));
            }


            Simbolo aux = ts.getSimbolo(identificador);
            if (aux != null)
            {
                return aux.Valor;
            }

                return null;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

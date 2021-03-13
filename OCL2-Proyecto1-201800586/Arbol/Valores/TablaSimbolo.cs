using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Valores
{
    class TablaSimbolo : LinkedList<Simbolo>, ICloneable
    {
        public TablaSimbolo(): base()
        {

        }

        public Simbolo getSimbolo(String identificador)
        {
            foreach (Simbolo s in this)
            {
                if (s.Identificador.Equals(identificador))
                {
                    return s;
                }
            }
            return null; 
        }
        public Boolean existe(String identificador)
        {
            foreach (Simbolo s in this)
            {
                if (s.Identificador.Equals(identificador))
                {
                    return true;
                }
            }
            //Form1.consola.Text += "La variable '" + identificador + "' es desconocida\n";
            return false;
        }

        public Object getValor(String identificador)
        {
            foreach(Simbolo s in this)
            {
                if (s.Identificador.Equals(identificador))
                {
                    return s.Valor;
                }
            }
            //Form1.consola.Text += "La variable '" + identificador + "' es desconocida\n";
            return null;
        }

        public Simbolo.Tipo getTipo(String identificador)
        {
            foreach (Simbolo s in this)
            {
                if (s.Identificador.Equals(identificador))
                {
                    return s.type;
                }
            }
            //Form1.consola.Text += "La variable '" + identificador + "' es desconocida\n";
            return Simbolo.Tipo.DESCONOCIDO;
        }

        public String getIdentificador(String identificador)
        {
            foreach (Simbolo s in this)
            {
                if (s.Identificador.Equals(identificador))
                {
                    return s.Identificador;
                }
            }
            return null;
        }

        public void setValor(String identificador, Object valor)
        {
            foreach(Simbolo s in this)
            {
                if (s.Identificador.Equals(identificador))
                {
                    s.Valor = valor;
                    return ;
                }
            }
            Form1.consola.Text += "La variable " + identificador + " no existe en este ámbito, por lo "
                    + "que no puede asignársele un valor.\n";
        }

        public object Clone()
        {
            TablaSimbolo nueva = new TablaSimbolo();
            foreach(Simbolo s in this)
            {
                Simbolo aux = (Simbolo)s.Clone();
                if(aux.Valor is TablaSimbolo)
                {
                    aux.Valor = ((TablaSimbolo)aux.Valor).Clone();
                }
                nueva.AddLast(aux);
            }
            return nueva;
        }

        public object Clone2()
        {
            return this.MemberwiseClone();
        }
    }
}

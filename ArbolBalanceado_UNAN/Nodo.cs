using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolBalanceado_UNAN
{
    class Nodo
    {
      public  int altura;
       public int info;
       public Nodo izq;
       public Nodo der;

      public  Nodo(int d)
        {
            info = d;
            altura = 1;
        }
    }
}

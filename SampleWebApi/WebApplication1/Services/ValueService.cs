using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class ValueService
    {
        public ValueService()
        {

        }

        public void Calculate()
        {
            // TOda la logica de la app correspondiente al calculo.....

            new ValueRepository().Calculate();
        }

    }
}

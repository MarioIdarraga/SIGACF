﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Domain.BusinessException
{
    public class NoSeEncontroLaPalabraException : Exception
    {

        public NoSeEncontroLaPalabraException() : base("No se encontró la palabra en el archivo de idioma.")
        {
               
        }

    }
}

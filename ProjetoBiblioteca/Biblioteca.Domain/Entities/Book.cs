﻿using Biblioteca.Domain.Entities.Base;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Nome { get; set; }
        public DateTime DataLancamento { get; set; }
        public int QuantidadePagina { get; set; }
        public Author Autor { get; set; }
        public int AutorId { get; set; }
        public Category Categoria { get; set; }
        public int  CategoriaId { get; set; }

        //Relacionamento com estoque
    }
}
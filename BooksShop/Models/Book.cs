﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BooksShop
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Pages { get; set; }
        public int CategoryId { get; set; }
        public int Cover { get; set; }
        public Category Category { get; set; }
    }
}
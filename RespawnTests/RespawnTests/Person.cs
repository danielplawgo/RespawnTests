﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespawnTests
{
    public class Person : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}

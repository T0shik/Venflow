﻿using System.Collections.Generic;

namespace Venflow.Tests.Models
{
    public class ReversePerson
    {
        public int Id { get; set; }

        public virtual string Name { get; set; }

        public List<ReverseEmail> Emails { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace projint.Models
{
    public partial class Leitura
    {
        public decimal Id { get; set; }
        public int IdLeitores { get; set; }
        public int IdEquipamentos { get; set; }
        public DateTime Data { get; set; }
    }
}

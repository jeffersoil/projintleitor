using System;
using System.Collections.Generic;

namespace projint.Models
{
    public partial class Leituras
    {
        public int Id { get; set; }
        public int IdLeitores { get; set; }
        public int IdEquipamentos { get; set; }
        public DateTime Data { get; set; }
    }
}

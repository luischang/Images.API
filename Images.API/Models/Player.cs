using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Images.API.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Dorsal { get; set; }
        public string Picture { get; set; }
    }
}

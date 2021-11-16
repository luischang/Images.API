using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Images.API.DTO_s
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Dorsal { get; set; }
        public string Picture { get; set; }
    }

    public class PlayerImageDTO
    {
        public string FullName { get; set; }   
        public string Picture { get; set; }
    }

    public class PlayerInsertImageDTO
    {
        public string FullName { get; set; }
        public byte[] Image { get; set; }
    }
}

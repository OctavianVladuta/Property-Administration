using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Administration
{
    class Picture
    {

        public int Id { get; set; } //cheia primara in tabel
        public string Name { get; set; }
        public string Location { get; set; }
        public int EstateId { get; set; }



        public override string ToString()
        {
            return $"{Id},{Name},{Location},{EstateId}";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Administration
{
    class Owner
    {

        public int Id { get; set; } //cheia primara din tabel
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Cnp { get; set; }
        public List<Estate> Estates { get; set; }

        public Owner()
        {
            Estates = new List<Estate>();
        }

        public Estate SearchbyId(int Id)
        {
            foreach (var x in Estates)
                if (x.Id == Id)
                    return x;
            return null;
        }

        public override string ToString()
        {
            return $"{Id},{Name},{Email},{Phone},{Cnp},{Estates}";
        }
    }
}

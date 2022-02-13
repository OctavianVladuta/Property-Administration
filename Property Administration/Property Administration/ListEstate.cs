using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Administration
{
    class ListEstate
    {
        public List<Estate> estateList = new List<Estate>();

        public void Add(Estate estate)
        {
            if (estateList.Contains(estate))
                throw new Exception($"Proprietatea exista deja");
            estateList.Add(estate);
        }

        public void Remove(Estate estate)
        {
            if (!estateList.Contains(estate))
                throw new Exception($"Anuntul nu exista");
            estateList.Remove(estate);

        }
        public Estate SearchbyId(int Id)
        {
            foreach (var x in estateList)
                if (x.Id == Id)
                    return x;
            return null;
        }

        public override string ToString()
        {
            return string.Join(" ", estateList);
        }
    }
}

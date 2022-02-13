using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Administration
{
    class ListOwner
    {
        public List<Owner> ownerList = new List<Owner>();

        public void Add(Owner owner)
        {
            if (ownerList.Contains(owner))
                throw new Exception($"Anuntul exista deja");
            ownerList.Add(owner);
        }

        public void Remove(Owner owner)
        {           
            if (!ownerList.Contains(owner))
                throw new Exception($"Anuntul nu exista");
            ownerList.Remove(owner);

        }
        public Owner SearchbyId(int Id)
        {
            foreach (var x in ownerList)
                if (x.Id == Id)
                    return x;
            return null;
        }

        public override string ToString()
        {
            return string.Join(" ", ownerList);
        }
    }
}

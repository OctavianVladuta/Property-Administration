using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Administration
{
    class ListPhoto
    {
        public List<Picture> pictureList = new List<Picture>();

        public void Add(Picture picture)
        {
            if (pictureList.Contains(picture))
                throw new Exception($"Anuntul exista deja");
            pictureList.Add(picture);
        }

        public override string ToString()
        {
            return string.Join(" ", pictureList);
        }
    }
}

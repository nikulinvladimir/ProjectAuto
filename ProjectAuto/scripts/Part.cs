using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAuto
{
    class Part
    {
        public int id { get; set; }
        public List<GoodsPart> goodsParts { get; set; } = new List<GoodsPart>();
        public string linkPictureScheme { get; set; }
        public string articlePart { get; set; }
        public string countGoods { get; set; }
        public string nameGoods { get; set; }
        public List<MissingPart> missingParts { get; set; } = new List<MissingPart>();
    }
}

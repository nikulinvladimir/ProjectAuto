using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAuto
{
    class CategoryPart
    {
       public int id { get; set; } 
       public string categoryName { get; set; }
       public int autoId { get; set; }
       public List<SubCategoryParts> linkSubPart { get; set; } = new List<SubCategoryParts>();
      
    }   
}

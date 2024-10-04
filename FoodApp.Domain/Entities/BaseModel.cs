using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Entities
{
    public class BaseModel
    {
        public int ID { get; set; }
        public bool Deleted { get; set; }
    }
}

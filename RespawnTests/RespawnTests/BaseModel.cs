using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespawnTests
{
    public class BaseModel
    {
        public BaseModel()
        {
            IsActive = true;
        }

        public int Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedUser { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedUser { get; set; }
    }
}

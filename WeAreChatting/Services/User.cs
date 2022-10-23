using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class User
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public OperationContext operationContext { get; set; }
    }
}

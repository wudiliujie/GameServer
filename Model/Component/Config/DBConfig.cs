using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class DBConfig : AConfigComponent
    {
        public string ConnectionString { get; set; }
        public string DBName { get; set; }
    }
}

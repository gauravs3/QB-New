using System;
using System.Collections.Generic;
using System.Text;

namespace QB.BAL.Models
{
    class BaseModel
    {
    }
    public class MetaData
    {
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
    public class CurrencyRef
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}

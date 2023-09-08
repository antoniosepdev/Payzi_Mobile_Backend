using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Payzi.Abstraction.Abstract
{
    public abstract class GenericBaseModel<T> : BaseModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public T? Data
        {
            get;
            set;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<T>? DataList
        {
            get;
            set;
        }
    }
}

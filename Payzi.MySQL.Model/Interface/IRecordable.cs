using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.MySQL.Model.Interface
{
    public interface IRecordable
    {
        public Task Save(Payzi.MySQL.Model.Context context);
    }
}

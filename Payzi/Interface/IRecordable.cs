using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Interface
{
    public interface IRecordable
    {
        public Task Save(Payzi.Context.Context context);

        public Task Delete(Payzi.Context.Context context);
    }
}

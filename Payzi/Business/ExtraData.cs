using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class ExtraData : Payzi.Persistent.ExtraData
    {
        public static async Task<ExtraData> GetAsync(Payzi.Context.Context context, Guid id)
        {
            Payzi.Model.ExtraDatum query = await Query.GetExtraData(context).SingleOrDefaultAsync<Payzi.Model.ExtraDatum>(x => x.Id == id);

            ExtraData extraData = query.SingleOrDefault<ExtraData>();

            return extraData;
        }
    }
}

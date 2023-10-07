using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using Payzi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class CustomFields : Payzi.Persistent.CustomFields
    {
        public static async Task<CustomFields> GetAsync(Payzi.Context.Context context, Guid idCustomFields)
        {
            Payzi.Model.CustomField query = await Query.GetCustomFields(context).SingleOrDefaultAsync<Payzi.Model.CustomField>(x => x.IdCustomFields == idCustomFields);

            CustomFields customField = query.SingleOrDefault<CustomFields>();

            return customField;
        }
    }
}

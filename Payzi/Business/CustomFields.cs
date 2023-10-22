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
        public static async Task<CustomFields> GetAsync(Payzi.Context.Context context, Guid? idCustomFields)
        {
            Payzi.Model.CustomField query = await Query.GetCustomFields(context).SingleOrDefaultAsync<Payzi.Model.CustomField>(x => x.IdCustomFields == idCustomFields);

            CustomFields customField = query.SingleOrDefault<CustomFields>();

            return customField;
        }

        public static async Task<List<CustomFields>> GetAll(Payzi.Context.Context context)
        {
            IQueryable<Payzi.Model.CustomField> query = (from q in Query.GetCustomFields(context) orderby q.IdCustomFields select q);

            List<CustomFields> list = await query.ToList<CustomFields>();

            return list;
        }

        public static async Task<List<CustomFields>> GetAll(Payzi.Context.Context context, ExtraData extraData)
        {
            IQueryable<Payzi.Model.CustomField> query = (from q in Query.GetCustomFields(context, extraData)
                                                         where q.IdCustomFields == extraData.CustomFields
                                                         select q);

            List<CustomFields> list = await query.ToList<CustomFields>();

            return list;
        }

    }
}

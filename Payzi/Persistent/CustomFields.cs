using Microsoft.EntityFrameworkCore;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class CustomFields : Payzi.Entity.CustomFields, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.CustomField? customField = await context.CustomFields.SingleOrDefaultAsync<Payzi.Model.CustomField>(x => x.IdCustomFields == this.IdCustomFields);

            if (customField == null)
            {
                customField = new CustomFields
                {
                    IdCustomFields = this.IdCustomFields
                };

                await context.CustomFields.AddAsync(customField);
            }

            customField.Name = this.Name;
            customField.Value = this.Value;
            customField.Print = this.Print;
        }

        public async Task Delete(Payzi.Context.Context context)
        {
            Payzi.Model.CustomField? customField = await context.CustomFields.SingleOrDefaultAsync<Payzi.Model.CustomField>(x => x.IdCustomFields == this.IdCustomFields);

            if (customField != null)
            {
                context.CustomFields.Remove(customField);
            }
        }
    }
}

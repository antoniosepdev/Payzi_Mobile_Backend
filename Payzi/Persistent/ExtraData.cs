using Microsoft.EntityFrameworkCore;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class ExtraData : Payzi.Entity.ExtraData, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.ExtraDatum? extraData = await context.ExtraData.SingleOrDefaultAsync<Payzi.Model.ExtraDatum>(x => x.Id == this.Id);

            if (extraData == null)
            {
                extraData = new ExtraData
                {
                    Id = this.Id
                };

                await context.ExtraData.AddAsync(extraData);
            }

            extraData.TaxIdnValidation = this.TaxIdnValidation;
            extraData.ExemptAmount = this.ExemptAmount;
            extraData.NetAmount = this.NetAmount;
            extraData.SourceName = this.SourceName;
            extraData.SourceVersion = this.SourceVersion;
            extraData.CustomFields = this.CustomFields;
        }

        public async Task Delete(Payzi.Context.Context context)
        {
            Payzi.Model.ExtraDatum? extraData = await context.ExtraData.SingleOrDefaultAsync<Payzi.Model.ExtraDatum>(x => x.Id == this.Id);

            if (extraData != null)
            {
                context.ExtraData.Remove(extraData);
            }
        }
    }
}

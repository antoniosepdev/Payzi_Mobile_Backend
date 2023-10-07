﻿using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class Voucher : Payzi.Persistent.Voucher
    {
        public static async Task<Voucher> GetAsync(Payzi.Context.Context context, long id)
        {
            Payzi.Model.Voucher query = await Query.GetVoucher(context).SingleOrDefaultAsync<Payzi.Model.Voucher>(x => x.Id == id);

            Voucher voucher = query.SingleOrDefault<Voucher>();

            return voucher;
        }
    }
}

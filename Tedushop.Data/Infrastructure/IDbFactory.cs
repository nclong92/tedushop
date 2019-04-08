using System;
using System.Collections.Generic;
using System.Text;

namespace Tedushop.Data.Infrastructure
{
    public interface IDbFactory: IDisposable
    {
        TeduShopDbContext Init();
    }
}

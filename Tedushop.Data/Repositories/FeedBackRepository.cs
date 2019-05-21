using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedushop.Data.Infrastructure;
using Tedushop.Model.Models;

namespace Tedushop.Data.Repositories
{
    public interface IFeedBackRepository : IRepository<FeedBack>
    {

    }
    public class FeedBackRepository : RepositoryBase<FeedBack>, IFeedBackRepository
    {
        public FeedBackRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}

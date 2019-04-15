using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedushop.Data.Infrastructure;
using Tedushop.Model.Models;

namespace Tedushop.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {

    }
    public class PostRepository: RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory): base(dbFactory)
        {

        }
    }
}

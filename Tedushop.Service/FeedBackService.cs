using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedushop.Data.Infrastructure;
using Tedushop.Data.Repositories;
using Tedushop.Model.Models;

namespace Tedushop.Service
{
    public interface IFeedBackSerive
    {
        FeedBack Create(FeedBack feedBack);
        void Save();
    }
    public class FeedBackService: IFeedBackSerive
    {
        private readonly IFeedBackRepository _feedBackRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FeedBackService(IFeedBackRepository feedBackRepository, IUnitOfWork unitOfWork)
        {
            this._feedBackRepository = feedBackRepository;
            this._unitOfWork = unitOfWork;
        }

        public FeedBack Create(FeedBack feedBack)
        {
            return _feedBackRepository.Add(feedBack);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}

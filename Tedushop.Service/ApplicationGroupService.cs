using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedushop.Common.Exceptions;
using Tedushop.Data.Infrastructure;
using Tedushop.Data.Repositories;
using Tedushop.Model.Models;

namespace Tedushop.Service
{
    public interface IApplicationGroupService
    {
        ApplicationGroup GetDetail(int id);
        IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter);
        IEnumerable<ApplicationGroup> GetAll();
        ApplicationGroup Add(ApplicationGroup appGroup);
        void Update(ApplicationGroup appGroup);
        ApplicationGroup Delete(int id);
        bool AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, string userId);
        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);
        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);
        void Save();
    }
    public class ApplicationGroupService : IApplicationGroupService
    {
        private readonly IApplicationUserGroupRepository _appUserGroupRepository;
        private readonly IApplicationGroupRepository _appGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationGroupService(
            IApplicationUserGroupRepository appUserGroupRepository,
            IApplicationGroupRepository appGroupRepository,
            IUnitOfWork unitOfWork)
        {
            this._appUserGroupRepository = appUserGroupRepository;
            this._appGroupRepository = appGroupRepository;
            this._unitOfWork = unitOfWork;
        }
        public ApplicationGroup Add(ApplicationGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name))
                throw new NameDuplicatedException("Tên không được trùng.");

            return _appGroupRepository.Add(appGroup);
        }

        public bool AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, string userId)
        {
            _appUserGroupRepository.DeleteMulti(x => x.UserId == userId);

            foreach(var userGroup in userGroups)
            {
                _appUserGroupRepository.Add(userGroup);
            }

            return true;

        }

        public ApplicationGroup Delete(int id)
        {
            var appGroup = _appGroupRepository.GetSingleById(id);

            return _appGroupRepository.Delete(appGroup);
        }

        public IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _appGroupRepository.GetAll();

            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.Contains(filter));

            totalRow = query.Count();

            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<ApplicationGroup> GetAll()
        {
            return _appGroupRepository.GetAll();
        }

        public ApplicationGroup GetDetail(int id)
        {
            return _appGroupRepository.GetSingleById(id);
        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            return _appGroupRepository.GetListGroupByUserId(userId);
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            return _appGroupRepository.GetListUserByGroupId(groupId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name && x.ID != appGroup.ID))
                throw new NameDuplicatedException("Tên không được trùng.");

            _appGroupRepository.Update(appGroup);
        }
    }
}

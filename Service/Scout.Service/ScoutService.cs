//
//  ScoutService.cs
//
//  Author:
//       bcline <bcline760@yahoo.com>
//
//  Copyright (c) 2019 ${CopyrightHolder}
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scout.Core.Contract;
using Scout.Core.Repository;
using Scout.Core.Service;

namespace Scout.Service
{
    public abstract class ScoutService<TContract> : IScoutService<TContract> where TContract : ScoutEntity
    {
        private IScoutRepository<TContract> _repo;

        protected ScoutService(IScoutRepository<TContract> repo)
        {
            _repo = repo;
        }

        public virtual async Task<ObjectModifyResult<Guid>> Delete(TContract contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(contract));

            var svcResult = new ObjectModifyResult<Guid>();
            try
            {
                contract.IsActive = false;

                var result = await _repo.SaveAsync(contract);
                svcResult.RecordsModified = result;
            }
            catch (Exception)
            {
                svcResult.RecordsModified = -1;
            }

            return svcResult;
        }

        public virtual async Task<IEnumerable<TContract>> GetAllAsync()
        {
            return await _repo.LoadAllAsync();
        }

        public virtual async Task<TContract> GetAsync(Guid id)
        {
            return await _repo.GetAsync(id);
        }

        public virtual async Task<ObjectModifyResult<Guid>> SaveAsync(TContract contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(contract));

            var svcResult = new ObjectModifyResult<Guid>();
            try
            {
                var result = await _repo.SaveAsync(contract);
                svcResult.RecordsModified = result;
            }
            catch (Exception)
            {
                svcResult.RecordsModified = -1;
            }

            return svcResult;
        }
    }
}

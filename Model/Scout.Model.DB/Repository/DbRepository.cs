using System;
using System.Collections.Generic;
using System.Text;
using Scout.Model.DB.Context;

namespace Scout.Model.DB.Repository
{
    public abstract class DbRepository
    {
        private IScoutContext _context;
        protected DbRepository(IScoutContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the instance of database context
        /// </summary>
        protected IScoutContext Context => _context;
    }
}

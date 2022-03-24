﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManualAction.DataAccessLayer.Repositories.Abstract;
namespace ManualAction.DataAccessLayer.Repositories.Concrete
{
    public class ManualActionReasonRepository : Repository<ManualActionReason>, IManualActionReasonRepository
    {
        public ManualActionReasonRepository(ManualActionsDBEntities dBEntities) : base(dBEntities)
        {

        }
        public ManualActionsDBEntities ManualActionsDBEntities { get { return _context as ManualActionsDBEntities; } }

    }
}


﻿using RentMyRide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.DataAcess.Repository.IRepository
{
    public interface IRentingRepository : IRepository<Renting>
    {
        void Update(Renting obj);
    }

}

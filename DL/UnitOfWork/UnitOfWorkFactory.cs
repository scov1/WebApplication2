using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.UnitOfWork
{
    public class UnitOfWorkFactory<T> where T : class
    {
        public IUnitOfWork<T> Create()
        {
            return new UnitOfWork<T>(new Model1());
        }
    }
}

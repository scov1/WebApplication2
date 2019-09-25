using AutoMapper;
using DL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public abstract class BOBase<T> where T : class
    {
        protected IMapper mapper;
        protected UnitOfWorkFactory<T> unitOfWorkFactory;

        public BOBase(IMapper mapper, UnitOfWorkFactory<T> unitOfWorkFactory)
        {
            this.mapper = mapper;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }
    }
}

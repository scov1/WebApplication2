using AutoMapper;
using DL.Entities;
using DL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace BL.BO
{
    public class OrderBO : BOBase<Orders>
    {
        private readonly IUnityContainer unityContainer;

        public int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int BookId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Period { get; set; }
        public DateTime? ReturnDate { get; set; }

        public OrderBO(IMapper mapper, UnitOfWorkFactory<Orders> unitOfWorkFactory, IUnityContainer unityContainer)
            : base(mapper, unitOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public OrderBO GetOrdersListById(int? id)
        {
            OrderBO orders;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                orders = unitOfWork.EntityRepository.GetAll().Where(o => o.Id == id).Select(item => mapper.Map<OrderBO>(item)).FirstOrDefault();
            }
            return orders;
        }

        public List<OrderBO> GetOrdersList()
        {
            List<OrderBO> orders = new List<OrderBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                orders = unitOfWork.EntityRepository.GetAll().Select(item => mapper.Map<OrderBO>(item)).ToList();
            }
            return orders;
        }

        public void Save()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                if (Id != 0)
                    Update(unitOfWork);
                else
                    Create(unitOfWork);
            }
        }

        void Create(IUnitOfWork<Orders> unitOfWork)
        {
            var order = mapper.Map<Orders>(this);
            unitOfWork.EntityRepository.Create(order);
            unitOfWork.Save();
        }

        void Update(IUnitOfWork<Orders> unitOfWork)
        {
            var order = mapper.Map<Orders>(this);
            unitOfWork.EntityRepository.Update(order);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.EntityRepository.Delete(id);
                unitOfWork.Save();
            }
        }
    }
}

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
    public class BookBO : BOBase<Books>
    {
        private readonly IUnityContainer unityContainer;

        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public string Title { get; set; }
        public int? Pages { get; set; }
        public int? Price { get; set; }
        public int GenreId { get; set; }
        public byte[] ImageData { get; set; }

        public BookBO(IMapper mapper, UnitOfWorkFactory<Books> unitOfWorkFactory, IUnityContainer unityContainer)
            : base(mapper, unitOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public BookBO GetBooksListById(int? id)
        {
            BookBO books;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                books = unitOfWork.EntityRepository.GetAll().Where(a => a.Id == id).Select(item => mapper.Map<BookBO>(item)).FirstOrDefault();
            }
            return books;
        }

        public List<BookBO> GetBooksList()
        {
            List<BookBO> books = new List<BookBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                books = unitOfWork.EntityRepository.GetAll().Select(item => mapper.Map<BookBO>(item)).ToList();
            }
            return books;
        }

        void Create(IUnitOfWork<Books> unitOfWork)
        {
            var book = mapper.Map<Books>(this);
            unitOfWork.EntityRepository.Create(book);
            unitOfWork.Save();
        }

        void Update(IUnitOfWork<Books> unitOfWork)
        {
            var book = mapper.Map<Books>(this);
            unitOfWork.EntityRepository.Update(book);
            unitOfWork.Save();
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

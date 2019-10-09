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
    public class GenreBO:BOBase<Genres>
    {
        private readonly IUnityContainer unityContainer;

        public int Id { get; set; }
        public string Name { get; set; }

        public GenreBO(IMapper mapper, UnitOfWorkFactory<Genres> unitOfWorkFactory, IUnityContainer unityContainer)
           : base(mapper, unitOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public GenreBO GetGenresListById(int? id)
        {
            GenreBO genres;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                genres = unitOfWork.EntityRepository.GetAll().Where(a => a.Id == id).Select(item => mapper.Map<GenreBO>(item)).FirstOrDefault();
            }
            return genres;
        }

 
        public List<GenreBO> GetGenreList()
        {
            List<GenreBO> genres = new List<GenreBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                genres = unitOfWork.EntityRepository.GetAll().Select(item => mapper.Map<GenreBO>(item)).ToList();
            }
            return genres;
        }

        void Add(IUnitOfWork<Genres> unitOfWork)
        {
            var genre = mapper.Map<Genres>(this);
            unitOfWork.EntityRepository.Add(genre);
            unitOfWork.Save();
        }

        void Update(IUnitOfWork<Genres> unitOfWork)
        {
            var genre = mapper.Map<Genres>(this);
            unitOfWork.EntityRepository.Update(genre);
            unitOfWork.Save();
        }

        public void Save()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                if (Id != 0)
                    Update(unitOfWork);
                else
                    Add(unitOfWork);
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

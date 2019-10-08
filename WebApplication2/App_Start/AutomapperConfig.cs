using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using System.Web.Mvc;
using DL.Entities;
using BL.BO;
using WebApplication2.ViewModel;

namespace WebApplication2.App_Start
{
    public static class AutomapperConfig
    {
        public static void RegisterWithUnity(IUnityContainer container)
        {
            IMapper mapper = CreateMapperConfig().CreateMapper();

            container.RegisterInstance<IMapper>(mapper);
        }

        static MapperConfiguration CreateMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Authors, AuthorBO>()//.ForMember(t=> t.Id, to => to.Ignore())
    .ConstructUsing(item => DependencyResolver.Current.GetService<AuthorBO>());

                cfg.CreateMap<AuthorBO, AuthorView>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<AuthorView>());

                cfg.CreateMap<AuthorView, AuthorBO>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<AuthorBO>());

                cfg.CreateMap<AuthorBO, Authors>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<Authors>());


                cfg.CreateMap<Books, BookBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                .ConstructUsing(item => DependencyResolver.Current.GetService<BookBO>());

                cfg.CreateMap<BookBO, BookView>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<BookView>());

                cfg.CreateMap<BookView, BookBO>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<BookBO>());

                cfg.CreateMap<BookBO, Books>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<Books>());


                cfg.CreateMap<Users, UserBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                .ConstructUsing(item => DependencyResolver.Current.GetService<UserBO>());

                cfg.CreateMap<UserBO, UserView>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<UserView>());

                cfg.CreateMap<UserView, UserBO>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<UserBO>());

                cfg.CreateMap<UserBO, Users>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<Users>());


                cfg.CreateMap<Orders, OrderBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                .ConstructUsing(item => DependencyResolver.Current.GetService<OrderBO>());

                cfg.CreateMap<OrderBO, OrderView>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<OrderView>());

                cfg.CreateMap<OrderView, OrderBO>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<OrderBO>());

                cfg.CreateMap<OrderBO, Orders>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<Orders>());


                cfg.CreateMap<Genres, GenreBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                .ConstructUsing(item => DependencyResolver.Current.GetService<GenreBO>());

                cfg.CreateMap<GenreBO, GenreView>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<GenreView>());

                cfg.CreateMap<GenreView, GenreBO>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<GenreBO>());

                cfg.CreateMap<GenreBO, Genres>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<Genres>());
            }
            );
        }

    }
}
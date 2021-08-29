using System.Linq;
using AutoMapper;
using Ninject;
using Ninject.Modules;
using Photo_album.BLL.DTOs;
using Photo_album.BLL.Services.Abstract;
using Photo_album.BLL.Services.Concrete;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.UOfW;

namespace Photo_album.BLL.DI
{
    /// <summary>
    /// Ninject module for registering needed dependencies for BLL
    /// </summary>
    public class BLLModule : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            Bind<IMapper>().ToMethod(ctx =>
                new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));

            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IUserService>().To<UserService>();
            Bind<ICommentService>().To<CommentService>();
            Bind<IPostService>().To<PostService>();
        }

        private static MapperConfiguration CreateConfiguration()
        {
            var cfg = new MapperConfiguration(config =>
            {
                config.CreateMap<Post, PostDTO>()
                    .ForMember(postDto => postDto.Id,
                        configurationExpression => configurationExpression.MapFrom(post => post.Id))
                    .ForMember(postDto => postDto.CreationDate,
                        configurationExpression => configurationExpression.MapFrom(post => post.CreationDate))
                    .ForMember(postDto => postDto.Image,
                        configurationExpression => configurationExpression.MapFrom(post => post.Image))
                    .ForMember(postDto => postDto.Likes,
                        configurationExpression => configurationExpression.MapFrom(post => post.Likes))
                    .ForMember(postDto => postDto.Description,
                        configurationExpression => configurationExpression.MapFrom(post => post.Description))
                    .ForMember(postDto => postDto.CommentDtos,
                        configurationExpression =>
                            configurationExpression.MapFrom(post => post.Comments))
                    .ForMember(postDto => postDto.UserDto,
                        configurationExpression =>
                            configurationExpression.MapFrom(post => post.User))
                    .ForMember(postDto => postDto.UserId,
                        configurationExpression =>
                            configurationExpression.MapFrom(post => post.UserId))
                    .ReverseMap();

                config.CreateMap<Comment, CommentDTO>()
                    .ForMember(commentDto => commentDto.Id,
                        configurationExpression => configurationExpression.MapFrom(comment => comment.Id))
                    .ForMember(commentDto => commentDto.CreationDate,
                        configurationExpression => configurationExpression.MapFrom(comment => comment.CreationDate))
                    .ForMember(commentDto => commentDto.Text,
                        configurationExpression => configurationExpression.MapFrom(comment => comment.Text))
                    .ForMember(commentDto => commentDto.UserDto,
                        configurationExpression =>
                            configurationExpression.MapFrom(comment => comment.User))
                    .ForMember(commentDto => commentDto.PostDto,
                        configurationExpression =>
                            configurationExpression.MapFrom(comment => comment.Post))
                    .ForMember(commentDto => commentDto.PostId,
                        configurationExpression => configurationExpression.MapFrom(comment => comment.PostId))
                    .ForMember(commentDto => commentDto.UserId,
                        configurationExpression => configurationExpression.MapFrom(comment => comment.UserId))
                    .ReverseMap();

                config.CreateMap<User, UserDTO>()
                    .ForMember(userDto => userDto.Email,
                        configurationExpression =>
                            configurationExpression.MapFrom(user => user.Email))
                    .ForMember(userDto => userDto.UserName,
                        configurationExpression =>
                            configurationExpression.MapFrom(user => user.UserName))
                    .ForMember(userDto => userDto.ProfilePhoto,
                        configurationExpression =>
                            configurationExpression.MapFrom(user => user.ProfilePhoto))
                    .ForMember(userDto => userDto.Description,
                        configurationExpression =>
                            configurationExpression.MapFrom(user => user.Description))
                    .ForMember(userDto => userDto.Role,
                        configurationExpression =>
                            configurationExpression.MapFrom(user => user.Roles.Select(role => role.RoleId)))
                    .ForMember(userDto => userDto.CommentDtos,
                        configurationExpression =>
                            configurationExpression.MapFrom(user => user.Comments))
                    .ForMember(userDto => userDto.PostDtos,
                        configurationExpression =>
                            configurationExpression.MapFrom(user => user.Posts))
                    .ReverseMap();
            });

            return cfg;
        }
    }
}

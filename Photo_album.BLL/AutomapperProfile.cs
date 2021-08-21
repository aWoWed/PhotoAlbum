using System.Linq;
using AutoMapper;
using Photo_album.BLL.DTOs;
using Photo_album.DataAccess.Entities;

namespace Photo_album.BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Post, PostDTO>()
                .ForMember(postDto => postDto.CommentDtos,
                configurationExpression =>
                    configurationExpression.MapFrom(post => post.Comments.Select(comment => comment)))
                .ForMember(postDto => postDto.UserDto,
                    configurationExpression =>
                        configurationExpression.MapFrom(post => post.User))
                .ForMember(postDto => postDto.UserId,
                    configurationExpression =>
                        configurationExpression.MapFrom(post => post.UserId))
                .ReverseMap();

            CreateMap<Comment, CommentDTO>()
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

            CreateMap<User, UserDTO>()
                .ForMember(userDto => userDto.CommentDtos,
                    configurationExpression =>
                        configurationExpression.MapFrom(user => user.Comments))
                .ForMember(userDto => userDto.PostDtos,
                    configurationExpression =>
                        configurationExpression.MapFrom(user => user.Posts))
                .ReverseMap();
        }
    }
}
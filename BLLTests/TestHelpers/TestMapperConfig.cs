using AutoMapper;
using Photo_album.BLL.DTOs;
using Photo_album.DataAccess.Entities;

namespace BLLTests.TestHelpers
{
    public static class TestMapperConfig
    {
        public static MapperConfiguration CreateConfiguration()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Post, PostDTO>()
                    .ForMember(postDto => postDto.Id,
                        configurationExpression => configurationExpression.MapFrom(post => post.Id))
                    .ForMember(postDto => postDto.CreationDate,
                        configurationExpression => configurationExpression.MapFrom(post => post.CreationDate))
                    .ForMember(postDto => postDto.Image,
                        configurationExpression => configurationExpression.MapFrom(post => post.Image))
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
                    .ForMember(postDto => postDto.LikeDtos,
                        configurationExpression =>
                            configurationExpression.MapFrom(post => post.Likes))
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
                    //.ForMember(userDto => userDto.Email,
                    //    configurationExpression =>
                    //        configurationExpression.MapFrom(user => user.Email))
                    //.ForMember(userDto => userDto.UserName,
                    //    configurationExpression =>
                    //        configurationExpression.MapFrom(user => user.UserName))
                    .ForMember(userDto => userDto.ProfilePhoto,
                        configurationExpression =>
                            configurationExpression.MapFrom(user => user.ProfilePhoto))
                    .ForMember(userDto => userDto.Description,
                        configurationExpression =>
                            configurationExpression.MapFrom(user => user.Description))
                    //.ForMember(userDto => userDto.Role,
                    //    configurationExpression =>
                    //        configurationExpression.MapFrom(user => user.Roles.Select(role => role.RoleId)))
                    .ForMember(userDto => userDto.CommentDtos,
                        configurationExpression =>
                            configurationExpression.MapFrom(user => user.Comments))
                    .ForMember(userDto => userDto.PostDtos,
                        configurationExpression =>
                            configurationExpression.MapFrom(user => user.Posts))
                    .ForMember(userDto => userDto.LikeDtos,
                        configurationExpression =>
                            configurationExpression.MapFrom(user => user.Likes))
                    .ReverseMap();

                config.CreateMap<Like, LikeDTO>()
                    .ForMember(likeDto => likeDto.UserId,
                        configurationExpression =>
                            configurationExpression.MapFrom(like => like.UserId))
                    .ForMember(likeDto => likeDto.UserDto,
                        configurationExpression =>
                            configurationExpression.MapFrom(like => like.User))
                    .ForMember(likeDto => likeDto.PostId,
                        configurationExpression =>
                            configurationExpression.MapFrom(like => like.PostId))
                    .ForMember(likeDto => likeDto.PostDto,
                        configurationExpression =>
                            configurationExpression.MapFrom(like => like.Post))
                    .ReverseMap();

            });

            return mapperConfiguration;
        }
    }
}

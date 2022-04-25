using AutoMapper;
using BookLibrary.Models;
using BookLibrary.ViewModels;

namespace BookLibrary.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(
                            dest => dest.AuthorName,
                            src => src.MapFrom(src => src.User == null ? string.Empty : src.User.FirstName + " " + src.User.LastName)
                )
                .ForMember(
                            dest => dest.CommentsCount,
                            src => src.MapFrom(src => src.Comments == null ? 0 : src.Comments.Count())
                );

            CreateMap<FileModel, Book>()
                .ForMember(
                    dest => dest.Name,
                    src => src.MapFrom(src => src.FileName)
                )
                .ForMember(
                    dest => dest.Date,
                    src => src.MapFrom(src => DateTime.Now)
                );

            CreateMap<CommentModel, Comment>()
                .ForMember(
                    dest => dest.Date,
                    src => src.MapFrom(src => DateTime.Now)
                );

            CreateMap<Comment, CommentViewModel>()
                 .ForMember(
                     dest => dest.AuthorName,
                     src => src.MapFrom(src => src.User == null ? string.Empty : src.User.FirstName + " " + src.User.LastName)
                )
                 .ForMember(
                     dest => dest.AuthorId,
                     src => src.MapFrom(src => src.UserId)
                );

            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.Password,
                src => src.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));
        }
    }
}

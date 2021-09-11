using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.Repositories.Abstract;
using Photo_album.DataAccess.UOfW;

namespace DALTests
{
    public class Tests
    {
        public class UnitMock
        {
            public Mock<IUnitOfWork> UnitOfWorkMock { get; set; }
            public Mock<IPostRepository> PostRepositoryMock { get; set; }
            public Mock<ICommentRepository> CommentRepositoryMock { get; set; }
            public Mock<ILikeRepository> LikeRepositoryMock { get; set; }

        }

        [SetUp]
        public void Setup()
        {
        }

        public static UnitMock CreatesNewMocks()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var postRepositoryMock = new Mock<IPostRepository>();
            var likeRepositoryMock = new Mock<ILikeRepository>();
            var commentRepositoryMock = new Mock<ICommentRepository>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.PostRepository).Returns(postRepositoryMock.Object);
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.LikeRepository).Returns(likeRepositoryMock.Object);
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CommentRepository).Returns(commentRepositoryMock.Object);
            return new UnitMock
            {
                UnitOfWorkMock = unitOfWorkMock,
                PostRepositoryMock = postRepositoryMock,
                CommentRepositoryMock = commentRepositoryMock,
                LikeRepositoryMock = likeRepositoryMock
            };
        }

        [Test]
        public async Task Insert_NewPost_ShouldCreateNewPost()
        {
            // arrange
            var unitMock = CreatesNewMocks();

            var post = new Post();

            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post));

            // act


            // assert

        }

        [Test]
        public async Task Get_Should_Get_All_Posts()
        {
            var unitMock = CreatesNewMocks();


        }
    }
}
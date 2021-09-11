using Moq;
using Photo_album.DataAccess.Repositories.Abstract;
using Photo_album.DataAccess.UOfW;

namespace BLLTests.TestHelpers
{
    public class UnitMock
    {
        public Mock<IUnitOfWork> UnitOfWorkMock { get; set; }
        public Mock<IPostRepository> PostRepositoryMock { get; set; }
        public Mock<ICommentRepository> CommentRepositoryMock { get; set; }
        public Mock<ILikeRepository> LikeRepositoryMock { get; set; }

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
                LikeRepositoryMock = likeRepositoryMock,
            };
        }
    }

}

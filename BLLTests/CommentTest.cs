using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLLTests.TestHelpers;
using Moq;
using NUnit.Framework;
using Photo_album.BLL.DTOs;
using Photo_album.BLL.Services.Concrete;
using Photo_album.DataAccess.Entities;

namespace BLLTests
{
    public class CommentTest
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void Insert_NewComment_ShouldCreateNewComment()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);
            var expectedId = Guid.NewGuid().ToString();

            var comment = new Comment {Id = expectedId};

            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment));

            // act
            commentService.Insert(mapper.Map<Comment, CommentDTO>(comment));

            // assert
            Assert.AreEqual(expectedId, comment.Id);
            unitMock.CommentRepositoryMock.Verify(commentRepository => commentRepository.Insert(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public async Task InsertAsync_NewComment_ShouldCreateNewComment()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);
            var expectedId = Guid.NewGuid().ToString();

            var comment = new Comment {Id = expectedId};

            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment));

            // act
            var commentDto = await commentService.InsertAsync(mapper.Map<Comment, CommentDTO>(comment));

            // assert
            Assert.NotNull(commentDto);
            Assert.AreEqual(expectedId, commentDto.Id);
            unitMock.CommentRepositoryMock.Verify(commentRepository => commentRepository.Insert(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void UpdateComment_ShouldUpdateComment()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);
            var expectedText = "Updated text";

            var comment = new Comment();

            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Update(comment));
            comment.Text = expectedText;

            // act
            commentService.Update(mapper.Map<Comment, CommentDTO>(comment));

            // assert
            Assert.AreEqual(expectedText, comment.Text);
            unitMock.CommentRepositoryMock.Verify(commentRepository => commentRepository.Update(It.IsAny<Comment>()), Times.Once);

        }

        [Test]
        public async Task UpdateAsyncComment_ShouldUpdateComment()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);
            var expectedText = "Updated text";

            var comment = new Comment();
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment));
            comment.Text = expectedText;
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Update(comment));

            // act
            var commentDto = await commentService.InsertAsync(mapper.Map<Comment, CommentDTO>(comment));
            await commentService.UpdateAsync(mapper.Map<Comment, CommentDTO>(comment));

            // assert
            Assert.NotNull(commentDto);
            Assert.AreEqual(commentDto.Text, comment.Text);
            unitMock.CommentRepositoryMock.Verify(commentRepository => commentRepository.Update(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void DeleteCommentByKey_ShouldDeleteComment()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);

            var comment = new Comment();
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.DeleteByKey(comment.Id));

            // act
            commentService.DeleteByKey(comment.Id);

            // assert
            unitMock.CommentRepositoryMock.Verify(commentRepository => commentRepository.DeleteByKey(comment.Id), Times.Once);
        }

        [Test]
        public async Task DeleteCommentByKeyAsync_ShouldDeleteComment()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);

            var comment = new Comment();
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.DeleteByKeyAsync(comment.Id));

            // act
            await commentService.DeleteByKeyAsync(comment.Id);

            // assert
            unitMock.CommentRepositoryMock.Verify(commentRepository => commentRepository.DeleteByKeyAsync(comment.Id), Times.Once);
        }

        [Test]
        public void DeleteComments_ShouldDeleteAllComments()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);

            var comment1 = new Comment();
            var comment2 = new Comment();
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment1));
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment2));
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.DeleteAll());

            // act
            commentService.DeleteAll();

            // assert
            Assert.AreEqual(0, unitMock.CommentRepositoryMock.Object.Get().Count());
            unitMock.CommentRepositoryMock.Verify(commentRepository => commentRepository.DeleteAll(), Times.Once);
        }

        [Test]
        public void GetCommentByKey_ShouldGetComment()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);

            var comment = new Comment();
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment));
            var expectedComment = unitMock.CommentRepositoryMock.Object.GetByKey(comment.Id);

            // act
            commentService.Insert(mapper.Map<Comment, CommentDTO>(comment));
            var commentDto = mapper.Map<CommentDTO, Comment>(commentService.GetByKey(comment.Id));

            // assert
            Assert.AreEqual(expectedComment, commentDto);
        }

        [Test]
        public async Task GetCommentByKeyAsync_ShouldGetComment()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);

            var comment = new Comment();
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment));
            var expectedComment = await unitMock.CommentRepositoryMock.Object.GetByKeyAsync(comment.Id);

            // act
            var commentDto = mapper.Map<CommentDTO, Comment>(await commentService.GetByKeyAsync(comment.Id));

            // assert
            Assert.AreEqual(expectedComment, commentDto);
        }

        [Test]
        public void GetCommentByUserKey_ShouldGetComments()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);

            var comment = new Comment {UserId = Guid.NewGuid().ToString()};
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment));
            var expectedComments = unitMock.CommentRepositoryMock.Object.GetByUserKey(comment.UserId);

            // act
            var commentDtos = mapper.Map<IEnumerable<CommentDTO>, IEnumerable<Comment>>(commentService.GetByUserKey(comment.UserId))
                .AsQueryable();

            // assert
            Assert.AreEqual(expectedComments, commentDtos);
        }

        [Test]
        public async Task GetCommentByUserKeyAsync_ShouldGetComments()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);

            var comment = new Comment {UserId = Guid.NewGuid().ToString()};
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment));
            var expectedComments = await unitMock.CommentRepositoryMock.Object.GetByUserKeyAsync(comment.UserId);

            // act
            var commentDtos = mapper
                .Map<IEnumerable<CommentDTO>, IEnumerable<Comment>>(await commentService.GetByUserKeyAsync(comment.UserId))
                .AsQueryable();

            // assert
            Assert.AreEqual(expectedComments, commentDtos);
        }

        [Test]
        public void GetComments_ShouldGetComments()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);

            var comment1 = new Comment {Text = "Meh"};
            var comment2 = new Comment {Text = "Yup"};
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment1));
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment2));
            var expectedComments = unitMock.CommentRepositoryMock.Object.Get();

            // act
            var commentDtos = mapper.Map<IEnumerable<CommentDTO>, IEnumerable<Comment>>(commentService.Get()).AsQueryable();

            // assert
            Assert.AreEqual(expectedComments, commentDtos);
        }

        [Test]
        public async Task GetAsyncComments_ShouldGetComments()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);

            var comment1 = new Comment { Text = "Meh" };
            var comment2 = new Comment { Text = "Yup" };
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment1));
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment2));
            var expectedComments = await unitMock.CommentRepositoryMock.Object.GetAsync();

            // act
            var commentDtos = mapper.Map<IEnumerable<CommentDTO>, IEnumerable<Comment>>(await commentService.GetAsync())
                .AsQueryable();

            // assert
            Assert.AreEqual(expectedComments, commentDtos);
        }

        [Test]
        public void GetCommentsByContainsText_ShouldGetCommentWhichContainsText()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);

            var comment1 = new Comment { Text = "Meh" };
            var comment2 = new Comment { Text = "Yup" };
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment1));
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment2));
            var expectedComments = unitMock.CommentRepositoryMock.Object.GetByContainsText("e");

            // act
            var commentDtos = mapper.Map<IEnumerable<CommentDTO>, IEnumerable<Comment>>(commentService.GetByContainsText("e"))
                .AsQueryable();

            // assert
            Assert.AreEqual(expectedComments, commentDtos);
        }

        [Test]
        public async Task GetCommentsByContainsTextAsync_ShouldGetCommentWhichContainsText()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var commentService = new CommentService(unitMock.UnitOfWorkMock.Object, mapper);

            var comment1 = new Comment { Text = "Meh" };
            var comment2 = new Comment { Text = "Yup" };
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment1));
            unitMock.CommentRepositoryMock.Setup(commentRepository => commentRepository.Insert(comment2));
            var expectedComments = await unitMock.CommentRepositoryMock.Object.GetByContainsTextAsync("e");

            // act
            var commentDtos = mapper
                .Map<IEnumerable<CommentDTO>, IEnumerable<Comment>>(await commentService.GetByContainsTextAsync("e"))
                .AsQueryable();

            // assert
            Assert.AreEqual(expectedComments, commentDtos);
        }

    }
}

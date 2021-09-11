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
    public class PostTests
    {

        [SetUp]
        public void Setup()
        { }

        [Test]
        public void Insert_NewPost_ShouldCreateNewPost()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);
            var expectedId = Guid.NewGuid().ToString();

            var post = new Post { Id = expectedId };

            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post));

            // act
            postService.Insert(mapper.Map<Post, PostDTO>(post));

            // assert
            Assert.AreEqual(expectedId, post.Id);
            unitMock.PostRepositoryMock.Verify(postRepository => postRepository.Insert(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public async Task InsertAsync_NewPost_ShouldCreateNewPost()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);
            var expectedId = Guid.NewGuid().ToString();

            var post = new Post { Id = expectedId };

            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post));

            // act
            var postDto = await postService.InsertAsync( mapper.Map<Post, PostDTO>(post));

            // assert
            Assert.NotNull(postDto);
            Assert.AreEqual(expectedId, postDto.Id);
            unitMock.PostRepositoryMock.Verify(postRepository => postRepository.Insert(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public void UpdatePost_ShouldUpdatePost()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);
            var expectedDescription = "Updated desc";

            var post = new Post();

            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Update(post));
            post.Description = expectedDescription;

            // act
            postService.Update(mapper.Map<Post, PostDTO>(post));

            // assert
            Assert.AreEqual(expectedDescription, post.Description);
            unitMock.PostRepositoryMock.Verify(postRepository => postRepository.Update(It.IsAny<Post>()), Times.Once);

        }

        [Test]
        public async Task UpdateAsyncPost_ShouldUpdatePost()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);
            var expectedDescription = "Updated desc";

            var post = new Post();
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post));
            post.Description = expectedDescription;
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Update(post));

            // act
            var postDto = await postService.InsertAsync(mapper.Map<Post, PostDTO>(post));
            await postService.UpdateAsync(mapper.Map<Post, PostDTO>(post));
            
            // assert
            Assert.NotNull(postDto);
            Assert.AreEqual(postDto.Description, post.Description);
            unitMock.PostRepositoryMock.Verify(postRepository => postRepository.Update(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public void DeletePostByKey_ShouldDeletePost()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);

            var post = new Post();
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.DeleteByKey(post.Id));

            // act
            postService.DeleteByKey(post.Id);

            // assert
            unitMock.PostRepositoryMock.Verify(postRepository => postRepository.DeleteByKey(post.Id), Times.Once);
        }

        [Test]
        public async Task DeletePostByKeyAsync_ShouldDeletePost()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);

            var post = new Post();
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.DeleteByKeyAsync(post.Id));

            // act
            await postService.DeleteByKeyAsync(post.Id);

            // assert
            unitMock.PostRepositoryMock.Verify(postRepository => postRepository.DeleteByKeyAsync(post.Id), Times.Once);
        }

        [Test]
        public void DeletePosts_ShouldDeleteAllPosts()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);

            var post1 = new Post();
            var post2 = new Post();
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post1));
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post2));
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.DeleteAll());

            // act
            postService.DeleteAll();

            // assert
            Assert.AreEqual(0, unitMock.PostRepositoryMock.Object.Get().Count());
            unitMock.PostRepositoryMock.Verify(postRepository => postRepository.DeleteAll(), Times.Once);
        }

        [Test]
        public void GetPostByKey_ShouldGetPost()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);

            var post = new Post();
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post));
            var expectedPost = unitMock.PostRepositoryMock.Object.GetByKey(post.Id);

            // act
            postService.Insert(mapper.Map<Post, PostDTO>(post));
            var postDto = mapper.Map<PostDTO, Post>(postService.GetByKey(post.Id));

            // assert
            Assert.AreEqual(expectedPost, postDto);
        }

        [Test]
        public async Task GetPostByKeyAsync_ShouldGetPost()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);

            var post = new Post();
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post));
            var expectedPost = await unitMock.PostRepositoryMock.Object.GetByKeyAsync(post.Id);

            // act
            var postDto = mapper.Map<PostDTO, Post>(await postService.GetByKeyAsync(post.Id));

            // assert
            Assert.AreEqual(expectedPost, postDto);
        }

        [Test]
        public void GetPostByUserKey_ShouldGetPosts()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);

            var post = new Post{ UserId = Guid.NewGuid().ToString() };
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post));
            var expectedPosts = unitMock.PostRepositoryMock.Object.GetByUserKey(post.UserId);

            // act
            var postDtos = mapper.Map< IEnumerable<PostDTO>, IEnumerable<Post>>(postService.GetByUserKey(post.UserId)).AsQueryable();

            // assert
            Assert.AreEqual(expectedPosts, postDtos);
        }

        [Test]
        public async Task GetPostByUserKeyAsync_ShouldGetPosts()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);

            var post = new Post { UserId = Guid.NewGuid().ToString() };
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post));
            var expectedPosts = await unitMock.PostRepositoryMock.Object.GetByUserKeyAsync(post.UserId);

            // act
            var postDtos = mapper.Map<IEnumerable<PostDTO>, IEnumerable<Post>>(await postService.GetByUserKeyAsync(post.UserId)).AsQueryable();

            // assert
            Assert.AreEqual(expectedPosts, postDtos);
        }

        [Test]
        public void GetPosts_ShouldGetPosts()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);

            var post1 = new Post{Description = "Lol"};
            var post2 = new Post{Description = "Kek"};
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post1));
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post2));
            var expectedPosts = unitMock.PostRepositoryMock.Object.Get();

            // act
            var postDtos = mapper.Map<IEnumerable<PostDTO>, IEnumerable<Post>>(postService.Get()).AsQueryable();

            // assert
            Assert.AreEqual(expectedPosts, postDtos);
        }

        [Test]
        public async Task GetAsyncPosts_ShouldGetPosts()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);

            var post1 = new Post { Description = "Lol" };
            var post2 = new Post { Description = "Kek" };
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post1));
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post2));
            var expectedPosts = await unitMock.PostRepositoryMock.Object.GetAsync();

            // act
            var postDtos = mapper.Map<IEnumerable<PostDTO>, IEnumerable<Post>>(await postService.GetAsync()).AsQueryable();

            // assert
            Assert.AreEqual(expectedPosts, postDtos);
        }

        [Test]
        public void GetPostsByContainsText_ShouldGetPostWhichContainsText()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);

            var post1 = new Post { Description = "Lol" };
            var post2 = new Post { Description = "Kek" };
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post1));
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post2));
            var expectedPosts = unitMock.PostRepositoryMock.Object.GetByContainsText("k");

            // act
            var postDtos = mapper.Map<IEnumerable<PostDTO>, IEnumerable<Post>>(postService.GetByContainsText("k")).AsQueryable();

            // assert
            Assert.AreEqual(expectedPosts, postDtos);
        }

        [Test]
        public async Task GetPostsByContainsTextAsync_ShouldGetPostWhichContainsText()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var postService = new PostService(unitMock.UnitOfWorkMock.Object, mapper);

            var post1 = new Post { Description = "Lol" };
            var post2 = new Post { Description = "Kek" };
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post1));
            unitMock.PostRepositoryMock.Setup(postRepository => postRepository.Insert(post2));
            var expectedPosts = await unitMock.PostRepositoryMock.Object.GetByContainsTextAsync("k");

            // act
            var postDtos = mapper.Map<IEnumerable<PostDTO>, IEnumerable<Post>>(await postService.GetByContainsTextAsync("k")).AsQueryable();

            // assert
            Assert.AreEqual(expectedPosts, postDtos);
        }
    }
}
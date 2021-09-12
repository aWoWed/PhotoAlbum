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
    public class LikeTests
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void Insert_NewLike_ShouldCreateNewLike()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var likeService = new LikeService(unitMock.UnitOfWorkMock.Object, mapper);
            var expectedId = Guid.NewGuid().ToString();

            var like = new Like { Id = expectedId };

            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Insert(like));

            // act
            likeService.Insert(mapper.Map<Like, LikeDTO>(like));

            // assert
            Assert.AreEqual(expectedId, like.Id);
            unitMock.LikeRepositoryMock.Verify(likeRepository => likeRepository.Insert(It.IsAny<Like>()), Times.Once);
        }

        [Test]
        public async Task InsertAsync_NewLike_ShouldCreateNewLike()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var likeService = new LikeService(unitMock.UnitOfWorkMock.Object, mapper);
            var expectedId = Guid.NewGuid().ToString();

            var like = new Like { Id = expectedId };

            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Insert(like));

            // act
            var likeDto = await likeService.InsertAsync(mapper.Map<Like, LikeDTO>(like));

            // assert
            Assert.NotNull(likeDto);
            Assert.AreEqual(expectedId, likeDto.Id);
            unitMock.LikeRepositoryMock.Verify(likeRepository => likeRepository.Insert(It.IsAny<Like>()), Times.Once);
        }

        [Test]
        public void UpdateLike_ShouldUpdateLike()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var likeService = new LikeService(unitMock.UnitOfWorkMock.Object, mapper);

            var like = new Like();

            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Update(like));

            // act
            likeService.Update(mapper.Map<Like, LikeDTO>(like));

            // assert
            unitMock.LikeRepositoryMock.Verify(likeRepository => likeRepository.Update(It.IsAny<Like>()), Times.Once);
        }

        [Test]
        public async Task UpdateAsyncLike_ShouldUpdateLike()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var likeService = new LikeService(unitMock.UnitOfWorkMock.Object, mapper);

            var like = new Like();
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Insert(like));
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Update(like));

            // act
            var likeDto = await likeService.InsertAsync(mapper.Map<Like, LikeDTO>(like));
            await likeService.UpdateAsync(mapper.Map<Like, LikeDTO>(like));

            // assert
            Assert.NotNull(likeDto);
            unitMock.LikeRepositoryMock.Verify(likeRepository => likeRepository.Update(It.IsAny<Like>()), Times.Once);
        }

        [Test]
        public void DeleteLikeByKey_ShouldDeleteLike()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var likeService = new LikeService(unitMock.UnitOfWorkMock.Object, mapper);

            var like = new Like();
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.DeleteByKey(like.Id));

            // act
            likeService.DeleteByKey(like.Id);

            // assert
            unitMock.LikeRepositoryMock.Verify(likeRepository => likeRepository.DeleteByKey(like.Id), Times.Once);
        }

        [Test]
        public async Task DeleteLikeByKeyAsync_ShouldDeleteLike()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var likeService = new LikeService(unitMock.UnitOfWorkMock.Object, mapper);

            var like = new Like();
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.DeleteByKeyAsync(like.Id));

            // act
            await likeService.DeleteByKeyAsync(like.Id);

            // assert
            unitMock.LikeRepositoryMock.Verify(likeRepository => likeRepository.DeleteByKeyAsync(like.Id), Times.Once);
        }
        
        [Test]
        public void DeleteLikes_ShouldDeleteAllLikes()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var likeService = new LikeService(unitMock.UnitOfWorkMock.Object, mapper);

            var like1 = new Like();
            var like2 = new Like();
            unitMock.LikeRepositoryMock.Setup(likeRepository=> likeRepository.Insert(like1));
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Insert(like2));
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.DeleteAll());

            // act
            likeService.DeleteAll();

            // assert
            Assert.AreEqual(0, unitMock.LikeRepositoryMock.Object.Get().Count());
            unitMock.LikeRepositoryMock.Verify(likeRepository => likeRepository.DeleteAll(), Times.Once);
        }

        [Test]
        public void GetLikeByKey_ShouldGetLike()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var likeService = new LikeService(unitMock.UnitOfWorkMock.Object, mapper);

            var like = new Like();
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Insert(like));
            var expectedLike = unitMock.LikeRepositoryMock.Object.GetByKey(like.Id);

            // act
            likeService.Insert(mapper.Map<Like, LikeDTO>(like));
            var likeDto = mapper.Map<LikeDTO, Like>(likeService.GetByKey(like.Id));

            // assert
            Assert.AreEqual(expectedLike, likeDto);
        }

        [Test]
        public async Task GetLikeByKeyAsync_ShouldGetLike()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var likeService = new LikeService(unitMock.UnitOfWorkMock.Object, mapper);

            var like = new Like();
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Insert(like));
            var expectedLike = await unitMock.LikeRepositoryMock.Object.GetByKeyAsync(like.Id);

            // act
            var likeDto = mapper.Map<LikeDTO, Like>(await likeService.GetByKeyAsync(like.Id));

            // assert
            Assert.AreEqual(expectedLike, likeDto);
        }

        [Test]
        public void GetLikeByUserKey_ShouldGetLikes()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var likeService = new LikeService(unitMock.UnitOfWorkMock.Object, mapper);

            var like = new Like { UserId = Guid.NewGuid().ToString() };
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Insert(like));
            var expectedLikes = unitMock.LikeRepositoryMock.Object.GetByUserKey(like.UserId);

            // act
            var likeDtos = mapper.Map<IEnumerable<LikeDTO>, IEnumerable<Like>>(likeService.GetByUserKey(like.UserId)).AsQueryable();

            // assert
            Assert.AreEqual(expectedLikes, likeDtos);
        }

        [Test]
        public async Task GetLikeByUserKeyAsync_ShouldGetLikes()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var likeService = new LikeService(unitMock.UnitOfWorkMock.Object, mapper);

            var like = new Like { UserId = Guid.NewGuid().ToString() };
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Insert(like));
            var expectedLikes = await unitMock.LikeRepositoryMock.Object.GetByUserKeyAsync(like.UserId);

            // act
            var likeDtos = mapper.Map<IEnumerable<LikeDTO>, IEnumerable<Like>>(await likeService.GetByUserKeyAsync(like.UserId)).AsQueryable();

            // assert
            Assert.AreEqual(expectedLikes, likeDtos);
        }

        [Test]
        public void GetLikes_ShouldGetLikes()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var likeService = new LikeService(unitMock.UnitOfWorkMock.Object, mapper);

            var like1 = new Like();
            var like2 = new Like();
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Insert(like1));
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Insert(like2));
            var expectedLikes = unitMock.LikeRepositoryMock.Object.Get();

            // act
            var likeDtos = mapper.Map<IEnumerable<LikeDTO>, IEnumerable<Like>>(likeService.Get()).AsQueryable();

            // assert
            Assert.AreEqual(expectedLikes, likeDtos);
        }

        [Test]
        public async Task GetAsyncLikes_ShouldGetLikes()
        {
            // arrange
            var unitMock = UnitMock.CreatesNewMocks();
            var config = TestMapperConfig.CreateConfiguration();
            var mapper = new Mapper(config);

            var likeService = new LikeService(unitMock.UnitOfWorkMock.Object, mapper);

            var like1 = new Like();
            var like2 = new Like();
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Insert(like1));
            unitMock.LikeRepositoryMock.Setup(likeRepository => likeRepository.Insert(like2));
            var expectedLikes = await unitMock.LikeRepositoryMock.Object.GetAsync();

            // act
            var likeDtos = mapper.Map<IEnumerable<LikeDTO>, IEnumerable<Like>>(await likeService.GetAsync()).AsQueryable();

            // assert
            Assert.AreEqual(expectedLikes, likeDtos);
        }
    }
}

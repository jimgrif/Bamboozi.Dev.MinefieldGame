using Bamboozi.Dev.MinefieldGame.Service;
using Bamboozi.Dev.MinefieldGame.Service.Dto;
using Bamboozi.Dev.MinefieldGame.StateRepository;
using Bamboozi.Dev.MinefieldGame.StateRepository.Models;

namespace Bamboozi.Dev.MinefieldGame.Tests
{
    public class GameServiceProcessMoveTests : GameServiceBaseTests
    {
        [Fact]
        public void GameService_ProcessMove_Throws_Exception_If_Not_Initialized()
        {
            // Arrange
            var sut = GetSystemUnderTest();

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(default(UserState));

            // Act
            void act() => sut.ProcessMove(MoveType.Right);

            // Assert
            var exception = Assert.Throws<NotInitialsedException>(() => act());
        }

        [Fact]
        public void GameService_ProcessMove_Throws_Exception_If_Current_User_X_Location_Invalid()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = Constants.BOARD_SIZE + 1, Y = 3 }, MovesTaken = 0, LivesRemaining = 3 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            // Act
            void act() => sut.ProcessMove(MoveType.Right);

            // Assert
            var exception = Assert.Throws<InvalidUserStateException>(() => act());
        }

        [Fact]
        public void GameService_ProcessMove_Throws_Exception_If_Current_User_Y_Location_Invalid()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = 3, Y = Constants.BOARD_SIZE + 1 }, MovesTaken = 0, LivesRemaining = 3 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            // Act
            void act() => sut.ProcessMove(MoveType.Right);

            // Assert
            var exception = Assert.Throws<InvalidUserStateException>(() => act());
        }

        [Fact]
        public void GameService_ProcessMove_Throws_Exception_If_Current_User_Has_No_Lives_Remaining()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = 3, Y = 3 }, MovesTaken = 0, LivesRemaining = 0 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            // Act
            void act() => sut.ProcessMove(MoveType.Right);

            // Assert
            var exception = Assert.Throws<InvalidUserStateException>(() => act());
        }

        [Theory]
        [InlineData(3, 0, MoveType.Up, true)]
        [InlineData(3, 0, MoveType.Down, false)]
        [InlineData(3, 0, MoveType.Left, false)]
        [InlineData(3, 0, MoveType.Right, false)]
        [InlineData(0, 3, MoveType.Up, false)]
        [InlineData(0, 3, MoveType.Down, false)]
        [InlineData(0, 3, MoveType.Left, true)]
        [InlineData(0, 3, MoveType.Right, false)]
        [InlineData(3, 7, MoveType.Up, false)]
        [InlineData(3, 7, MoveType.Down, true)]
        [InlineData(3, 7, MoveType.Left, false)]
        [InlineData(3, 7, MoveType.Right, false)]
        public void GameService_ProcessMove_Handles_OutOfBoundMoves(int x, int y, MoveType moveType, bool isOutOfBounds)
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = x, Y = y }, MovesTaken = 0, LivesRemaining = 3 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            _mockMinesState
                .Setup(x => x.IsMined(It.IsAny<UserLocation>()))
                .Returns(false);

            // Act
            var response = sut.ProcessMove(moveType);

            // Assert
            if (isOutOfBounds)
                response.MoveOutcome.Should().Be(MoveOutcome.OutOfBounds);
            else
                response.MoveOutcome.Should().Be(MoveOutcome.OK);
        }

        [Fact]
        public void GameService_ProcessMove_Valid_Move_Right_Updates_Game_State()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = 3, Y = 3 }, MovesTaken = 0, LivesRemaining = 3 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            _mockMinesState
                .Setup(x => x.IsMined(It.IsAny<UserLocation>()))
                .Returns(false);

            // Act
            var response = sut.ProcessMove(MoveType.Right);

            // Assert
            response.MoveOutcome.Should().Be(MoveOutcome.OK);
            _mockGameState.Verify(x => x.SetCurrent(It.Is<UserState>(i =>
                i.MovesTaken == userState.MovesTaken + 1 &&
                i.LivesRemaining == userState.LivesRemaining &&
                i.Location.X == userState.Location.X + 1 &&
                i.Location.Y == userState.Location.Y
            )), Times.Once);
        }

        [Fact]
        public void GameService_ProcessMove_Valid_Move_Left_Updates_Game_State()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = 3, Y = 3 }, MovesTaken = 0, LivesRemaining = 3 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            _mockMinesState
                .Setup(x => x.IsMined(It.IsAny<UserLocation>()))
                .Returns(false);

            // Act
            var response = sut.ProcessMove(MoveType.Left);

            // Assert
            response.MoveOutcome.Should().Be(MoveOutcome.OK);
            _mockGameState.Verify(x => x.SetCurrent(It.Is<UserState>(i =>
                i.MovesTaken == userState.MovesTaken + 1 &&
                i.LivesRemaining == userState.LivesRemaining &&
                i.Location.X == userState.Location.X - 1 &&
                i.Location.Y == userState.Location.Y
            )), Times.Once);
        }

        [Fact]
        public void GameService_ProcessMove_Valid_Move_Up_Updates_Game_State()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = 3, Y = 3 }, MovesTaken = 0, LivesRemaining = 3 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            _mockMinesState
                .Setup(x => x.IsMined(It.IsAny<UserLocation>()))
                .Returns(false);

            // Act
            var response = sut.ProcessMove(MoveType.Up);

            // Assert
            response.MoveOutcome.Should().Be(MoveOutcome.OK);
            _mockGameState.Verify(x => x.SetCurrent(It.Is<UserState>(i =>
                i.MovesTaken == userState.MovesTaken + 1 &&
                i.LivesRemaining == userState.LivesRemaining &&
                i.Location.X == userState.Location.X &&
                i.Location.Y == userState.Location.Y - 1
            )), Times.Once);
        }

        [Fact]
        public void GameService_ProcessMove_Valid_Move_Down_Updates_Game_State()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = 3, Y = 3 }, MovesTaken = 0, LivesRemaining = 3 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            _mockMinesState
                .Setup(x => x.IsMined(It.IsAny<UserLocation>()))
                .Returns(false);

            // Act
            var response = sut.ProcessMove(MoveType.Down);

            // Assert
            response.MoveOutcome.Should().Be(MoveOutcome.OK);
            _mockGameState.Verify(x => x.SetCurrent(It.Is<UserState>(i =>
                i.MovesTaken == userState.MovesTaken + 1 &&
                i.LivesRemaining == userState.LivesRemaining &&
                i.Location.X == userState.Location.X &&
                i.Location.Y == userState.Location.Y + 1
            )), Times.Once);
        }

        [Fact]
        public void GameService_ProcessMove_Mined()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = 3, Y = 3 }, MovesTaken = 0, LivesRemaining = 3 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            _mockMinesState
                .Setup(x => x.IsMined(It.IsAny<UserLocation>()))
                .Returns(true);

            // Act
            var response = sut.ProcessMove(MoveType.Right);

            // Assert
            response.MoveOutcome.Should().Be(MoveOutcome.Mine);
        }

        [Fact]
        public void GameService_ProcessMove_Lose_If_Mined_With_One_Life_Remaining()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = 3, Y = 3 }, MovesTaken = 0, LivesRemaining = 1 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            _mockMinesState
                .Setup(x => x.IsMined(It.IsAny<UserLocation>()))
                .Returns(true);

            // Act
            var response = sut.ProcessMove(MoveType.Right);

            // Assert
            response.MoveOutcome.Should().Be(MoveOutcome.Lose);
        }

        [Fact]
        public void GameService_ProcessMove_Lose_If_Mined_With_One_Life_Remaining_Even_When_Reaching_Finish()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = 6, Y = 3 }, MovesTaken = 0, LivesRemaining = 1 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            _mockMinesState
                .Setup(x => x.IsMined(It.IsAny<UserLocation>()))
                .Returns(true);

            // Act
            var response = sut.ProcessMove(MoveType.Right);

            // Assert
            response.MoveOutcome.Should().Be(MoveOutcome.Lose);
        }

        [Fact]
        public void GameService_ProcessMove_Win_When_Reaching_Finish_Even_If_Mined()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = 6, Y = 3 }, MovesTaken = 0, LivesRemaining = 2 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            _mockMinesState
                .Setup(x => x.IsMined(It.IsAny<UserLocation>()))
                .Returns(true);

            // Act
            var response = sut.ProcessMove(MoveType.Right);

            // Assert
            response.MoveOutcome.Should().Be(MoveOutcome.Win);
        }

        [Fact]
        public void GameService_ProcessMove_Valid_Move_Increments_Moves_Taken()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = 3, Y = 3 }, MovesTaken = 3, LivesRemaining = 2 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            _mockMinesState
                .Setup(x => x.IsMined(It.IsAny<UserLocation>()))
                .Returns(false);

            // Act
            var response = sut.ProcessMove(MoveType.Right);

            // Assert
            response.UserState.MovesTaken.Should().Be(userState.MovesTaken + 1);
        }

        [Fact]
        public void GameService_ProcessMove_If_Mined_Decrements_Lives_Remaining()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = 3, Y = 3 }, MovesTaken = 3, LivesRemaining = 2 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            _mockMinesState
                .Setup(x => x.IsMined(It.IsAny<UserLocation>()))
                .Returns(true);

            // Act
            var response = sut.ProcessMove(MoveType.Right);

            // Assert
            response.UserState.LivesRemaining.Should().Be(userState.LivesRemaining - 1);
        }

        [Fact]
        public void GameService_ProcessMove_OutOfBounds_Move_Does_Not_Increment_Moves_Taken()
        {
            // Arrange
            var sut = GetSystemUnderTest();
            var userState = new UserState { Location = new UserLocation { X = 3, Y = 0 }, MovesTaken = 3, LivesRemaining = 2 };

            _mockGameState
                .Setup(x => x.GetCurrent())
                .Returns(userState);

            _mockMinesState
                .Setup(x => x.IsMined(It.IsAny<UserLocation>()))
                .Returns(false);

            // Act
            var response = sut.ProcessMove(MoveType.Up);

            // Assert
            response.UserState.MovesTaken.Should().Be(userState.MovesTaken);
        }
    }
}
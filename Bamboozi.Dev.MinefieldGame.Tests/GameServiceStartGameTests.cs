using Bamboozi.Dev.MinefieldGame.Service.Dto;
using Bamboozi.Dev.MinefieldGame.Service;
using Bamboozi.Dev.MinefieldGame.StateRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bamboozi.Dev.MinefieldGame.Tests
{
    public class GameServiceStartGameTests : GameServiceBaseTests
    {
        protected readonly UserState _validUserState;
        protected readonly int[,] _validInitialMinesState;

        public GameServiceStartGameTests() : base()
        {
            _validUserState = new UserState
            {
                Location = new UserLocation { X = 0, Y = 0 },
                LivesRemaining = 3,
                MovesTaken = 0
            };

            int[,] initialMinesState =
            {
                // A,B,C,D,E,F,G,H
                {0, 0, 1, 0, 0, 0, 1, 0}, // 1
                {1, 0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 1, 0, 1},
                {0, 0, 1, 0, 1, 0, 0, 0},
                {1, 0, 0, 1, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 1, 1, 0},
                {0, 0, 1, 0, 1, 0, 0, 0},
                {0, 1, 0, 0, 0, 0, 1, 0}  // 8
            };

            _validInitialMinesState = initialMinesState;
        }

        [Theory]
        [InlineData(1, 0, 3, 0)]
        [InlineData(0, 1, 3, 0)]
        [InlineData(0, 0, 3, 1)]
        [InlineData(0, 0, 0, 0)]
        public void GameService_StartGame_Throws_Exception_If_User_State_Invalid(int x, int y, int livesRemaining, int movesTaken)
        {
            // Arrange
            var sut = GetSystemUnderTest();

            var invalidUserState = new UserState
            {
                Location = new UserLocation { X = x, Y = y },
                LivesRemaining = livesRemaining,
                MovesTaken = movesTaken
            };

            // Act
            void act() => sut.StartGame(invalidUserState, _validInitialMinesState);

            // Assert
            var exception = Assert.Throws<InvalidInitialisationException>(() => act());
        }

        [Fact]
        public void GameService_StartGame_Returns_User_State_If_Valid()
        {
            // Arrange
            var sut = GetSystemUnderTest();

            // Act
            var response = sut.StartGame(_validUserState, _validInitialMinesState);

            // Assert
            _mockGameState.Verify(x => x.SetCurrent(_validUserState), Times.Once);
            _mockMinesState.Verify(x => x.Initialise(_validInitialMinesState), Times.Once);

            response.UserState.Should().BeEquivalentTo(_validUserState);
        }

        [Fact]
        public void GameService_StartGame_Throws_Exception_If_MinesState_Rows_Invalid()
        {
            // Arrange
            var sut = GetSystemUnderTest();

            int[,] invalidInitialMinesState =
            {
                // A,B,C,D,E,F,G,H
                {0, 0, 1, 0, 0, 0, 1, 0}, // 1
                {1, 0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 1, 0, 1},
                {0, 0, 1, 0, 1, 0, 0, 0},
                {1, 0, 0, 1, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 1, 1, 0},
                {0, 0, 1, 0, 1, 0, 0, 0}, // 7
            };

            // Act
            void act() => sut.StartGame(_validUserState, invalidInitialMinesState);

            // Assert
            var exception = Assert.Throws<InvalidInitialisationException>(() => act());
        }

        [Fact]
        public void GameService_StartGame_Throws_Exception_If_MinesState_Columns_Invalid()
        {
            // Arrange
            var sut = GetSystemUnderTest();

            int[,] invalidInitialMinesState =
            {
                // A,B,C,D,E,F,G
                {0, 0, 1, 0, 0, 0, 1}, // 1
                {1, 0, 0, 0, 1, 0, 0},
                {0, 0, 0, 0, 0, 1, 0},
                {0, 0, 1, 0, 1, 0, 0},
                {1, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 1, 1},
                {0, 0, 1, 0, 1, 0, 0},
                {0, 1, 0, 0, 0, 0, 1}  // 8
            };

            // Act
            void act() => sut.StartGame(_validUserState, invalidInitialMinesState);

            // Assert
            var exception = Assert.Throws<InvalidInitialisationException>(() => act());
        }
    }
}

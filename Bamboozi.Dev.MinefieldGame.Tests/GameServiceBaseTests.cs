using Bamboozi.Dev.MinefieldGame.Service;
using Bamboozi.Dev.MinefieldGame.StateRepository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bamboozi.Dev.MinefieldGame.Tests
{
    public abstract class GameServiceBaseTests
    {
        protected readonly Mock<IGameState> _mockGameState;
        protected readonly Mock<IMinesState> _mockMinesState;

        public GameServiceBaseTests()
        {
            _mockGameState = new Mock<IGameState>();
            _mockMinesState = new Mock<IMinesState>();
        }

        protected GameService GetSystemUnderTest()
        {
            return new GameService(
                _mockGameState.Object,
                _mockMinesState.Object
            );
        }
    }
}

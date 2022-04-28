using PokerFace.Evaluator;
using PokerFace.Exceptions;
using Xunit;

namespace PokerFace.Tests.Unit
{
    public class EvaluatorTests
    {
        private HandEvaluator _evaluator;

        public EvaluatorTests()
        {
            _evaluator = new HandEvaluator();
        }

        [Theory]
        [InlineData("AS 4C 7D JS KC QH", "Hands should have 5 cards only, but found 6 cards")]
        [InlineData("AS 7D JS KC", "Hands should have 5 cards only, but found 4 cards")]
        [InlineData("", "Hands should have 5 cards only, but found 0 cards")]
        public void Evaluator_ShouldThrowInvalidHandException(string input, string expectedMessage)
        {
            AssertExceptionForInvalidHand(input, expectedMessage);
        }

        [Theory]
        [InlineData("AH 3C 4D 5S 9H", "High card")]
        [InlineData("AH 3C 3D 5S 9H", "One pair")]
        [InlineData("AH 3C 3D 5S 5H", "Two pair")]
        [InlineData("AH 3C 3D 3S 9H", "Three of a kind")]
        [InlineData("AH 2C 3D 4S 5H", "Straight")]
        [InlineData("AH 3H 6H 5H 9H", "Flush")]
        [InlineData("AH 3C 3D AS AD", "Full house")]
        [InlineData("3H 3C 3D 3S 9H", "Four of a kind")]
        [InlineData("AH 2H 3H 4H 5H", "Straight flush")]
        [InlineData("TS JS QS KS AS", "Royal flush")]
        public void Evaluator_ShouldReturnCorrectWinningHand(string input, string expectedResult)
        {
            AssertHand(input, expectedResult);
        }

        private void AssertHand(string input, string expectedWinningHand)
        {
            var result = _evaluator.Evaluate(input);
            Assert.Equal(expectedWinningHand, result);
        }

        private void AssertExceptionForInvalidHand(string input, string expectedMessage)
        {
            var exception = Assert.Throws<InvalidHandException>(() =>
            {
                _evaluator.Evaluate(input);
            });

            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}

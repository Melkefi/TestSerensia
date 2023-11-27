using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test_Serensia.Dto;
using Test_Serensia.Services;

namespace TestSerensia.Tests
{
    [TestFixture]
    public class AmTheTestServiceTests
    {
        private AmTheTestService _service;

        [SetUp]
        public void Setup()
        {
            _service = new AmTheTestService();
        }

        [Test]
        public async Task GetSuggestions_ReturnsCorrectSuggestions()
        {
            // Arrange
            var term = "gros";
            var choices = new List<string> { "gros", "gras", "graisse", "agressif", "go", "ros", "gro" };
            var expectedSuggestions1 = new List<string> { "gros", "gras" };
            var expectedSuggestions2 = new List<string> { "gros", "gras", "agressif" };

            var request1 = new TestRequest { Term = term, Choices = choices, NumberOfSuggestions = expectedSuggestions1.Count };
            var request2 = new TestRequest { Term = term, Choices = choices, NumberOfSuggestions = expectedSuggestions2.Count };

            // Act
            var result1 = await _service.GetSuggestionAsync(request1);
            var result2 = await _service.GetSuggestionAsync(request2);

            // Assert
            Assert.AreEqual(expectedSuggestions1, result1);
            Assert.AreEqual(expectedSuggestions2, result2);
        }

        [Test]
        public async Task GetSuggestions_ReturnsEmpty_WhenTermIsEmpty()
        {
            // Arrange
            var term = "";
            var choices = new List<string> { "gros", "gras" };
            var request = new TestRequest { Term = term, Choices = choices, NumberOfSuggestions = 1 };

            // Act
            var result = await _service.GetSuggestionAsync(request);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetSuggestions_ReturnsEmpty_WhenChoicesIsEmpty()
        {
            // Arrange
            var term = "gros";
            var choices = new List<string>();
            var request = new TestRequest { Term = term, Choices = choices, NumberOfSuggestions = 1 };

            // Act
            var result = await _service.GetSuggestionAsync(request);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetSuggestions_ReturnsCorrectSuggestions_WhenChoicesContainsEmptyStrings()
        {
            // Arrange
            var term = "gros";
            var choices = new List<string> { "gros", "gras", "" };
            var expectedSuggestions = new List<string> { "gros", "gras" };
            var request = new TestRequest { Term = term, Choices = choices, NumberOfSuggestions = expectedSuggestions.Count };

            // Act
            var result = await _service.GetSuggestionAsync(request);

            // Assert
            Assert.AreEqual(expectedSuggestions, result);
        }

        [Test]
        public async Task GetSuggestions_ReturnsAllChoices_WhenNumberOfSuggestionsIsGreaterThanChoicesCount()
        {
            // Arrange
            var term = "gros";
            var choices = new List<string> { "gros", "gras" };
            var request = new TestRequest { Term = term, Choices = choices, NumberOfSuggestions = 10 };

            // Act
            var result = await _service.GetSuggestionAsync(request);

            // Assert
            Assert.AreEqual(choices, result);
        }

        [Test]
        public async Task GetSuggestions_ReturnsEmpty_WhenTermIsNotInChoices()
        {
            // Arrange
            var term = "test";
            var choices = new List<string> { "gros", "gras" };
            var request = new TestRequest { Term = term, Choices = choices, NumberOfSuggestions = 1 };

            // Act
            var result = await _service.GetSuggestionAsync(request);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetSuggestions_ThrowsException_WhenNumberOfSuggestionsIsNegative()
        {
            // Arrange
            var term = "gros";
            var choices = new List<string> { "gros", "gras" };
            var request = new TestRequest { Term = term, Choices = choices, NumberOfSuggestions = -1 };

            // Act
            var result = await _service.GetSuggestionAsync(request);

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
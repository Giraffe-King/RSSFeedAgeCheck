using CodeHollow.FeedReader;
using Moq;
using NUnit.Framework;
using RSSFeedAgeCheck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSSFeedAgeCheckTests
{
    [TestFixture]
    [TestOf(typeof(Program))]
    public class ProgramTest 
    {
        [Test]
        public void IsFeedStale_FeedNotStale()
        {
            Mock<Program> programMock = new Mock<Program>();
            programMock.CallBase = true;

            Feed feed = new Feed
            {
                Items = new List<FeedItem>
                {
                    new FeedItem
                    {
                        PublishingDate = DateTime.Now
                    }
                }
            };

            programMock.Setup(x => x.ReadFeed(It.IsAny<string>())).Returns(feed);
            Assert.AreEqual(false, programMock.Object.IsFeedStale("Test", 1));
        }

        [Test]
        public void IsFeedStale_FeedStale()
        {
            Mock<Program> programMock = new Mock<Program>();
            programMock.CallBase = true;

            Feed feed = new Feed
            {
                Items = new List<FeedItem>
                {
                    new FeedItem
                    {
                        PublishingDate = DateTime.Now.AddDays(-100)
                    }
                }
            };

            programMock.Setup(x => x.ReadFeed(It.IsAny<string>())).Returns(feed);
            Assert.AreEqual(true, programMock.Object.IsFeedStale("Test", 1));
        }

        [Test]
        public void IsFeedStale_FeedHasNoItems()
        {
            Mock<Program> programMock = new Mock<Program>();
            programMock.CallBase = true;

            Feed feed = new Feed
            {
                Items = new List<FeedItem>
                {
                }
            };

            programMock.Setup(x => x.ReadFeed(It.IsAny<string>())).Returns(feed);
            Assert.AreEqual(true, programMock.Object.IsFeedStale("Test", 1));
        }
    }
}
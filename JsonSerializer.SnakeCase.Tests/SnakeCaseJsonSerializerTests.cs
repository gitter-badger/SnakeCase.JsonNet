﻿using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace JsonSerializer.SnakeCase.Tests
{
    [TestFixture]
    public class SnakeCaseJsonSerializerTests
    {
        private SnakeCaseJsonSerializer _serializer;

        [SetUp]
        public void SetUp()
        {
            _serializer = new SnakeCaseJsonSerializer();
        }

        [TearDown]
        public void TearDown()
        {
            _serializer = null;
        }

        [Test]
        public void Serialize_Should_Convert_Property_Names_To_Snake_Case()
        {
            var obj = new TestObject {Title = "Mr", FirstName = "John", LastName = "Smith"};
            
            var result = PerformSerialize(obj);

            Assert.That(result.Contains("title"));
            Assert.That(result.Contains("first_name"));
            Assert.That(result.Contains("last_name"));
        }

        private string PerformSerialize(TestObject obj)
        {
            using (var sw = new StringWriter())
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    _serializer.Serialize(jw, obj);
                }
                return sw.ToString();
            }
        }

        class TestObject
        {
            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}

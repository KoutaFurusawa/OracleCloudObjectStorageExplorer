using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCISDK.Common;

namespace OCISDK.Test.Common
{
    [TestClass]
    public class JsonDefaultSerializerTest
    {
        IJsonSerializer JsonSerializer;

        public JsonDefaultSerializerTest() {
            this.JsonSerializer = new JsonDefaultSerializer();
        }

        // Serializa class model to text Test
        [TestMethod]
        void SerializaSimpleTest() {
            var model = new TestModels() {
                Name = "John",
                Countury = "Japan"
            };
            var res = this.JsonSerializer.Serialize(model);

            Assert.Equals("{\"countury\":\"Japan\",\"name\":\"John\"}", res);
        }

        // Deserialize class test to model Test
        [TestMethod]
        void DeserializeSimpleTest()
        {
            var text = "{\"countury\":\"Japan\",\"name\":\"John\"}";

            var res = this.JsonSerializer.Deserialize<TestModels>(text);

            Assert.Equals("John",res.Name);
            Assert.Equals("Japan", res.Countury);

        }

        // null empty json test
        [TestMethod]
        void SerializaNullableTest()
        {
            var model = new TestModels()
            {
                Name = "John",
                Countury = null
            };
            var res = this.JsonSerializer.Serialize(model);

            Assert.Equals("{\"name\":\"John\"}", res);
        }

        // empty not Deserialize test
        [TestMethod]
        void DeserializeNullableTest()
        {
            var text = "{\"name\":\"John\"}";

            var res = this.JsonSerializer.Deserialize<TestModels>(text);

            Assert.Equals("John", res.Name);
            Assert.IsNull(res.Countury);

        }

        // SubModel calss Serializa
        [TestMethod]
        void SerializaSubModelTest()
        {
            var model = new TestModels()
            {
                Name = "John",
                Countury = "Japan",
                SubInfo = new SubModel() {
                    Company = "SmithCompany",
                    Department = "Development"
                }
            };
            var res = this.JsonSerializer.Serialize(model);

            Assert.Equals(
                "{\"subInfo\":{\"department\":\"Development\",\"company\":\"SmithCompany\"},\"countury\":\"Japan\",\"name\":\"John\"}",
                res);
        }

        // SubModel calss Deserialize
        [TestMethod]
        void DeserializeSubModelTest()
        {
            var text = "{\"subInfo\":{\"department\":\"Development\",\"company\":\"SmithCompany\"},\"countury\":\"Japan\",\"name\":\"John\"}";

            var res = this.JsonSerializer.Deserialize<TestModels>(text);

            Assert.Equals("John", res.Name);
            Assert.Equals("Japan", res.Countury);
            Assert.Equals("SmithCompany", res.SubInfo.Company);
            Assert.Equals("Development", res.SubInfo.Department);

        }

        private class TestModels
        {
            public string Name { get; set; }

            public string Countury { get; set; }

            public SubModel SubInfo { get; set; }
        }

        private class SubModel
        {
            public string Company { get; set; }

            public string Department { get; set; }
        }
    }
}

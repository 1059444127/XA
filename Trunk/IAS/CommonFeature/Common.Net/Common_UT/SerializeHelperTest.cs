using UIH.XA.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UIH.XA.Common_UT
{


    /// <summary>
    ///This is a test class for SerializeHelperTest and is intended
    ///to contain all SerializeHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SerializeHelperTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion



        [TestMethod()]
        public void SerializeTestException()
        {
            SerializeTestHelper<GenericParameterHelper>();
            DesrializeTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Serialize
        ///</summary>
        public void SerializeTestHelper<T>()
        {
            T expected = default(T); // TODO: Initialize to an appropriate value
            byte[] actual = null;
            try
            {
                actual = SerializeHelper.Serialize<T>(expected);
            }
            catch
            {

            }
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for Desrialize
        ///</summary>
        public void DesrializeTestHelper<T>()
        {
            byte[] expected = null; // TODO: Initialize to an appropriate value
            T actual = default(T);
            try
            {
                actual = SerializeHelper.Desrialize<T>(expected);
            }
            catch
            {

            }
            Assert.IsNull(actual);

        }





        [TestMethod()]
        public void SerializeTest()
        {
            SerializeTestClass obj = new SerializeTestClass() { Field1 = "Test", Field2 = 100, Field3 = true };
            SerializeTestHelper<SerializeTestClass>(obj);
        }

        /// <summary>
        ///A test for Serialize
        ///</summary>
        public void SerializeTestHelper<T>(T obj)
        {
            byte[] buffer = SerializeHelper.Serialize<T>(obj);
            T newObj = SerializeHelper.Desrialize<T>(buffer);
            Assert.AreEqual(obj, newObj);
        }

        [Serializable]
        internal class SerializeTestClass
        {
            public string Field1 { get; set; }

            public int Field2 { get; set; }

            public bool Field3 { get; set; }

            public static bool operator ==(SerializeTestClass c1, SerializeTestClass c2)
            {
                return Object.Equals(c1,c2);
            }

            public static bool operator !=(SerializeTestClass c1, SerializeTestClass c2)
            {
                return !Object.Equals(c1, c2);
            }

            public override bool Equals(object obj)
            {
                SerializeTestClass targetObj = obj as SerializeTestClass;
                if (targetObj == null)
                    return false;

                if (this.Field1 == targetObj.Field1 &
                    this.Field2 == targetObj.Field2 &
                    this.Field3 == targetObj.Field3)
                    return true;
                return false;
            }

            public override int GetHashCode()
            {
                int result = string.IsNullOrEmpty(Field1) ? 0 : Field1.GetHashCode() + Field2.GetHashCode();
                return result;
            }
        }

    }

}

﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Argard;

namespace cmdValidatorTests
{
    [TestClass]
    public class UnitTestAdvanced
    {
        #region Test requirements
        bool expectedEventGotRaised;
        bool wrongEventGotRaised;
        ParameterSetArgs args;

        private void InitializeTest()
        {
            this.expectedEventGotRaised = false;
            this.wrongEventGotRaised = false;
            this.args = null;
        }

        private bool GetRaisingResults()
        {
            return expectedEventGotRaised && !wrongEventGotRaised;
        }

        void dummyFunc(ParameterSetArgs args)
        {
            this.wrongEventGotRaised = true;
        }

        void expectedFunc(ParameterSetArgs args)
        {
            this.expectedEventGotRaised = true;
            this.args = args;
        }
        #endregion

        [TestMethod]
        public void TestMethodAdvanced1()
        {
            InitializeTest();

            ParameterSetParser validator = new ParameterSetParser(false);
            validator.AddParameterSet("list  : x", expectedFunc);

            validator.CheckArgs("list x");

            bool actual = args.CMD["list"].ArgumentValues.FirstValue == "x";

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void TestMethodAdvanced2()
        {
            InitializeTest();

            ParameterSetParser validator = new ParameterSetParser(false);
            validator.AddParameterSet("list  : x |   y |   z", expectedFunc);

            validator.CheckArgs("list y");

            bool actual = args.CMD["list"].ArgumentValues.ParsedValues[0] == "y";

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void TestMethodAdvanced3()
        {
            InitializeTest();

            ParameterSetParser validator = new ParameterSetParser(false);
            validator.AddParameterSet("l[i]st  :( x |   y | z  | n     )", expectedFunc);

            validator.CheckArgs("lst z");

            bool actual = args.CMD["list"].ArgumentValues.ParsedValues[0] == "z";

            Assert.AreEqual(true, actual);
        }
    }
}

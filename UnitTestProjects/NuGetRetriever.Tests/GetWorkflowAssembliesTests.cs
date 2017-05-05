﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Models;

namespace NuGetRetriever.Tests
{
    [TestClass]
    public class GetWorkflowAssembliesTests
    {
        private const string PackageId = "Microsoft.CrmSdk.Workflow";
        private static List<NuGetPackage> _packages;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            _packages = PackageLister.GetPackagesbyId(PackageId);
        }

        [TestMethod]
        public void GetWorkflowAssembliesCount()
        {
            Assert.IsTrue(_packages.Count > 0);
        }

        [TestMethod]
        public void GetWorkflowAssemblies()
        {
            Assert.AreEqual(PackageId, _packages[0].Id);
        }

    }
}

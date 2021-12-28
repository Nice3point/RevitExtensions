using System;
using NUnit.Framework;

namespace Nice3point.Revit.Extensions.Tests;

[TestFixture]
public class StringTests
{
    [TestCase]
    public void ContainsTest()
    {
        Assert.AreEqual(true, "some Test string".Contains("test", StringComparison.OrdinalIgnoreCase));
        Assert.AreEqual(true, "some test string".Contains("test", StringComparison.OrdinalIgnoreCase));
        Assert.AreEqual(true, "some TEST string".Contains("TeSt", StringComparison.OrdinalIgnoreCase));
        Assert.AreEqual(false, "some TEST string".Contains("invalid", StringComparison.OrdinalIgnoreCase));
    }

    [TestCase]
    public void AppendPathTest()
    {
        Assert.AreEqual(@"C:\\Folder\file.txt", @"C:\\Folder".AppendPath("file.txt"));
    }
}
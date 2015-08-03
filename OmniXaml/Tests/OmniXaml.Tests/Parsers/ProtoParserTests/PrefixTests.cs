﻿namespace OmniXaml.Tests.Parsers.ProtoParserTests
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Classes;
    using Classes.Another;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OmniXaml.Parsers.ProtoParser;

    [TestClass]
    public class PrefixTests : GivenAWiringContext
    {
        private readonly ProtoNodeBuilder builder;
        private readonly ProtoParser sut;

        public PrefixTests()
        {
            builder = new ProtoNodeBuilder(WiringContext.TypeContext);
            sut = new ProtoParser(WiringContext.TypeContext);
        }

        [TestMethod]
        public void SingleCollapsed()
        {
            var actualNodes = sut.Parse("<x:Foreigner xmlns:x=\"another\"/>").ToList();
            
            var expectedNodes = new List<ProtoXamlNode>
            {
                builder.NamespacePrefixDeclaration(anotherNs),
                builder.EmptyElement(typeof (Foreigner), anotherNs),
                builder.None()
            };

            CollectionAssert.AreEqual(expectedNodes, actualNodes);
        }

        [TestMethod]
        public void AttachedProperty()
        {
            var actualNodes = sut.Parse(@"<DummyClass xmlns=""root"" xmlns:x=""another"" x:Foreigner.Property=""Value""></DummyClass>").ToList();

            var expectedNodes = new Collection<ProtoXamlNode>
            {
                builder.NamespacePrefixDeclaration(rootNs),
                builder.NamespacePrefixDeclaration(anotherNs),
                builder.NonEmptyElement(typeof (DummyClass), rootNs),
                builder.AttachableProperty<Foreigner>("Property", "Value", anotherNs),
                builder.EndTag(),
                builder.None()
            };

            CollectionAssert.AreEqual(expectedNodes, actualNodes);
        }

        [TestMethod]
        public void ElementWithPrefixThatIsDefinedAfterwards()
        {
            var actualNodes = sut.Parse(@"<x:DummyClass xmlns:x=""another""></x:DummyClass>").ToList();

            var expectedNodes = new Collection<ProtoXamlNode>
            {
                builder.NamespacePrefixDeclaration(anotherNs),
                builder.NonEmptyElement(typeof (DummyClass), anotherNs),
                builder.EndTag(),
                builder.None()
            };

            CollectionAssert.AreEqual(expectedNodes, actualNodes);
        }
    }
}
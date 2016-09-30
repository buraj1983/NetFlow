using System;
using NetFlow.CrossCutting.Extensions;
using NUnit.Framework;

namespace NetFlow.CrossCutting.Tests.Extensions
{
    [TestFixture]
    class TypeExtensionTests
    {
        [Test]
        public void GetGenericTypeDefinitionInterfaces_OnClassTypeWithoutGenericInterface_ResturnsEmptyCollection()
        {
            CollectionAssert.IsEmpty(typeof(Base).GetGenericInterfaces());
        }

        [Test]
        public void GetGenericTypeDefinitionInterfaces_OnClassWithGenericInterface_ResturnsGenericInterfaceDefinitions()
        {
            CollectionAssert.AreEquivalent(new[] {typeof(IImplementation<int>)},
                typeof(BaseImplemented).GetGenericInterfaces());
        }

        [Test]
        public void FindGenericInterface_ForGenericInterfaceType_ReturnsCollection()
        {
            CollectionAssert.AreEquivalent(new[] {typeof(IImplementation<int>)},
                typeof(BaseImplemented).FindGenericInterface(typeof(IImplementation<>)));
        }

        [Test]
        public void FindGenericInterface_ForNullGenericInterfaceType_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                typeof(BaseImplemented).FindGenericInterface(null);
            });
        }

        [Test]
        public void FindGenericInterface_WithClassArgument_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                typeof(BaseImplemented).FindGenericInterface(typeof(Base));
            });
        }

        [Test]
        public void FindGenericInterface_WithNonGenericInterfaceArgument_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                typeof(BaseImplemented).FindGenericInterface(typeof(IImplementation));
            });
        }

        private class Base : IImplementation
        {
        }

        private class BaseImplemented : IImplementation<int>, IImplementation
        {
        }

        private interface IImplementation
        {
        }

        private interface IImplementation<T>
        {
        }
    }
}

using Core.Collections;
using NUnit.Framework;
using System;

namespace Core.Test
{
    [TestFixture]
    public class HashStack_Tests
    {
        [Test]
        public void foo()
        {
            int x = 4;
            int x2 = 6;
            int dx = Math.Abs(x2 - x);

            
        }



        [Test]
        public void Push_ValueType()
        {
            var coll = new HashStack<int>();

            coll.Push(0);
            coll.Push(1);
            coll.Push(2);
            coll.Push(3);

            Assert.IsTrue(coll[0] == 3);
            Assert.IsTrue(coll[1] == 2);
            Assert.IsTrue(coll[2] == 1);
            Assert.IsTrue(coll[3] == 0);

            coll.Push(3);

            Assert.IsTrue(coll[0] == 3);
            Assert.AreEqual(coll.Count, 4);
        }

        [Test]
        public void Push_ReferenceType()
        {
            var coll = new HashStack<Exception>();

            var byZero = new DivideByZeroException();
            var arg = new ArgumentException();
            var oflow = new OverflowException();

            coll.Push(byZero);
            coll.Push(arg);
            coll.Push(oflow);

            Assert.IsTrue(coll[0] == oflow);
            Assert.IsTrue(coll[1] == arg);
            Assert.IsTrue(coll[2] == byZero);

            coll.Push(byZero);


            Assert.IsTrue(coll[0] == byZero);
            Assert.AreEqual(coll.Count, 3);
        }

        [Test]
        public void Pop()
        {
            var coll = new HashStack<int>();

            coll.Push(0);
            coll.Push(1);

            Assert.AreEqual(coll.Pop(), 1);
            Assert.AreEqual(coll.Pop(), 0);
            Assert.AreEqual(coll.Count, 0);
        }

        [Test]
        public void Pop_EmptyIsDefault()
        {
            var coll = new HashStack<Exception>();

            Assert.AreEqual(coll.Pop(), default);
        }

        [Test]  
        public void Peek()
        {
            var coll = new HashStack<int>();

            coll.Push(0);
            coll.Push(1);

            Assert.AreEqual(coll.Peek(), 1);
            coll.Pop();
            Assert.AreEqual(coll.Peek(), 0);
        }

        [Test]
        public void Peek_EmptyIsDefault()
        {
            var coll = new HashStack<Exception>();

            Assert.AreEqual(coll.Peek(), default);
        }
    }
}
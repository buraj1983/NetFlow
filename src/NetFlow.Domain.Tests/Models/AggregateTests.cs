using System;
using NetFlow.Common.Messaging;
using NetFlow.Domain.Models;
using NUnit.Framework;

namespace NetFlow.Domain.Tests.Models
{
    [TestFixture]
    class AggregateTests
    {
        [Test]
        public void ApplyEvent_ForUnknownEvent_ThrowsException()
        {
            var id = new Guid("7d353d82-3786-4584-b798-a8b75a86140e");
            var aggregate = new FakeAggregate(id);
            
            Assert.Throws<ArgumentException>(() =>
            {
                aggregate.ApplyEvent(new UnknownEvent(id));
            });
        }

        [Test]
        public void ApplyEvent_ForKnownEvent_IsHandled()
        {
            var id = new Guid("7d353d82-3786-4584-b798-a8b75a86140e");
            var aggregate = new FakeAggregate(id);

            aggregate.ApplyEvent(new KnownEvent(id));
            aggregate.ApplyEvent(new KnownEvent(id));

            Assert.IsTrue(aggregate.IsEventApplied);
        }

        private class FakeAggregate : Aggregate, IDomainEventHandler<KnownEvent>
        {
            public bool IsEventApplied { get; private set; }

            public FakeAggregate(Guid id) : base(id)
            {
            }

            public void HandleEvent(KnownEvent @event)
            {
                IsEventApplied = @event != null;
            }
        }

        private class KnownEvent : DomainEvent
        {
            public KnownEvent(Guid aggregateId) : base(aggregateId)
            {
            }
        }

        private class UnknownEvent : DomainEvent
        {
            public UnknownEvent(Guid aggregateId) : base(aggregateId)
            {
            }
        }
    }
}

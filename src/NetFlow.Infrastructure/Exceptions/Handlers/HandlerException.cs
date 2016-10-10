using System;
using System.Runtime.Serialization;

namespace NetFlow.Infrastructure.Exceptions.Handlers
{
    [Serializable]
    public class HandlerException : Exception
    {
        private const string DefaultMessage = "Handler exception.";

        public Type HandlerType { get; }

        public HandlerException(Type handlerType) : base(DefaultMessage)
        {
            HandlerType = handlerType;
        }

        public HandlerException(string message, Type handlerType) : base(message)
        {
            HandlerType = handlerType;
        }

        public HandlerException(string message, Type handlerType, Exception innerException)
            : base(message, innerException)
        {
            HandlerType = handlerType;
        }

        protected HandlerException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
            HandlerType = (Type)info.GetValue(nameof(HandlerType), typeof(Type));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(HandlerType), HandlerType);
        }

    }

    [Serializable]
    public class HandlerException<THandle> : HandlerException
    {
        public HandlerException()
            : base(typeof(THandle))
        {
        }

        public HandlerException(string message) 
            : base(message, typeof(THandle))
        {
        }

        public HandlerException(string message, Exception innerException)
            : base(message, typeof(THandle), innerException)
        {
        }
    }
}

﻿namespace NetFlow.Infrastructure.Queries
{
    public interface IRequestProcessor
    {
        TResult Process<TRequest, TResult>(TRequest request) where TRequest : IDataRequest<TResult>;
    }
}
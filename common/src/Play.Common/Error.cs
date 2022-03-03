namespace Play.Common
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    public readonly struct Error
    {
        public Error(string code, string message)
            : this(code, message, StatusCodes.Status400BadRequest, default)
        {
        }

        public Error(string code, string message, int statusCode)
            : this(code, message, statusCode, default)
        {
        }

        public Error(string code, string message, IInnerError innerError)
            : this(code, message, StatusCodes.Status400BadRequest, default)
        {
            InnerError = innerError;
        }

        public Error(string code, string message, int statusCode, IInnerError innerError)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            StatusCode = statusCode == default ? StatusCodes.Status400BadRequest : statusCode;
            InnerError = innerError;
            Details = new List<Error>();
        }

        public string Code { get; }
        public string Message { get; }
        public int StatusCode { get; }
        public IInnerError InnerError { get; }
        public List<Error> Details { get; }
    }
}
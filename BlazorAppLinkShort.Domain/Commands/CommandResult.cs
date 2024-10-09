using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestUpload.Domain.Commands
{
    public interface ICommandResult
    {
        HttpStatusCode GetStatusCode();
        bool Succeeded();
        string GetMessage();
        object? GetData();
        bool Success { get; }
        string Message { get; }
        object Data { get; }

        CommandResult GetResult();
    }

    public class CommandResult : ICommandResult
    {
        public CommandResult(HttpStatusCode httpStatusCode,
                            bool success,
                            string? message = null,
                            object? data = null)
        {
            HttpStatusCode = httpStatusCode;
            Success = success;
            Message = message ?? httpStatusCode.ToString();
            Data = data;
        }

        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.Unused;
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public object Data { get; set; } = null;

        public HttpStatusCode GetStatusCode() => this.HttpStatusCode;
        public bool Succeeded() => this.Success;
        public string GetMessage() => this.Message;
        public object? GetData() => this.Data;
        public CommandResult GetResult() => this;
    }
}

using MediatR;
using System;

namespace Tests.Application
{
    public record CreatedJobEvent(string message) : INotification;
}
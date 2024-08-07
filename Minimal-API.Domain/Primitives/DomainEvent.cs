﻿using MediatR;

namespace Minimal_API.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;
﻿using GrowManager.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GrowManager.Application.Specifications.Base
{
    public interface ISpecification<T> where T : class, IEntity
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
    }
}
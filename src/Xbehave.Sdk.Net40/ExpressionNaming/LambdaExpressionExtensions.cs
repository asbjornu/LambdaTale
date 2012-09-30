﻿// <copyright file="LambdaExpressionExtensions.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace Xbehave.Sdk.ExpressionNaming
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class LambdaExpressionExtensions
    {
        public static IEnumerable<string> SelectNaturalLanguageTokens(this LambdaExpression expression)
        {
            if (expression == null)
            {
                yield break;
            }

            foreach (var token in expression.Body.SelectNaturalLanguageTokens())
            {
                yield return token;
            }
        }
    }
}

﻿using System.Linq;

using Abp.Extensions;

using Castle.DynamicProxy;

using DynamicTranslator.Application.Model;
using DynamicTranslator.Configuration.Startup;
using DynamicTranslator.Exceptions;

namespace DynamicTranslator.Application
{
    public class TextGuardInterceptor : IInterceptor
    {
        private readonly IApplicationConfiguration _configuration;

        public TextGuardInterceptor(IApplicationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Arguments.Any())
            {
                var request = invocation.Arguments[0].As<TranslateRequest>();

                if (request.CurrentText.Length > _configuration.SearchableCharacterLimit)
                {
                    throw new MaximumCharacterLimitException("You have exceed maximum character limit");
                }

                invocation.Proceed();
            }
        }
    }
}

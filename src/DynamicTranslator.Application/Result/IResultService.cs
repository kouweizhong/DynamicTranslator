﻿using System.Threading.Tasks;

using Abp.Application.Services;
using Abp.Domain.Uow;

using DynamicTranslator.Orchestrators.Model;

namespace DynamicTranslator.Application.Result
{
    public interface IResultService : IApplicationService
    {
        [UnitOfWork]
        CompositeTranslateResult Get(string key);

        [UnitOfWork]
        Task<CompositeTranslateResult> GetAsync(string key);

        [UnitOfWork]
        CompositeTranslateResult Save(string key, CompositeTranslateResult translateResult);

        [UnitOfWork]
        CompositeTranslateResult SaveAndUpdateFrequency(string key, CompositeTranslateResult translateResult);

        [UnitOfWork]
        Task<CompositeTranslateResult> SaveAndUpdateFrequencyAsync(string key, CompositeTranslateResult translateResult);

        [UnitOfWork]
        Task<CompositeTranslateResult> SaveAsync(string key, CompositeTranslateResult translateResult);
    }
}
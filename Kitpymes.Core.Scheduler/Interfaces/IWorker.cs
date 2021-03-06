// -----------------------------------------------------------------------
// <copyright file="IWorker.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Scheduler
{
    using System.Threading.Tasks;

    public interface IWorker
    {
        ValueTask<bool> Execute(JobContext context);
    }
}

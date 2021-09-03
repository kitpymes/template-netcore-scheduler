// -----------------------------------------------------------------------
// <copyright file="IDbContextScheduler.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Scheduler
{
    using Microsoft.EntityFrameworkCore;

    public interface IDbContextScheduler
    {
        DbSet<Job> Jobs { get; set; }
    }
}

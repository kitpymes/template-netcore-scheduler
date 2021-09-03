// -----------------------------------------------------------------------
// <copyright file="JobState.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Scheduler
{
    public enum JobState
    {
        Scheduled = 1,

        Executing = 2,

        Executed = 3,

        Failed = 4,
    }
}

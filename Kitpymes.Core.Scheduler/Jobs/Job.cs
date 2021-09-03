// -----------------------------------------------------------------------
// <copyright file="Job.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Scheduler
{
    using System;

    public class Job
    {
        public long Id { get; set; }

        public string GroupId { get; set; }

        public string WorkerId { get; set; }

        public string Data { get; set; }

        public DateTime ScheduledAt { get; set; }

        public string State { get; set; }

        public int RetriesCounter { get; set; }

        public int MaxRetries { get; set; }

        public DateTime? ExecutedAt { get; set; }

        public int? UserId { get; set; }

        public string? Description { get; set; }
    }
}

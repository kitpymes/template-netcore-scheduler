// -----------------------------------------------------------------------
// <copyright file="JobDynamicData.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Scheduler
{
    using System.Collections.Generic;

    internal class JobDynamicData
    {
        public List<JobDataItem> Items { get; set; }

        public JobDynamicData() => this.Items = new ();
    }
}

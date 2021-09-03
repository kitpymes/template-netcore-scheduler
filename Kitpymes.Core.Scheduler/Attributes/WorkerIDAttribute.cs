// -----------------------------------------------------------------------
// <copyright file="WorkerIDAttribute.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Scheduler
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class WorkerIDAttribute : Attribute
    {
        public string WorkerID { get; private set; }

        public WorkerIDAttribute(string workerID) => this.WorkerID = workerID;
    }
}

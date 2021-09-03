// -----------------------------------------------------------------------
// <copyright file="SchedulerException.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Scheduler
{
    using System;

    [Serializable]
    public class SchedulerException : Exception
    {
        public SchedulerException(string message)
            : base(message)
        {
        }

        public SchedulerException(string message, Exception exception)
            : base(message, exception)
        {
        }

    }
}

// -----------------------------------------------------------------------
// <copyright file="JobContext.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Scheduler
{
    using System;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class JobContext
    {
        public long JobId { get; private set; }

        public string Group { get; private set; }

        public DateTime ActivationDate { get; private set; }

        private string Data { get; set; }

        public JobContext(long jobId, string groupId, string data, DateTime activationDate)
        {
            this.JobId = jobId;
            this.Group = groupId;
            this.Data = data;
            this.ActivationDate = activationDate;
        }

        public ValueTask<TData?> GetData<TData>()
        {
            try
            {
                var value = JsonConvert.DeserializeObject<TData>(this.Data);

                return new ValueTask<TData?>(value);
            }
            catch (Exception ex)
            {
                throw new SchedulerException(ErrorMsg.InvalidDeserialized(typeof(TData)), ex);
            }
        }

        public async ValueTask<TData?> GetDataAsync<TData>()
        {
            try
            {
                var value = JsonConvert.DeserializeObject<TData>(this.Data);

                return value;
            }
            catch (Exception ex)
            {
                throw new SchedulerException(ErrorMsg.InvalidDeserialized(typeof(TData)), ex);
            }
        }

        public dynamic GetData()
        {
            try
            {
                var value = JsonConvert.DeserializeObject<JobDynamicData>(this.Data);

                return value is not null && new JobData(value) as dynamic;
            }
            catch (Exception ex)
            {
                throw new SchedulerException(ErrorMsg.InvalidDeserialized(), ex);
            }
        }
    }
}
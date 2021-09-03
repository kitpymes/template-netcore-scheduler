// -----------------------------------------------------------------------
// <copyright file="JobData.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Scheduler
{
    using System.Collections.Generic;
    using System.Dynamic;

    public class JobData : DynamicObject
    {
        private readonly Dictionary<string, object> properties = new ();

        internal JobData(JobDynamicData dynamicData)
        => dynamicData.Items.ForEach(item => this.properties.Add(item.Key, item.Value));

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (this.properties.ContainsKey(binder.Name))
            {
                result = this.properties[binder.Name];
                return true;
            }
            else
            {
                throw new SchedulerException(ErrorMsg.InvalidKeyDictionary(binder.Name));
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this.properties[binder.Name] = value;
            return true;
        }
    }
}

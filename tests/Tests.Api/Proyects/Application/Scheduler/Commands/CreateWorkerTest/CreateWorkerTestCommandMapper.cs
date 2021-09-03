using AutoMapper;
using Kitpymes.Core.Scheduler;
using Newtonsoft.Json;
using System;

namespace Tests.Application
{
    public partial class CreateWorkerTestCommandMapper : Profile
    {
        public CreateWorkerTestCommandMapper()
        {
            CreateMap<CreateWorkerTestCommand, Job>()
                .ForMember(to => to.Data, from => from.MapFrom(from => JsonConvert.SerializeObject(from.Data)))
                .ForMember(c => c.RetriesCounter, opt => opt.MapFrom(_ => 1))
                .ForMember(c => c.MaxRetries, opt => opt.MapFrom(_ => 5))
                .ForMember(c => c.ScheduledAt, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(c => c.State, opt => opt.MapFrom(_ => JobState.Scheduled));

            CreateMap<Job, JobDto>()
                .ForMember(cd => cd.ScheduledAt, opt => opt.MapFrom(c => c.ScheduledAt.ToLongTimeString()))
                .ForMember(cd => cd.ExecutedAt, opt => opt.MapFrom(c => c.ExecutedAt.HasValue ? c.ExecutedAt.Value.ToLongTimeString() : ""));
        }
    }
}
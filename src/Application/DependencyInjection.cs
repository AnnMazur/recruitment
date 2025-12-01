using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVacancyService, VacancyService>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IInterviewService, InterviewService>();
            //services.AddScoped<IEvaluationFormService, EvaluationFormService>();
            //services.AddScoped<IEvaluationCriterionService, EvaluationCriterionService>();
            //services.AddScoped<ICriterionScoreService, CriterionScoreService>();


            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}

using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User 
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<UserRole>(src.Role)));

            CreateMap<UpdateUserRequest, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Candidate 
            CreateMap<Candidate, CandidateDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.EvaluationFormsCount,
                       opt  => opt.MapFrom(src => src.EvaluationForms.Count));

            CreateMap<CreateCandidateRequest, Candidate>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Status)
                        ? CandidateStatus.OnReview
                        : Enum.Parse<CandidateStatus>(src.Status)));

            CreateMap<UpdateCandidateRequest, Candidate>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Vacancy 
            CreateMap<Vacancy, VacancyDto>()
                .ForMember(dest => dest.CandidatesCount, opt => opt.MapFrom(src => src.Candidates.Count))
                .ForMember(dest => dest.InterviewsCount, opt => opt.MapFrom(src => src.Interviews.Count))
                .ForMember(dest => dest.ActiveCandidatesCount,
                    opt => opt.MapFrom(src => src.Candidates.Count(c => c.Status != CandidateStatus.Rejected)));

            CreateMap<CreateVacancyRequest, Vacancy>();
            CreateMap<UpdateVacancyRequest, Vacancy>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Interview 
            CreateMap<Interview, InterviewDto>()
                .ForMember(dest => dest.CandidateFullName,
                    opt => opt.MapFrom(src => $"{src.Candidate.LastName} {src.Candidate.FirstName} {src.Candidate.SecondName}"))
                .ForMember(dest => dest.VacancyTitle, opt => opt.MapFrom(src => src.Vacancy.Title))
                .ForMember(dest => dest.CreatedByUserName,
                    opt => opt.MapFrom(src => $"{src.CreatedBy.LastName} {src.CreatedBy.FirstName} {src.CreatedBy.SecondName}"))
                .ForMember(dest => dest.InterviewerUserName,
                    opt => opt.MapFrom(src => $"{src.Interviewer.LastName} {src.Interviewer.FirstName} {src.Interviewer.SecondName}"))
                .ForMember(dest => dest.HasEvaluationForm,
                    opt => opt.MapFrom(src => src.EvaluationForm != null));

            CreateMap<CreateInterviewRequest, Interview>();
            CreateMap<UpdateInterviewRequest, Interview>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // EvaluationCriterion
            CreateMap<EvaluationCriterion, EvaluationCriterionDto>();
            CreateMap<CreateEvaluationCriterionRequest, EvaluationCriterion>();
            CreateMap<UpdateEvaluationCriterionRequest, EvaluationCriterion>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // CriterionScore
            CreateMap<CriterionScore, CriterionScoreDto>()
                .ForMember(dest => dest.CriterionName, opt => opt.MapFrom(src => src.Criterion.Name))
                .ForMember(dest => dest.CriterionWeight, opt => opt.MapFrom(src => src.Criterion.Weight));

            CreateMap<CreateCriterionScoreRequest, CriterionScore>();
            CreateMap<UpdateCriterionScoreRequest, CriterionScore>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // EvaluationForm
            CreateMap<EvaluationForm, EvaluationFormDto>()
                .ForMember(dest => dest.CandidateFullName,
                    opt => opt.MapFrom(src => $"{src.Candidate.LastName} {src.Candidate.FirstName} {src.Candidate.SecondName}"))
                .ForMember(dest => dest.InterviewerName,
                    opt => opt.MapFrom(src => $"{src.Interviewer.LastName} {src.Interviewer.FirstName} {src.Candidate.SecondName}"))
                .ForMember(dest => dest.InterviewScheduledAt, opt => opt.MapFrom(src => src.Interview.ScheduledAt));

            CreateMap<CreateEvaluationFormRequest, EvaluationForm>();
            CreateMap<UpdateEvaluationFormRequest, EvaluationForm>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
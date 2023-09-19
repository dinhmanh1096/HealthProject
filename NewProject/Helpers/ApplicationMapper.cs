using AutoMapper;
using NewProject.Data;
using NewProject.Models;
using NewProject.Models.Authentication.SignUp;

namespace NewProject.Helpers
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Sport, SportModel>().ReverseMap();
            CreateMap<Sport, RequestSportModel>().ReverseMap();
            CreateMap<Workout, WorkoutModel>().ReverseMap();
            CreateMap<Workout, RequestWorkoutModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, RequestUserModel>().ReverseMap();
            CreateMap<Role, RoleModel>().ReverseMap();
            CreateMap<Role, RequestRoleModel>().ReverseMap();
            CreateMap<User, RegisterUser>().ReverseMap();
        }
    }
    
}

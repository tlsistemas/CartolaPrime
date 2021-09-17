using AutoMapper;
using CartoPrime.Application.ViewModel;
using CartoPrime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Application.AutoMapper
{
    public class ViewModelToModel : Profile
    {
        public ViewModelToModel()
        {
            CreateMap<PushResponse, PushNotification>();
            CreateMap<UserResponse, User>();

        }
    }
}

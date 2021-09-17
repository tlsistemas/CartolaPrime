using AutoMapper;
using CartoPrime.Application.ViewModel;
using CartoPrime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Application.AutoMapper
{
    public class ModelToViewModel : Profile
    {
        public ModelToViewModel()
        {
            CreateMap<PushNotification, PushResponse>();
            CreateMap<User, UserResponse>();

        }
    }
}

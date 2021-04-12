using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Mapper
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            AllowNullCollections = true;

            #region UserDto
            CreateMap<User, UserDto>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(d => d.Role, o => o.MapFrom(s => s.Role))
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.UserId));
            #endregion
        }
    }
}

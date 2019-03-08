﻿using AutoMapper;
using BookingApp.Data.Models;
using BookingApp.DTOs;
using BookingApp.Services.Interfaces;
using System;

namespace BookingApp.Services
{
    public class MapperService : IMapperService
    {
        readonly IMapper autoMapperInstance;

        public MapperService()
        {
            autoMapperInstance = new Mapper(new MapperConfiguration(cfg => {
                cfg.ValidateInlineMaps = false;
                cfg.CreateMap<ApplicationUser, AuthRegisterDto>().ReverseMap().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            }));
        }

        /// <summary>
        /// Internal wrapper.
        /// </summary>
        public TDestination Map<TDestination>(object source) => (TDestination)Map(source, source.GetType(), typeof(TDestination));

        /// <summary>
        /// Internal wrapper.
        /// </summary>
        public TDestination Map<TSource, TDestination>(TSource source) => (TDestination)Map(source, typeof(TSource), typeof(TDestination));

        /// <summary>
        /// Internal wrapper.
        /// </summary>
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) => (TDestination)Map(source, destination, typeof(TSource), typeof(TDestination));

        /// <summary>
        /// Automapper wrapper.
        /// </summary>
        public object Map(object source, Type sourceType, Type destinationType) => autoMapperInstance.Map(source, sourceType, destinationType);

        /// <summary>
        /// Automapper wrapper.
        /// </summary>
        public object Map(object source, object destination, Type sourceType, Type destinationType) => autoMapperInstance.Map(source, destination, sourceType, destinationType);
    }
}
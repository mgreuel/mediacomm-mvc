using System;
using System.Linq;

using AutoMapper;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Features.Account.ViewModels;

namespace MediaCommMvc.Web
{
    public static class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(
                configuration =>
                {
                    configuration.CreateMap<User, EditUserProfileViewModel>()
                    .ForMember(
                        u => u.DateOfBirth,
                        opt =>
                        {
                            opt.Condition(user => user.DateOfBirth.HasValue);
                            opt.MapFrom(user => user.DateOfBirth.Value.ToString("yyyy-MM-dd"));
                        });

                    configuration.CreateMap<EditUserProfileViewModel, User>().IgnoreAllNonExistingSource().ForMember(u => u.UserName, opt => opt.Ignore());
                });

            Mapper.AssertConfigurationIsValid();
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExistingSource<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType == sourceType && x.DestinationType == destinationType);
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                if (destinationType.GetProperty(property) != null)
                {
                    expression.ForMember(property, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}
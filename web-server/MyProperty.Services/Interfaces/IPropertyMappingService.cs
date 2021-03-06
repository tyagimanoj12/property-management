using MyProperty.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProperty.Services.Interfaces
{
    public interface IPropertyMappingService
    {
        void InitPropertyMapping<TSource, TDestination>(Dictionary<string, PropertyMappingValue> propertyMapping);
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        bool ValidMappingExistsFor<TSource, TDestination>(string fields);
    }
}

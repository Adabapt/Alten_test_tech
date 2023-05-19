using System;
using System.Collections.Generic;
using System.Text;

using AutoMapper;

// A adapter

using Alten_test_tech.Back.Application.ViewModels;

using Alten_test_tech.Back.Domain.Responses;
using Alten_test_tech.Back.Domain.Products.Entities;

namespace Alten_test_tech.Back.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // A adapater
            _ = this.CreateMap<Product, ProductViewModel>().ReverseMap();
            _ = this.CreateMap<Response, ResponseViewModel>().ReverseMap();
        }

    }
}

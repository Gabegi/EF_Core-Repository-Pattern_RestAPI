﻿using BrandApplication.Business.DTOs;
using BrandApplication.DataAccess.Models;


namespace BrandApplication.Business.Services.IServices.IServiceMappings
{

    public interface IBrandService: IReadServiceAsync<Brand, BrandDto>
    {
    }
}

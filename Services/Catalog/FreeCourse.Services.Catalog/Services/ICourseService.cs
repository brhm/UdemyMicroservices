﻿using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
   internal interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string id);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto createDto);
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto updateDto);
        Task<Response<NoContent>> DeleteAsync(string id);

    }
}

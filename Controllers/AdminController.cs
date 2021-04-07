using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using portar_proyectos_api.Data.Entities;
using portar_proyectos_api.Data.Interfaces;
using portar_proyectos_api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace portar_proyectos_api.Controllers
{
    [Authorize(Roles = Role.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;
        private IMapper _mapper;

        public AdminController(IAdminService adminService, IMapper mapper)
        {
            _mapper = mapper;
            _adminService = adminService;
        }

        [HttpPut("EditTeacher")]
        public async Task<IActionResult> EditTeacher(Teacher teacher)
        {
            try
            {
                await _adminService.EditTeacher(teacher);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetAllTeacher")]
        public IActionResult GetAllTeacher()
        {
            try
            {
                var teachers = _adminService.GetAllTeacher();
                return Ok(teachers);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("registerTeacher")]
        public async Task<IActionResult> RegisterTeacher([FromBody] TeacherDto userDto)
        {
            try
            {
                // save 
                await _adminService.RegisterTeacher(userDto);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteTeacher/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                // save 
                await _adminService.DeleteTeacher(id);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

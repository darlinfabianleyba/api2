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
    [Authorize(Roles = Role.Student)]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;
        private IMapper _mapper;

        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _mapper = mapper;
            _studentService = studentService;
        }

        [HttpGet("UpdateUserForFinalProject/{Id}/{HomeState}")]
        public async Task<IActionResult> UpdateUserForFinalProject(int Id, string HomeState)
        {
            try
            {
                await _studentService.UpdateUserForFinalProject(Id, HomeState);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("CreateChapterProject")]
        public async Task<IActionResult> CreateChapterProject(ChapterProject chapterProject)
        {
            try
            {
                await _studentService.CreateChapterProject(chapterProject);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("CreateProposedProject")] 
        public async Task<IActionResult> CreateProposedProject(ProposedProject proposedProject)
        {
            try
            {
                await _studentService.CreateProposedProject(proposedProject);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetAllChapterProject/{StudentId}")]
        public IActionResult GetAllChapterProject(int StudentId)
        {
            try
            {
                _studentService.GetAllChapterProject(StudentId);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetAllProposedProject/{UserId}/{GroupId}")]
        public IActionResult GetAllProposedProject(int UserId, string GroupId)
        {
            try
            {
                var resurt = _studentService.GetAllProposedProject(UserId, GroupId);
                return Ok(resurt);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetFinalProyect/{StudentId}")]
        public IActionResult GetFinalProyect(int StudentId)
        {
            try
            {
                var finalProject = _studentService.GetFinalProyect(StudentId);
                return Ok(finalProject);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetProposedProject/{StudentId}")]
        public async Task<IActionResult> GetProposedProject(int StudentId)
        {
            try
            {
                await _studentService.GetProposedProject(StudentId);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("CreateFinalProyect")]
        public async Task<IActionResult> CreateFinalProyect(FinalProject finalProyect)
        {
            try
            {
                await _studentService.CreateFinalProyect(finalProyect);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("UpdateProposedProject")]
        public async Task<IActionResult> UpdateProposedProject(ProposedProject proposedProject)
        {
            try
            {
                await _studentService.UpdateProposedProject(proposedProject);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using portar_proyectos_api.Data.Entities;
using portar_proyectos_api.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace portar_proyectos_api.Controllers
{
    [Authorize(Roles = Role.Teacher)]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private ITeacherService _teacherService;
        private IMapper _mapper;

        public TeacherController(ITeacherService teacherService, IMapper mapper)
        {
            _mapper = mapper;
            _teacherService = teacherService;
        }

        [HttpGet("GetAllChapterProject/{TeacherId}")]
        public IActionResult GetAllChapterProject(int TeacherId)
        {
            try
            {
                var resurt = _teacherService.GetAllChapterProject(TeacherId);
                return Ok(resurt);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            throw new NotImplementedException();
        }

        [HttpGet("GetAllFinalProjectForEvaluate/{TeacherId}/{section}/{projectState}")]
        public IActionResult GetAllFinalProjectForEvaluate(int TeacherId, string section, string projectState)
        {
            try
            {
                var resurt = _teacherService.GetAllFinalProjectForEvaluate(TeacherId, section, projectState);
                return Ok(resurt);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetAllProjectForEvaluate/{TeacherId}/{section}/{projectState}")]
        public IActionResult GetAllProjectForEvaluate(int TeacherId, string section, string projectState)
        {
            try
            {
                var resurt = _teacherService.GetAllProjectForEvaluate(TeacherId, section, projectState);
                return Ok(resurt);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetAllSection/{TeacherId}")]
        public IActionResult GetAllSection(int TeacherId)
        {
            try
            {
                var resurt = _teacherService.GetAllSection(TeacherId);
                return Ok(resurt);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetAllStudentForCredentials/{TeacherId}/{section}/{estudentState}")]
        public IActionResult GetAllStudentForCredentials(int TeacherId, string section, string estudentState)
        {
            try
            {
                var resurt = _teacherService.GetAllStudentForCredentials(TeacherId, section, estudentState);
                return Ok(resurt);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /* public IActionResult GetAllStudentForCredentials(int TeacherId, string estudentState = "todo", string section = "todo")
         {
             throw new NotImplementedException();
         }*/
        [HttpGet("UpdateUserForFinalProject/{Id}/{HomeState}")]
        public async Task<IActionResult> UpdateUserForFinalProject(int Id, string HomeState)
        {
            try
            {
                if(HomeState == "null")
                    await _teacherService.UpdateUserForFinalProject(Id, null);
                if (HomeState != "null")
                    await _teacherService.UpdateUserForFinalProject(Id, HomeState);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateFinalProject")]
        public async Task<IActionResult> updateFinalProject(FinalProject finalProject)
        {
            try
            {
                await _teacherService.updateFinalProject(finalProject);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateProjectForEvaluate")]
        public async Task<IActionResult> updateProjectForEvaluate(ProposedProject proposedProject)
        {
            try
            {
                await _teacherService.updateProjectForEvaluate(proposedProject);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateStudentForCredentials")]
        public async Task<IActionResult> updateStudentForCredentials(Student student)
        {
            try
            {
                await _teacherService.updateStudentForCredentials(student);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("updateChapterProject")]
        public async Task<IActionResult> updateChapterProject(ChapterProject chapterProject)
        {
            try
            {
                await _teacherService.updateChapterProject(chapterProject);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using NextFap.Application.Common.Models;
using Application.Dtos;
using Application.Services;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ApiControllerBase
    {
        private readonly StudentService _studentServices;
        private readonly ILogger<StudentsController> _logger;


        public StudentsController(StudentService studentServices, ILogger<StudentsController> logger)
        {
            _studentServices = studentServices;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetStudents(int? pageNumber, int? pageSize)
        {
            _logger.LogInformation($"Request to GetStudents with pageNumber: {pageNumber}, pageSize: {pageSize}");

            try
            {
                if (pageNumber != null && pageSize != null)
                {
                    _apiResponse = ApiResponse.Success(await _studentServices.GetAllWithPagination(pageNumber.Value, pageSize.Value));
                }
                _apiResponse = ApiResponse.Success(_studentServices.GetAll());
            }
            catch (Exception ex)
            {
                _apiResponse = ApiResponse.Failure(HttpStatusCode.BadRequest, ex.Message);

                _logger.LogError($"Request to GetStudents failture");
            }

            return _apiResponse;
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse> GetStudent(int id)
        {
            _logger.LogInformation($"Request to GetStudent have id: {id}");
            
            try
            {
                _apiResponse = ApiResponse.Success(_studentServices.GetById(id));
            }
            catch (Exception ex)
            {
                _apiResponse = ApiResponse.Failure(HttpStatusCode.BadRequest, ex.Message);

                _logger.LogError($"Request to GetStudent failture");
            }

            return _apiResponse;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateStudent(StudentCreateDto studentCreateDto)
        {
            _logger.LogInformation($"Request to CreateStudent: {JsonSerializer.Serialize<StudentCreateDto>(studentCreateDto)}");

            try
            {
                var newStudent = await _studentServices.Create(studentCreateDto);
                _apiResponse = ApiResponse.Success(newStudent);
            }
            catch (Exception ex)
            {
                _apiResponse = ApiResponse.Failure(HttpStatusCode.BadRequest, ex.Message);

                _logger.LogError($"Request to GetStudent failture");
            }

            return _apiResponse;
        }

        [HttpPost("update")]
        public async Task<ActionResult<ApiResponse>> UpdateStudent(int id, [FromBody] StudentUpdateDto studentUpdateDto)
        {
            _logger.LogInformation($"Request to UpdateStudent have id: {id}, {JsonSerializer.Serialize<StudentUpdateDto>(studentUpdateDto)}");

            try
            {
                _apiResponse = ApiResponse.Success(await _studentServices.Update(id, studentUpdateDto));
            }
            catch (Exception ex)
            {
                _apiResponse = ApiResponse.Failure(HttpStatusCode.BadRequest, ex.Message);

                _logger.LogError($"Request to UpdateStudent failture");
            }

            return _apiResponse;
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ApiResponse>> DeleteStudent(int id)
        {
            _logger.LogInformation($"Request to DeleteStudent have id: {id}");

            try
            {
                await _studentServices.Delete(id);
                _apiResponse = ApiResponse.Success(null);
            }
            catch (Exception ex)
            {
                _apiResponse = ApiResponse.Failure(HttpStatusCode.BadRequest, ex.Message);

                _logger.LogError($"Request to DeleteStudent failture");
            }

            return _apiResponse;
        }
    }
}

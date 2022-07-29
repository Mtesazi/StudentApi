using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentApi.Models;
using StudentApi.Repository; 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApi.Controllers
{
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  public class StudentsController : ControllerBase
  {
    private readonly ILogger<StudentsController> _logger;
    private readonly IStudentRespository _studentRespository;
    public StudentsController(
      ILogger<StudentsController> logger,
      IStudentRespository studentRespository)
    {
      _logger = logger;
      _studentRespository = studentRespository;
    }

    /// <summary>
    /// Get top 3 students
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
      try
      {
        return Ok(await _studentRespository.GetStudentsAsync());
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    /// <summary>
    /// Add new student
    /// </summary>
    /// <param name="student"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("Add")]
    public async Task<ActionResult> AddStudent(Student student)
    {
      try
      {
        var isInserted = await _studentRespository.AddStudentAsync(student).ConfigureAwait(false);
        if (isInserted) return Ok();
        return StatusCode(StatusCodes.Status400BadRequest);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    /// <summary>
    /// Delete student by id
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("Delete/{studentId}")]
    public async Task<ActionResult> DeleteStudent(int studentId)
    {
      try
      {
        var isDeleted = await _studentRespository.DeleteStudentAsync(studentId).ConfigureAwait(false);
        if (isDeleted) return Ok();
        return StatusCode(StatusCodes.Status400BadRequest);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }
  }
}

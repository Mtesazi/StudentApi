using Dapper;
using StudentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Repository
{
  public interface IStudentRespository
  {
    Task<List<Student>> GetStudentsAsync();
    Task<bool> AddStudentAsync(Student student);
    Task<bool> DeleteStudentAsync(int studentId);
  }
  public class StudentRespository : IStudentRespository
  {
    private readonly DapperContext _context;
    public StudentRespository(DapperContext context)
    {
      _context = context;
    }

    public async Task<bool> AddStudentAsync(Student student)
    {
      using var connection = _context.InitialiseConnection();
      var studentId = await connection.ExecuteAsync(@"INSERT INTO 
                                                      [dbo].[Student]
                                                      ([FirstName],[LastName],[Grade],[IsActive]) 
                                                      VALUES(@FirstName, @LastName, @Grade, 1)", student);
      if (studentId > 0) return true;
      return false;
    }

    //TODO maybe i update the IsActive flag to false instead of hard delete
    public async Task<bool> DeleteStudentAsync(int studentId)
    {
      using var connection = _context.InitialiseConnection();
      var deletedId = await connection.ExecuteAsync(@$"DELETE FROM [dbo].[Student]
                                                       WHERE StudentId = {studentId}");
      if (deletedId > 0) return true;
      return false;
    }

    public async Task<List<Student>> GetStudentsAsync()
    {
      using var connection = _context.InitialiseConnection();
      var students = await connection.QueryAsync<Student>(@"SELECT TOP 3 * FROM 
                                                           [dbo].[Student] WHERE IsActive = 1");
      return students.ToList();
    }
  }
}

using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.DbContexts
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CoursesDto>> GetCoursesForAuthor(Guid authorId)
        {
            var courses = _courseLibraryRepository.GetCourses(authorId);
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CoursesDto>>(courses));
        }

        [HttpGet("{courseId}")]
        public ActionResult<CoursesDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            var courseForAuthorFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);

            if (!_courseLibraryRepository.AuthorExists(authorId) || courseForAuthorFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CoursesDto>(courseForAuthorFromRepo));
        }
    }
}

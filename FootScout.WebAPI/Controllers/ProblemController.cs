﻿using AutoMapper;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;
using FootScout.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootScout.WebAPI.Controllers
{
    [Route("api/problems")]
    [Authorize(Policy = "AdminOrUserRights")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        private readonly IProblemRepository _problemRepository;
        private readonly IMapper _mapper;

        public ProblemController(IProblemRepository problemRepository, IMapper mapper)
        {
            _problemRepository = problemRepository;
            _mapper = mapper;
        }

        // GET: api/problems/:problemId
        [HttpGet("{problemId}")]
        public async Task<ActionResult<Problem>> GetProblem(int problemId)
        {
            var problem = await _problemRepository.GetProblem(problemId);
            if (problem == null)
                return NotFound();

            return Ok(problem);
        }

        // GET: api/problems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Problem>>> GetAllProblems()
        {
            var problems = await _problemRepository.GetAllProblems();
            return Ok(problems);
        }

        // GET: api/problems/solved
        [HttpGet("solved")]
        public async Task<ActionResult<IEnumerable<Problem>>> GetSolvedProblems()
        {
            var solvedProblemss = await _problemRepository.GetSolvedProblems();
            return Ok(solvedProblemss);
        }

        // GET: api/problems/unsolved
        [HttpGet("unsolved")]
        public async Task<ActionResult<IEnumerable<Problem>>> GetUnsolvedProblems()
        {
            var unsolvedProblemss = await _problemRepository.GetUnsolvedProblems();
            return Ok(unsolvedProblemss);
        }

        // POST: api/problems
        [HttpPost]
        public async Task<ActionResult> CreateProblem([FromBody] ProblemCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid dto data.");

            var problem = _mapper.Map<Problem>(dto);
            await _problemRepository.CreateProblem(problem);

            return Ok(problem);
        }

        // PUT: api/problems/:problemId
        [HttpPut("{problemId}")]
        public async Task<ActionResult> CheckProblemSolved(int problemId, [FromBody] Problem problem)
        {
            if (problemId != problem.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _problemRepository.CheckProblemSolved(problem);
            return NoContent();
        }
    }
}
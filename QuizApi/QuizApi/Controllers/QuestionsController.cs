﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApi.Data;
using QuizApi.Models;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuizContext _context;

        public QuestionsController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
         
            var random5Qns = await (_context.Questions
                  .Select(x => new
                  {
                      x.QnId,
                      x.QnInWords,
                      x.ImageName,
                      Options = new string[] { x.Optionl1, x.Optionl2, x.Optionl3, x.Optionl4 }
                  }))
                  .OrderBy(y => Guid.NewGuid())
                  .Take(5)
                  .ToListAsync();
            return Ok(random5Qns);
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
          if (_context.Questions == null)
          {
              return NotFound();
          }
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            if (id != question.QnId)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Questions/GetAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("GetAnswers")]
        public async Task<ActionResult<Question>> RetrieveAnswers(int[] qnIds)
        {
        
            var answers = await(_context.Questions
                .Where(x=> qnIds.Contains(x.QnId)).Select(y => new
                {
                    y.QnId,
                    y.QnInWords,
                    y.ImageName,
                    Options = new string[] { y.Optionl1, y.Optionl2, y.Optionl3, y.Optionl4 },
                    y.Answer
                }))
                  .ToListAsync();
            return Ok(answers);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(int id)
        {
            return (_context.Questions?.Any(e => e.QnId == id)).GetValueOrDefault();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API;
using DataLayer.DBObject;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly TempContext dbContext;

        public GroupsController(TempContext context)
        {
            dbContext = context;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
          if (dbContext.Groups == null)
          {
              return NotFound();
          }
            return await dbContext.Groups.ToListAsync();
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
          if (dbContext.Groups == null)
          {
              return NotFound();
          }
            var @group = await dbContext.Groups.FindAsync(id);

            if (@group == null)
            {
                return NotFound();
            }

            return @group;
        }

        // PUT: api/Groups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, Group @group)
        {
            if (id != @group.Id)
            {
                return BadRequest();
            }

            dbContext.Entry(@group).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(Group @group)
        {
          if (dbContext.Groups == null)
          {
              return Problem("Entity set 'TempContext.Groups'  is null.");
          }
            dbContext.Groups.Add(@group);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = @group.Id }, @group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (dbContext.Groups == null)
            {
                return NotFound();
            }
            var @group = await dbContext.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            dbContext.Groups.Remove(@group);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupExists(int id)
        {
            return (dbContext.Groups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using FinanceTrackerAPI.Core;
using FinanceTrackerAPI.Data;
using FinanceTrackerAPI.Models.MembershipFees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MembershipFeesController(FinanceTrackerDbContext context) : Controller
    {
        private readonly FinanceTrackerDbContext _context = context;

        [HttpPost(Name = "CreateMembershipFee")]
        public async Task<IActionResult> CreateMembershipFee([FromBody] CreateMembershipFees membershipFee)
        {
            if (membershipFee == null)
            {
                return BadRequest("Membership fee is null");
            }

            var newMembershipFee = new MembershipFees
            {
                Name = membershipFee.Name,
                Amount = membershipFee.Amount,
                Notes = membershipFee.Notes,
                IsRecurrent = membershipFee.IsRecurrent,
                RecurrencePeriod = membershipFee.RecurrencePeriod,
                Category = await _context.Categories.FindAsync(membershipFee.CategoryId) ?? throw new InvalidOperationException("Category not found")
            };

            _context.MembershipFees.Add(newMembershipFee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateMembershipFee), new { id = newMembershipFee.Id }, newMembershipFee);
        }


        [HttpGet(Name = "GetMembershipFees")]
        public async Task<IActionResult> GetMembershipFees()
        {
            var membershipFees = await _context.MembershipFees.ToListAsync();
            return Ok(membershipFees);
        }


        [HttpPut("{id}", Name = "UpdateMembershipFeeById")]
        public async Task<IActionResult> UpdateMembershipFeeById(
              Guid id,
              [FromBody] UpdateMembershipFeesById membershipFee
              )
        {
            var membershipFeesExists = await _context.MembershipFees.FindAsync(id);

            if (membershipFeesExists == null)
            {
                return NotFound("Membership fee not found");
            }

            // Atualiza a assinatura com os novos valores recebidos
            membershipFeesExists.Name = membershipFee.Name ?? membershipFeesExists.Name;
            membershipFeesExists.Amount = membershipFee.Amount ?? membershipFeesExists.Amount;
            membershipFeesExists.Notes = membershipFee.Notes ?? membershipFeesExists.Notes;
            membershipFeesExists.IsRecurrent = membershipFee.IsRecurrent;
            membershipFeesExists.RecurrencePeriod = membershipFee.RecurrencePeriod ?? membershipFeesExists.RecurrencePeriod;

            if(id != membershipFeesExists.CategoryId)
            {
                membershipFeesExists.Category = await _context.Categories.FindAsync(id) ?? membershipFeesExists.Category;
            }

            Console.WriteLine(membershipFeesExists);

            await _context.SaveChangesAsync();


            return Ok(membershipFeesExists);
        }

        [HttpDelete("{id}", Name = "DeleteMembershipFeeById")]
        public async Task<IActionResult> DeleteMembershipFeeById(string id)
        {
            if(id == null)
            {
                return BadRequest("Membership fee id is null");
            }

            var membershipFee = await _context.MembershipFees.FindAsync(id);

            if(membershipFee == null)
            {
                return NotFound("Membership fee not found");
            }

            _context.MembershipFees.Remove(membershipFee);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

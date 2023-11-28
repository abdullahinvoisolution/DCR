﻿using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactPersonController : ControllerBase
    {

        private readonly IContactPersonRepos _contactPersonRepos;

        public ContactPersonController(IContactPersonRepos contactPersonRepos)
        {
            _contactPersonRepos = contactPersonRepos;
        }

        [HttpPost]
        public async Task<ActionResult> GetPersons(DataTableViewModel model)
        {

            try
            {
                return Ok(await _contactPersonRepos.GetPersons(model));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<ContactPersonViewModel>> GetPerson(int ContactPersonId)
        {
            try
            {
                var result = await _contactPersonRepos.GetPerson(ContactPersonId);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult> CreatePerson([FromBody] ContactPersonViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }
                var CreatedContactPerson = await _contactPersonRepos.AddPerson(model);
                return StatusCode(StatusCodes.Status201Created, CreatedContactPerson);
            
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdatePerson( [FromBody] ContactPersonViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data. Please provide valid Person data.");
                }

                // Assuming you have a method like UpdateCustomer in your _customerRepos
                var UpdatePerson = await _contactPersonRepos.UpdatePerson( model);

                if (UpdatePerson != null)
                {
                    return Ok(UpdatePerson); // Return 200 OK with the updated customer
                }
                else
                {
                    return NotFound("Person not found"); // Return 404 Not Found if the customer doesn't exist
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult> DeletePerson([FromBody] int ContactPersonId)
        {
            try
            {
                if (ContactPersonId == null)
                {
                    return BadRequest();
                }
                var CreatedPerson = await _contactPersonRepos.DeletePerson(ContactPersonId);
                return StatusCode(StatusCodes.Status201Created, CreatedPerson);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }
    }
}

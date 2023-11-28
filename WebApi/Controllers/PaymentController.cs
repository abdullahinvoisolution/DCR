using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepos _paymentRepos;

        public PaymentController(IPaymentRepos paymentRepos)
        {
            _paymentRepos = paymentRepos;
        }

        [HttpPost]
        public async Task<ActionResult> GetPayments()
        {

            try
            {
                return Ok(await _paymentRepos.GetPayments());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<PaymentViewModel>> GetPayment([FromBody] int PaymentId)
        {
            try
            {
                var Result = await _paymentRepos.GetPayment(PaymentId);
                if (Result == null)
                {
                    throw new Exception("Null Data!");
                }
                return Result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }



        [HttpPost]
        public async Task<ActionResult> CreateInventory([FromBody] PaymentViewModel Model)
        {
            try
            {

                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedPayment = await _paymentRepos.AddPayment(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedPayment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdatePayment([FromBody] PaymentViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }

                var UpdatedPayment = await _paymentRepos.UpdatePayment(Model);

                if (UpdatedPayment != null)
                {
                    return Ok(UpdatedPayment); 
                }
                else
                {
                    throw new Exception("Data Not Found!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeletePayment([FromBody] int PaymentId)
        {
            try
            {
                if (PaymentId == null)
                {
                    return BadRequest();
                }
                var CreatedPayment = await _paymentRepos.DeletePayment(PaymentId);
                return StatusCode(StatusCodes.Status201Created, CreatedPayment);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Models;
using API.Services.SouvenirService;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers_
{
    [Route("api/[controller]")]
    [ApiController]
    public class SouvenirsController : ControllerBase
    {
        private readonly ISouvenirService _souvenirService;

        public SouvenirsController(ISouvenirService service)
        {
            _souvenirService = service;
        }

        // GET: api/Souvenirs
        [HttpGet]
        public async Task<ActionResult<ResponseData<List<Souvenir>>>> GetSouvenir(string? category, int pageNo = 0, int pageSize = 3)
        {
            return Ok(await _souvenirService.GetSouvenirListAsync(category, pageNo, pageSize));
        }

        // GET: api/Souvenirs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseData<List<Souvenir>>>> GetSouvenir(int id)
        {
            return Ok(await _souvenirService.GetSouvenirByIdAsync(id));
        }

        // PUT: api/Souvenirs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseData<List<Souvenir>>>> PutSouvenir(int id, Souvenir souvenir)
        {
            try
            {
                await _souvenirService.UpdateSouvenirAsync(id, souvenir);
            } catch (Exception e)
            {
                return NotFound(new ResponseData<Souvenir>()
                {
                    Success = false,
                    ErrorMessage = e.Message
                });
            }

            return Ok(new ResponseData<Souvenir>()
            {
                Data = souvenir,
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseData<List<Souvenir>>>> PostSouvenir(Souvenir souvenir)
        {
            if (souvenir is null)
            {
                return BadRequest(new ResponseData<Souvenir>()
                {
                    Success = false,
                    ErrorMessage = "Souvenir is null"
                });
            }
            var response = await _souvenirService.CreateSouvenirAsync(souvenir);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return CreatedAtAction("GetSouvenir", new { id = souvenir.Id }, new ResponseData<Souvenir>()
            {
                Data = souvenir
            });
        }

		[HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseData<string>>> PostImage(int id, [FromForm] IFormFile formFile)
		{
			var response = await _souvenirService.SaveImageAsync(id, formFile);
			if (response.Success)
			{
				return Ok(response);
			}
			return NotFound(response);
		}

		// DELETE: api/Souvenirs/5
		[HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSouvenir(int id)
        {
            try
            {
                await _souvenirService.DeleteSouvenirAsync(id);
            }
            catch (Exception e)
            {
                return NotFound(new ResponseData<Souvenir>()
                {
                    Success = false,
                    ErrorMessage = e.Message
                });
            }

            return NoContent();
        }
    }
}

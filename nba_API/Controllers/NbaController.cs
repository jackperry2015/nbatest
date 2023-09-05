using Microsoft.AspNetCore.Mvc;
using nba_API.Db;
using nba_API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace nba_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NbaController : ControllerBase
    {
        private readonly NbaContext _nbaContext;

        public NbaController(NbaContext nbaContext)
        {
            _nbaContext = nbaContext;
        }

        [HttpGet(Name = "NbaTable")]
        public async Task<ActionResult<List<NbaTableDto>>> GetNbaTable()
        {
                var result = await _nbaContext.NbaTable.FromSql($"exec GetTeamStatistics").ToListAsync();
                return result;
        }
    }
}
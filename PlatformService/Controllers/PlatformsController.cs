using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Entities;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController
    (IPlatformRepository platformRepository,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms()
    {
        var platforms = await platformRepository.GetAllPlatformsAsync();
        return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
    {
        var platform = await platformRepository.GetPlatformByIdAsync(id);
        if (platform == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<PlatformReadDto>(platform));
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platform = mapper.Map<Platform>(platformCreateDto);

        platformRepository.CreatePlatform(platform);

        if (!await platformRepository.SaveChangesAsync())
        {
            return BadRequest("Failed to create the platform");
        }

        return CreatedAtAction(
            nameof(GetPlatformById),
            new { id = platform.Id },
            mapper.Map<PlatformReadDto>(platform)
        );
    }
}

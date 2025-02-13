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
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        var platforms = platformRepository.GetAllPlatforms();
        return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
    }

    [HttpGet("{id:int}")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        var platform = platformRepository.GetPlatformById(id);
        if (platform == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<PlatformReadDto>(platform));
    }

    [HttpPost]
    public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platform = mapper.Map<Platform>(platformCreateDto);

        platformRepository.CreatePlatform(platform);

        if (!platformRepository.SaveChanges())
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

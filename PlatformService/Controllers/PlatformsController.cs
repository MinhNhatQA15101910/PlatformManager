using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Entities;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController(
    IPlatformRepository platformRepository,
    IMapper mapper,
    ICommandDataClient commandDataClient
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
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platform = mapper.Map<Platform>(platformCreateDto);

        platformRepository.CreatePlatform(platform);

        if (!platformRepository.SaveChanges())
        {
            return BadRequest("Failed to create the platform");
        }

        var platformReadDto = mapper.Map<PlatformReadDto>(platform);

        try
        {
            await commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
        }

        return CreatedAtAction(
            nameof(GetPlatformById),
            new { id = platform.Id },
            platformReadDto
        );
    }
}

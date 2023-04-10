using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo platformRepo;
    private readonly IMapper mapper;

    public PlatformsController(IPlatformRepo platformRepo, IMapper mapper)
    {
        this.platformRepo = platformRepo;
        this.mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        var platforms = this.platformRepo.GetAllPlatforms();
        var platformDtos = this.mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
        return Ok(platformDtos);
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        var platform = this.platformRepo.GetPlatformById(id);
        if (platform == null) return NotFound();
        var platformDto = this.mapper.Map<PlatformReadDto>(platform);
        return Ok(platformDto);
    }

    [HttpPost]
    public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platformModel = this.mapper.Map<Platform>(platformCreateDto);
        this.platformRepo.CreatePlatform(platformModel);
        this.platformRepo.SaveChanges();

        var platformReadDto = this.mapper.Map<PlatformReadDto>(platformModel);
        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }
}
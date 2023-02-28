using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using Notes.API.Notes.Domain.Models;
using Notes.API.Notes.Domain.Services;
using Notes.API.Notes.Resources.Create;
using Notes.API.Notes.Resources.Show;
using Notes.API.Notes.Resources.Update;
using Notes.API.Notes.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Notes.API.Notes.Interface.Rest;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("CRUD for Note")]

public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;
    private readonly IMapper _mapper;

    public NoteController(INoteService noteService, IMapper mapper)
    {
        _noteService = noteService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<NoteResource>> GetAllNotes()
    {
        var result = await _noteService.ListAllNotesAsync();
        var mappedResult = _mapper.Map<IEnumerable<Note>, IEnumerable<NoteResource>>(result);
        return mappedResult;
    }
    
    [HttpGet("archived")]
    public async Task<IEnumerable<NoteResource>> GetAllArchivedNotes()
    {
        var result = await _noteService.ListAllArchivedNotesAsync();
        var mappedResult = _mapper.Map<IEnumerable<Note>, IEnumerable<NoteResource>>(result);
        return mappedResult;
    }
    
    [HttpGet("not-archived")]
    public async Task<IEnumerable<NoteResource>> GetAllNotArchivedNotes()
    {
        var result = await _noteService.ListAllNotArchivedNotesAsync();
        var mappedResult = _mapper.Map<IEnumerable<Note>, IEnumerable<NoteResource>>(result);
        return mappedResult;
    }
    
    [HttpGet("{noteId}")]
    public async Task<IActionResult> GetNote(long noteId)
    {
        var result = await _noteService.FindAsync(noteId);
        if (!result.Success)
            return BadRequest(result.Message);
        var mappedResource = _mapper.Map<Note, NoteResource>(result.Resource);
        return Ok(mappedResource);
    }

    [HttpPost]
    public async Task<IActionResult> AddNote([FromBody, SwaggerRequestBody("New Note")] CreateNoteResource createNoteResource)
    {
        var mappedNote = _mapper.Map<CreateNoteResource, Note>(createNoteResource);
        var result = await _noteService.AddAsync(mappedNote);
        if (!result.Success)
            return BadRequest(result.Message);
        var mappedResource = _mapper.Map<Note, NoteResource>(result.Resource);
        return Ok(new { message = "Successfully added.", resource = mappedResource });
    }

    [HttpPut("{noteId}")]
    public async Task<IActionResult> UpdateNote(long noteId, [FromBody, SwaggerRequestBody("Update Note")] UpdateNoteResource updateNoteResource)
    {
        var mappedNote = _mapper.Map<UpdateNoteResource, Note>(updateNoteResource);
        var result = await _noteService.UpdateAsync(noteId, mappedNote);
        if (!result.Success)
            return BadRequest(result.Message);
        var mappedResource = _mapper.Map<Note, NoteResource>(result.Resource);
        return Ok(new { message = "Successfully updated.", resource = mappedResource });;
    }
    
    [HttpDelete("{noteId}")]
    public async Task<IActionResult> UpdateNote(long noteId)
    {
        var result = await _noteService.RemoveAsync(noteId);
        if (!result.Success)
            return BadRequest(result.Message);
        var mappedResource = _mapper.Map<Note, NoteResource>(result.Resource);
        return Ok(new { message = "Successfully updated.", resource = mappedResource });;
    }

    [HttpGet("exist/{noteId}")]
    public async Task<IActionResult> NoteExistence(long noteId)
    {
        var exist = await _noteService.ExistAsync(noteId);
        return Ok(new { exist });
    }

    [HttpPut("status/{noteId}")]
    public async Task<IActionResult> SetArchiveStatus(long noteId, [FromBody, SwaggerRequestBody("boolean status")] bool status)
    {
        var result = await _noteService.ArchiveNoteAsync(noteId, status);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(new {message = $"Note archived status change to {status}"});
    }


}
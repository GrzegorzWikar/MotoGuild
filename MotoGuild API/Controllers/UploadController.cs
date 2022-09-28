using System.Net;
using System.Net.Mime;
using System.Text.Json;
using AutoMapper;
using Data;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoGuild_API.Dto.GroupDtos;
using MotoGuild_API.Helpers;
using MotoGuild_API.Repository.Interface;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{

    private IHostingEnvironment _hostingEnvironment;
    private MotoGuildDbContext _dbContext;

    public UploadController(IHostingEnvironment hostingEnvironment, MotoGuildDbContext dbContext)
    {
        _hostingEnvironment = hostingEnvironment;
        _dbContext = dbContext;
    }

    [HttpPost]
    public IActionResult UploadImages()
    {
        try
        {
            var files = HttpContext.Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    FileInfo fileInfo = new FileInfo(file.FileName);
                    var newfilename = "Image_" + DateTime.Now.Ticks + fileInfo.Extension;
                    var path = Path.Combine("",
                        _hostingEnvironment.ContentRootPath + "\\Uploads\\GroupPictures\\" + newfilename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(newfilename);
                }
            }
            else
            {
                BadRequest("No files selected to upload");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return NotFound();
    }


    [HttpGet("{imageName}")]
    public FileContentResult GetImageUpload(string imageName)
    {
        string imagePath = Path.Combine("",
            _hostingEnvironment.ContentRootPath + "\\Uploads\\GroupPictures\\" + imageName);
        string[] ext = imagePath.Split(".");

        if (System.IO.File.Exists(imagePath))
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, $"image/{ext.Last()}");
        }


        string placeholderImagePath = Path.Combine("",
            _hostingEnvironment.ContentRootPath + "\\Uploads\\noimage.jpg");
        byte[] placeholderImageBytes = System.IO.File.ReadAllBytes(placeholderImagePath);
        return File(placeholderImageBytes, $"image/jpg");
    }


    [HttpDelete("{imageName}")]
    public IActionResult DeleteImageUpload(string imageName)
    {
        string imagePath = Path.Combine("",
            _hostingEnvironment.ContentRootPath + "\\Uploads\\GroupPictures\\" + imageName);
        string[] ext = imagePath.Split(".");

        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
            return NoContent();
        }

        return BadRequest("File not exist");
    }

}
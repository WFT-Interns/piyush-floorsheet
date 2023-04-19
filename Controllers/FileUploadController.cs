using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]/[Action]")]
public class MyController : ControllerBase
{
    [HttpPost]
    [Route("floorsheet")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        try
        {
            // Check if the file exists
            if (file == null || file.Length == 0)
                return BadRequest("Please select a file to upload.");

            // Check if the file is a XLS file
            if (!file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".xlsx"))
                return BadRequest("Please upload a XLS file.");

            // Create a unique filename to store the file
            var fileName = Guid.NewGuid().ToString() + ".xls";

            // Create the file path
            var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var filePath = Path.Combine(homeDir, "Documents/floor-sheet/public", fileName);


            // Copy the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return a success response
            return Ok("File uploaded successfully.");
        }
        catch (Exception ex)
        {
            // Return an error response if an exception occurs
            return StatusCode(500, $"Internal server error: {ex}");

        }
    }

    [HttpGet]
    [Route("message")]
    public string Hello()

    {
        return "Hello";
    }
}

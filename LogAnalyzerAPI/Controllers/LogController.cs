using LogAnalyzerLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogAnalyzerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogAnalyzerService _logAnalyzerService;

        public LogController(ILogAnalyzerService logAnalyzerService)
        {
            _logAnalyzerService = logAnalyzerService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchLogsInDirectories([FromQuery] List<string> directories)
        {
            var result = await _logAnalyzerService.SearchLogsInDirectoriesAsync(directories);
            return Ok(result);
        }

        [HttpGet("countUniqueErrors")]
        public async Task<IActionResult> CountUniqueErrorsPerLog([FromQuery] string logFilePath)
        {
            var result = await _logAnalyzerService.CountUniqueErrorsPerLogAsync(logFilePath);
            return Ok(result);
        }

        [HttpGet("countDuplicatedErrors")]
        public async Task<IActionResult> CountDuplicatedErrorsPerLog([FromQuery] string logFilePath)
        {
            var result = await _logAnalyzerService.CountDuplicatedErrorsPerLogAsync(logFilePath);
            return Ok(result);
        }

        [HttpDelete("deleteArchived")]
        public async Task<IActionResult> DeleteArchivedLogsFromPeriod([FromQuery] string directory, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            await _logAnalyzerService.DeleteArchivedLogsFromPeriodAsync(directory, startDate, endDate);
            return Ok();
        }

        [HttpPost("archive")]
        public async Task<IActionResult> ArchiveLogsFromPeriod([FromQuery] string directory, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            await _logAnalyzerService.ArchiveLogsFromPeriodAsync(directory, startDate, endDate);
            return Ok();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadLogsToRemoteServer([FromQuery] string logFilePath, [FromQuery] string apiEndpoint)
        {
            await _logAnalyzerService.UploadLogsToRemoteServerAsync(logFilePath, apiEndpoint);
            return Ok();
        }

        [HttpDelete("deleteLogs")]
        public async Task<IActionResult> DeleteLogsFromPeriod([FromQuery] string directory, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            await _logAnalyzerService.DeleteLogsFromPeriodAsync(directory, startDate, endDate);
            return Ok();
        }

        [HttpGet("countLogs")]
        public async Task<IActionResult> CountTotalLogsInPeriod([FromQuery] string directory, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _logAnalyzerService.CountTotalLogsInPeriodAsync(directory, startDate, endDate);
            return Ok(result);
        }

        [HttpGet("searchBySize")]
        public async Task<IActionResult> SearchLogsBySize([FromQuery] string directory, [FromQuery] long minSizeKb, [FromQuery] long maxSizeKb)
        {
            var result = await _logAnalyzerService.SearchLogsBySizeAsync(directory, minSizeKb, maxSizeKb);
            return Ok(result);
        }

        [HttpGet("searchByDirectory")]
        public async Task<IActionResult> SearchLogsByDirectory([FromQuery] string directory)
        {
            var result = await _logAnalyzerService.SearchLogsByDirectoryAsync(directory);
            return Ok(result);
        }

    }
}



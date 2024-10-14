using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace OData.Controllers
{
    [Route("api/[controller]")]
    public class GPSDataController : ODataController
    {
        private readonly ILogger<GPSDataController> _logger;
        private readonly IGPSDataService _gpsDataService;


        public GPSDataController(IGPSDataService gpsDataService, ILogger<GPSDataController> logger)
        {
            _gpsDataService = gpsDataService;
            _logger = logger;
        }

        // GET: api/GPSData
        [HttpGet]
        [EnableQuery]
        public IQueryable<GPSDataModel> Get()
        {
            // Sử dụng IGPSDataService để lấy tất cả danh sách GPSData từ nguồn dữ liệu
            var gpsData = _gpsDataService.GetAllGPSData();
            return gpsData;
        }
        // GET: api/GPSData/{id}
        [HttpGet("{id}")]
        [EnableQuery]
        public SingleResult<GPSDataModel> Get([FromRoute] int id)
        {
            // Truy vấn dữ liệu GPS theo ID
            var gpsData = _gpsDataService.GetGPSDataByIndex(id);
            return SingleResult.Create(gpsData);
        }
    }

}

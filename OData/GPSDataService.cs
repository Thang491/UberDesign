using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection;

namespace OData
{
    public class GPSDataService : IGPSDataService
    {
        private readonly List<GPSDataModel> _gpsData;

        public GPSDataService()
        {
            // Khởi tạo service và load dữ liệu từ file CSV
            var relativePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data", "new_clean_bo3.csv");
            _gpsData = LoadVehicleDataFromCsv(relativePath);
        }

        // Tải dữ liệu GPS từ file CSV
        public List<GPSDataModel> LoadVehicleDataFromCsv(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<VehicleDataMap>();
            var records = csv.GetRecords<GPSDataModel>().ToList();
            return records;
        }

        // Lấy tất cả dữ liệu GPS
        public IQueryable<GPSDataModel> GetAllGPSData()
        {
            return _gpsData.AsQueryable();
        }

        // Lấy dữ liệu GPS theo ID
        public IQueryable<GPSDataModel> GetGPSDataByIndex(int index)
        {
            var gpsData = _gpsData.Where(d => d.Index == index).AsQueryable();
            return gpsData;
        }

        // Thêm mới dữ liệu GPS
        public void AddGPSData(GPSDataModel newGPSData)
        {
            // Thêm bản ghi mới vào danh sách
            _gpsData.Add(newGPSData);
        }

        // Cập nhật dữ liệu GPS theo ID
        public void UpdateGPSData(int index, GPSDataModel updatedGPSData)
        {
            var existingGPSData = _gpsData.FirstOrDefault(d => d.Index == index);
            if (existingGPSData != null)
            {
                // Cập nhật các trường của bản ghi hiện có
                existingGPSData.VehicleId = updatedGPSData.VehicleId;
                existingGPSData.PStart = updatedGPSData.PStart;
                existingGPSData.PTemp = updatedGPSData.PTemp;
                existingGPSData.PEnd = updatedGPSData.PEnd;
                existingGPSData.PreRoutes = updatedGPSData.PreRoutes;
                existingGPSData.Freq = updatedGPSData.Freq;
                existingGPSData.Label = updatedGPSData.Label;
                existingGPSData.Regions = updatedGPSData.Regions;
            }
        }

        // Xóa dữ liệu GPS theo ID
        public void DeleteGPSData(int index)
        {
            var gpsDataToDelete = _gpsData.FirstOrDefault(d => d.Index == index);
            if (gpsDataToDelete != null)
            {
                _gpsData.Remove(gpsDataToDelete);
            }
        }
    }
    public class VehicleDataMap : ClassMap<GPSDataModel>
    {
        public VehicleDataMap()
        {
            Map(m => m.Index).Name("index");
            Map(m => m.VehicleId).Name("vehicle_id");
            Map(m => m.PStart).Convert(args => ConvertToCoordinate(args.Row.GetField("p_start")));
            Map(m => m.PTemp).Convert(args => ConvertToCoordinate(args.Row.GetField("p_temp")));
            Map(m => m.PEnd).Convert(args => ConvertToCoordinate(args.Row.GetField("p_end")));
            Map(m => m.PreRoutes).Convert(args => args.Row.GetField("pre_routes").Split(',').ToList());
            Map(m => m.Freq).Name("freq");
            Map(m => m.Label).Name("label").TypeConverterOption.BooleanValues(true, true, "TRUE", "FALSE");
            Map(m => m.Regions).Convert(args => args.Row.GetField("regions").Split(',').ToList());
        }

        private static Coordinate ConvertToCoordinate(string field)
        {
            // Tách chuỗi tọa độ theo định dạng "(long, lat)"
            var parts = field.Trim('(', ')').Split(',');
            return new Coordinate(double.Parse(parts[0]), double.Parse(parts[1]));
        }
    }
}

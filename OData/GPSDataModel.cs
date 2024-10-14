namespace OData
{
    public class GPSDataModel
    {
        public int Index { get; set; }
        public long VehicleId { get; set; }  // vehicle_id là số lớn, nên ta dùng long
        public Coordinate PStart { get; set; }  // Tạo class Coordinate để biểu diễn các giá trị tọa độ
        public Coordinate PTemp { get; set; }
        public Coordinate PEnd { get; set; }
        public List<string> PreRoutes { get; set; }  // Các giá trị trong pre_routes có dạng chuỗi trong dấu ngoặc đơn
        public int Freq { get; set; }
        public bool Label { get; set; }
        public List<string> Regions { get; set; }  // Cột regions chứa danh sách các địa danh
    }
    public class Coordinate
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Coordinate(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}

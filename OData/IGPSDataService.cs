namespace OData
{
    public interface IGPSDataService
    {
        IQueryable<GPSDataModel> GetAllGPSData();
        IQueryable<GPSDataModel> GetGPSDataByIndex(int id);
        void AddGPSData(GPSDataModel newGPSData);
        void UpdateGPSData(int id, GPSDataModel updatedGPSData);
        void DeleteGPSData(int id);
    }
}

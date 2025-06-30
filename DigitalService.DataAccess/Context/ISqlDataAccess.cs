namespace DigitalService.DataAccess
{
    public interface ISqlDataAccess
    {
        public Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters, string connectionStringName);

        public Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connectionId = "DefaultConnection");

        public Task<bool> SaveData<T>(string spName, T parameters, string connectionId = "DefaultConnection");
    }
}
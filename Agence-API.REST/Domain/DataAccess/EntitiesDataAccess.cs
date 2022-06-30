using Agence_API.REST.Domain.Models;

namespace Agence_API.REST.Domain.DataAccess
{
    public class EntitiesDataAccess
    {
        private static dbEntities _dbEntities;
        private EntitiesDataAccess() { }

        public static dbEntities Instance {
            get {
                if (_dbEntities == null)
                {
                    _dbEntities = new dbEntities();
                    _dbEntities.Database.Connection.Open();
                }
                return _dbEntities;
            }
        }

    }
}
using OMC2016.Models.Tools;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace OMC2016.Controllers.Tools
{
    public class ctlMaster
    {
        #region Chaeck Instance

        private static ctlMaster _instance;

        private ctlMaster() { }

        public static ctlMaster Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ctlMaster();
                }
                return _instance;
            }
        }

        #endregion

        public void GetConnectionString(ApplicationConfig model)
        {
            #region System
            string _System = ConfigurationManager.ConnectionStrings["System"].ConnectionString;
            EntityConnectionStringBuilder _SystemEF = new EntityConnectionStringBuilder(_System);
            SqlConnectionStringBuilder _SystemSQL = new SqlConnectionStringBuilder(_SystemEF.ProviderConnectionString);

            model.SystemServer = _SystemSQL.DataSource;
            model.SystemDatabase = _SystemSQL.InitialCatalog;
            model.SystemUsername = _SystemSQL.UserID;
            model.SystemPassword = _SystemSQL.Password;
            #endregion

            #region Accuont
            string _Account = ConfigurationManager.ConnectionStrings["Account"].ConnectionString;
            EntityConnectionStringBuilder _AccountEF = new EntityConnectionStringBuilder(_Account);
            SqlConnectionStringBuilder _AccountSQL = new SqlConnectionStringBuilder(_AccountEF.ProviderConnectionString);

            model.AccuontServer = _AccountSQL.DataSource;
            model.AccuontDatabase = _AccountSQL.InitialCatalog;
            model.AccuontUsername = _AccountSQL.UserID;
            model.AccuontPassword = _AccountSQL.Password;
            #endregion
        }

        public void UpdateConnectionString(ApplicationConfig model)
        {
            #region System
            string _System = ConfigurationManager.ConnectionStrings["System"].ConnectionString;
            Configuration _ConfigSystem = WebConfigurationManager.OpenWebConfiguration("~");
            EntityConnectionStringBuilder _SystemEF = new EntityConnectionStringBuilder(_System);
            SqlConnectionStringBuilder _SystemSQL = new SqlConnectionStringBuilder(_SystemEF.ProviderConnectionString);

            _SystemSQL.DataSource = model.SystemServer;
            _SystemSQL.InitialCatalog = model.SystemDatabase;
            _SystemSQL.UserID = model.SystemUsername;
            _SystemSQL.Password = model.SystemPassword;

            _SystemEF.ProviderConnectionString = _SystemSQL.ConnectionString;
            _ConfigSystem.ConnectionStrings.ConnectionStrings["System"].ConnectionString = _SystemEF.ConnectionString;

            _ConfigSystem.Save(ConfigurationSaveMode.Modified);

            #endregion

            #region Account
            string _Account = ConfigurationManager.ConnectionStrings["Account"].ConnectionString;
            Configuration _ConfigAccount = WebConfigurationManager.OpenWebConfiguration("~");
            EntityConnectionStringBuilder _AccountEF = new EntityConnectionStringBuilder(_Account);
            SqlConnectionStringBuilder _AccountSQL = new SqlConnectionStringBuilder(_AccountEF.ProviderConnectionString);

            _AccountSQL.DataSource = model.AccuontServer;
            _AccountSQL.InitialCatalog = model.AccuontDatabase;
            _AccountSQL.UserID = model.AccuontUsername;
            _AccountSQL.Password = model.AccuontPassword;

            _AccountEF.ProviderConnectionString = _AccountSQL.ConnectionString;
            _ConfigAccount.ConnectionStrings.ConnectionStrings["Account"].ConnectionString = _AccountEF.ConnectionString;

            _ConfigAccount.Save(ConfigurationSaveMode.Modified);
            #endregion
        }

        public DataTable GetEmployee()
        {
            return null;
        }

        public bool ChangePassword(string UserID,string NewPWD)
        {
            return true;
        }
    }
}
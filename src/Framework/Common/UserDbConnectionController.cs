using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Portolo.Framework.Common
{
    public class UserDbConnectionController
    {
        private readonly string spliter = "___";

        public UserDbConnectionController(string userName, string password)
        {
            var obj = userName.Split(new[] { this.spliter }, StringSplitOptions.None);
            if (obj.Length > 0)
            {
                this.UserName = obj[0];
            }
            else
            {
                this.UserName = userName;
            }

            if (obj.Length > 1)
            {
                this.DbConnection = obj[1];
            }
            else
            {
                this.DbConnection = string.Empty;
            }

            if (obj.Length > 2)
            {
                this.OwnerID = Convert.ToInt32(obj[2]);
            }
            else
            {
                this.OwnerID = 0;
            }

            if (obj.Length > 3)
            {
                this.ServiceAccess = obj[3];
            }
            else
            {
                this.ServiceAccess = string.Empty;
            }
        }

        internal UserDbConnectionController()
        {
        }

        private string UserName { get; }
        private string Password { get; set; }
        private string DbConnection { get; }
        private string ServiceAccess { get; }
        private int OwnerID { get; }

        public bool ValidateServiceUser(int serviceModuleID, ref string dBConnection)
        {
            if (!string.IsNullOrEmpty(this.DbConnection))
            {
                var lstServiceAccessID = this.ServiceAccess.Split(',').Select(int.Parse).ToList();
                if (lstServiceAccessID.Contains(serviceModuleID))
                {
                    dBConnection = this.DbConnection;
                    return true;
                }

                throw new FaultException("Please check if License is either not issued or Expired for this User");
            }

            return false;
        }

        internal string CreateUserConnection(string userName, string password, string dBConnection, int? ownerID, string serviceAccess) =>
            string.Format("{0}{1}{2}{1}{3}{1}{4}",
                          userName,
                          this.spliter,
                          string.IsNullOrEmpty(dBConnection) ? string.Empty : dBConnection,
                          ownerID ?? 0,
                          string.IsNullOrEmpty(serviceAccess) ? string.Empty : serviceAccess);
    }
}
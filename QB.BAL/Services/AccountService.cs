using Newtonsoft.Json;
using QB.BAL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QB.BAL.Services
{
    public class AccountService
    {
        string baseUrl = "https://sandbox-quickbooks.api.intuit.com/v3/";
        string RealMeId = "123146326714794";
        string minorversion = "14";
        public string PostAccount(string Name, string AccountType ,  string token )
        {
            var body = JsonConvert.SerializeObject(new { AccountType, Name });
           var response =  CustomHttpHelper.MakeHttpRequest("POST", $"{baseUrl}company/{RealMeId}/account?minorversion={minorversion}", body, token);
            return response;
        }

     
    }
}

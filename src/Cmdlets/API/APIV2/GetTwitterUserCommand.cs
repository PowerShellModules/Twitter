﻿using System.Collections.Generic;
using System.Management.Automation;
using BluebirdPS.Cmdlets.Base;
using BluebirdPS.Core.Processors;
using BluebirdPS.Models;
using BluebirdPS.Models.APIV2;
using Tweetinvi.Exceptions;
using Tweetinvi.Models.V2;
using Tweetinvi.Parameters.V2;

namespace BluebirdPS.Cmdlets.API.APIV2
{
    [Cmdlet(VerbsCommon.Get, "TwitterUser")]
    public class GetTwitterUserCommand : UserCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        [ValidateCount(1, 100)]

        public List<string> User { get; set; }

        private Dictionary<string, List<string>> userList = new()
        {
            { "Names", new List<string>() },
            { "Ids", new List<string>() }
        };

        protected override void BeginProcessing()
        {
            SharedState.Name = MyInvocation.MyCommand.Name;
            if (Client == null)
            {
                WriteObject("client is null, bish");
                return;
            }
        }
        protected override void ProcessRecord()
        {
            if (User != null)
            {
                foreach (string user in User)
                {
                    try
                    {
                        _ = long.Parse(user);
                        userList["Ids"].Add(user);
                    }
                    catch
                    {
                        userList["Names"].Add(user);
                    }
                }
            }
        }

        protected override void EndProcessing()
        {
            string message = (userList["Names"].Count + userList["Ids"].Count) == 0 ? "the authenticated user" : $"{userList["Names"].Count + userList["Ids"].Count} users";
            WriteVerbose($"Attempting to retrieve {message}.");

            List<object> results = new();

            if (userList["Names"].Count == 0 && userList["Ids"].Count == 0)
            {
                try
                {
                    if (Configuration.OutputType == OutputType.JSON)
                    {
                        string result = Client.Raw.UsersV2.GetUserAsync(new GetUserByNameV2Parameters(Configuration.AuthUserName) { Expansions = null }).GetAwaiter().GetResult().Content;
                        results.Add(result);
                    }
                    else
                    {
                        UserV2 result = Client.UsersV2.GetUserByNameAsync(Configuration.AuthUserName).GetAwaiter().GetResult().User;
                        results.Add(mapper.Map<User>(result));
                    }
                }
                catch (TwitterException ex)
                {
                    results.Add(GetTwitterErrorRecord.ProcessV2Error(ex));
                }
            }

            if (userList["Names"].Count > 0)
            {
                try
                {
                    if (userList["Names"].Count == 1)
                    {
                        UserV2Response apiResponse = Client.UsersV2.GetUserByNameAsync(string.Join(",", userList["Names"])).GetAwaiter().GetResult();
                        results.Add(mapper.Map<User>(apiResponse.User));
                    }
                    else
                    {
                        UsersV2Response apiResponse = Client.UsersV2.GetUsersByNameAsync(string.Join(",", userList["Names"])).GetAwaiter().GetResult();
                        foreach (UserV2 result in apiResponse.Users)
                        {
                            results.Add(mapper.Map<User>(result));
                        }
                    }
                }
                catch (TwitterException ex)
                {
                    results.Add(GetTwitterErrorRecord.ProcessV2Error(ex));
                }
            }

            if (userList["Ids"].Count > 0)
            {
                try
                {
                    if (userList["Ids"].Count == 1)
                    {
                        UserV2Response apiResponse = Client.UsersV2.GetUserByIdAsync(string.Join(",", userList["Ids"])).GetAwaiter().GetResult();
                        results.Add(mapper.Map<User>(apiResponse.User));
                    }
                    else
                    {
                        UsersV2Response apiResponse = Client.UsersV2.GetUsersByIdAsync(string.Join(",", userList["Ids"])).GetAwaiter().GetResult();
                        foreach (UserV2 result in apiResponse.Users)
                        {
                            results.Add(mapper.Map<User>(result));
                        }
                    }
                }
                catch (TwitterException ex)
                {
                    results.Add(GetTwitterErrorRecord.ProcessV2Error(ex));
                }

            }

            WriteObject(results, true);
        }

        //internal static List<object> GetUser(IDictionary<string, object> parameters, IDictionary<string, List<string>> userlist)
        //{

        //    List<object> results = new List<object>();

        //    if (userlist["Names"].Count == 0 && userlist["Ids"].Count == 0)
        //    {
        //        try
        //        {
        //            if (Configuration.OutputType == OutputType.JSON)
        //            {
        //                string result = client.Raw.UsersV2.GetUserAsync(new GetUserByNameV2Parameters(Configuration.AuthUserName) {Expansions = null }).GetAwaiter().GetResult().Content;
        //                results.Add(result);
        //            }
        //            else
        //            {
        //                UserV2 result = client.UsersV2.GetUserByNameAsync(Configuration.AuthUserName).GetAwaiter().GetResult().User;
        //                results.Add(mapper.Map<User>(result));
        //            }                    
        //        }
        //        catch (TwitterException ex)
        //        {
        //            results.Add(GetTwitterErrorRecord.ProcessV2Error(ex));
        //        }
        //    }

        //    if (userlist["Names"].Count > 0)
        //    {
        //        try
        //        {
        //            if (userlist["Names"].Count == 1)
        //            {
        //                UserV2Response apiResponse = client.UsersV2.GetUserByNameAsync(string.Join(",", userlist["Names"])).GetAwaiter().GetResult();
        //                results.Add(mapper.Map<User>(apiResponse.User));
        //            }
        //            else
        //            {
        //                UsersV2Response apiResponse = client.UsersV2.GetUsersByNameAsync(string.Join(",", userlist["Names"])).GetAwaiter().GetResult();
        //                foreach (UserV2 result in apiResponse.Users)
        //                {
        //                    results.Add(mapper.Map<User>(result));
        //                }
        //            }
        //        }
        //        catch (TwitterException ex)
        //        {
        //            results.Add(GetTwitterErrorRecord.ProcessV2Error(ex));
        //        }
        //    }

        //    if (userlist["Ids"].Count > 0)
        //    {
        //        try
        //        {
        //            if (userlist["Ids"].Count == 1)
        //            {
        //                UserV2Response apiResponse = client.UsersV2.GetUserByIdAsync(string.Join(",", userlist["Ids"])).GetAwaiter().GetResult();
        //                results.Add(mapper.Map<User>(apiResponse.User));
        //            }
        //            else
        //            {
        //                UsersV2Response apiResponse = client.UsersV2.GetUsersByIdAsync(string.Join(",", userlist["Ids"])).GetAwaiter().GetResult();
        //                foreach (UserV2 result in apiResponse.Users)
        //                {
        //                    results.Add(mapper.Map<User>(result));
        //                }
        //            }
        //        }
        //        catch (TwitterException ex)
        //        {
        //            results.Add(GetTwitterErrorRecord.ProcessV2Error(ex));
        //        }

        //    }
        //    return results;
        //}
    }
}

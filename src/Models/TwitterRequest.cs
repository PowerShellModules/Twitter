﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using BluebirdPS.Models.APIV2;
using System.Linq;
using System.Management.Automation.Runspaces;
using BluebirdPS.Core;

namespace BluebirdPS.Models
{
    public class TwitterRequest
    {
        // ----------------------------------------------------------------------------------------
        // Properties
        public HttpMethod HttpMethod { get; set; }
        public Uri Endpoint { get; set; }
        public OAuthVersion OAuthVersion { get; set; }
        public Hashtable Query { get; set; } = new Hashtable();
        public string Body { get; set; }
        public Hashtable Form { get; set; }
        public string ContentType { get; set; } = "application/json";        
        public ExpansionTypes? ExpansionType { get; set; }
        public bool NonPublicMetrics { get; set; }
        public bool PromotedMetrics { get; set; }
        public bool OrganicMetrics { get; set; }
        public bool IncludeExpansions { get; set; }
        public string CommandName { get; set; }

        public InvocationInfo InvocationInfo { get; private set; }

        private bool _hasExpansionsIncluded { get; set; }
        private bool _hasFieldsIncluded { get; set; }

        // ----------------------------------------------------------------------------------------
        // Constructor
        // The intent is for the class to be instantiated using accelerator and hashtable
        public TwitterRequest() 
        {
            try {
                CallStackFrame _callStackFrame = Runspace.DefaultRunspace.Debugger.GetCallStack().ToList().First();
                CommandName = _callStackFrame.InvocationInfo.MyCommand.Name;
                InvocationInfo = _callStackFrame.InvocationInfo;
            }
            catch
            {

            }
        }

        // ----------------------------------------------------------------------------------------
        // Public methods

        public void SetCommandName(string command)
        {
            CommandName = command;
        }

        public string GetAPIVersion()
        {
            return Endpoint.Segments[1].Trim('/');
        }

        public Uri GetUri()
        {
            SetFields();
            SetExpansions();

            string RequestUri;

            if (Query.Count > 0)
            {
                List<string> queryFields = (from DictionaryEntry kvp in Query
                                            select $"{Uri.EscapeDataString(kvp.Key.ToString())}={Uri.EscapeDataString(kvp.Value.ToString())}").ToList();
                
                RequestUri = $"{Endpoint.AbsoluteUri.TrimEnd('/')}?{string.Join("&", queryFields)}";
            
            } 
            else
            {
                RequestUri = Endpoint.AbsoluteUri;
            }

            return new Uri(RequestUri);
        }

        // ----------------------------------------------------------------------------------------
        // Public methods
        private void SetFields()
        {
            if (_hasFieldsIncluded == false && ExpansionType != null)
            {
                if (ExpansionType == ExpansionTypes.Tweet)
                {
                    Query.Add("tweet.fields", ObjectFields.GetFieldList("Tweet", NonPublicMetrics, OrganicMetrics, PromotedMetrics));
                }
                else if (ExpansionType == ExpansionTypes.User)
                {
                    Query.Add("user.fields", ObjectFields.GetFieldList("User"));
                }
                _hasFieldsIncluded = true;
            }
        }

        public void SetExpansions()
        {
            if (_hasExpansionsIncluded == false && ExpansionType != null && IncludeExpansions == true)
            {
                if (ExpansionType == ExpansionTypes.Tweet)
                {
                    Query.Add("expansions", ExpansionFields.GetExpansionFields(ExpansionTypes.Tweet));
                    Query.Add("user.fields", ObjectFields.GetFieldList("User"));
                    Query.Add("media.fields", ObjectFields.GetFieldList("Media", NonPublicMetrics, OrganicMetrics, PromotedMetrics));
                    Query.Add("poll.fields", ObjectFields.GetFieldList("Poll"));
                    Query.Add("place.fields", ObjectFields.GetFieldList("Place"));
                }            
                else if (ExpansionType == ExpansionTypes.User)
                {
                    Query.Add("expansions", ExpansionFields.GetExpansionFields(ExpansionTypes.User));
                    Query.Add("tweet.fields", ObjectFields.GetFieldList("Tweet", NonPublicMetrics, OrganicMetrics, PromotedMetrics));
                }
            }
             _hasExpansionsIncluded = true;
        }

        //public void InvokeRequest()
        //{            
        //    using (PowerShell pwsh = PowerShell.Create(RunspaceMode.CurrentRunspace))
        //    {
        //        pwsh.AddCommand("Invoke-TwitterRequest").AddParameter("RequestParameters", this);
        //        var response = pwsh.Invoke();
        //        if (response != null)
        //        {
        //            ICommandRuntime.WriteObject(response, true);
        //        }                
        //    }           
        //}
    }
}

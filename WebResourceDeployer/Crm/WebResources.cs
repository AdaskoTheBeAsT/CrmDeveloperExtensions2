﻿using System;
using System.ServiceModel;
using CrmDeveloperExtensions.Core.Enums;
using CrmDeveloperExtensions.Core.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace WebResourceDeployer.Crm
{
    public static class WebResources
    {
        public static EntityCollection RetrieveWebResourcesFromCrm(CrmServiceClient client)
        {
            EntityCollection results = null;
            try
            {
                int pageNumber = 1;
                string pagingCookie = null;
                bool moreRecords = true;

                while (moreRecords)
                {
                    QueryExpression query = new QueryExpression
                    {
                        EntityName = "solutioncomponent",
                        ColumnSet = new ColumnSet("solutionid"),
                        Criteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "componenttype",
                                    Operator = ConditionOperator.Equal,
                                    Values = { 61 }
                                }
                            }
                        },
                        LinkEntities =
                        {
                            new LinkEntity
                            {
                                Columns = new ColumnSet("name", "displayname", "webresourcetype", "ismanaged", "webresourceid"),
                                EntityAlias = "webresource",
                                LinkFromEntityName = "solutioncomponent",
                                LinkFromAttributeName = "objectid",
                                LinkToEntityName = "webresource",
                                LinkToAttributeName = "webresourceid"
                            }
                        },
                        PageInfo = new PagingInfo
                        {
                            PageNumber = pageNumber,
                            PagingCookie = pagingCookie
                        }
                    };

                    EntityCollection partialResults = client.RetrieveMultiple(query);

                    if (partialResults.MoreRecords)
                    {
                        pageNumber++;
                        pagingCookie = partialResults.PagingCookie;
                    }

                    moreRecords = partialResults.MoreRecords;

                    if (partialResults.Entities == null) continue;

                    if (results == null)
                        results = new EntityCollection();

                    results.Entities.AddRange(partialResults.Entities);
                }

                return results;
            }
            catch (FaultException<OrganizationServiceFault> crmEx)
            {
                OutputLogger.WriteToOutputWindow("Error Retrieving Web Resources From CRM: " + crmEx.Message + Environment.NewLine + crmEx.StackTrace, MessageType.Error);
                return null;
            }
            catch (Exception ex)
            {
                OutputLogger.WriteToOutputWindow("Error Retrieving Web Resources From CRM: " + ex.Message + Environment.NewLine + ex.StackTrace, MessageType.Error);
                return null;
            }
        }

        public static Entity RetrieveWebResourceContentFromCrm(CrmServiceClient client, Guid webResourceId)
        {
            try
            {
                Entity webResource = client.Retrieve("webresource", webResourceId, new ColumnSet("content", "name"));

                return webResource;
            }
            catch (FaultException<OrganizationServiceFault> crmEx)
            {
                OutputLogger.WriteToOutputWindow("Error Retrieving Web Resource From CRM: " + crmEx.Message + Environment.NewLine + crmEx.StackTrace, MessageType.Error);
                return null;
            }
            catch (Exception ex)
            {
                OutputLogger.WriteToOutputWindow("Error Retrieving Web Resource From CRM: " + ex.Message + Environment.NewLine + ex.StackTrace, MessageType.Error);
                return null;
            }
        }

        public static void DeleteWebResourcetFromCrm(CrmServiceClient client, Guid webResourceId)
        {
            try
            {
                client.Delete("webresource", webResourceId);
            }
            catch (FaultException<OrganizationServiceFault> crmEx)
            {
                OutputLogger.WriteToOutputWindow("Error Retrieving Web Resource From CRM: " + crmEx.Message + Environment.NewLine + crmEx.StackTrace, MessageType.Error);
            }
            catch (Exception ex)
            {
                OutputLogger.WriteToOutputWindow("Error Retrieving Web Resource From CRM: " + ex.Message + Environment.NewLine + ex.StackTrace, MessageType.Error);
            }
        }

        public static string GetWebResourceTypeNameByNumber(string type)
        {
            switch (type)
            {
                case "1":
                    return "HTML";
                case "2":
                    return "CSS";
                case "3":
                    return "JS";
                case "4":
                    return "XML";
                case "5":
                    return "PNG";
                case "6":
                    return "JPG";
                case "7":
                    return "GIF";
                case "8":
                    return "XAP";
                case "9":
                    return "XSL";
                case "10":
                    return "ICO";
                default:
                    return String.Empty;
            }
        }

        public static byte[] DecodeWebResource(string value)
        {
            return Convert.FromBase64String(value);
        }
    }
}

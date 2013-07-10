﻿/*
 * Licensed to Jasig under one or more contributor license
 * agreements. See the NOTICE file distributed with this work
 * for additional information regarding copyright ownership.
 * Jasig licenses this file to you under the Apache License,
 * Version 2.0 (the "License"); you may not use this file
 * except in compliance with the License. You may obtain a
 * copy of the License at:
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using DotNetCasClient;

public partial class Controls_CookieViewer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie ticketCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        if (ticketCookie != null)
        {
            CookieDomain.Text = ticketCookie.Domain;
            CookieExpires.Text = ticketCookie.Expires.ToString();
            CookieName.Text = ticketCookie.Name;
            CookiePath.Text = ticketCookie.Path;
            CookieSecure.Text = ticketCookie.Secure.ToString();

            if (!string.IsNullOrEmpty(ticketCookie.Value))
            {
                int i = 0;
                StringBuilder cookieValueBuilder = new StringBuilder();
                while (i < ticketCookie.Value.Length)
                {
                    string line = ticketCookie.Value.Substring(i, Math.Min(ticketCookie.Value.Length - i, 50));
                    cookieValueBuilder.Append(line + "<br />");
                    i += line.Length;
                }
                CookieValue.Text = cookieValueBuilder.ToString();
            }

            if (!string.IsNullOrEmpty(ticketCookie.Value))
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(ticketCookie.Value);
                if (ticket != null)
                {
                    TicketCookiePath.Text = ticket.CookiePath;
                    TicketExpiration.Text = ticket.Expiration.ToString();
                    TicketExpired.Text = ticket.Expired.ToString();
                    TicketIsPersistent.Text = ticket.IsPersistent.ToString();
                    TicketIssueDate.Text = ticket.IssueDate.ToString();
                    TicketName.Text = ticket.Name;
                    TicketUserData.Text = ticket.UserData;
                    TicketVersion.Text = ticket.Version.ToString();
                }

                if (CasAuthentication.ServiceTicketManager != null)
                {
                    CasAuthenticationTicket casTicket = CasAuthentication.ServiceTicketManager.GetTicket(ticket.UserData);
                    if (casTicket != null)
                    {
                        CasNetId.Text = casTicket.NetId;
                        CasServiceTicket.Text = casTicket.ServiceTicket;
                        CasOriginatingServiceName.Text = casTicket.OriginatingServiceName;
                        CasClientHostAddress.Text = casTicket.ClientHostAddress;
                        CasValidFromDate.Text = casTicket.ValidFromDate.ToString();
                        CasValidUntilDate.Text = casTicket.ValidUntilDate.ToString();
                        ProxyGrantingTicket.Text = casTicket.ProxyGrantingTicket;
                        ProxyGrantingTicketIou.Text = casTicket.ProxyGrantingTicketIou;
                        
                        StringBuilder proxiesBuilder = new StringBuilder();
                        foreach (string proxy in casTicket.Proxies)
                        {
                            proxiesBuilder.AppendLine(proxy + "<br />");
                        }
                        Proxies.Text = proxiesBuilder.ToString();

                        AssertionPrincipalName.Text = casTicket.Assertion.PrincipalName;
                        AssertionValidFromDate.Text = casTicket.Assertion.ValidFromDate.ToString();
                        AssertionValidUntilDate.Text = casTicket.Assertion.ValidUntilDate.ToString();

                        AssertionAttributesTable.Rows.Clear();
                        string newLine = "<br />";
                        StringBuilder assertionValuesBuilder = new StringBuilder();
                        foreach (KeyValuePair<string, IList<string>> item in casTicket.Assertion.Attributes)
                        {
                            TableRow assertionRow = new TableRow();
                            
                            TableCell assertionKeyCell = new TableCell();
                            assertionKeyCell.VerticalAlign = VerticalAlign.Top;
                            assertionKeyCell.Text = item.Key;

                            TableCell assertionValuesCell = new TableCell();
                            assertionValuesCell.VerticalAlign = VerticalAlign.Top;

                            foreach (string value in item.Value)
                            {
                                assertionValuesBuilder.Append(value + newLine);
                            }
                            
                            if (assertionValuesBuilder.Length > newLine.Length)
                            {
                                assertionValuesBuilder.Length -= newLine.Length;
                            }

                            assertionValuesCell.Text = assertionValuesBuilder.ToString();

                            assertionRow.Cells.Add(assertionKeyCell);
                            assertionRow.Cells.Add(assertionValuesCell);

                            AssertionAttributesTable.Rows.Add(assertionRow);
                            assertionValuesBuilder.Length = 0;
                        }
                    }
                }
            }
        }
    }
}
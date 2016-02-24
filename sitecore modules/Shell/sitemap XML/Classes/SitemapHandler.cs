﻿/* *********************************************************************** *
 * File   : SitemapHandler.cs                             Part of Sitecore *
 * Version: 1.0.0                                         www.sitecore.net *
 *                                                                         *
 *                                                                         *
 * Purpose: Contains logic which fires when event submitted                *
 *                                                                         *
 * Copyright (C) 1999-2009 by Sitecore A/S. All rights reserved.           *
 *                                                                         *
 * This work is the property of:                                           *
 *                                                                         *
 *        Sitecore A/S                                                     *
 *        Meldahlsgade 5, 4.                                               *
 *        1613 Copenhagen V.                                               *
 *        Denmark                                                          *
 *                                                                         *
 * This is a Sitecore published work under Sitecore's                      *
 * shared source license.                                                  *
 *                                                                         *
 * *********************************************************************** */

using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.ContentSearch;

namespace Sitecore.Modules.SitemapXML
{
    public class SitemapHandler
    {
        public void RefreshSitemap(object sender, EventArgs args)
        {
            Database db = Factory.GetDatabase(SitemapManagerConfiguration.WorkingDatabase);
            Item sitemapConfig = db.Items[SitemapManagerConfiguration.SitemapConfigurationItemPath];
            if (sitemapConfig != null)
            {
                if (!string.IsNullOrEmpty(SitemapManagerConfiguration.IndexName))
                {
                    ContentSearchManager.GetIndex(SitemapManagerConfiguration.IndexName).Rebuild();
                }
                SitemapManager sitemapManager = new SitemapManager();

                sitemapManager.SubmitSitemapToSearchenginesByHttp();
                if (SitemapManagerConfiguration.GenerateRobotsTxt)
                {
                    sitemapManager.RegisterSitemapToRobotsFile();
                }
            }
        }
    }
}
